using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.Converters;
using IRL_Gui_Image_Builder_Library.Exceptions;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;
using IRL_Gui_Image_Builder_Library.Projects;
using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts
{
    public class FontBuilder
    {
        private readonly ImageBuilderSettings m_builderSettings;
        private readonly BuilderStatusUpdater m_statusUpdater;
        private readonly List<FsFont> m_fonts = [];

        public List<DataLocation> DataLocations { get; private set; } = [];
        public bool FontsCreated => m_fonts.Count > 0;

        public FontBuilder(ImageBuilderSettings builderSettings, BuilderStatusUpdater statusUpdater)
        {
            m_builderSettings = builderSettings;
            m_statusUpdater = statusUpdater;
        }

        
        public List<FsFont> Fonts => m_fonts;
        public bool HasNumberOnlyFonts { get; set; } = false;

        public bool CreateFonts()
        {
            DirectoryInfo directoryInfoRoot = new(FileConstants.GetFontImportFolder());
            DirectoryInfo[] directoryInfos = directoryInfoRoot.GetDirectories();

            if (directoryInfos.Length != 0)
            {
                ushort fontId = 0;
                foreach (DirectoryInfo dirInfo in directoryInfos)
                {
                    CreateFont(fontId, dirInfo);
                    fontId += 1;
                }
            }

            return m_fonts.Count > 0;
        }

        public void CreateCharInfoSearchData(ref byte[] searchData)
        {
            int writeIndex = 0;
            foreach (FsFont font in m_fonts)
            {
                foreach (CharacterInfo charInfo in font.CharacterInfos)
                {
                    CharacterInfo.GetExternalBytes(charInfo).CopyTo(searchData, writeIndex);
                    writeIndex += CharacterInfo.GetSize();
                }
            }
        }

        public int CharacterInfoSize
        {
            get
            {
                int size = 0;
                foreach (FsFont font in m_fonts)
                {
                    size += font.CharacterInfos.Count * CharacterInfo.GetSize();
                }

                return size;
            }
        }

        public void AddPixelData(PixelData pixelData)
        {
            foreach (FsFont font in m_fonts)
            {
                m_statusUpdater.UpdateStatusAndFilesConverted("Converting Font pixeldata: " + font.Name, font.CharacterInfos.Count);
                AddFontPixelData(pixelData, font);
            }
        }

        public void AddExternalPixelData(ref byte[] pixelData, uint offset)
        {
            foreach (FsFont font in m_fonts)
            {
                offset += AddFontExternalPixelData(ref pixelData, font, offset);
            }
        }

        private void CreateFont(ushort fontId, DirectoryInfo dirInfo)
        {
            FileInfo[] fileInfos = dirInfo.GetFiles("*.png");
            bool isNumberOnly = IsFontNumberOnly(ref fileInfos);
            
            if (isNumberOnly)
            {
                HasNumberOnlyFonts = true;
            }

            DataLocation dataLocation = GetDataLocationFromFolderName(dirInfo);
            string fontName = DataLocation.RemoveDataLocationFromFolderName(dirInfo.Name);

            FsFont font = new(fontId, fontName, dirInfo.FullName, isNumberOnly, dataLocation);
            m_fonts.Add(font);

            AddCharacterInfos(font, ref fileInfos);
        }

        private DataLocation GetDataLocationFromFolderName(DirectoryInfo dirInfo)
        {
            int dataLocationId = DataLocation.GetDataLocationFromFolderName(dirInfo.Name);

            if (dataLocationId == -1)
            {
                Log.Error($"DataLocation ID not found in folder name: {dirInfo.Name}. Please include the DataLocation ID in the folder name using the format '__DLID_<ID>'.");
            
                throw new ImageBuilderException($"DataLocation ID not found in folder name: {dirInfo.Name}");
            }
            else
            {
                DataLocation? dataLocation = DataLocation.GetDataLocation(dataLocationId, m_builderSettings.DataLocations);
                if (dataLocation == null)
                {
                    Log.Error($"DataLocation with ID: {dataLocationId} not found in ImageBuilder settings.");

                    throw new ImageBuilderException($"DataLocation with ID: {dataLocationId} not found in ImageBuilder settings.");
                }
                else
                {
                    AddDataLocation(dataLocation);
                }

                return dataLocation;
            }
        }

        private void AddDataLocation(DataLocation dataLocation)
        {
            bool exists = false;
            foreach (DataLocation dl in DataLocations)
            {
                if (dl.LocationID == dataLocation.LocationID)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                DataLocations.Add(dataLocation);
            }
        }

        private static void AddCharacterInfos(FsFont font, ref FileInfo[] fileInfos)
        {
            foreach (FileInfo fileInfo in fileInfos)
            {
                byte ASSCI = CharacterInfo.GetCharValue(fileInfo.Name);

                font.CharacterInfos.Add(new CharacterInfo(font, ASSCI, fileInfo.FullName, fileInfo.Extension));
            }

            SortCharacterSet(font);
        }

        private static bool IsFontNumberOnly(ref FileInfo[] fileInfos)
        {
            if (fileInfos.Length == 95)
            {
                return false;
            }
            else if (fileInfos.Length == 12)    // 0-9 . ,
            {
                return true;
            }
            else
            {
                Log.Error("Number of Characters in font does not match expected values for full font (95) or number only font (12)");
                throw new ImageBuilderException("Number of Characters in font does not match expected values for full font (95) or number only font (12)");
            }
        }

        private void AddFontPixelData(PixelData pixelData, FsFont font)
        {
            CompressionType compression = pixelData.DataLocation.CompressionType;
            int offset = pixelData.Offset;
            int bytesPerPixel = m_builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? 3 : 2;
            int totalUncompressedBytes = 0;
            int totalCompressedBytes = 0;

            foreach (CharacterInfo charInfo in font.CharacterInfos)
            {
                if (!FileUtils.CanImportFile(charInfo.FileExtension))
                {
                    Log.Error($"Cannot import file: {charInfo.FilePath} with extension: {charInfo.FileExtension}");
                    throw new ImageBuilderException($"Cannot import font character bitmap: {charInfo.ASSCI}");
                }

                using Bitmap bitmap = new(charInfo.FilePath, true);
                int dataOffset = pixelData.DataLength;

                if (compression == CompressionType.None && font.DataLocation.CompressionType == compression)
                {
                    byte[] convertedPixelData = PixelDataConverter.GetConvertedPixelData(bitmap, m_builderSettings.PixelDataFormat);
                    pixelData.AppendData(convertedPixelData, charInfo);
                    charInfo.UpdateValues((uint)(dataOffset + offset), (ushort)bitmap.Width, (ushort)bitmap.Height, (uint)convertedPixelData.Length);
                }
                else if (compression == CompressionType.RLE && font.DataLocation.CompressionType == compression)
                {
                    byte[] convertedPixelData = PixelDataConverter.GetPixelData_RLE(bitmap, m_builderSettings.PixelDataFormat);
                    pixelData.AppendData(convertedPixelData, charInfo);
                    charInfo.UpdateValues((uint)(dataOffset + offset), (ushort)bitmap.Width, (ushort)bitmap.Height, (uint)convertedPixelData.Length);

                    int uncompressedBytes = bitmap.Width * bitmap.Height * bytesPerPixel;
                    totalUncompressedBytes += uncompressedBytes;
                    totalCompressedBytes += convertedPixelData.Length;
                }
                else if (compression == CompressionType.RLE_Alpha && font.DataLocation.CompressionType == compression)
                {
                    byte[] convertedPixelData = PixelDataConverter.GetPixelData_RLE_Alpha(bitmap);
                    pixelData.AppendData(convertedPixelData, charInfo);
                    charInfo.UpdateValues((uint)(dataOffset + offset), (ushort)bitmap.Width, (ushort)bitmap.Height, (uint)convertedPixelData.Length);

                    int uncompressedBytes = bitmap.Width * bitmap.Height * bytesPerPixel;
                    totalUncompressedBytes += uncompressedBytes;
                    totalCompressedBytes += convertedPixelData.Length;
                }
                else
                {
                    Log.Error($"Font character bitmap compression type does not match DataLocation compression type for character: {charInfo.ASSCI} in font: {font.Name}");
                    throw new ImageBuilderException($"Font character bitmap compression type does not match DataLocation compression type for character: {charInfo.ASSCI} in font: {font.Name}");
                }
            }

            if (compression != CompressionType.None)
            {
                double compressionRatio = (double)totalUncompressedBytes / totalCompressedBytes;
                Log.Verbose($"Font: {font.Name} - Total Uncompressed Bytes: {totalUncompressedBytes}" +
                    $", Total Compressed Bytes: {totalCompressedBytes}, Bytes saved: {totalUncompressedBytes - totalCompressedBytes}" +
                    $", Compression Ratio: {compressionRatio:F2}");
            }
        }

        private uint AddFontExternalPixelData(ref byte[] pixelData, FsFont font, uint offset)
        {
            CompressionType compression = font.DataLocation.CompressionType;
            uint writeIndex = offset;

            foreach (CharacterInfo charInfo in font.CharacterInfos)
            {
                using Bitmap bitmap = new(charInfo.FilePath, true);
                byte[] convertedPixelData = [];
  
                if (compression == CompressionType.RLE_Alpha)
                {
                    convertedPixelData = PixelDataConverter.GetPixelData_RLE_Alpha(bitmap);
                }
                else
                {
                    PixelDataFormat pixelDataFormat = new()
                    {
                        PixelFormat = PixelFormat.RGB,
                        RGB565SwapBytes = false,
                        PixelFormatRGB = PixelFormatRGB.RGB
                    };

                    convertedPixelData = PixelDataConverter.GetPixelData_RLE(bitmap, pixelDataFormat);
                }

                int currentLength = pixelData.Length;
                Array.Resize(ref pixelData, currentLength + convertedPixelData.Length);
                Array.Copy(convertedPixelData, 0, pixelData, currentLength, convertedPixelData.Length);

                charInfo.ExternalDisplayDataOffset = writeIndex;
                writeIndex += (uint)convertedPixelData.Length;
            }

            return writeIndex - offset;
        }

        private static void SortCharacterSet(FsFont font)
        {
            font.CharacterInfos.Sort((x, y) => x.ASSCI.CompareTo(y.ASSCI));
        }
    }
}
