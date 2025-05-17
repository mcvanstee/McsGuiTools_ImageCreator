using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class CrcHeaderGenerator
    {
        public static void CreateCrcHeader(string projectPath)
        {
            StreamWriter sw = new StreamWriter(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.CrcFile + ".h");

            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.SearchTreeFile);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.IncludeStdLib(sw);
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine("uint32_t crc_32(const unsigned char *input_str, size_t num_bytes);");
            sw.WriteLine("uint32_t update_crc_32(uint32_t crc, unsigned char c);");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.SearchTreeFile);

            sw.Close();
        }
    }
}
