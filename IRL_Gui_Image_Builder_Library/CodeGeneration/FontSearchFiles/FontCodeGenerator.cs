using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.Fonts;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.FontSearchFiles
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
                if (fontBuilder.HasNumberOnlyFonts)
                {
                    FontNumbersOnlyCFile.CreateFontNumbersOnlyCharInfoSearchCFile(projectPath, fontBuilder, builderSettings.FileSystemFormat.FileFormat);
                }
                else
                {
                    CreateFontCharInfoSearchCFile(projectPath, fontBuilder, builderSettings.FileSystemFormat.FileFormat);
                }

                
            }
        }

        private static void CreateFontCharInfoSearchHeader(ImageBuilderSettings builderSettings, string projectPath, FontBuilder fontBuilder, uint crc)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.CharInfoSearchFile + ".h");
            int bytesPerPixel = builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? 3 : 2;

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.CharInfoSearchFile);
            CodeGenegrationUtils.AddExternCBegin(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.IncludeStdBool(sw);
            CodeGenegrationUtils.IncludeStdInt(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.DefineIfNotDefined(sw, "FS_PIXEL_DATA_CRC", crc.ToString() + "u");
            CodeGenegrationUtils.BlankLine(sw);
            if (fontBuilder.HasNumberOnlyFonts)
            {
                int digitOnlyFontCount = fontBuilder.Fonts.FindAll(f => f.IsNumberOnly).Count;
                int fullFontsCount = fontBuilder.Fonts.Count - digitOnlyFontCount;

                if (fullFontsCount > 0)
                {
                    CodeGenegrationUtils.Define(sw, "FS_FONTS", fullFontsCount.ToString());
                    CodeGenegrationUtils.Define(sw, "FS_CHAR_INFOS_IN_FONT", "95");
                }
                else
                {
                    CodeGenegrationUtils.Define(sw, "FS_FONTS", "0");
                    CodeGenegrationUtils.Define(sw, "FS_CHAR_INFOS_IN_FONT", "0");
                }

                CodeGenegrationUtils.Define(sw, "FS_FONTS_DIGIT_ONLY", digitOnlyFontCount.ToString());
                CodeGenegrationUtils.Define(sw, "FS_CHAR_INFOS_IN_DIGIT_ONLY_FONT", "12");
            }
            else
            {
                CodeGenegrationUtils.Define(sw, "FS_FONTS", fontBuilder.Fonts.Count.ToString());
                CodeGenegrationUtils.Define(sw, "FS_CHAR_INFOS_IN_FONT", "95");
            }
            CodeGenegrationUtils.Define(sw, "FS_BYTES_PER_PIXEL", bytesPerPixel.ToString());
            CodeGenegrationUtils.BlankLine(sw);
            sw.WriteLine("typedef enum");
            sw.WriteLine("{");
            if (fontBuilder.Fonts.Count == 0)
            {
                sw.WriteLine("    NoFonts = 0,           // No fonts");
            }
            else
            {
                for (int i = 0; i < fontBuilder.Fonts.Count; i++)
                {
                    sw.WriteLine("    FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + " = " + i.ToString() + ",");
                }
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

        private static void CreateFontCharInfoSearchCFile(string projectPath, FontBuilder fontBuilder, FileFormat fileFormat)
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

                    string dataOffsetStr = charInfo.DataOffset.ToString();

                    sw.WriteLine("    { .dataOffset = " + dataOffsetStr + ", .width = " + width.ToString() + ", .height = " + height.ToString() + " },");
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

        //private static void CreateFontNumbersOnlyCharInfoSearchCFile(string projectPath, FontBuilder fontBuilder, FileFormat fileFormat)
        //{
        //    StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.CharInfoSearchFile + ".c");
        //    int digitOnlyFontCount = fontBuilder.Fonts.FindAll(f => f.IsNumberOnly).Count;
        //    int fullFontsCount = fontBuilder.Fonts.Count - digitOnlyFontCount;
        //    int noOfFonts = fontBuilder.Fonts.Count;
        //    const int noOfChars = 95;
        //    const int noOfDigitOnlyChars = 12;

        //    CodeGenegrationUtils.Include(sw, FileConstants.CharInfoSearchFile);
        //    CodeGenegrationUtils.BlankLine(sw);
        //    sw.WriteLine("const fs_char_info_s fs_char_info[" + fullFontsCount + "][" + noOfChars + "] =");
        //    sw.WriteLine("{");

        //    for (int i = 0; i < noOfFonts; i++)
        //    {
        //        if (!fontBuilder.Fonts[i].IsNumberOnly)
        //        {
        //            sw.WriteLine("    {");

        //            foreach (CharacterInfo charInfo in fontBuilder.Fonts[i].CharacterInfos)
        //            {
        //                byte width = (byte)charInfo.Width;
        //                byte height = (byte)charInfo.Height;

        //                string dataOffsetStr = charInfo.DataOffset.ToString();

        //                sw.WriteLine("    { .dataOffset = " + dataOffsetStr + ", .width = " + width.ToString() + ", .height = " + height.ToString() + " },");
        //            }

        //            sw.WriteLine("    },");
        //        }     
        //    }
        //    sw.WriteLine("};");

        //    CodeGenegrationUtils.BlankLine(sw);

        //    sw.WriteLine("const fs_char_info_s fs_char_info_digits[" + digitOnlyFontCount + "][" + noOfDigitOnlyChars + "] =");
        //    sw.WriteLine("{");

        //    for (int i = 0; i < noOfFonts; i++)
        //    {
        //        if (fontBuilder.Fonts[i].IsNumberOnly)
        //        {
        //            sw.WriteLine("    {");

        //            foreach (CharacterInfo charInfo in fontBuilder.Fonts[i].CharacterInfos)
        //            {
        //                byte width = (byte)charInfo.Width;
        //                byte height = (byte)charInfo.Height;

        //                string dataOffsetStr = charInfo.DataOffset.ToString();

        //                sw.WriteLine("    { .dataOffset = " + dataOffsetStr + ", .width = " + width.ToString() + ", .height = " + height.ToString() + " },");
        //            }

        //            sw.WriteLine("    },");
        //        }
        //    }
        //    sw.WriteLine("};");

        //    CodeGenegrationUtils.BlankLine(sw);

        //    sw.WriteLine("static inline int32_t fs_getCharIndex(const font_key_e font_key, const char c)");
        //    sw.WriteLine("{");
        //    sw.WriteLine("    int32_t charIndex = 0;");
        //    sw.WriteLine("");
        //    sw.WriteLine("    switch (font_key)");
        //    sw.WriteLine("    {");

        //    if (fullFontsCount > 0)
        //    {
        //        for (int i = 0; i < noOfFonts; i++)
        //        {
        //            if (!fontBuilder.Fonts[i].IsNumberOnly)
        //            {
        //                sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
        //            }
        //        }

        //        sw.WriteLine("            charIndex = (c - 32);");
        //        sw.WriteLine("            break;");
        //    }

        //    for (int i = 0; i < noOfFonts; i++)
        //    {
        //        if (fontBuilder.Fonts[i].IsNumberOnly)
        //        {
        //            sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
        //        }
        //    }
        //    sw.WriteLine(
        //        "        {\n" +
        //        "            if ((c >= '0') && (c <= '9'))\n" +
        //        "            {\n" +
        //        "                charIndex = (c - '0' + 2);\n" +
        //        "            }\n" +
        //        "            else if (c == '.')\n" +
        //        "            {\n" +
        //        "                charIndex = 0;\n" +
        //        "            }\n" +
        //        "            else if (c == ',')\n" +
        //        "            {\n" +
        //        "                charIndex = 1;\n" +
        //        "            }\n" +
        //        "            else\n" +
        //        "            {\n" +
        //        "            }\n" +
        //        "            break;\n" +
        //        "        }");      

        //    sw.WriteLine("        default:");
        //    sw.WriteLine("            break;");
        //    sw.WriteLine("    }");
        //    sw.WriteLine("");
        //    sw.WriteLine("    return charIndex;");
        //    sw.WriteLine("}");

        //    CodeGenegrationUtils.BlankLine(sw);

        //    sw.WriteLine("");
        //    sw.WriteLine("");




        //    sw.WriteLine("bool fs_getCharInfo(const char c, const font_key_e font_key, fs_char_info_s *p_out_char_info)");
        //    sw.WriteLine("{");
        //    sw.WriteLine("    bool charInfoFound = false;");
        //    sw.WriteLine("    const int32_t charIndex = (c - 32);");
        //    sw.WriteLine("    const int32_t fontIndex = (int32_t)font_key;");
        //    sw.WriteLine("");
        //    sw.WriteLine("    if ((FS_CHAR_INFOS_IN_FONT > charIndex) && (FS_FONTS > fontIndex) && (charIndex >= 0) && (fontIndex >= 0))");
        //    sw.WriteLine("    {");
        //    sw.WriteLine("        *p_out_char_info = fs_char_info[fontIndex][charIndex];");
        //    sw.WriteLine("        charInfoFound = true;");
        //    sw.WriteLine("    }");
        //    sw.WriteLine("");
        //    sw.WriteLine("    return charInfoFound;");
        //    sw.WriteLine("}");

        //    CodeGenegrationUtils.BlankLine(sw);
        //    CodeGenegrationUtils.EndOfFile(sw);

        //    sw.Close();
        //}

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
