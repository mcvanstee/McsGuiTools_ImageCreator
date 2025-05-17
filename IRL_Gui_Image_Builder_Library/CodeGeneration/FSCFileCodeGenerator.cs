using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FSCFileCodeGenerator
    {
        public static void CreateFileSystemCFile(ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.SearchTreeFile + ".c");

            CodeGenegrationUtils.Include(sw, FileConstants.SearchTreeFile);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_FILE_INFO_SIZE", "sizeof(fs_file_info_s)");
            CodeGenegrationUtils.BlankLine(sw);
            if (builderSettings.FileSystemFormat.SeparateSearchTreeFromData)
            {
                sw.WriteLine("const fs_file_info_s fs_file_infos[] =");
                sw.WriteLine("{");
                foreach (FsbFileInfo fsbFileInfo in fsbBuilder.FsbFileInfos)
                {
                    FsbFile file = fsbFileInfo.FsbFile;
                    sw.WriteLine("    { .dataOffset = " + file.DataOffset.ToString() + ", .width = " + file.Width.ToString() + ", .height = " + file.Height.ToString() + " },");
                }
                sw.WriteLine("};");
            }
            else
            {
                sw.WriteLine("extern bool fs_readData(const int32_t offset, uint8_t *p_out_data, const int32_t size);");
            }
            sw.WriteLine("");
            sw.WriteLine("bool fs_getFileInfo(const file_key_e file_key, fs_file_info_s *p_out_file_info)");
            sw.WriteLine("{");

            if (builderSettings.FileSystemFormat.SeparateSearchTreeFromData)
            {
                sw.WriteLine("    bool fileFound = false;");
                sw.WriteLine("    const uint32_t fileIndex = (int32_t)file_key;");
                sw.WriteLine("");
                sw.WriteLine("    if (FS_FILES > fileIndex)");
                sw.WriteLine("    {");
                sw.WriteLine("        *p_out_file_info = fs_file_infos[fileIndex];");
                sw.WriteLine("        fileFound = true;");
                sw.WriteLine("    }");
                sw.WriteLine("");
                sw.WriteLine("    return fileFound;");
            }
            else
            {
                sw.WriteLine("    bool fileFound = false;");
                sw.WriteLine("    const uint32_t fileIndex = (int32_t)file_key;");
                sw.WriteLine("    const uint32_t offset = fileIndex * FS_FILE_INFO_SIZE;");
                sw.WriteLine("");
                sw.WriteLine("    if ((FS_FILES > fileIndex) && (fileIndex > 0))");
                sw.WriteLine("    {");
                sw.WriteLine("        uint8_t data[FS_FILE_INFO_SIZE];");
                sw.WriteLine("        fileFound = fs_readData(offset, data, FS_FILE_INFO_SIZE);");
                sw.WriteLine("");
                sw.WriteLine("        p_out_file_info->dataOffset = *((uint32_t *)&data[0]);");
                sw.WriteLine("        p_out_file_info->width = *((uint16_t *)&data[4]);");
                sw.WriteLine("        p_out_file_info->height = *((uint16_t *)&data[6]);");
                sw.WriteLine("    }");
                sw.WriteLine("");
                sw.WriteLine("    return fileFound;");
            }
            sw.WriteLine("}");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }
    }
}
