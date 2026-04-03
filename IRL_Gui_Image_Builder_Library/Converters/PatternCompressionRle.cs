using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Text;
using System.Text.RegularExpressions;

namespace IRL_Gui_Image_Builder_Library.Converters
{
    public class PatternCompressionRle
    {
        private const int MaxPatterns = 127;//sizeof(sbyte);
        //private const int NoOfPatternsToRemove = 3;
        private const int MinPatternCountThreshold = 10;

        private int m_patternLength;
        private int m_patternCount;
        private List<PatternRLE> m_patternLookUpTable = [];

        public int PatternsRemoved { get; private set; } = 0;

        public PatternCompressionRle(int patternLength, int patternCount)
        {
            m_patternLength = patternLength;
            m_patternCount = patternCount;
        }
        
        public void CompressPixelDataWithPatterns(PixelData pixelData)
        {
            if (m_patternLength <= 0 || m_patternCount <= 0)
            {
                throw new ArgumentException("Pattern length and count must be greater than zero.");
            }

            int startDataSize = pixelData.DataItems.Sum(d => d.Data.Length);
            Debug.WriteLine($"Start data size: {startDataSize} bytes");

            //while (m_patternLookUpTable.Count < MaxPatterns)
            //{
                List<PatternRLE> patterns = [];
                SearchPatternsInPixelData(pixelData, patterns, m_patternLength, m_patternCount);
                AnalyzeRleDataPatterns(pixelData, patterns, m_patternLength, m_patternCount);

                //if ((patterns.Count == 0) || patterns[0].Count < 50)
                //{
                //    break; // No more patterns to analyze
                //}

                //RemovePatternsFromPixelData(pixelData, patterns);

                Debug.WriteLine($"Patterns in LUT: {m_patternLookUpTable.Count}/{MaxPatterns}");
                Debug.WriteLine($"Total patterns removed: {PatternsRemoved}");
                Debug.WriteLine($"Current data size: {pixelData.DataItems.Sum(d => d.Data.Length)} bytes");
                Debug.WriteLine($"Data size reduction: {startDataSize - pixelData.DataItems.Sum(d => d.Data.Length)} bytes ({((double)(startDataSize - pixelData.DataItems.Sum(d => d.Data.Length)) / startDataSize * 100):F2}%)");
                Debug.WriteLine("");
           // }
        }

        private static void AnalyzeRleDataPatterns(PixelData pixelData, List<PatternRLE> patterns, int patternLength, int patternCount)
        {   
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (PatternRLE pattern in patterns)
            {
                CountRLEDataPatterns(pixelData, pattern, patternLength, patternCount);
                //LogPatternOccurrences(pattern, patternLength, patternCount);
            }

            // Remove patterns that do not meet the minimum count threshold
            patterns.RemoveAll(p => p.Count < MinPatternCountThreshold);

            // Sort patterns by count in descending order
            patterns.Sort((a, b) => b.Count.CompareTo(a.Count));

            //foreach (PatternRLE pattern in patterns)
            //{
            //    LogPatternOccurrences(pattern, patternLength, patternCount);
            //}

            stopwatch.Stop();
            Debug.WriteLine($"Time taken to count pattern occurrences: {stopwatch.ElapsedMilliseconds} ms");
            Debug.WriteLine($"Total patterns meeting threshold: {patterns.Count}");
        }

        private static void SearchPatternsInPixelData(PixelData pixelData, List<PatternRLE> patterns, int patternLength, int patternCount)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            //foreach (byte[] data in pixelData.ImageDataList)
            foreach (DataItemBase dataItem in pixelData.DataItems)
            {
                byte[] data = dataItem.Data;

                for (int i = 0; i < data.Length;)
                {
                    if (data[i] == 0)
                    {
                        i += 2; // Skip already removed patterns (assuming 2 bytes for pattern indicator)
                    }
                    else
                    {
                        bool potentialPattern = true;

                        for (int j = patternLength; j < (patternLength * patternCount); j += patternLength)
                        {
                            if (data[i + j] == 0)
                            {
                                potentialPattern = false;
                                i += patternLength;

                                break;
                            }
                        }

                        if (potentialPattern)
                        {
                            byte[] pattern = new byte[patternLength * patternCount];
                            Array.Copy(data, i, pattern, 0, patternLength * patternCount);

                            PatternRLE patternRLE = new PatternRLE(pattern, patternLength, patternCount, i);

                            if (patterns.Any(p => p.Pattern.SequenceEqual(patternRLE.Pattern)) ||
                                patterns.Any(p => p.Pattern.SequenceEqual(patternRLE.PatternReversed)))
                            {
                                // Pattern already exists in the list, skip adding it again
                            }
                            else
                            {
                                patterns.Add(patternRLE);
                            }

                            i += patternLength; // Move to the next potential pattern start
                        }
                    }

                    if (i >= data.Length - (patternLength * patternCount))
                    {
                        break;
                    }
                }
            }

