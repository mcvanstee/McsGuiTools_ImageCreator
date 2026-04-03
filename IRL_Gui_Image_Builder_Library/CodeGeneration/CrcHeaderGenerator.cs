using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class CrcHeaderGenerator
    {
        public static void CreateCrcHeader()
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.CRC_FILE + ".h");
            StreamWriter sw = new StreamWriter(filePath);

            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.CRC_FILE);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.IncludeStdLib(sw);
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine("uint32_t crc_32(const unsigned char *input_str, size_t num_bytes);");
            sw.WriteLine("uint32_t update_crc_32(uint32_t crc, const unsigned char *input_str, size_t num_bytes);");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.SEARCH_TREE_FILE);

            sw.Close();
        }
    }
}
