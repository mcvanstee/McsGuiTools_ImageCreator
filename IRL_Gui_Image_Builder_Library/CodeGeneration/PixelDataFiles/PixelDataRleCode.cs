using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.PixelDataFiles
{
    public static class PixelDataRleCode
    {
        public static void CreatePixelDataCode(ImageBuilderSettings builderSettings, List<PixelData> pixelDatas)
        {
            PixelFormat pixelFormat = builderSettings.PixelDataFormat.PixelFormat;
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.PIXEL_DATA_RLE_FILE + ".c");
            StreamWriter sw = new(filePath);

            CodeGenegrationUtils.Include(sw, FileConstants.PIXEL_DATA_RLE_FILE);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CreatePixelDataArray(sw, pixelDatas, pixelFormat);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }

        private static void CreatePixelDataArray(StreamWriter sw, List<PixelData> pixelDatas, PixelFormat pixelFormat)
        {
            sw.WriteLine(
                "const uint8_t pixeldata[] = \n" +
                "{");

            foreach (PixelData pixelData in pixelDatas)
            {
                if ((pixelData.DataLocation.DataLocationType == DataLocationType.Code) &&
                    (pixelData.DataLocation.CompressionType == CompressionType.RLE))
                {
                    AddPixelData(sw, pixelData, pixelFormat);
                }
            }

            sw.WriteLine(
                "};");
        }

        private static void AddPixelData(StreamWriter sw, PixelData pixelData, PixelFormat pixelFormat)
        {
            int rowLength = (pixelFormat == PixelFormat.RGB) ? 64 : 63;
            int rleDataLength = (pixelFormat == PixelFormat.RGB) ? 4 : 3;

            foreach (DataItemBase dataItem in pixelData.DataItems)
            {
                WriteFileInfoComments(sw, dataItem);

                byte[] imageData = dataItem.Data;
                

                for (int j = 0; j < imageData.Length; j += rleDataLength)
                {
                    if ((j % rowLength) == 0)
                    {
                        sw.Write("    ");
                    }

                    sw.Write(imageData[j] + ",");
                    sw.Write("0x" + imageData[j + 1].ToString("X2") + ",");
                    sw.Write("0x" + imageData[j + 2].ToString("X2") + ",");
                    if (pixelFormat == PixelFormat.RGB)
                    {
                        sw.Write("0x" + imageData[j + 3].ToString("X2") + ",");
                    }

                    if (((j + rleDataLength) < imageData.Length) && ((j + rleDataLength) % rowLength == 0))
                    {
                        sw.WriteLine("");
                    }
                    else
                    {
                        sw.Write(" ");
                    }
                }

                sw.WriteLine("");
            }
        }

        private static void WriteFileInfoComments(StreamWriter sw, DataItemBase dataItem)
        {
            if (dataItem.Type == DataType.Bitmap)
            {
                ImageDataItem imageDataItem = (ImageDataItem)dataItem;
                FsbFileInfo fileInfo = imageDataItem.FileInfo;
                sw.WriteLine($"// File key: {fileInfo.FileKey} Name: {fileInfo.Filename} W: {fileInfo.FsbFile.Width}, H: {fileInfo.FsbFile.Height}");
            }
            else
            {
                FontDataItem fontDataItem = (FontDataItem)dataItem;
                CharacterInfo charInfo = fontDataItem.CharacterInfo;
                sw.WriteLine($"// Character: {charInfo.ASSCI} Font: {charInfo.Font.FontId} W: {charInfo.Width}, H: {charInfo.Height}");
            }

            sw.WriteLine($"// Compression RLE, Data length: {dataItem.Data.Length} bytes");
        }
    }
}
