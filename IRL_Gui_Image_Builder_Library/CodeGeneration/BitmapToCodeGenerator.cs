using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.Projects;
using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class BitmapToCodeGenerator
    {
        public static void CreateFiles(ImageBuilderSettings builderSettings, string projectPath)
        {
            string[] files = Directory.GetFiles(projectPath + FileConstants.BmpImportFolder);

            CreateHeaderFile(builderSettings, projectPath, files);
            CreateCodeFile(builderSettings, projectPath, files);
        }

        private static void CreateHeaderFile(ImageBuilderSettings builderSettings, string projectPath, string[] files)
        {
            StreamWriter sw = new StreamWriter(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.BitmapDataFile + ".h");
            string pixelDataSize = builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? "uint32_t" : "uint16_t";

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.BitmapDataFile);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.BlankLine(sw);

            foreach (string filePath in files)
            {
                string extension = filePath.Substring(filePath.IndexOf('.'));

                if (FileUtils.CanImportFile(extension))
                {
                    string filename = ImageBuilder.GetFileName(filePath);
                    filename = filename.Replace(" ", "_");
                    filename = filename.Replace("-", "_");

                    string bitmapName = $"extern const {pixelDataSize} {filename}[];";
                    sw.WriteLine(bitmapName);
                }
            }

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.BitmapDataFile);

            sw.Close();
        }

        private static void CreateCodeFile(ImageBuilderSettings builderSettings, string projectPath, string[] files)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.BitmapDataFile + ".c");
            string pixelDataSize = builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? "uint32_t" : "uint16_t";

            CodeGenegrationUtils.Include(sw, FileConstants.BitmapDataFile);
            CodeGenegrationUtils.BlankLine(sw);

            foreach (string filePath in files)
            {
                string extension = filePath.Substring(filePath.IndexOf('.'));

                if (FileUtils.CanImportFile(extension))
                {
                    using Bitmap bitmap = new(filePath, true);
                    byte[] convertedPixelData = PixelDataConverter.GetConvertedPixelData_1(bitmap, builderSettings.PixelDataFormat);

                    string filename = ImageBuilder.GetFileName(filePath);
                    filename = filename.Replace(" ", "_");
                    filename = filename.Replace("-", "_");

                    string bitmapName = $"const {pixelDataSize} {filename}[] =";
                    sw.WriteLine(bitmapName);
                    sw.WriteLine("{");

                    int lineLength = 0;

                    for (int i = 0; i < convertedPixelData.Length; i += 2)
                    {
                        ushort pixelData = (ushort)(convertedPixelData[i] | (convertedPixelData[i + 1] << 8));

                        if (lineLength == 0)
                        {
                            sw.Write("   ");
                        }

                        sw.Write($" 0x{pixelData:X4},");

                        lineLength++;

                        if (lineLength == 20)
                        {
                            sw.WriteLine();
                            lineLength = 0;
                        }
                    }

                    if (lineLength != 0)
                    {
                        sw.WriteLine();
                    }

                    sw.WriteLine("};");
                    CodeGenegrationUtils.BlankLine(sw);
                }
            }

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }
    }
}
