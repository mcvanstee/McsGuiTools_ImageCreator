using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FSCFileCodeGenerator
    {
        public static void CreateFileSystemCFile(ImageBuilderSettings builderSettings, FsbBuilder fsbBuilder)
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.SEARCH_TREE_FILE + ".c");
            StreamWriter sw = new(filePath);

            CodeGenegrationUtils.Include(sw, FileConstants.SEARCH_TREE_FILE);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_FILE_INFO_SIZE", "sizeof(fs_file_info_s)");
            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("const fs_file_info_s fs_file_infos[] =");
            sw.WriteLine("{");
            foreach (FsbFileInfo fsbFileInfo in fsbBuilder.FileInfos)
            {
                FsbFile file = fsbFileInfo.FsbFile;
                sw.WriteLine("    { .dataOffset = " + file.DataOffset.ToString() + ", .width = " + file.Width.ToString() + ", .height = " + file.Height.ToString() + " },");
            }
            sw.WriteLine("};");
            sw.WriteLine("");
            sw.WriteLine("bool fs_getFileInfo(const file_key_e file_key, fs_file_info_s *p_out_file_info, uint8_t *p_dataLocation)");
            sw.WriteLine("{");
            sw.WriteLine("    const uint32_t fileIndex = (uint32_t)((int32_t)file_key - 1);");
            sw.WriteLine("");
            sw.WriteLine("    if ((int32_t)file_key <= 0)");
            sw.WriteLine("    {");
            sw.WriteLine("        return false;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            CodeGenegrationUtils.WriteDataLocationFileIndex(sw, fsbBuilder);
            sw.WriteLine("");
            sw.WriteLine("    bool fileFound = false;"); 
            sw.WriteLine("");
            sw.WriteLine("    if (FS_FILES > fileIndex)");
            sw.WriteLine("    {");
            sw.WriteLine("        *p_out_file_info = fs_file_infos[fileIndex];");
            sw.WriteLine("        fileFound = true;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return fileFound;");
            sw.WriteLine("}");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddFileCompressionFunction(sw, fsbBuilder.DataLocations);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }
    }
}
