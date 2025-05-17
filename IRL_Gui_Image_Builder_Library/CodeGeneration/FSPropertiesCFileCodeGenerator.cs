using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FSPropertiesCFileCodeGenerator
    {
        public static void CreateFileSystemCFile(ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.SearchTreeFile + ".c");

            CodeGenegrationUtils.Include(sw, FileConstants.SearchTreeFile);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_FILE_INFO_SIZE", fsbBuilder.SizeOfFileInfo.ToString());
            CodeGenegrationUtils.Define(sw, "FS_FILE_DUMMY", "0xFFFFFFFFU");
            CodeGenegrationUtils.BlankLine(sw);

            if (builderSettings.FileSystemFormat.SeparateSearchTreeFromData)
            {
                sw.WriteLine("const fs_file_info_s fs_file_infos[] =");
                sw.WriteLine("{");
                foreach (FsbFileInfo fsbFileInfo in fsbBuilder.FsbFileInfos)
                {
                    WriteFileInfo(sw, fsbFileInfo, builderSettings.PropertiesUsed);
                }
                sw.WriteLine("};");
            }
            else
            {
                sw.WriteLine("extern bool fs_readData(const int32_t offset, uint8_t *p_out_data, const int32_t size);");
                CodeGenegrationUtils.BlankLine(sw);
                sw.WriteLine("static bool fs_readFileInfo(const int32_t offset, fs_file_info_s *p_out_file_info);");
            }

            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("const uint8_t maxProperty[FS_MAX_FILE_PROPERTIES] =");
            sw.WriteLine("{");
            string maxProperties = GetMaxPropertyValues(builderSettings.Properties, builderSettings.PropertiesUsed);
            sw.WriteLine("    " + maxProperties);
            sw.WriteLine("};");
            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("file_search_result_e fs_getFileInfo(");
            sw.WriteLine("                            const file_key_e file_key,");
            sw.WriteLine("                            const uint8_t *p_properties,");
            sw.WriteLine("                            const uint8_t propertiesLength,");
            sw.WriteLine("                            fs_file_info_s *p_out_file_info)");

            if (builderSettings.FileSystemFormat.SeparateSearchTreeFromData)
            {
                sw.WriteLine("{");
                WriteFileSearch(sw);
                sw.WriteLine("}");
            }
            else
            {
                sw.WriteLine("{");
                WriteFileSearchInDataFile(sw);
                sw.WriteLine("}");
                CodeGenegrationUtils.BlankLine(sw);
                WriteReadFileInfoFuncion(sw, fsbBuilder.SizeOfFileInfo);
            }

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }

        public static void WriteFileInfo(StreamWriter sw, FsbFileInfo fsbFileInfo, int maxProperties)
        {
            FsbFile file = fsbFileInfo.FsbFile;

            if (maxProperties != 0)
            {
                string propertiesStr = ", .properties = " + file.Properties;
                string filename = fsbFileInfo.IsDummy ? "Dummy file, " + fsbFileInfo.FileKey : fsbFileInfo.Filename;
                string comment = "    /* " + filename + ", " + Convert.ToString(file.Properties, 2) + " */";

                sw.WriteLine("    { .dataOffset = " + file.DataOffset.ToString() + propertiesStr + ", .width = " + file.Width.ToString() + ", .height = " + file.Height.ToString() + " }," + comment);
            }
            else
            {
                sw.WriteLine("    { .dataOffset = " + file.DataOffset.ToString() + ", .width = " + file.Width.ToString() + ", .height = " + file.Height.ToString() + " },");
            }
        }

        private static string GetMaxPropertyValues(Property[] properties, int maxProperties)
        {
            string maxPropertyValues = "";

            for (int i = 0; i < maxProperties; i++)
            {
                Property property = properties[i];

                if (property.Use)
                {
                    maxPropertyValues += property.Max.ToString() + ", ";
                }
            }

            return maxPropertyValues;
        }

        private static void WriteFileSearch(StreamWriter sw)
        {
            sw.WriteLine("    const int32_t fileIndex = (int32_t)file_key;");
            sw.WriteLine("");
            sw.WriteLine("    if ((fileIndex < 0) || (fileIndex >= FS_FILES))");
            sw.WriteLine("    {");
            sw.WriteLine("        return FILE_SEARCH_OUT_OF_BOUNDS;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    if (0U == propertiesLength)");
            sw.WriteLine("    {");
            sw.WriteLine("        *p_out_file_info = fs_file_infos[fileIndex];");
            sw.WriteLine("");
            sw.WriteLine("        return FILE_SEARCH_OK;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    if (FS_MAX_FILE_PROPERTIES != propertiesLength)");
            sw.WriteLine("    {");
            sw.WriteLine("        return FILE_SEARCH_PROPERTY_LENGTH;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    const fs_file_info_s *p_fileInfo = &fs_file_infos[fileIndex];");
            sw.WriteLine("    ");
            sw.WriteLine("    if (p_fileInfo->properties == 0)");
            sw.WriteLine("    {");
            sw.WriteLine("        *p_out_file_info = fs_file_infos[fileIndex];");
            sw.WriteLine("");
            sw.WriteLine("        return FILE_SEARCH_OK;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    uint32_t keyOffset = 0;");
            sw.WriteLine("    uint32_t multiplier = 1;");
            sw.WriteLine("");
            sw.WriteLine("    for (int32_t i = (propertiesLength - 1); i >= -1; i--)");
            sw.WriteLine("    {");
            sw.WriteLine("        const uint16_t propertyBit = (p_fileInfo->properties & (0x01U << i));");
            sw.WriteLine("");
            sw.WriteLine("        if (propertyBit > 0)");
            sw.WriteLine("        {");
            sw.WriteLine("            if (maxProperty[i] > p_properties[i])");
            sw.WriteLine("            {");
            sw.WriteLine("                keyOffset += (multiplier * p_properties[i]);");
            sw.WriteLine("                multiplier *= maxProperty[i];");
            sw.WriteLine("            }");
            sw.WriteLine("            else");
            sw.WriteLine("            {");
            sw.WriteLine("                return FILE_SEARCH_PROPERTY_NOT_FOUND;");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    *p_out_file_info = fs_file_infos[fileIndex + keyOffset];");
            sw.WriteLine("");
            sw.WriteLine("    if (FS_FILE_DUMMY == p_out_file_info->dataOffset)");
            sw.WriteLine("    {");
            sw.WriteLine("        return FILE_SEARCH_PROPERTY_NOT_FOUND;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return FILE_SEARCH_OK;");
        }

        private static void WriteFileSearchInDataFile(StreamWriter sw)
        {
            sw.WriteLine("    const int32_t fileIndex = (int32_t)file_key;");
            sw.WriteLine("    uint32_t offset = fileIndex * FS_FILE_INFO_SIZE;");
            sw.WriteLine("");
            sw.WriteLine("    if ((fileIndex < 0) || (fileIndex >= FS_FILES))");
            sw.WriteLine("    {");
            sw.WriteLine("        return FILE_SEARCH_OUT_OF_BOUNDS;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    bool fileFound = fs_readFileInfo(offset, p_out_file_info);");
            sw.WriteLine("    if (!fileFound)");
            sw.WriteLine("    {");
            sw.WriteLine("        return FILE_SEARCH_ERROR_READING_DATA;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    if ((0U == propertiesLength) || (0U == p_out_file_info->properties))");
            sw.WriteLine("    {");
            sw.WriteLine("        return ((FS_FILE_DUMMY == p_out_file_info->dataOffset) ? FILE_SEARCH_DUMMY_NO_DATA : FILE_SEARCH_OK);");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    if (FS_MAX_FILE_PROPERTIES != propertiesLength)");
            sw.WriteLine("    {");
            sw.WriteLine("        return FILE_SEARCH_PROPERTY_LENGTH;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    uint32_t keyOffset = 0;");
            sw.WriteLine("    uint32_t multiplier = 1;");
            sw.WriteLine("");
            sw.WriteLine("    for (int32_t i = (propertiesLength - 1); i >= -1; i--)");
            sw.WriteLine("    {");
            sw.WriteLine("        const uint16_t propertyBit = (p_out_file_info->properties & (0x01U << i));");
            sw.WriteLine("");
            sw.WriteLine("        if (propertyBit > 0)");
            sw.WriteLine("        {");
            sw.WriteLine("            if (maxProperty[i] > p_properties[i])");
            sw.WriteLine("            {");
            sw.WriteLine("                keyOffset += (multiplier * p_properties[i]);");
            sw.WriteLine("                multiplier *= maxProperty[i];");
            sw.WriteLine("            }");
            sw.WriteLine("            else");
            sw.WriteLine("            {");
            sw.WriteLine("                return FILE_SEARCH_PROPERTY_NOT_FOUND;");
            sw.WriteLine("            }");
            sw.WriteLine("        }");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    offset += (keyOffset * FS_FILE_INFO_SIZE);");
            sw.WriteLine("");
            sw.WriteLine("    fileFound = fs_readFileInfo(offset, p_out_file_info);");
            sw.WriteLine("    if (!fileFound)");
            sw.WriteLine("    {");
            sw.WriteLine("        return FILE_SEARCH_ERROR_READING_DATA;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return ((FS_FILE_DUMMY == p_out_file_info->dataOffset) ? FILE_SEARCH_DUMMY_NO_DATA : FILE_SEARCH_OK);");
        }

        private static void WriteReadFileInfoFuncion(StreamWriter sw, int sizeOfFileInfo)
        {
            sw.WriteLine("static bool fs_readFileInfo(const int32_t offset, fs_file_info_s *p_out_file_info)");
            sw.WriteLine("{");
            sw.WriteLine("    uint8_t data[FS_FILE_INFO_SIZE];");
            sw.WriteLine("    const bool fileFound = fs_readData(offset, data, FS_FILE_INFO_SIZE);");
            sw.WriteLine("");
            sw.WriteLine("    if (fileFound)");
            sw.WriteLine("    {");
            if (sizeOfFileInfo == 9)
            {
                sw.WriteLine("        p_out_file_info->dataOffset = *((uint32_t *)&data[0]);");
                sw.WriteLine("        p_out_file_info->properties = *((uint8_t *)&data[4]);");
                sw.WriteLine("        p_out_file_info->width = *((uint16_t *)&data[5]);");
                sw.WriteLine("        p_out_file_info->height = *((uint16_t *)&data[7]);");
            }
            else
            {
                sw.WriteLine("        p_out_file_info->dataOffset = *((uint32_t *)&data[0]);");
                sw.WriteLine("        p_out_file_info->properties = *((uint16_t *)&data[4]);");
                sw.WriteLine("        p_out_file_info->width = *((uint16_t *)&data[6]);");
                sw.WriteLine("        p_out_file_info->height = *((uint16_t *)&data[8]);");
            }
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return fileFound;");
            sw.WriteLine("}");
        }
    }
}