            stopwatch.Stop();
            Debug.WriteLine($"Time taken to analyze patterns: {stopwatch.ElapsedMilliseconds} ms");
        }

        private void RemovePatternsFromPixelData(PixelData pixelData, List<PatternRLE> patterns)
        {
            //int patternsToRemove = patterns.Count;
            //int patternsRemoved = 0;

            //for (int i = 0; i < patterns.Count; i++) 
            while (patterns.Count > 0 && m_patternLookUpTable.Count < MaxPatterns)
            {
                PatternRLE pattern = patterns[0];

                if (m_patternLookUpTable.Count < MaxPatterns)
                {
                    int removedCount = RemovePatternFromPixelData(pixelData, pattern, false);
                    PatternsRemoved += removedCount;

                    if (removedCount > 0)
                    {
                        m_patternLookUpTable.Add(pattern);
                        //patternsRemoved++;
                        PatternsRemoved += RemovePatternFromPixelData(pixelData, pattern, true);
                    }
                }   

                patterns.RemoveAt(0); // Remove the pattern from the list after processing
                AnalyzeRleDataPatterns(pixelData, patterns, m_patternLength, m_patternCount); // Re-analyze patterns after removal
            }
        }

        private int RemovePatternFromPixelData(PixelData pixelData, PatternRLE pattern, bool reverse)
        {
            int removedCount = 0;
            int indexPattern = m_patternLookUpTable.IndexOf(pattern);

            for (int bmpIndex = 0; bmpIndex < pixelData.DataItems.Count; bmpIndex++)
            {
                List<byte> compressedData = [];
                byte[] data = pixelData.DataItems[bmpIndex].Data;

                // Look for the pattern in the data and remove it
                for (int i = 0; i < data.Length;)
                {
                    if (data[i] == 0) // Skip already removed patterns
                    {
                        compressedData.Add(data[i++]);
                        compressedData.Add(data[i++]);

                        continue;
                    }

                    byte[] currentPattern = reverse ? pattern.PatternReversed : pattern.Pattern;
                    
                    if (currentPattern[0] == data[i]) // Potential match, check the rest of the pattern
                    {
                        bool isMatch = true;

                        for (int j = 0; j < currentPattern.Length; j++)
                        {
                            if (currentPattern[j] != data[i + j])
                            {
                                isMatch = false;

                                break;
                            }
                        }

                        if (isMatch)
                        {
                            removedCount++;

                            byte patternLUTIndex = (byte)indexPattern;
                            if (reverse)
                            {
                                patternLUTIndex |= 0x80; // Set the highest bit to indicate reversed pattern
                            }

                            byte[] patternIndicatorBytes = new byte[2];
                            patternIndicatorBytes[0] = 0; // Example: Set to 0 to indicate pattern match
                            patternIndicatorBytes[1] = patternLUTIndex; // Store the index of the pattern in the lookup table
                            compressedData.AddRange(patternIndicatorBytes); // Add the pattern indicator to the compressed data

                            i += currentPattern.Length; // Skip the matched pattern in the input data
                        }
                        else //If it's not a match, add the RLE data to the compressed data list
                        {
                            byte[] bytesToAdd = new byte[pattern.PatternLength];
                            Array.Copy(data, i, bytesToAdd, 0, bytesToAdd.Length);
                            compressedData.AddRange(bytesToAdd); // Add the current byte to the compressed data

                            i += bytesToAdd.Length;
                        }
                    }
                    else //If it's not a match, add the RLE data to the compressed data list
                    {
                        byte[] bytesToAdd = new byte[pattern.PatternLength];
                        Array.Copy(data, i, bytesToAdd, 0, bytesToAdd.Length);
                        compressedData.AddRange(bytesToAdd); // Add the current byte to the compressed data

                        i+= bytesToAdd.Length;
                    }

                    if (i >= data.Length - (pattern.PatternLength * pattern.PatternCount))
                    {
                        // Add any remaining bytes that are not part of a full pattern
                        compressedData.AddRange(data.Skip(i));
                        break;
                    }
                }

                //pixelData.ImageDataList[bmpIndex] = compressedData.ToArray();
            }

            string reversedString = reverse ? " Reversed " : " ";
            Debug.WriteLine($"Removed {removedCount} occurrences of the{reversedString}pattern.");

            return removedCount;
        }

        private static void CountRLEDataPatterns(PixelData pixelData, PatternRLE pattern, int patternLength, int patternCount)
        {
            pattern.Count = 0;

            //foreach (byte[] data in pixelData.ImageDataList)
            foreach (DataItemBase dataItem in pixelData.DataItems)
            {
                byte[] data = dataItem.Data;

                for (int i = 0; i < data.Length - (patternLength * patternCount); i += patternLength)
                {
                    if (pattern.Pattern[0] == data[i])
                    {
                        bool isMatch = true;

                        for (int j = 0; j < pattern.Pattern.Length; j++)
                        {
                            if (pattern.Pattern[j] != data[i + j])
                            {
                                isMatch = false;
                                break;
                            }
                        }

                        if (isMatch)
                        {
                            pattern.Count++;
                        }
                    }
                }
            }
        }

