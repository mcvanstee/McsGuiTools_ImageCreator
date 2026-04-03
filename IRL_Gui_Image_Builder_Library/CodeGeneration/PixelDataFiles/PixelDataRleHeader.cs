using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.PixelDataFiles
{
    public static class PixelDataRleHeader
    {
        public static void CreatePixelDataHeader(ImageBuilderSettings builderSettings)
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.PIXEL_DATA_RLE_FILE + ".h");
            StreamWriter sw = new(filePath);

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.PIXEL_DATA_RLE_FILE);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.BlankLine(sw);


            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.PIXEL_DATA_RLE_FILE);
            sw.Close();
        }
    }
}
