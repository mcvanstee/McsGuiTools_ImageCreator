using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FSColorHeaderGenerator
    {
        public static void CreateColorHeader(List <FSColor> colors, string projectPath)
        {
            if (colors.Count == 0)
            {
                return;
            }

            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.ColorFile + ".h");

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.VersionFile);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);

            foreach (var color in colors)
            {
                string colorName = $"COLOR_{color.Name.Replace(" ", "_").ToUpper()}";
                string colorValue = $"0x{color.Value:X6}";
                CodeGenegrationUtils.Define(sw, colorName, colorValue);
            }

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.VersionFile);

            sw.Close();
        }
    }
}
