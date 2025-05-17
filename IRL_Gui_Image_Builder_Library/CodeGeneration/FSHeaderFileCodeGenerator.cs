using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FSHeaderFileCodeGenerator
    {
        public static void CreateFileKeyHeader(ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder)
        {
            StreamWriter sw = new StreamWriter(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.SearchTreeFile + ".h");
            int bytesPerPixel = builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? 3 : 2;

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.SearchTreeFile);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdBool(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.DefineIfNotDefined(sw, "FS_PIXEL_DATA_CRC", fsbBuilder.CRC.ToString() + "u");
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_FILES", fsbBuilder.FsbFileInfos.Count.ToString());
            CodeGenegrationUtils.Define(sw, "FS_BYTES_PER_PIXEL", bytesPerPixel.ToString());
            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("typedef struct");
            sw.WriteLine("{");
            sw.WriteLine("    uint32_t dataOffset;    /* Pixeldata starts at this byte offset */");
            sw.WriteLine("    uint16_t width;         /* Width of image in pixels */");
            sw.WriteLine("    uint16_t height;        /* Height of image in pixels */");
            sw.WriteLine("} fs_file_info_s;");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine("typedef enum");
            sw.WriteLine("{");

            foreach (FsbFileInfo fsbFileInfo in fsbBuilder.FsbFileInfos)
            {
                string key = "FILE_KEY_";

                sw.WriteLine("    " + key + fsbFileInfo.FileKey + " = " + fsbFileInfo.FileIndex.ToString() + ",");
            }

            sw.WriteLine("    FILE_KEY_NONE = 0xFFFFFFFF");
            sw.WriteLine("} file_key_e;");
            sw.WriteLine("");
            sw.WriteLine("/**");
            sw.WriteLine("* @brief  Search the file info according to the file-key");
            sw.WriteLine("*         and copy the data to the fs_file_info_s struct.");
            sw.WriteLine("* @param  file_key a value from the file_key_e enum");
            sw.WriteLine("* @param  p_out_file_info Pointer to a fs_file_info_s structure");
            sw.WriteLine("*         where the file info data is copied to.");
            sw.WriteLine("* @retval file_search_result_e");
            sw.WriteLine("*/");
            sw.WriteLine("bool fs_getFileInfo(const file_key_e file_key, fs_file_info_s *p_out_file_info);");
            sw.WriteLine("");
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.SearchTreeFile);

            sw.Close();
        }
    }
}
