using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.Fonts;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FontCodeGenerator
    {
        public static void CreateFontCodeFiles(ImageBuilderSettings builderSettings, string projectPath, FontBuilder fontBuilder, uint crc)
        {
            CreateFontCharInfoSearchHeader(builderSettings, projectPath, fontBuilder, crc);

            if (builderSettings.FontDataInImage)
            {
                CreateFontCharInfoSearchInImageCFileOne(projectPath, fontBuilder);
            }
            else
            {
                CreateFontCharInfoSearchCFile(projectPath, fontBuilder);
            }
        }

        private static void CreateFontCharInfoSearchHeader(ImageBuilderSettings builderSettings, string projectPath, FontBuilder fontBuilder, uint crc)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.CharInfoSearchFile + ".h");
            int bytesPerPixel = builderSettings.PixelDataFormat.PixelFormat == GuiImageBuilder.Builder.PixelFormat.RGB ? 3 : 2;

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.CharInfoSearchFile);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdBool(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.DefineIfNotDefined(sw, "FS_PIXEL_DATA_CRC", crc.ToString() + "u");
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_FONTS", fontBuilder.Fonts.Count.ToString());
            CodeGenegrationUtils.Define(sw, "FS_CHAR_INFOS_IN_FONT", "95");
            CodeGenegrationUtils.Define(sw, "FS_BYTES_PER_PIXEL", bytesPerPixel.ToString());
            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("typedef enum");
            sw.WriteLine("{");
            for (int i = 0; i < fontBuilder.Fonts.Count; i++)
            {
                sw.WriteLine("    FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + " = " + i.ToString() + ",");
            }
            sw.WriteLine("} font_key_e;");
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine("typedef struct");
            sw.WriteLine("{");
            sw.WriteLine("    uint32_t dataOffset;    // Pixeldata starts at this byte offset");
            sw.WriteLine("    uint8_t width;          // Width of bmp in pixels");
            sw.WriteLine("    uint8_t height;         // Height of bmp in pixels");
            sw.WriteLine("} fs_char_info_s;");

            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("bool fs_getCharInfo(const char c, const font_key_e font_key, fs_char_info_s *p_out_char_info);");
            CodeGenegrationUtils.BlankLine(sw);

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.CharInfoSearchFile);

            sw.Close();
        }

        private static void CreateFontCharInfoSearchCFile(string projectPath, FontBuilder fontBuilder)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.CharInfoSearchFile + ".c");
            int fonts = fontBuilder.Fonts.Count;
            const int noOfChars = 95;

            CodeGenegrationUtils.Include(sw, FileConstants.CharInfoSearchFile);
            sw.WriteLine("");
            sw.WriteLine("const fs_char_info_s fs_char_info[" + fonts + "][" + noOfChars + "] =");
            sw.WriteLine("{");

            for (int i = 0; i < fontBuilder.Fonts.Count; i++)
            {
                sw.WriteLine("    {");
                foreach (CharacterInfo charInfo in fontBuilder.Fonts[i].CharacterInfos)
                {
                    byte width = (byte)charInfo.Width;
                    byte height = (byte)charInfo.Height;

                    sw.WriteLine("    { .dataOffset = " + charInfo.DataOffset.ToString() + ", .width = " + width.ToString() + ", .height = " + height.ToString() + " },");
                }
                sw.WriteLine("    },");
            }
            sw.WriteLine("};");
            sw.WriteLine("");
            sw.WriteLine("bool fs_getCharInfo(const char c, const font_key_e font_key, fs_char_info_s *p_out_char_info)");
            sw.WriteLine("{");
            sw.WriteLine("    bool charInfoFound = false;");
            sw.WriteLine("    const int32_t charIndex = (c - 32);");
            sw.WriteLine("    const int32_t fontIndex = (int32_t)font_key;");
            sw.WriteLine("");
            sw.WriteLine("    if ((FS_CHAR_INFOS_IN_FONT > charIndex) && (FS_FONTS > fontIndex) && (charIndex >= 0) && (fontIndex >= 0))");
            sw.WriteLine("    {");
            sw.WriteLine("        *p_out_char_info = fs_char_info[fontIndex][charIndex];");
            sw.WriteLine("        charInfoFound = true;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return charInfoFound;");
            sw.WriteLine("}");
            sw.WriteLine("");
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }

        private static void CreateFontCharInfoSearchInImageCFileOne(string projectPath, FontBuilder fontBuilder)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.CharInfoSearchFile + ".c");

            CodeGenegrationUtils.Include(sw, FileConstants.CharInfoSearchFile);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_CHAR_INFO_SIZE", "6");
            CodeGenegrationUtils.Define(sw, "FS_FONT_CHAR_INFO_SIZE", "(FS_CHAR_INFOS_IN_FONT * FS_CHAR_INFO_SIZE)");
            CodeGenegrationUtils.Define(sw, "FS_CHAR_INFO_OFFSET", fontBuilder.CharInfoOffset.ToString());
            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("extern bool fs_readData(const int32_t offset, uint8_t *p_out_data, const int32_t size);");
            sw.WriteLine("");
            sw.WriteLine("bool fs_getCharInfo(const char c, const font_key_e font_key, fs_char_info_s *p_out_char_info)");
            sw.WriteLine("{");
            sw.WriteLine("    bool charInfoFound = false;");
            sw.WriteLine("    const int32_t charIndex = (c - 32);");
            sw.WriteLine("    const int32_t fontIndex = (int32_t)font_key;");
            sw.WriteLine("    const int32_t offset = (charIndex * FS_CHAR_INFO_SIZE) + (fontIndex * FS_FONT_CHAR_INFO_SIZE) + FS_CHAR_INFO_OFFSET;");
            sw.WriteLine("");
            sw.WriteLine("    if ((FS_CHAR_INFOS_IN_FONT > charIndex) && (FS_FONTS > fontIndex) && (charIndex >= 0) && (fontIndex >= 0))");
            sw.WriteLine("    {");
            sw.WriteLine("        uint8_t data[FS_CHAR_INFO_SIZE];");
            sw.WriteLine("        charInfoFound = fs_readData(offset, data, FS_CHAR_INFO_SIZE);");
            sw.WriteLine("        p_out_char_info->dataOffset = *((uint32_t *)&data[0]);");
            sw.WriteLine("        p_out_char_info->width = data[4];");
            sw.WriteLine("        p_out_char_info->height = data[5];");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return charInfoFound;");
            sw.WriteLine("}");
            sw.WriteLine("");
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }
    }
}
