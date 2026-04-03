using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace IRL_Gui_Image_Builder_Library.Converters
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

        static public byte[] GetPixelData_RLE(Bitmap bitmap, PixelDataFormat pixelDataFormat)
        {
            byte[] pixelDataConverted = [];

            AddPixelData_RLE(bitmap, ref pixelDataConverted, pixelDataFormat);

            return pixelDataConverted;
        }

        static public byte[] GetPixelData_RLE_Alpha(Bitmap bitmap)
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
            CorrectPixelDataForAlpha(ref pixelDataConverted);

            return AddPixelData_RLE_Alpha(ref pixelDataConverted);
        }

        private static byte[] AddPixelData_RLE_Alpha(ref byte[] pixelData)
        {
            byte currentPixel = 0;
            int pixelCount = 0;

            // Change pixel data first!!!!!!!!!!!!!!!!!!!!!!!! Before compressing

            byte[] compressedData = [];

            for (uint i = 0; i < pixelData.Length; i += 3)
            {
                byte newPixel = pixelData[i];

                if (i == 0)
                {
                    currentPixel = newPixel;
                    pixelCount = 1;
                }
                else if (currentPixel == newPixel)
                {
                    pixelCount++;
                }
                else
                {
                    AddPixelsRleAlpha(ref compressedData, pixelCount, currentPixel);

                    currentPixel = newPixel;
                    pixelCount = 1;
                }
            }

            AddPixelsRleAlpha(ref compressedData, pixelCount, currentPixel);

            return compressedData;
        }

        private static void AddPixelsRleAlpha(ref byte[] compressedData, int pixelCount, byte pixel)
        {
            if (pixelCount == 0)
            {
                Debug.WriteLine("Pixel count is 0, skipping pixel info addition.");
                return;
            }

            if (pixel == 0) // Black
            {
                while (pixelCount > 0)
                {
                    byte countToWrite = pixelCount > 255 ? (byte)255 : (byte)pixelCount;

                    ArrayUtils.AppendByteToArray(ref compressedData, 0x00);
                    ArrayUtils.AppendByteToArray(ref compressedData, countToWrite);
                    pixelCount -= countToWrite;
                }
            }
            else if (pixel == 255) // White
            {
                while (pixelCount > 0)
                {
                    byte countToWrite = pixelCount > 255 ? (byte)255 : (byte)pixelCount;

                    ArrayUtils.AppendByteToArray(ref compressedData, 0x0F);
                    ArrayUtils.AppendByteToArray(ref compressedData, countToWrite);
                    pixelCount -= countToWrite;
                }
            }
            else
            {
                while (pixelCount > 0)
                {
                    byte countToWrite = pixelCount > 15 ? (byte)15 : (byte)pixelCount;
                    
                    byte pixelInfo = (byte)((countToWrite << 4) | (pixel >> 4));
                    ArrayUtils.AppendByteToArray(ref compressedData, pixelInfo);
                    pixelCount -= countToWrite;
                }
            }
        }

        private static void CorrectPixelDataForAlpha(ref byte[] pixelData)
        {
            for (uint i = 0; i < pixelData.Length; i += 3)
            {
                byte pixel = pixelData[i];
                byte correctedPixel = GetColorFromPallete(pixel);
                pixelData[i] = correctedPixel;
            }
        }

        private static byte GetColorFromPallete(byte pixel)
        {
            if ((pixel == 0x00) || (pixel == 0xFF))
            {
                return pixel;
            }

            List<byte> palette = [0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0];

            byte closestColor = 0;
            int smallestDifference = int.MaxValue;

            foreach (byte color in palette)
            {
                int difference = Math.Abs(pixel - color);
                if (difference < smallestDifference)
                {
                    smallestDifference = difference;
                    closestColor = color;
                }
            }

            return closestColor;
        }


        //private static byte[] AddPixelData_RLE_Alpha(ref byte[] pixelData)
        //{
        //    ushort currentPixel = 0xFFFF;
        //    ushort pixelCount = 0;

        //    byte[] compressedData = [];

        //    for (uint i = 0; i < pixelData.Length; i += 3)
        //    {
        //        byte pixel = pixelData[i];

        //        if (pixel == currentPixel)
        //        {
        //            if (pixelCount < 255)
        //            {
        //                pixelCount++;
        //            }
        //            else
        //            {
        //                AddPixelInfo(ref compressedData, (byte)(pixelCount), (byte)currentPixel);

        //                currentPixel = pixel;
        //                pixelCount = 1;
        //            }
        //        }
        //        else if (currentPixel == 0xFFFF)
        //        {
        //            currentPixel = pixel;
        //            pixelCount = 1;
        //        }
        //        else
        //        {
        //            AddPixelInfo(ref compressedData, (byte)(pixelCount), (byte)currentPixel);

        //            currentPixel = pixel;
        //            pixelCount = 1;
        //        }
        //    }

        //    AddPixelInfo(ref compressedData, (byte)(pixelCount), (byte)currentPixel);

        //    return compressedData;
        //}

        private static void AddPixelInfo(ref byte[] compressedData, byte pixelCount, byte colorCode)
        {
            if (pixelCount == 0)
            {
                Debug.WriteLine("Pixel count is 0, skipping pixel info addition.");

                return;
            }

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

        static private void AddPixelData_RLE(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            if (pixelDataFormat.PixelFormat == PixelFormat.RGB)
            {
                AddPixelDataRGB_RLE(bitmap, ref convertedPixelData, pixelDataFormat);
            }
            else
            {
                AddPixelDataRGB565_RLE(bitmap, ref convertedPixelData, pixelDataFormat);
            }
        }

        static private void AddPixelDataRGB_RLE(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            Color currentPixelColor = Color.Empty;
            uint currentPixelCount = 0;

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color nextPixelColor = bitmap.GetPixel(j, i);

                    if (i == 0 && j == 0)
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
                        AddRGBPixels_RLE(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.PixelFormatRGB);
                        currentPixelColor = nextPixelColor;
                        currentPixelCount = 1;
                    }
                }
            }

            AddRGBPixels_RLE(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.PixelFormatRGB);
        }

        static private void AddPixelDataRGB565_RLE(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            ushort currentPixelColor = 0;
            uint currentPixelCount = 0;

            for (int i = 0; i < bitmap.Height; i++)
            {
                for (int j = 0; j < bitmap.Width; j++)
                {
                    Color color = bitmap.GetPixel(j, i);
                    ushort nextPixelColor = ConvertToRGB565(color.R, color.G, color.B);

                    if (i == 0 && j == 0)
                    {
                        currentPixelColor = nextPixelColor;
                        currentPixelCount = 1;
                    }
                    else if (currentPixelColor == nextPixelColor)
                    {
                        currentPixelCount++;
                    }
                    else
                    {
                        AddRGB565Pixels_RLE(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.RGB565SwapBytes);
                        currentPixelColor = nextPixelColor;
                        currentPixelCount = 1;
                    }
                }
            }

            AddRGB565Pixels_RLE(ref convertedPixelData, currentPixelCount, currentPixelColor, pixelDataFormat.RGB565SwapBytes);
        }

        static private bool IsColorEqual(Color color1, Color color2)
        {
            return color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
        }

        static private void AddRGBPixels_RLE(ref byte[] pixelData, uint pixelCount, Color pixelColor, PixelFormatRGB pixelFormatRGB)
        {
            if (pixelCount == 0)
            {
                return;
            }

            byte[] pixelColorData = pixelFormatRGB == PixelFormatRGB.RGB
                ? [pixelColor.R, pixelColor.G, pixelColor.B]
                : [pixelColor.B, pixelColor.G, pixelColor.R];

            if (pixelCount <= 255)
            {
                ArrayUtils.AppendByteToArray(ref pixelData, (byte)(pixelCount));
                ArrayUtils.AppendToArray(ref pixelData, pixelColorData);
            }
            else
            {
                uint remainingPixelCount = pixelCount;
                while (remainingPixelCount > 0)
                {
                    byte countToWrite = remainingPixelCount > 255 ? (byte)255 : (byte)(remainingPixelCount);
                    ArrayUtils.AppendByteToArray(ref pixelData, countToWrite);
                    ArrayUtils.AppendToArray(ref pixelData, pixelColorData);
                    remainingPixelCount -= countToWrite;
                }
            }
        }

        static private void AddRGB565Pixels_RLE(ref byte[] pixelData, uint pixelCount, ushort rgb565Pixel, bool swapBytes)
        {
            if (pixelCount == 0)
            {
                return;
            }

            if (swapBytes)
            {
                rgb565Pixel = (ushort)(((rgb565Pixel & 0x00FF) << 8) + ((rgb565Pixel & 0xFF00) >> 8));
            }

            if (pixelCount <= 255)
            {
                ArrayUtils.AppendByteToArray(ref pixelData, (byte)(pixelCount));
                ArrayUtils.AppendUint16ToArray(ref pixelData, rgb565Pixel);
            }
            else
            {
                uint remainingPixelCount = pixelCount;

                while (remainingPixelCount > 0)
                {
                    byte countToWrite = remainingPixelCount >= 255 ? (byte)255 : (byte)(remainingPixelCount);
                    ArrayUtils.AppendByteToArray(ref pixelData, countToWrite);
                    ArrayUtils.AppendUint16ToArray(ref pixelData, rgb565Pixel);
                    remainingPixelCount -= countToWrite;
                }
            }
        }



        static private void AddRGB565Pixels_RLE_2(ref byte[] pixelData, uint pixelCount, ushort rgb565Pixel, bool swapBytes)
        {
            if (pixelCount == 0)
            {
                return;
            }

            if (swapBytes)
            {
                rgb565Pixel = (ushort)(((rgb565Pixel & 0x00FF) << 8) + ((rgb565Pixel & 0xFF00) >> 8));
            }

            if (pixelCount <= 256)
            {
                ArrayUtils.AppendByteToArray(ref pixelData, (byte)(pixelCount - 1));
                ArrayUtils.AppendUint16ToArray(ref pixelData, rgb565Pixel);
            }
            else
            {
                uint remainingPixelCount = pixelCount;

                while (remainingPixelCount > 0)
                {
                    byte countToWrite = remainingPixelCount > 255 ? (byte)255 : (byte)(remainingPixelCount - 1);
                    ArrayUtils.AppendByteToArray(ref pixelData, countToWrite);
                    ArrayUtils.AppendUint16ToArray(ref pixelData, rgb565Pixel);
                    remainingPixelCount -= (uint)(countToWrite + 1);
                }
            }
        }

        static private void AddRGBPixels_RLE_2(ref byte[] pixelData, uint pixelCount, Color pixelColor, PixelFormatRGB pixelFormatRGB)
        {
            if (pixelCount == 0)
            {
                return;
            }

            byte[] pixelColorData = pixelFormatRGB == PixelFormatRGB.RGB
                ? [pixelColor.R, pixelColor.G, pixelColor.B]
                : [pixelColor.B, pixelColor.G, pixelColor.R];

            if (pixelCount <= 256)
            {
                ArrayUtils.AppendByteToArray(ref pixelData, (byte)(pixelCount - 1));
                ArrayUtils.AppendToArray(ref pixelData, pixelColorData);
            }
            else
            {
                uint remainingPixelCount = pixelCount;
                while (remainingPixelCount > 0)
                {
                    byte countToWrite = remainingPixelCount > 255 ? (byte)255 : (byte)(remainingPixelCount - 1);
                    ArrayUtils.AppendByteToArray(ref pixelData, countToWrite);
                    ArrayUtils.AppendToArray(ref pixelData, pixelColorData);
                    remainingPixelCount -= (uint)(countToWrite + 1);
                }
            }
        }

        private static byte[] AddPixelData_RLE_Alpha_2(ref byte[] pixelData)
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
    }
}
