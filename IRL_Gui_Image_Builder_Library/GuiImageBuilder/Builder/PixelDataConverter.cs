using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    public static class PixelDataConverter
    {
        static public byte[] GetConvertedPixelData_1(Bitmap bitmap, PixelDataFormat pixelDataFormat)
        {
            int pixelDataLengthBytes = CalculateConvertedPixelDataLength(bitmap, pixelDataFormat);
            byte[] pixelDataConverted = new byte[pixelDataLengthBytes];

            AddAndConvertPixelData(bitmap, ref pixelDataConverted, pixelDataFormat);

            return pixelDataConverted;
        }


        static private void AddAndConvertPixelData(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
        {
            if (pixelDataFormat.PixelFormat == PixelFormat.RGB)
            {
                AddAndConvertPixelDataRGB(bitmap, ref convertedPixelData, pixelDataFormat);
            }
            else
            {
                AddAndConvertPixelDataRGB565(bitmap, ref convertedPixelData, pixelDataFormat);
            }
        }

        static private void AddAndConvertPixelDataRGB(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
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

        static private void AddAndConvertPixelDataRGB565(Bitmap bitmap, ref byte[] convertedPixelData, PixelDataFormat pixelDataFormat)
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
    }
}
