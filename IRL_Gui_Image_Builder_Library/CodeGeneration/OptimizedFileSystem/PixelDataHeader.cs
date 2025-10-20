using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.OptimizedFileSystem
{
    public static class PixelDataHeader
    {
        public static void CreatePixelDataHeader(ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.PixelDataFile + ".h");


            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.PixelDataFile);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "typedef struct\n" +
                "{\n" +
                "    uint32_t readIndex;\n" +
                "    uint32_t pixelsToRead;\n" +
                "    uint16_t noOfPixelsLeft;\n" +
                "    uint16_t colorPixelLeft;\n" +
                "    uint32_t foreColor;\n" +
                "    uint32_t backColor;\n" +
                "} fs_pixeldata_info_s;"
                );

            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine(
                "void fs_read(uint16_t *p_buffer, uint32_t bufferLength, fs_pixeldata_info_s *p_pixelDataInfo);\n" +
                "void fs_transferPixels(fs_pixeldata_info_s *p_pixelDataInfo, void (*transferPixels)(const uint16_t color, const int32_t noOfpixels));"
                );
            
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.PixelDataFile);
            sw.Close();
        }
    }
}
