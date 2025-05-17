using System.Drawing;
using IRL_Common_Library.Utils;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.StatusUpdater;
using IRL_Bitmap_Converter_Tools.ConverterInstructions;

namespace IRL_Bitmap_Converter_Tools.Converters
{
    public static class FontToBitmapConverter
    {
        public static bool BuildFontBitmaps(
            FontBitmap fontBitmap, List<FontBitmapStyle> fontBitmapStyles, List<ConverterFont> fonts, string outputPath, 
            FontFileKeyFormat fontFileKeyFormat, ConverterStatusUpdater statusUpdater)
        {
            bool result = true;

            if (fontBitmap.FontBitmapStyleIds.Count == 0)
            {
                Log.WriteLine($"No font styles found for font: {fontBitmap.FontName}");

                return false;
            }

            string converterFontName = GetConverterFontName(fontBitmap.FontConverterId, fonts);
            Font font = new(fontBitmap.FontName, fontBitmap.FontSize, fontBitmap.FontStyle, GraphicsUnit.Pixel);

            foreach (int styleId in fontBitmap.FontBitmapStyleIds)
            {
                FontBitmapStyle style = FontBitmapStyle.GetFontStyleById(styleId, fontBitmapStyles);

                if (style != null)
                {
                    string fontName = GetFontName(font, fontBitmap, fontFileKeyFormat, style, converterFontName);
                    string path = $"{outputPath}\\{fontName}";

                    statusUpdater.UpdateStatusAndInstructionsConverted($"Font: {fontName}", 1);

                    if (Directory.Exists(path))
                    {
                        Log.WriteLine($"Font already exists: {fontName}");

                        result = false;
                    }
                    else
                    {
                        Directory.CreateDirectory(path);

                        int xLeft = 0;
                        int xRight = 0;

                        for (int i = 32; i <= 126; i++)
                        {
                            string charString = ((char)i).ToString();
                            string filename = "C_" + i;

                            GenerateCharacterBitmap(
                                charString, font, style.MonospaceNumbers,
                                style.Margin.Left, style.Margin.Top, style.Margin.Right, style.Margin.Bottom,
                                style.TextColor, style.BackColor,
                                path, filename, ref xLeft, ref xRight);
                        }
                    }
                }
                else
                {
                    Log.WriteLine($"Font style not found: {styleId}");

                    result = false;
                }
            }

            return result;
        }

        private static void GenerateCharacterBitmap(
            string text, Font font, bool monoSpaceNumbers,
            int leftMargin, int topMargin, int rightMargin, int bottomMargin,
            Color textColor, Color backColor,
            string path, string filename, ref int xLeft, ref int xRight)
        {
            SizeF textSize = StringToBitmapConverter.MeasureStringSize(text, font);
            using Image image = GetBitmap(
                (int)textSize.Width, (int)textSize.Height, monoSpaceNumbers,
                leftMargin, topMargin, rightMargin, bottomMargin,
                text, font, textColor, backColor, ref xLeft, ref xRight);

            using MemoryStream ms = new();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            string filePath = FileUtils.CreateUniqeFileName(path, filename, ".png");

            using FileStream stream = new(filePath, FileMode.CreateNew, FileAccess.ReadWrite);
            stream.Write(ms.ToArray());
            stream.Close();
        }

        private static Bitmap GetBitmap(
            int width, int height, bool monoSpaceNumbers,
            int leftMargin, int topMargin, int rightMargin, int bottomMargin,
            string text, Font font, Color textColor, Color backColor,
            ref int xLeft, ref int xRight)
        {
            leftMargin += 1;
            rightMargin += 1;

            Bitmap bitmap = StringToBitmapConverter.DrawStringOnBitmap(text, font, textColor, backColor, width, height, leftMargin, topMargin, rightMargin, bottomMargin);

            if (monoSpaceNumbers && text == "0")
            {
                xLeft = StringToBitmapConverter.GetXPositionFirstLeftPixel(bitmap, backColor);
                xRight = StringToBitmapConverter.GetXPositionFirstRightPixel(bitmap, backColor);
            }
            else if (monoSpaceNumbers && IsNumber(text))
            {
                // Use the same xLeft and xRight for all numbers
            }
            else
            {
                xLeft = StringToBitmapConverter.GetXPositionFirstLeftPixel(bitmap, backColor);
                xRight = StringToBitmapConverter.GetXPositionFirstRightPixel(bitmap, backColor);
            }

            return StringToBitmapConverter.GetCroppedBitmap(
                bitmap, xLeft - leftMargin, xRight + rightMargin, leftMargin, rightMargin);
        }

        private static bool IsNumber(string text)
        {
            return text.All(char.IsDigit);
        }

        private static string GetFontStyleString(FontStyle fontStyle)
        {
            string result = string.Empty;

            if (fontStyle == FontStyle.Regular)
            {
                result = "R";
            }
            else if (fontStyle == FontStyle.Italic)
            {
                result = "I";
            }
            else if (fontStyle == FontStyle.Bold)
            {
                result = "B";
            }
            else
            {

            }
            
            return result;

        }

        private static string GetConverterFontName(int fontConverterId, List<ConverterFont> fonts)
        {
            if (fontConverterId == -1)
            {
                return "";
            }

            ConverterFont font = ConverterFont.GetFontById(fontConverterId, fonts);
            if (font != null)
            {
                return font.Name.Replace(" ", "_");
            }

            return "";
        }

        private static string GetFontName(
            Font font, FontBitmap fontBitmap, FontFileKeyFormat fontFileKeyFormat, FontBitmapStyle style, string converterFontName)
        {
            int fontSize = (int)font.Size;
            string fontName;

            switch (fontFileKeyFormat)
            {
                case FontFileKeyFormat.FontName:
                    fontName = $"{fontBitmap.FontName}_{fontSize}_{GetFontStyleString(fontBitmap.FontStyle)}";
                    break;
                case FontFileKeyFormat.ProjectFontName:
                    if (string.IsNullOrEmpty(converterFontName))
                    {
                        fontName = $"{fontBitmap.FontName}_{fontSize}_{GetFontStyleString(fontBitmap.FontStyle)}";
                    }
                    else
                    {
                        fontName = converterFontName;
                    }
                        break;
                case FontFileKeyFormat.Both:
                default:
                    fontName = $"{fontBitmap.FontName}_{ fontSize}_{GetFontStyleString(fontBitmap.FontStyle)}";
                    break;

            }

            string styleName = style.Name.Replace(" ", "_");
            fontName = fontName.Replace(" ", "_");
            
            return $"{fontName}_{styleName}";
        }
    }
}