        private static void LogPatternOccurrences(PatternRLE pattern, int patternLength, int patternCount)
        {
            StringBuilder patternString = new StringBuilder();
            for (int i = 0; i < pattern.Pattern.Length; i += 3)
            {
                byte pixelCount = pattern.Pattern[i];
                ushort pixelDataValue = BitConverter.ToUInt16(pattern.Pattern, i + 1);
                patternString.Append($"{pixelCount} {pixelDataValue.ToString("X4")}, ");
            }
            if (patternString.Length > 2)
            {
                patternString.Length -= 2; // Remove the trailing comma and space
            }
            Debug.WriteLine($"Pattern: {patternString} Count: {pattern.Count}");
        }



        










        //private static void FindRLEDataPatterns(byte[] pixelData, int patternLength)
        //{
        //    List<byte[]> patterns = new List<byte[]>();

        //    for (int i = 0; i < pixelData.Length - (3 * (patternLength - 1)); i += 3)
        //    {
        //        byte[] pattern = new byte[3 * patternLength];
        //        Array.Copy(pixelData, i, pattern, 0, 3 * patternLength);

        //        if (!patterns.Any(p => p.SequenceEqual(pattern)))
        //        {
        //            patterns.Add(pattern);
        //        }
        //    }

        //    int totalUniquePatterns = 0;
        //    int totalPatternCount = 0;

        //    Debug.WriteLine($"Total unique patterns: {patterns.Count}");
        //    foreach (byte[] pattern in patterns)
        //    {
        //        CountRLEDataPatterns(pixelData, pattern, patternLength, ref totalUniquePatterns, ref totalPatternCount);
        //    }

        //    Debug.WriteLine($"Total Unique Patterns: {totalUniquePatterns}");
        //    Debug.WriteLine($"Total Pattern Count: {totalPatternCount}");
        //    Debug.WriteLine("");
        //}

        //private static void CountRLEDataPatterns(byte[] pixelData, byte[] pattern, int patternLength, ref int totalUniquePatterns, ref int totalPatternCount)
        //{
        //    int count = 0;

        //    for (int i = 0; i < pixelData.Length - pattern.Length; i += 3)
        //    {
        //        if (pattern.SequenceEqual(pixelData.Skip(i).Take(pattern.Length)) && pattern.Length > 0)
        //        {
        //            count++;
        //        }
        //    }

        //    if (count > 10)
        //    {
        //        if (patternLength == 2)
        //        {
        //            byte pixelCount1 = pattern[0];
        //            ushort pixelDataValue = BitConverter.ToUInt16(pattern, 1);
        //            byte pixelCount2 = pattern[3];
        //            ushort pixelDataValue2 = BitConverter.ToUInt16(pattern, 4);
        //            Debug.WriteLine($"Pattern: {pixelCount1} {pixelDataValue.ToString("X4")}, {pixelCount2} {pixelDataValue2.ToString("X4")} Count: {count}");

        //            totalUniquePatterns++;
        //            totalPatternCount += count;
        //        }
        //        else if (patternLength == 3)
        //        {
        //            byte pixelCount1 = pattern[0];
        //            ushort pixelDataValue1 = BitConverter.ToUInt16(pattern, 1);
        //            byte pixelCount2 = pattern[3];
        //            ushort pixelDataValue2 = BitConverter.ToUInt16(pattern, 4);
        //            byte pixelCount3 = pattern[6];
        //            ushort pixelDataValue3 = BitConverter.ToUInt16(pattern, 7);
        //            Debug.WriteLine($"Pattern: {pixelCount1} {pixelDataValue1.ToString("X4")}, {pixelCount2} {pixelDataValue2.ToString("X4")}, {pixelCount3} {pixelDataValue3.ToString("X4")} Count: {count}");

        //            totalUniquePatterns++;
        //            totalPatternCount += count;
        //        }
        //        else if (patternLength == 4)
        //        {
        //            byte pixelCount1 = pattern[0];
        //            ushort pixelDataValue1 = BitConverter.ToUInt16(pattern, 1);
        //            byte pixelCount2 = pattern[3];
        //            ushort pixelDataValue2 = BitConverter.ToUInt16(pattern, 4);
        //            byte pixelCount3 = pattern[6];
        //            ushort pixelDataValue3 = BitConverter.ToUInt16(pattern, 7);
        //            byte pixelCount4 = pattern[9];
        //            ushort pixelDataValue4 = BitConverter.ToUInt16(pattern, 10);
        //            Debug.WriteLine($"Pattern: {pixelCount1} {pixelDataValue1.ToString("X4")}, {pixelCount2} {pixelDataValue2.ToString("X4")}, {pixelCount3} {pixelDataValue3.ToString("X4")}, {pixelCount4} {pixelDataValue4.ToString("X4")} Count: {count}");

        //            totalUniquePatterns++;
        //            totalPatternCount += count;
        //        }
        //    }
        //}
    }
}
