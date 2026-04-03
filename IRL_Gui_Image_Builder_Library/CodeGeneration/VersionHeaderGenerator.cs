using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class VersionHeaderGenerator
    {
        public static void CreateVersionHeader(ImageBuilderSettings builderSettings)
        {
            string versionFilePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.VERSION_FILE + ".h");
            StreamWriter sw = new(versionFilePath);

            string version = builderSettings.GetVerion().Replace(".", "_");
            string dataFileName = builderSettings.GuiPixelDataFile + "_" + version + FileConstants.IMAGE_FILE_EXTENSION;
            string value = "\"" + dataFileName + "\"";

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.VERSION_FILE);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddVersion(sw, builderSettings);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_IMAGE_FILE_NAME", value);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.VERSION_FILE);

            sw.Close();
        }
    }
}
