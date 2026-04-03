using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.FontSearchFiles
{
    public static class FontNumbersOnlyCFile
    {
        public static void CreateFontNumbersOnlyCharInfoSearchCFile(FontBuilder fontBuilder)
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.CHAR_INFO_SEARCH_FILE + ".c");
            StreamWriter sw = new(filePath);
            int digitOnlyFontCount = fontBuilder.Fonts.FindAll(f => f.IsNumberOnly).Count;
            int fullFontsCount = fontBuilder.Fonts.Count - digitOnlyFontCount;
            int noOfFonts = fontBuilder.Fonts.Count;
            const int noOfChars = 95;
            const int noOfDigitOnlyChars = 12;

            CodeGenegrationUtils.Include(sw, FileConstants.CHAR_INFO_SEARCH_FILE);
            CodeGenegrationUtils.BlankLine(sw);
            AddCharInfoArray(sw, fontBuilder, noOfFonts, fullFontsCount, noOfChars);
            CodeGenegrationUtils.BlankLine(sw);
            AddNumOnlyCharInfoArray(sw, fontBuilder, noOfFonts, digitOnlyFontCount, noOfDigitOnlyChars);
            CodeGenegrationUtils.BlankLine(sw);
            AddGetCharIndexFunction(sw, fontBuilder, noOfFonts, fullFontsCount);
            CodeGenegrationUtils.BlankLine(sw);
            AddGetFontIndexFunction(sw, fontBuilder, noOfFonts);
            CodeGenegrationUtils.BlankLine(sw);
            AddGetCharInfoFunction(sw, fontBuilder, noOfFonts);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }

        private static void AddCharInfoArray(StreamWriter sw, FontBuilder fontBuilder,
            int noOfFonts, int fullFontsCount, int noOfChars)
        {
            sw.WriteLine("const fs_char_info_s fs_char_info[" + fullFontsCount + "][" + noOfChars + "] =");
            sw.WriteLine("{");

            for (int i = 0; i < noOfFonts; i++)
            {
                if (!fontBuilder.Fonts[i].IsNumberOnly)
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
            }
            sw.WriteLine("};");
        }

        private static void AddNumOnlyCharInfoArray(StreamWriter sw, FontBuilder fontBuilder,
            int noOfFonts, int digitOnlyFontCount, int noOfDigitOnlyChars)
        {
            sw.WriteLine("const fs_char_info_s fs_char_info_digits[" + digitOnlyFontCount + "][" + noOfDigitOnlyChars + "] =");
            sw.WriteLine("{");

            for (int i = 0; i < noOfFonts; i++)
            {
                if (fontBuilder.Fonts[i].IsNumberOnly)
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
            }
            sw.WriteLine("};");
        }

        private static void AddGetCharIndexFunction(StreamWriter sw, FontBuilder fontBuilder, int noOfFonts, int fullFontsCount)
        {
            sw.WriteLine("static inline int32_t fs_getCharIndex(const font_key_e font_key, const char c)");
            sw.WriteLine("{");
            sw.WriteLine("    int32_t charIndex = -1;");
            sw.WriteLine("");
            sw.WriteLine("    switch (font_key)");
            sw.WriteLine("    {");

            if (fullFontsCount > 0)
            {
                for (int i = 0; i < noOfFonts; i++)
                {
                    if (!fontBuilder.Fonts[i].IsNumberOnly)
                    {
                        sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
                    }
                }

                sw.WriteLine("            charIndex = (c - 32);");
                sw.WriteLine("            break;");
            }

            for (int i = 0; i < noOfFonts; i++)
            {
                if (fontBuilder.Fonts[i].IsNumberOnly)
                {
                    sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
                }
            }
            sw.WriteLine(
                "        {\n" +
                "            if ((c >= '0') && (c <= '9'))\n" +
                "            {\n" +
                "                charIndex = (c - '0' + 2);\n" +
                "            }\n" +
                "            else if (c == '.')\n" +
                "            {\n" +
                "                charIndex = 0;\n" +
                "            }\n" +
                "            else if (c == ',')\n" +
                "            {\n" +
                "                charIndex = 1;\n" +
                "            }\n" +
                "            else\n" +
                "            {\n" +
                "            }\n" +
                "            break;\n" +
                "        }");

            sw.WriteLine("        default:");
            sw.WriteLine("            break;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return charIndex;");
            sw.WriteLine("}");
        }

        private static void AddGetFontIndexFunction(StreamWriter sw, FontBuilder fontBuilder, int noOfFonts)
        {
            sw.WriteLine("static inline int32_t fs_getFontIndex(const font_key_e font_key)");
            sw.WriteLine("{");
            sw.WriteLine("    int32_t fontIndex = -1;");
            sw.WriteLine("");
            sw.WriteLine("    switch (font_key)");
            sw.WriteLine("    {");

            int fullFontCounter = 0;
            for (int i = 0; i < noOfFonts; i++)
            {
                if (!fontBuilder.Fonts[i].IsNumberOnly)
                {
                    sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
                    sw.WriteLine("            fontIndex = " + fullFontCounter.ToString() + ";");
                    sw.WriteLine("            break;");
                    fullFontCounter++;
                }
            }

            int digitOnlyFontCounter = 0;
            for (int i = 0; i < noOfFonts; i++)
            {
                if (fontBuilder.Fonts[i].IsNumberOnly)
                {
                    sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
                    sw.WriteLine("            fontIndex = " + digitOnlyFontCounter.ToString() + ";");
                    sw.WriteLine("            break;");
                    digitOnlyFontCounter++;
                }
            }
            sw.WriteLine("        default:");
            sw.WriteLine("            break;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return fontIndex;");
            sw.WriteLine("}");
        }

        private static void AddGetCharInfoFunction(StreamWriter sw, FontBuilder fontBuilder, int noOfFonts)
        {
            sw.WriteLine("");
            sw.WriteLine("bool fs_getCharInfo(const char c, const font_key_e font_key, fs_char_info_s *p_out_char_info, uint8_t *p_dataLocation)");
            sw.WriteLine("{");
            sw.WriteLine("    bool charInfoFound = false;");
            sw.WriteLine("    const int32_t charIndex = fs_getCharIndex(font_key, c);");
            sw.WriteLine("    const int32_t fontIndex = fs_getFontIndex(font_key);");
            sw.WriteLine("");
            sw.WriteLine("    switch (font_key)");
            sw.WriteLine("    {");
            for (int i = 0; i < noOfFonts; i++)
            {
                if (!fontBuilder.Fonts[i].IsNumberOnly)
                {
                    sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
                }
                
            }
            sw.WriteLine(
                "        {\n" +
                "            if ((FS_CHAR_INFOS_IN_FONT > charIndex) && (FS_FONTS > fontIndex) && (charIndex >= 0) && (fontIndex >= 0))\n" +
                "            {\n" +
                "                *p_out_char_info = fs_char_info[fontIndex][charIndex];\n" +
                "                charInfoFound = true;\n" +
                "                *p_dataLocation = FS_FONT_DATA_LOCATION;\n" +
                "            }\n" +
                "            break;\n" +
                "        }");
            for (int i = 0; i < noOfFonts; i++)
            {
                if (fontBuilder.Fonts[i].IsNumberOnly)
                {
                    sw.WriteLine("        case FONT_KEY_" + fontBuilder.Fonts[i].Name.ToUpper() + ":");
                }

            }
            sw.WriteLine(
                "        {\n" +
                "            if ((FS_CHAR_INFOS_IN_DIGIT_ONLY_FONT > charIndex) && (FS_CHAR_INFOS_IN_DIGIT_ONLY_FONT > fontIndex) && (charIndex >= 0) && (fontIndex >= 0))\n" +
                "            {\n" +
                "                *p_out_char_info = fs_char_info_digits[fontIndex][charIndex];\n" +
                "                charInfoFound = true;\n" +
                "                *p_dataLocation = FS_FONT_DATA_LOCATION;\n" +
                "            }\n" +
                "            break;\n" +
                "        }");

            sw.WriteLine("        default:");
            sw.WriteLine("            break;");
            sw.WriteLine("    }");
            sw.WriteLine("");
            sw.WriteLine("    return charInfoFound;");
            sw.WriteLine("}");
        }
    }
}
