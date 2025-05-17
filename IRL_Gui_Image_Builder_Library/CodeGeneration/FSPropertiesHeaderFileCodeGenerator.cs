using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FSPropertiesHeaderFileCodeGenerator
    {
        public static void CreateFileKeyHeader(ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder)
        {
            ImageBuilderSettings settings = builderSettings;
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.SearchTreeFile + ".h");
            int bytesPerPixel = settings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? 3 : 2;
            int longestFileKeyLength = GetLongestFileKeyLength(fsbBuilder.FsbFileInfos);

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
            CodeGenegrationUtils.Define(sw, "FS_MAX_FILE_PROPERTIES", settings.PropertiesUsed + "U");
            CodeGenegrationUtils.Define(sw, "FS_BYTES_PER_PIXEL", bytesPerPixel.ToString());
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine("typedef struct");
            sw.WriteLine("{");
            sw.WriteLine("    uint32_t dataOffset; /* Pixeldata starts at this byte offset */");
            if (settings.PropertiesUsed > 8)
            {
                sw.WriteLine("    uint16_t properties; /* Properties in use */");
            }
            else
            {
                sw.WriteLine("    uint8_t properties;  /* Properties in use */");
            }
            sw.WriteLine("    uint16_t width;      /* Width of image in pixels */");
            sw.WriteLine("    uint16_t height;     /* Height of image in pixels */");
            sw.WriteLine("} fs_file_info_s;");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddFileSearchResultEnum(sw);
            CodeGenegrationUtils.BlankLine(sw);
            AddProperties(sw, settings);
            CodeGenegrationUtils.BlankLine(sw);
            AddPropertyValues(sw, settings);
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine("typedef enum");
            sw.WriteLine("{");

            foreach (FsbFileInfo fsbFileInfo in fsbBuilder.FsbFileInfos)
            {
                string key = "FILE_KEY_";

                if (!fsbFileInfo.HasFileProperties)
                {
                    sw.WriteLine("    " + key + fsbFileInfo.FileKey + " = " + fsbFileInfo.FileIndex.ToString() + ",");
                }
                else if (fsbFileInfo.IsRootFileProperty)
                {
                    int offset = longestFileKeyLength - fsbFileInfo.FileNameWithoutProperties.Length - fsbFileInfo.FileIndex.ToString().Length;
                    offset += 2;

                    string comment = GetFilePropertiesComment(fsbFileInfo.FsbFileProperties, offset);
                    sw.WriteLine("    " + key + fsbFileInfo.FileNameWithoutProperties.ToUpper() + " = " + fsbFileInfo.FileIndex.ToString() + "," + comment);
                }
                else
                {
                    // Do not add to file key enum
                }
            }
            sw.WriteLine("    FILE_KEY_NONE = 0xFFFFFFFF");
            sw.WriteLine("} file_key_e;");
            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("/**");
            sw.WriteLine("* @brief  Search the file info according to the file-key");
            sw.WriteLine("*         and copy the data to the fs_file_info_s struct.");
            sw.WriteLine("* @param  file_key a value from the file_key_e enum");
            sw.WriteLine("* @param  p_out_file_info Pointer to a fs_file_info_s structure");
            sw.WriteLine("*         where the file info data is copied to.");
            sw.WriteLine("* @retval file_search_result_e");
            sw.WriteLine("*/");
            sw.WriteLine("file_search_result_e fs_getFileInfo(");
            sw.WriteLine("                            const file_key_e file_key,");
            sw.WriteLine("                            const uint8_t *p_properties,");
            sw.WriteLine("                            const uint8_t propertiesLength,");
            sw.WriteLine("                            fs_file_info_s *p_out_file_info);");
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.SearchTreeFile);

            sw.Close();
        }

        private static void AddProperties(StreamWriter sw, ImageBuilderSettings settings)
        {
            sw.WriteLine("typedef enum");
            sw.WriteLine("{");

            int propertyKeyValue = 0;

            foreach (Property property in settings.Properties)
            {
                if (property.Use)
                {
                    string propertyKey = GetPropertyKey(property);

                    sw.WriteLine("    " + propertyKey + " = " + propertyKeyValue + ",");

                    propertyKeyValue += 1;
                }
            }

            sw.WriteLine("} file_property_e;");
        }

        private static void AddPropertyValues(StreamWriter sw, ImageBuilderSettings settings)
        {
            foreach (Property property in settings.Properties)
            {
                if (property.Use)
                {
                    sw.WriteLine("typedef enum");
                    sw.WriteLine("{");

                    string propertyName = GetPropertyName(property);

                    for (int i = 0; i < property.Max; i++)
                    {
                        string propertyValue = GetPropertyValue(property.PropertyValues[i], propertyName);
                        //string propertyValue = "PROPERTY_" + propertyName + "_VALUE_";

                        //if (string.IsNullOrEmpty(property.PropertyValues[i].Alias))
                        //{
                        //    propertyValue += property.PropertyValues[i].Name.ToUpper();
                        //}
                        //else
                        //{
                        //    propertyValue += property.PropertyValues[i].Alias.ToUpper().Trim();
                        //}

                        sw.WriteLine("    " + propertyValue + " = " + i.ToString() + ",");
                    }

                    sw.WriteLine("} property_value_" + propertyName.ToLower() + "_e;");
                    CodeGenegrationUtils.BlankLine(sw);
                }
            }
        }

        private static int GetLongestFileKeyLength(List<FsbFileInfo> fsbFileInfos)
        {
            int length = 0;
            int lengthFileValue = 0;

            foreach (FsbFileInfo fsbFileInfo in fsbFileInfos)
            {
                if (!string.IsNullOrEmpty(fsbFileInfo.FileNameWithoutProperties))
                {
                    if (fsbFileInfo.FileNameWithoutProperties.Length > length)
                    {
                        length = fsbFileInfo.FileNameWithoutProperties.Length;
                    }

                    if (fsbFileInfo.FileIndex.ToString().Length > lengthFileValue)
                    {
                        lengthFileValue = fsbFileInfo.FileIndex.ToString().Length;
                    }
                }
            }

            return length + lengthFileValue;
        }

        private static string GetFilePropertiesComment(List<FsbFileProperty> fsbFileProperties, int offset)
        {
            string result = "";

            for (int i = 0; i < offset; i++)
            {
                result += " ";
            }

            result += "/*";

            foreach (FsbFileProperty fsbFileProperty in fsbFileProperties)
            {
                result += " ";
                result += fsbFileProperty.Name;
                result += ": ";
                result += fsbFileProperty.Alias;
            }

            result += " */";

            return result;
        }

        private static string GetPropertyKey(Property property)
        {
            string propertyKey = "FILE_PROPERTY_" + GetPropertyName(property);

            return propertyKey;
        }

        private static string GetPropertyName(Property property)
        {
            string propertyName;

            if (string.IsNullOrEmpty(property.Alias))
            {
                propertyName = property.Name.ToUpper();
            }
            else
            {
                propertyName = property.Alias.ToUpper().Trim();
                propertyName = propertyName.Replace(" ", "_");
            }

            return propertyName;
        }

        private static string GetPropertyValue(PropertyValue propertyValue, string propertyName)
        {
            string propertyValueStr = "PROPERTY_" + propertyName + "_VALUE_";

            if (string.IsNullOrEmpty(propertyValue.Alias))
            {
                propertyValueStr += propertyValue.Name.ToUpper().Trim();
            }
            else
            {
                propertyValueStr += propertyValue.Alias.ToUpper().Trim();
            }

            propertyValueStr = propertyValueStr.Replace(" ", "_");

            return propertyValueStr;
        }
    }
}
