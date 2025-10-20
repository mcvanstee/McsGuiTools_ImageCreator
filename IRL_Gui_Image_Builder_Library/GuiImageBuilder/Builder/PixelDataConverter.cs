using IRL_Common_Library.Utils;
using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    public static class PixelDataConverter
    {
        static public byte[] GetConvertedPixelData(Bitmap bitmap, PixelDataFormat pixelDataFormat)
        {
            int pixelDataLengthBytes = CalculateConvertedPixelDataLength(bitmap, pixelDataFormat);
            byte[] pixelDataConverted = new byte[pixelDataLengthBytes];

            AddConvertedPixelData(bitmap, ref pixelDataConverted, pixelDataFormat);

            return pixelDataConverted;
        }

        static public byte[] GetCompressedPixelData(Bitmap bitmap, PixelDataFormat pixelDataFormat, ref uint compressedPixels)
        {
            byte[] pixelDataConverted = [];

            compressedPixels = AddCompressedPixelData(bitmap, ref pixelDataConverted, pixelDataFormat);

            return pixelDataConverted;
        }

        static public byte[] GetOptimizedPixelData(Bitmap bitmap)
        {
            PixelDataFormat pixelDataFormat = new()
            {
                PixelFormat = PixelFormat.RGB,
                RGB565SwapBytes = false,
                PixelFormatRGB = PixelFormatRGB.RGB
            };

            int pixelDataLengthBytes = CalculateConvertedPixelDataLength(bitmap, pixelDataFormat);
            byte[] pixelDataConverted = new byte[pixelDataLengthBytes];

            AddConvertedPixelData(bitmap, ref pixelDataConverted, pixelDataFormat);

            return OptimizeBitmapPixelData(ref pixelDataConverted);
        }

        private static byte[] OptimizeBitmapPixelData(ref byte[] pixelData)
        {
            ushort currentPixel = 0xFFFF;
            ushort pixelCount = 0;

            byte[] compressedData = [];

            for (uint i = 0; i < pixelData.Length; i += 3)
            {
                byte pixel = pixelData[i];

                if (pixel == currentPixel)
                {
                    if (pixelCount < 256)
                    {
                        pixelCount++;
                    }
                    else
                    {
                        AddPixelInfo(ref compressedData, (byte)(pixelCount - 1), (byte)currentPixel);

                        currentPixel = pixel;
                        pixelCount = 1;
                    }
                }
                else if (currentPixel == 0xFFFF)
                {
                    currentPixel = pixel;
                    pixelCount = 1;
                }
                else
                {
                    AddPixelInfo(ref compressedData, (byte)(pixelCount - 1), (byte)currentPixel);

                    currentPixel = pixel;
                    pixelCount = 1;
                }
            }

            AddPixelInfo(ref compressedData, (byte)(pixelCount - 1), (byte)currentPixel);

            return compressedData;
        }

        private static void AddPixelInfo(ref byte[] compressedData, byte pixelCount, byte colorCode)
        {
            byte[] newData = { pixelCount, colorCode };
            ArrayUtils.AppendToArray(ref compressedData, newData);
        }


        static private void AddConvertedPixelData(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            if (pixelDataFormat.PixelFormat == PixelFormat.RGB)
            {
                AddConvertedPixelDataRGB(bitmap, ref convertedPixelData, pixelDataFormat);
            }
            else
            {
                AddConvertedPixelDataRGB565(bitmap, ref convertedPixelData, pixelDataFormat);
            }
        }

        static private void AddConvertedPixelDataRGB(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            int pixelIndex = 0;

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color color = bitmap.GetPixel(j, i);
                    if (pixelDataFormat.PixelFormatRGB == PixelFormatRGB.RGB)
                    {
                        convertedPixelData[pixelIndex * 3] = color.R;
                        convertedPixelData[pixelIndex * 3 + 1] = color.G;
                        convertedPixelData[pixelIndex * 3 + 2] = color.B;
                    }
                    else
                    {
                        convertedPixelData[pixelIndex * 3] = color.B;
                        convertedPixelData[pixelIndex * 3 + 1] = color.G;
                        convertedPixelData[pixelIndex * 3 + 2] = color.R;
                    }

                    pixelIndex += 1;
                }
            }
        }

        static private void AddConvertedPixelDataRGB565(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            int writeIndex = 0;

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color color = bitmap.GetPixel(j, i);

                    ushort rgb565Pixel = ConvertToRGB565(color.R, color.G, color.B);

                    if (pixelDataFormat.RGB565SwapBytes)
                    {
                        ushort reverseRGB565Pixel = (ushort)(((rgb565Pixel & 0x00FF) << 8) + ((rgb565Pixel & 0xFF00) >> 8));

                        BitConverter.GetBytes(reverseRGB565Pixel).CopyTo(convertedPixelData, writeIndex);
                    }
                    else
                    {
                        BitConverter.GetBytes(rgb565Pixel).CopyTo(convertedPixelData, writeIndex);
                    }

                    writeIndex += sizeof(ushort);
                }
            }
        }

        static private int CalculateConvertedPixelDataLength(Bitmap bitmap, PixelDataFormat pixelDataFormat)
        {
            int noOfPixels = bitmap.Width * bitmap.Height;
            int bytesPerPixel = pixelDataFormat.PixelFormat == PixelFormat.RGB565 ? 2 : 3;
            int pixelDataLengthBytes = noOfPixels * bytesPerPixel;

            return pixelDataLengthBytes;
        }

        static private ushort ConvertToRGB565(byte r, byte g, byte b)
        {
            ushort rgb565 = (ushort)((r >> 3 << 11) + (g >> 2 << 5) + (b >> 3));

            return rgb565;
        }

        static private uint AddCompressedPixelData(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            uint totalCompressedPixels;

            if (pixelDataFormat.PixelFormat == PixelFormat.RGB)
            {
                totalCompressedPixels = AddCompressedPixelDataRGB(bitmap, ref convertedPixelData, pixelDataFormat);
            }
            else
            {
                totalCompressedPixels = AddCompressedPixelDataRGB565(bitmap, ref convertedPixelData, pixelDataFormat);
            }

            return totalCompressedPixels;
        }

        static private uint AddCompressedPixelDataRGB(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            uint totalCompressedPixels = 0;

            Color currentPixelColor = Color.Empty;
            uint currentPixelCount = 0;

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color nextPixelColor = bitmap.GetPixel(j, i);

                    if ((i == 0) && j == 0)
                    {
                        currentPixelColor = nextPixelColor;
                        currentPixelCount = 1;
                    }
                    else if (IsColorEqual(currentPixelColor, nextPixelColor))
                    {
                        currentPixelCount++;
                    }
                    else
                    {
                        AddCompressedRGBPixels(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.PixelFormatRGB);
                        totalCompressedPixels += currentPixelCount;
                        currentPixelColor = nextPixelColor;
                        currentPixelCount = 1;
                    }
                }
            }

            AddCompressedRGBPixels(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.PixelFormatRGB);
            totalCompressedPixels += currentPixelCount;

            return totalCompressedPixels;
        }

        static private uint AddCompressedPixelDataRGB565(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            uint totalCompressedPixels = 0;

            Color currentPixelColor = Color.Empty;
            uint currentPixelCount = 0;

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color nextPixelColor = bitmap.GetPixel(j, i);

                    if ((i == 0) && j == 0)
                    {
                        currentPixelColor = nextPixelColor;
                        currentPixelCount = 1;
                    }
                    else if (IsColorEqual(currentPixelColor, nextPixelColor))
                    {
                        currentPixelCount++;
                    }
                    else
                    {
                        AddCompressedRGB565Pixels(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.RGB565SwapBytes);
                        totalCompressedPixels += currentPixelCount;
                        currentPixelColor = nextPixelColor;
                        currentPixelCount = 1;
                    }
                }
            }

            AddCompressedRGB565Pixels(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.RGB565SwapBytes);
            totalCompressedPixels += currentPixelCount;

            return totalCompressedPixels;
        }

        static private bool IsColorEqual(Color color1, Color color2)
        {
            return color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
        }

        static private void AddCompressedRGBPixels(ref byte[] pixelData, uint pixelCount, Color pixelColor, PixelFormatRGB pixelFormatRGB)
        {
            if (pixelCount > 0)
            {
                byte[] pixelColorData = pixelFormatRGB == PixelFormatRGB.RGB
                    ? [pixelColor.R, pixelColor.G, pixelColor.B]
                    : [pixelColor.B, pixelColor.G, pixelColor.R];
                ArrayUtils.AppendUint32ToArray(ref pixelData, pixelCount);
                ArrayUtils.AppendToArray(ref pixelData, pixelColorData);
            }
        }

        static private void AddCompressedRGB565Pixels(ref byte[] pixelData, uint pixelCount, Color color, bool swapBytes)
        {
            if (pixelCount > 0)
            {
                ushort rgb565Pixel = ConvertToRGB565(color.R, color.G, color.B);

                if (swapBytes)
                {
                    rgb565Pixel = (ushort)(((rgb565Pixel & 0x00FF) << 8) + ((rgb565Pixel & 0xFF00) >> 8));
                }

                ArrayUtils.AppendUint32ToArray(ref pixelData, pixelCount);
                ArrayUtils.AppendUint16ToArray(ref pixelData, rgb565Pixel);
            }
        }
    }
}
