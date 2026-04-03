using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FSColorHeaderGenerator
    {
        public static void CreateColorHeader(List <FSColor> colors)
        {
            if (colors.Count == 0)
            {
                return;
            }

            string headerFilePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.COLOR_FILE + ".h");
            StreamWriter sw = new(headerFilePath);

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.VERSION_FILE);
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
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.VERSION_FILE);

            sw.Close();
        }
    }
}
