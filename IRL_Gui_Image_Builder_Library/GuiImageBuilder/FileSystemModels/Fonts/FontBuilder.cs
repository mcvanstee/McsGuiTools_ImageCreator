using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.Exceptions;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.Projects;
using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.Fonts
{
    public class FontBuilder
    {
        private readonly string m_projectPath;
        private readonly ImageBuilderSettings m_builderSettings;
        private readonly BuilderStatusUpdater m_statusUpdater;
        private readonly List<FsFont> m_fonts = new();
        private byte[] m_charInfoSearchData = Array.Empty<byte>();

        public FontBuilder(ImageBuilderSettings builderSettings, string projectPath, BuilderStatusUpdater statusUpdater)
        {
            m_builderSettings = builderSettings;
            m_projectPath = projectPath;
            m_statusUpdater = statusUpdater;
        }

        public byte[] CharInfoSearchData => m_charInfoSearchData;
        public int CharInfoOffset { get; set; } = 0;
        public List<FsFont> Fonts => m_fonts;
        public bool HasNumberOnlyFonts { get; set; } = false;

        public bool CreateFonts()
        {
            DirectoryInfo directoryInfoRoot = new(BuildFolders.FontInputFolderPath(m_projectPath));
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

        public bool CreatOptimizedFonts()
        {
            DirectoryInfo directoryInfoRoot = new(BuildFolders.FontInputFolderPath(m_projectPath));
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

            foreach (FsFont font in m_fonts)
            {
                foreach(CharacterInfo characterInfo in font.CharacterInfos)
                {
                    characterInfo.DataCompression = FontDataCompression.SourcePixelDataFileOptimized;
                }
            }

            return m_fonts.Count > 0;
        }

        public void CreateCharInfoSearchData()
        {
            m_charInfoSearchData = new byte[CharacterInfoSize];

            int writeIndex = 0;
            foreach (FsFont font in m_fonts)
            {
                foreach (CharacterInfo charInfo in font.CharacterInfos)
                {
                    CharacterInfo.GetBytes(charInfo).CopyTo(m_charInfoSearchData, writeIndex);
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

        private void CreateFont(ushort fontId, DirectoryInfo dirInfo)
        {
            FileInfo[] fileInfos = dirInfo.GetFiles("*.png");
            bool isNumberOnly;

            try
            {
                isNumberOnly = IsFontNumberOnly(ref fileInfos);
                HasNumberOnlyFonts = true;
            }
            catch (ArgumentException e)
            {
                Log.Error(e.Message);

                return;
            }          

            FsFont font = new(fontId, dirInfo.Name, dirInfo.FullName, isNumberOnly);
            m_fonts.Add(font);

            AddCharacterInfos(font, ref fileInfos);
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
                throw new ArgumentException("Number of Characters does not match");
            }
        }

        public void AddPixelData(ref byte[] pixelData, int offset)
        {
            foreach (FsFont font in m_fonts)
            {
                m_statusUpdater.UpdateStatusAndFilesConverted("Converting Font pixeldata: " + font.Name, 95);
                AddFontPixelData(font, m_builderSettings.PixelDataFormat, ref pixelData, offset);
            }
        }

        private static void AddFontPixelData(FsFont font, PixelDataFormat pixelDataFormat, ref byte[] fontPixelData, int offset)
        {
            foreach (CharacterInfo charInfo in font.CharacterInfos)
            {
                if (!FileUtils.CanImportFile(charInfo.FileExtension))
                {
                    Log.Error($"Cannot import file: {charInfo.FilePath} with extension: {charInfo.FileExtension}");
                    throw new ImageBuilderException($"Cannot import font character bitmap: {charInfo.ASSCI}");
                }

                if (charInfo.DataCompression == FontDataCompression.None)
                {
                    using Bitmap bitmap = new(charInfo.FilePath, true);
                    int dataOffset = fontPixelData.Length;

                    byte[] convertedPixelData = PixelDataConverter.GetConvertedPixelData(bitmap, pixelDataFormat);

                    ArrayUtils.AppendToArray(ref fontPixelData, convertedPixelData);
                    charInfo.UpdateValues((uint)(dataOffset + offset), (ushort)bitmap.Width, (ushort)bitmap.Height, (uint)convertedPixelData.Length);
                }
                else if (charInfo.DataCompression == FontDataCompression.PixelDataFileOptimized)
                {
                    using Bitmap bitmap = new(charInfo.FilePath, true);
                    uint compressedPixels = 0;
                    int dataOffset = fontPixelData.Length;

                    byte[] convertedPixelData = PixelDataConverter.GetCompressedPixelData(bitmap, pixelDataFormat, ref compressedPixels);

                    ArrayUtils.AppendToArray(ref fontPixelData, convertedPixelData);
                    charInfo.UpdateValues((uint)(dataOffset + offset), (ushort)bitmap.Width, (ushort)bitmap.Height, (uint)convertedPixelData.Length);
                }
                else
                {
                }
            }
        }

        public void AddOptimizedPixelData(ref byte[] pixelData, int offset)
        {
            foreach (FsFont font in m_fonts)
            {
                m_statusUpdater.UpdateStatusAndFilesConverted("Converting Font pixeldata: " + font.Name, 95);
                AddOptimizedFontPixelData(font, m_builderSettings.PixelDataFormat, ref pixelData, offset);
            }
        }

        private static void AddOptimizedFontPixelData(FsFont font, PixelDataFormat pixelDataFormat, ref byte[] fontPixelData, int offset)
        {
            foreach (CharacterInfo charInfo in font.CharacterInfos)
            {
                if (!FileUtils.CanImportFile(charInfo.FileExtension))
                {
                    Log.Error($"Cannot import file: {charInfo.FilePath} with extension: {charInfo.FileExtension}");
                    throw new ImageBuilderException($"Cannot import font character bitmap: {charInfo.ASSCI}");
                }

                if (charInfo.DataCompression == FontDataCompression.SourcePixelDataFileOptimized)
                {
                    using Bitmap bitmap = new(charInfo.FilePath, true);
                    int dataOffset = fontPixelData.Length;

                    byte[] convertedPixelData = PixelDataConverter.GetOptimizedPixelData(bitmap);
                    uint compressedPixels = (uint)(convertedPixelData.Length / 2);
                    ArrayUtils.AppendToArray(ref fontPixelData, convertedPixelData);

                    charInfo.UpdateValues((uint)(dataOffset + offset), (ushort)bitmap.Width, (ushort)bitmap.Height, (uint)convertedPixelData.Length, compressedPixels);
                }
                else
                {
                }
            }
        }

        private static void SortCharacterSet(FsFont font)
        {
            font.CharacterInfos.Sort((x, y) => x.ASSCI.CompareTo(y.ASSCI));
        }
    }
}
