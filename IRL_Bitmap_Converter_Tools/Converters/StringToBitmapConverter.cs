using IRL_Common_Library.Utils;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace IRL_Bitmap_Converter_Tools.Converters
{
    public static class StringToBitmapConverter
    {
        public static void GenerateImage(
            string text, Font font,
            int leftMargin, int topMargin, int rightMargin, int bottomMargin,
            Color textColor, Color backColor,
            string path, string filename)
        {
            SizeF textSize = MeasureStringSize(text, font);
            using Image image = GetBitmap(
                (int)textSize.Width, (int)textSize.Height,
                leftMargin, topMargin, rightMargin, bottomMargin,
                text, font, textColor, backColor);

            using MemoryStream ms = new();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            string filePath = FileUtils.CreateUniqeFileName(path, filename, ".png");

            using FileStream stream = new(filePath, FileMode.CreateNew, FileAccess.ReadWrite);
            stream.Write(ms.ToArray());
            stream.Close();
        }

        public static SizeF MeasureStringSize(string text, Font font)
        {
            using Image image = new Bitmap(2048, 512, System.Drawing.Imaging.PixelFormat.Format64bppArgb);
            using Graphics graphics = Graphics.FromImage(image);
            SizeF textSize = graphics.MeasureString(text, font);

            return textSize;
        }

        private static Bitmap GetBitmap(
            int width, int height,
            int leftMargin, int topMargin, int rightMargin, int bottomMargin,
            string text, Font font, Color
            textColor, Color backColor)
        {
            Bitmap bitmap = DrawStringOnBitmap(text, font, textColor, backColor, width, height, leftMargin, topMargin, rightMargin, bottomMargin);

            int xLeft = GetXPositionFirstLeftPixel(bitmap, backColor);
            int xRight = GetXPositionFirstRightPixel(bitmap, backColor);

            return GetCroppedBitmap(bitmap, xLeft - leftMargin, xRight + rightMargin, leftMargin, rightMargin);
        }

        public static Bitmap DrawStringOnBitmap(
            string text, Font font, Color textColor, Color backColor,
            int width, int height, int leftMargin, int topMargin, int rightMargin, int bottomMargin)
        {
            if (leftMargin > 0)
            {
                width += leftMargin;
            }

            if (rightMargin > 0)
            {
                width += rightMargin;
            }

            height = height + topMargin + bottomMargin;

            Bitmap bitmap = new(width, height);
            using Graphics graphics = Graphics.FromImage(bitmap);

            int strX = leftMargin;
            int strY = topMargin;

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;// GridFit;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.TextContrast = 4;
            graphics.Clear(backColor);
            graphics.DrawString(text, font, new SolidBrush(textColor), strX, strY);
            //RenderDropshadowText(graphics, text, font, textColor, Color.DimGray, 64, new PointF(10, 10));
            graphics.Save();

            return bitmap;
        }

        public static int GetXPositionFirstLeftPixel(Bitmap bitmap, Color backColor)
        {
            int xLeft = -1;

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    if (backColor != bitmap.GetPixel(x, y)) // TODO only compare rgb values!!!
                    {
                        xLeft = x;
                        break;
                    }
                }

                if (xLeft > -1)
                {
                    break;
                }
            }

            return xLeft;
        }

        public static int GetXPositionFirstRightPixel(Bitmap bitmap, Color backColor)
        {
            int xRight = -1;

            for (int x = (bitmap.Width - 1); x >= 0; x--)
            {
                for (int y = (bitmap.Height - 1); y >= 0; y--)
                {
                    if (backColor != bitmap.GetPixel(x, y))
                    {
                        xRight = x;
                        break;
                    }
                }

                if (xRight > -1)
                {
                    break;
                }
            }

            return xRight;
        }

        public static Bitmap GetCroppedBitmap(Bitmap bitmap, int xLeft, int xRight, int leftMargin, int rightMargin)
        {
            if ((xLeft - leftMargin) > -1 && (xRight + rightMargin) > -1)
            {
                Rectangle section = new(xLeft, 0, (xRight - xLeft + 1), bitmap.Height);

                Bitmap croppedBitmap = new((xRight - xLeft + 1), bitmap.Height);
                using Graphics g = Graphics.FromImage(croppedBitmap);
                Rectangle targetRect = new(0, 0, croppedBitmap.Width, croppedBitmap.Height);
                g.DrawImage(bitmap, targetRect, section, GraphicsUnit.Pixel);
                
                return croppedBitmap;
            }
       
            return bitmap;
        }

        private static void RenderDropshadowText(
            Graphics graphics, string text, Font font, Color foreground, Color shadow,
            int shadowAlpha, PointF location)
        {
            const int DISTANCE = 2;
            for (int offset = 1; 0 <= offset; offset--)
            {
                Color color = ((offset < 1) ?
                    foreground : Color.FromArgb(shadowAlpha, shadow));

                using SolidBrush brush = new(color);
                PointF point = new()
                {
                    X = location.X + (offset * DISTANCE),
                    Y = location.Y + (offset * DISTANCE)
                };

                if (offset > 0)
                {
                    using SolidBrush blurBrush = new(Color.FromArgb((shadowAlpha / 2), color));
                    graphics.DrawString(text, font, blurBrush, (point.X + 1), point.Y);
                    graphics.DrawString(text, font, blurBrush, (point.X - 1), point.Y);
                }

                graphics.DrawString(text, font, brush, point);
            }
        }

        private static bool AreColorsEqual(Color color1, Color color2)
        {
            return (color1.R == color2.R) && (color1.G == color2.G) && (color1.B == color2.B);
        }
    }
}
