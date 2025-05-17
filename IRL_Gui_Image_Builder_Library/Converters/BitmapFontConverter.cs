using IRL_Common_Library.Utils;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.Converters
{
    internal class BitmapCharacterInfo
    {
        public int StartPosition { get; set; }
        public int Width { get; set; }
    }

    public class BitmapFontConverter
    {
        private const int SpaceAsciiOffset = 32;
        private readonly List<BitmapCharacterInfo> m_characterInfos = new();
        private int m_spaceWidth;
        private readonly List<FileInfo> m_fontsFilesToImport = new();

        public void ConvertFontBitmapsToCharacterBitmaps(string importFolderPath)
        {
            AddAllFontFileNamesToImport(importFolderPath);

            if (m_fontsFilesToImport.Count == 0)
            {
                return;
            }

            foreach (FileInfo fileInfo in m_fontsFilesToImport)
            {
                ConvertFont(fileInfo);
            }
        }

        private void ConvertFont(FileInfo fileInfo)
        {
            string newFolderName = fileInfo.Name.Replace(" ", "_");
            newFolderName = newFolderName.Replace(fileInfo.Extension, "");
            string fontPath = fileInfo.DirectoryName + "\\" + newFolderName;
            string fontWidthDataPath = fileInfo.DirectoryName + "\\" + fileInfo.Name.Replace(fileInfo.Extension, "") + "_w" + fileInfo.Extension;

            m_characterInfos.Clear();
            m_spaceWidth = 0;

            try
            {
                Directory.CreateDirectory(fontPath);
            }
            catch
            {
                Log.Error("Can not create Folder: " + fontPath);
            }

            try
            {
                Bitmap fontWidthData = new(fontWidthDataPath, true);
                Bitmap fontData = new(fileInfo.FullName, true);

                if (fontWidthData.Width != fontData.Width)
                {
                    Debug.WriteLine("Width of font files not equal");

                    return;
                }

                GetCharacterWidths(fontWidthData);
                CreateCharacters(fontData, fontPath);
            }
            catch
            {
                Log.Error("Converting Font bitmap to Character bitmaps");
            }
        }

        private void GetCharacterWidths(Bitmap font)
        {
            List<int> spaceWidths = new();
            Color backgroundColor = font.GetPixel(0, 0);
            Color previousPixelColor = font.GetPixel(0, font.Height - 1);

            int spaceLength = 0;
            int characterLength = 1;
            int characterIndex = 0;

            m_characterInfos.Add(new BitmapCharacterInfo());
            m_characterInfos[0].StartPosition = 0;

            for (int i = 1; i < font.Width; i++)
            {
                Color newPixelColor = font.GetPixel(i, font.Height - 1);

                if (backgroundColor == newPixelColor) // New Pixel is Space
                {
                    if (previousPixelColor == backgroundColor) // Still space
                    {
                        // Handle space
                        //
                        spaceLength += 1;
                    }
                    else // Start of space
                    {
                        // End of character
                        //
                        m_characterInfos[characterIndex].Width = characterLength;
                        characterIndex += 1;

                        // Start of space
                        //
                        spaceLength = 1;
                    }
                }
                else // New Pixel is Character
                {
                    if (previousPixelColor == backgroundColor) // Start of character
                    {
                        // End of space
                        //
                        spaceWidths.Add(spaceLength);

                        // Start of character
                        //
                        characterLength = 1;

                        BitmapCharacterInfo characterInfo = new();
                        characterInfo.StartPosition = i;
                        m_characterInfos.Add(characterInfo);
                    }
                    else // Still character
                    {
                        // Handle character
                        //
                        characterLength += 1;
                    }
                }

                previousPixelColor = newPixelColor;

                if (i == font.Width - 1)
                {
                    m_characterInfos[characterIndex].Width = characterLength;
                }
            }

            int totalSumOfWidths = 0;

            foreach (int spaceWidth in spaceWidths)
            {
                totalSumOfWidths += spaceWidth;
            }

            m_spaceWidth = totalSumOfWidths / spaceWidths.Count;
        }

        private void CreateCharacters(Bitmap fontData, string fontPath)
        {
            CreateSpaceBitmap(fontData, fontPath);

            for (int characterIndex = 0; characterIndex < m_characterInfos.Count; characterIndex++)
            {
                CreateCharacterBitmap(fontData, fontPath, characterIndex);
            }
        }

        private void CreateSpaceBitmap(Bitmap fontData, string fontPath)
        {
            Color backgroundColor = fontData.GetPixel(0, 0);
            Bitmap bitmap = new(m_spaceWidth, fontData.Height);

            using (Graphics gfx = Graphics.FromImage(bitmap))
            using (SolidBrush brush = new(backgroundColor))
            {
                gfx.FillRectangle(brush, 0, 0, m_spaceWidth, fontData.Height);
            }

            SaveCharacterBitmap(bitmap, SpaceAsciiOffset, fontPath);
            bitmap.Dispose();
        }

        private void CreateCharacterBitmap(Bitmap fontData, string fontPath, int characterIndex)
        {
            BitmapCharacterInfo characterInfo = m_characterInfos[characterIndex];
            Bitmap newCharacter = new(characterInfo.Width, fontData.Height);

            for (int y = 0; y < fontData.Height; y++)
            {

                for (int x = 0; x < characterInfo.Width; x++)
                {
                    newCharacter.SetPixel(x, y, fontData.GetPixel(x + characterInfo.StartPosition, y));
                }
            }

            SaveCharacterBitmap(newCharacter, (SpaceAsciiOffset + characterIndex + 1), fontPath);
            newCharacter.Dispose();
        }

        private static void SaveCharacterBitmap(Bitmap character, int assciValue, string fontPath)
        {
            string charFileName = "C_" + assciValue;
            string charFilePath = fontPath + "\\" + charFileName + ".bmp";
            character.Save(charFilePath, ImageFormat.Bmp);
        }

        private void AddAllFontFileNamesToImport(string path)
        {
            DirectoryInfo directory = new(path);
            List<FileInfo> fontFiles = new();
            List<FileInfo> widthFontFiles = new();

            foreach (FileInfo fileInfo in directory.GetFiles())
            {
                if (fileInfo.Extension == ".png")
                {
                    if (!fileInfo.Name.EndsWith("_w.png"))
                    {
                        fontFiles.Add(fileInfo);
                    }
                    else
                    {
                        widthFontFiles.Add(fileInfo);
                    }
                }
                else
                {
                    Log.Warning("Wrong Font file extension, can not convert. Only PNG supported " + fileInfo.Name);
                }
            }

            foreach (FileInfo widthFontFileInfo in widthFontFiles)
            {
                foreach (FileInfo fontFileInfo in fontFiles)
                {
                    if (widthFontFileInfo.Name.Contains(fontFileInfo.Name.Replace(".png", "")))
                    {
                        m_fontsFilesToImport.Add(fontFileInfo);

                        break;
                    }
                }
            }

            if ((widthFontFiles.Count != fontFiles.Count) || (fontFiles.Count != m_fontsFilesToImport.Count))
            {
                Log.Warning("Can not convert Bitmap Font");
            }
        }
    }
}
