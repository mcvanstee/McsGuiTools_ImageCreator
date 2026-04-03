using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.FontSearchFiles
{
    public static class FontCodeGenerator
    {
        public static void CreateFontCodeFiles(ImageBuilderSettings builderSettings, FontBuilder fontBuilder, uint crc)
        {
            if (!fontBuilder.FontsCreated)
            {
                return;
            }

            CreateFontCharInfoSearchHeader(builderSettings, fontBuilder, crc);

            if (fontBuilder.HasNumberOnlyFonts)
            {
                FontNumbersOnlyCFile.CreateFontNumbersOnlyCharInfoSearchCFile(fontBuilder);
            }
            else
            {
                CreateFontCharInfoSearchCFile(fontBuilder);
            }         
        }

        private static void CreateFontCharInfoSearchHeader(ImageBuilderSettings builderSettings, FontBuilder fontBuilder, uint crc)
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.CHAR_INFO_SEARCH_FILE + ".h");
            StreamWriter sw = new(filePath);
            int bytesPerPixel = builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? 3 : 2;

            DataLocation dataLocation = fontBuilder.DataLocations[0];

            CodeGenegrationUtils.AddCopyRight(sw);
            CodeGenegrationUtils.AddHeaderGuardBegin(sw, FileConstants.CHAR_INFO_SEARCH_FILE);
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
            CodeGenegrationUtils.Define(sw, "FS_FONT_DATA_LOCATION", dataLocation.LocationID.ToString());
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
            sw.WriteLine("bool fs_getCharInfo(const char c, const font_key_e font_key, fs_char_info_s *p_out_char_info, uint8_t *p_dataLocation);");
            CodeGenegrationUtils.BlankLine(sw);

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddExternCEnd(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.AddHeaderGuardEnd(sw, FileConstants.CHAR_INFO_SEARCH_FILE);

            sw.Close();
        }

        private static void CreateFontCharInfoSearchCFile(FontBuilder fontBuilder)
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.CHAR_INFO_SEARCH_FILE + ".c");
            StreamWriter sw = new(filePath);
            int fonts = fontBuilder.Fonts.Count;
            const int noOfChars = 95;

            CodeGenegrationUtils.Include(sw, FileConstants.CHAR_INFO_SEARCH_FILE);
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
            sw.WriteLine("bool fs_getCharInfo(const char c, const font_key_e font_key, fs_char_info_s *p_out_char_info, uint8_t *p_dataLocation)");
            sw.WriteLine("{");
            sw.WriteLine("    bool charInfoFound = false;");
            sw.WriteLine("    const int32_t charIndex = (c - 32);");
            sw.WriteLine("    const int32_t fontIndex = (int32_t)font_key;");
            sw.WriteLine("");
            sw.WriteLine("    if ((FS_CHAR_INFOS_IN_FONT > charIndex) && (FS_FONTS > fontIndex) && (charIndex >= 0) && (fontIndex >= 0))");
            sw.WriteLine("    {");
            sw.WriteLine("        *p_out_char_info = fs_char_info[fontIndex][charIndex];");
            sw.WriteLine("        charInfoFound = true;");
            sw.WriteLine("        *p_dataLocation = FS_FONT_DATA_LOCATION;");
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
