using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Common_Library.Utils;
using System.Drawing;

namespace IRL_Bitmap_Converter_Tools.Converters
{
    public static class TextConverter
    {
        public static bool ConvertTextInstructionToBitmaps(
            TextInstruction textInstruction, List<TextStyle> textStyles, int noOfTranslationPropertyValues, string outputFolder)
        {
            if (Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            bool success = true;

            foreach (TextRecord record in textInstruction.Table.Records)
            {
                if (record.TextStyleIDs.Count == 0) 
                {
                    Log.WriteLine($"No TextStyle selected for record {record.Key}");
                    success = false;
                }

                if (textInstruction.Table.Translate)
                {
                    success &= ConvertTranslationTextRecord(record, textStyles, textInstruction.Table.TranslationProperty, noOfTranslationPropertyValues, textInstruction.FileKeyPrefix, outputFolder);
                }
                else
                {

                    success &= ConvertTextRecord(record, textStyles, textInstruction.FileKeyPrefix, outputFolder);
                }
            }

            return success;
        }

        private static bool ConvertTextRecord(TextRecord record, List<TextStyle> textStyles, string fileKeyPrefix, string outputFolder)
        {
            foreach (int styleId in record.TextStyleIDs)
            {
                TextStyle style = TextStyle.GetTextStyleById(styleId, textStyles);

                if (style != null)
                {
                    string prefix = GetPrefix(fileKeyPrefix, style.Prefix);
                    string filename = CreateFileNameWithProperties(record.Key, prefix, style.BitmapProperties);
                    string fileOutputFolder = CreateOutputFolder(outputFolder, prefix);

                    for (int i = 0; i < record.Text.Count; i++)
                    {
                        try
                        {
                            Font font = new(style.FontName, style.FontSize, style.FontStyle, GraphicsUnit.Pixel);

                            StringToBitmapConverter.GenerateImage(
                                    record.Text[i], font,
                                    style.Margin.Left, style.Margin.Top, style.Margin.Right, style.Margin.Bottom,
                                    style.TextColor, style.BackColor,
                                    fileOutputFolder, filename);
                        }
                        catch (Exception exception)
                        {
                            Log.WriteLine($"Error creating Font, {exception.Message}");

                            return false;
                        }
                    }
                }
                else
                {
                    Log.WriteLine($"Text style not found {styleId}");
                }
            }

            return true;
        }

        private static bool ConvertTranslationTextRecord(
            TextRecord record, List<TextStyle> textStyles, BitmapProperty translationProperty,
            int noOfTranslationPropertyValues, string fileKeyPrefix, string outputFolder)
        {
            bool success = true;

            foreach (int styleId in record.TextStyleIDs)
            {
                TextStyle style = TextStyle.GetTextStyleById(styleId, textStyles);

                if (style != null)
                {
                    string prefix = GetPrefix(fileKeyPrefix, style.Prefix);
                    string fileOutputFolder = CreateOutputFolder(outputFolder, prefix);

                    if (noOfTranslationPropertyValues != record.Text.Count)
                    {
                        Log.WriteLine($"Number of translation properties does not match the number of translations for record {record.Key}");
                        
                        success = false;
                    }

                    for (int i = 0; i < record.Text.Count; i++)
                    {
                        List<BitmapProperty> properties = new();
                        translationProperty.Value = i;
                        properties.Add(translationProperty);
                        properties.AddRange(style.BitmapProperties);

                        string filename = CreateFileNameWithProperties(record.Key, prefix, properties);

                        try
                        {
                            Font font = new(style.FontName, style.FontSize, style.FontStyle, GraphicsUnit.Pixel);

                            StringToBitmapConverter.GenerateImage(
                                    record.Text[i], font,
                                    style.Margin.Left, style.Margin.Top, style.Margin.Right, style.Margin.Bottom,
                                    style.TextColor, style.BackColor,
                                    fileOutputFolder, filename);
                        }
                        catch (Exception exception)
                        {
                            Log.WriteLine($"Error creating Font, {exception.Message}");

                            success = false;
                        }
                    }
                }
                else
                {
                    Log.WriteLine($"Error getting TextStyle with id {styleId}");

                    success = false;
                }
            }

            return success;
        }

        private static string CreateFileNameWithProperties(string fileName, string fileKeyPrefix, List<BitmapProperty> properties)
        {
            string newFileName;

            if (string.IsNullOrEmpty(fileKeyPrefix))
            {
                newFileName = fileName;
            }
            else
            {
                newFileName = $"{fileKeyPrefix}_{fileName}";
            }

            bool firstPropertyAdded = false;
            foreach (BitmapProperty property in properties)
            {
                if (!string.IsNullOrEmpty(property.Name))
                {
                    if (firstPropertyAdded)
                    {
                        newFileName = $"{newFileName}_{property.Name}{property.Value}";
                    }
                    else
                    {
                        newFileName = $"{newFileName}@{property.Name}{property.Value}";
                        firstPropertyAdded = true;
                    }
                }
            }

            return newFileName;
        }

        private static string GetPrefix(string fileKeyPrefix, string stylePrefix)
        {
            if (!string.IsNullOrEmpty(fileKeyPrefix) && !string.IsNullOrEmpty(stylePrefix))
            {
                return $"{fileKeyPrefix}_{stylePrefix}";
            }
            else if (!string.IsNullOrEmpty(fileKeyPrefix))
            {
                return fileKeyPrefix;
            }
            else if (!string.IsNullOrEmpty(stylePrefix))
            {
                return stylePrefix;
            }
            else
            {
                return "";
            }
        }

        private static string CreateOutputFolder(string outputFolder, string prefix)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                return outputFolder;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory($"{outputFolder}\\_{prefix}");
                    return $"{outputFolder}\\_{prefix}";
                }
                catch (Exception exception)
                {
                    Log.WriteLine($"Error creating output folder, {exception.Message}");
                    return outputFolder;
                }
            }
        }
    }
}
