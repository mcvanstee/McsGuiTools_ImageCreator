using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.PixelDataFiles
{
    public static class PixelDataHeader
    {
        public static void CreatePixelDataHeader(ImageBuilderSettings builderSettings, FsbBuilder fsbBuilder)
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.PIXEL_DATA_RLE_A_FILE + ".h");
            StreamWriter sw = new(filePath);

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.PIXEL_DATA_RLE_A_FILE);
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
                "uint32_t fs_read(uint16_t *p_buffer, uint32_t bufferLength, fs_pixeldata_info_s *p_pixelDataInfo);\n" +
                "void fs_transferPixels(fs_pixeldata_info_s *p_pixelDataInfo, void (*transferPixels)(const uint16_t color, const int32_t noOfpixels));\n" +
                "uint16_t fs_getPixelColor(const uint32_t foreColor, const uint32_t backColor, const uint8_t pixelValue);"
                );
            
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.PIXEL_DATA_RLE_A_FILE);
            sw.Close();
        }
    }
}
