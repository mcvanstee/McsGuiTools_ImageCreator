using IRL_Common_Library.Utils;
using IRL_Image_Creator_Tools.BuilderInstructions;
using IRL_Image_Creator_Tools.BuilderSettings;
using IRL_Image_Creator_Tools.Enums;
using IRL_String_To_Bitmap_Converter;
using System.Drawing;
using System.IO;

namespace IRL_Image_Creator_Tools.Builders
{
    public static class ImageBuilder
    {
        public static bool CreateBitmaps(List<BuilderInstruction> instructions, string outputFolder)
        {
            bool result = false;

            FileUtils.ClearDirectory(outputFolder);

            foreach (BuilderInstruction instruction in instructions)
            {
                if (instruction is FontInstrucion fontInstrucion)
                {
                    result = BuildFontBitmaps(fontInstrucion, outputFolder);
                }
                else if (instruction is TextInstruction textInstuction)
                {
                    BuildTextBitmaps(textInstuction, outputFolder);
                }
                //else if (instruction is ImageInstruction imageInstruction)
                //{

                //}
                else
                {
                    // Error
                }
            }

            return result;
        }

        public static bool BuildTextBitmaps(TextInstruction textInstuction, string outputFolder)
        {
            if (textInstuction == null)
            {
                return false;
            }

            TextToBitmapConverter converter = new(textInstuction.BuilderSetting);

            foreach (BitmapStyle bitmapStyle in textInstuction.Styles)
            {
                converter.ConvertTextFileToBitmaps(bitmapStyle, outputFolder);
            }

            return false;
        }

        public static bool BuildFontBitmaps(FontInstrucion fontInstrucion, string outputPath) 
        {
            int fontId = 1;

            foreach (FontBuilderSetting setting in fontInstrucion.BuilderSettings)
            {
                int styleId = 1;
                string fontPathPart = $"{outputPath}\\{setting.FontName}_FontId_{fontId}";

                Font font = new(setting.FontName, setting.FontSize, setting.FontStyle);

                foreach (FontBuilderStyle style in fontInstrucion.FontBuilderStyles)
                {
                    string path = $"{fontPathPart}_ColorId_{styleId}";
                    Directory.CreateDirectory(path);

                    for (int i = 32; i <= 126; i++)
                    {
                        string charString = ((char)i).ToString();
                        string filename = "C_" + i.ToString(); ;

                        StringToBitmapConverter.GenerateImage(
                            charString, font, 
                            fontInstrucion.Margin.Left, fontInstrucion.Margin.Top, fontInstrucion.Margin.Right, fontInstrucion.Margin.Bottom,
                            style.TextColor, style.BackColor, 
                            path, filename);
                    }

                    styleId += 1;
                }

                fontId += 1;
            }

            return false;
        }

        public static ClearFolderResult ClearOutputFolder(string outputFolder)
        {
            ClearFolderResult result = ClearFolderResult.FolderNotExist;

            if (Directory.Exists(outputFolder))
            {
                bool folderEmpty = FileUtils.ClearDirectory(outputFolder);
                if (folderEmpty)
                {
                    result = ClearFolderResult.Ok;
                }
                else
                {
                    result = ClearFolderResult.Error;
                }
            }

            return result;
        }
    }
}
