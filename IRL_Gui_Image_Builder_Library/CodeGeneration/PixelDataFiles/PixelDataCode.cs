using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.PixelDataFiles
{
    public static class PixelDataCode
    {
        public static void CreatePixelDataCode(ImageBuilderSettings builderSettings, List<PixelData> pixelDatas)
        {
            string filePath = Path.Combine(FileConstants.GetSourceFolder(), FileConstants.PIXEL_DATA_RLE_A_FILE + ".c");
            StreamWriter sw = new(filePath);

            CodeGenegrationUtils.Include(sw, FileConstants.PIXEL_DATA_RLE_A_FILE);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "FS_COLOR_PALETTE_SIZE", "18");
            CodeGenegrationUtils.BlankLine(sw);
            CreatePixelDataArray(sw, pixelDatas);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);

            AddGetRgb565Color(sw, builderSettings);
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "static uint8_t colorPalette[FS_COLOR_PALETTE_SIZE] = { 0, 0x10, 0x20, 0x30, 0x40, 0x50, 0x60, 0x70, 0x80, 0x90, 0xA0, 0xB0, 0xC0, 0xD0, 0xE0, 0xF0, 0x00, 0xFF };\n" +
                "static uint16_t customColorPalette565[FS_COLOR_PALETTE_SIZE] = { 0, 0x1082, 0x2104, 0x3186, 0x4208, 0x528A, 0x630C, 0x738E, 0x8410, 0x9492, 0xA514, 0xB596, 0xC618, 0xD69A, 0xE71C, 0xF79E, 0x0000, 0xFFFF };\n" +
                "static uint32_t foreColorRGB = 0x000000;\n" +
                "static uint32_t backColorRGB = 0xFFFFFF;");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "static inline uint8_t fs_getColorValue(const uint8_t fore, const uint8_t back, const uint8_t pixelVal, const float alpha)\n" +
                "{\n" +
                "    uint8_t colorValue;\n" +
                "\n" +
                "    if (fore < back)\n" +
                "    {\n" +
                "        if ((fore == 0) && (back == 0xFF))\n" +
                "        {\n" +
                "            colorValue = pixelVal;\n" +
                "        }\n" +
                "        else\n" +
                "        {\n" +
                "            colorValue = (uint8_t)(fore + (uint8_t)((back - fore) * alpha));\n" +
                "        }\n" +
                "    }\n" +
                "    else\n" +
                "    {\n" +
                "        colorValue = (uint8_t)(fore - (uint8_t)((fore - back) * alpha));\n" +
                "    }\n" +
                "\n" +
                "    return colorValue;\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "static inline uint16_t fs_getColor(const uint32_t foreColor, const uint32_t backColor, const uint8_t pixelValue)\n" +
                "{\n" +
                "    uint16_t color;\n" +
                "\n" +
                "    if (pixelValue == 0)\n" +
                "    {\n" +
                "        const uint8_t r = (foreColor & 0x00FF0000u) >> 16;\n" +
                "        const uint8_t g = (foreColor & 0x0000FF00u) >> 8;\n" +
                "        const uint8_t b = foreColor & 0x000000FFu;\n" +
                "\n" +
                "        color = fs_getRGB565(r, g, b);\n" +
                "    }\n" +
                "    else if (pixelValue == 0xFF)\n" +
                "    {\n" +
                "        const uint8_t r = (backColor & 0x00FF0000u) >> 16;\n" +
                "        const uint8_t g = (backColor & 0x0000FF00u) >> 8;\n" +
                "        const uint8_t b = backColor & 0x000000FFu;\n" +
                "\n" +
                "        color = fs_getRGB565(r, g, b);\n" +
                "    }\n" +
                "    else\n" +
                "    {\n" +
                "        const uint8_t foreR = (foreColor & 0x00FF0000u) >> 16;\n" +
                "        const uint8_t foreG = (foreColor & 0x0000FF00u) >> 8;\n" +
                "        const uint8_t foreB = foreColor & 0x000000FFu;\n" +
                "\n" +
                "        const uint8_t backR = (backColor & 0x00FF0000u) >> 16;\n" +
                "        const uint8_t backG = (backColor & 0x0000FF00u) >> 8;\n" +
                "        const uint8_t backB = backColor & 0x000000FFu;\n" +
                "\n" +
                "        const float alpha = pixelValue / 255.0f;\n" +
                "\n" +
                "        const uint8_t r = fs_getColorValue(foreR, backR, pixelValue, alpha);\n" +
                "        const uint8_t g = fs_getColorValue(foreG, backG, pixelValue, alpha);\n" +
                "        const uint8_t b = fs_getColorValue(foreB, backB, pixelValue, alpha);\n" +
                "\n" +
                "        color = fs_getRGB565(r, g, b);\n" +
                "    }\n" +
                "\n" +
                "    return color;\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "static inline void fs_setColorPalette(const uint32_t fore, const uint32_t back)\n" +
                "{\n" +
                "    foreColorRGB = fore;\n" +
                "    backColorRGB = back;\n" +
                "\n" +
                "    for (uint8_t i = 1; i < 16; i++)\n" +
                "    {\n" +
                "        customColorPalette565[i] = fs_getColor(fore, back, colorPalette[i]);\n" +
                "    }\n" +
                "\n" +
                "    customColorPalette565[16] = fs_getColor(fore, back, colorPalette[16]);\n" +
                "    customColorPalette565[17] = fs_getColor(fore, back, colorPalette[17]);\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "static inline void fs_addPixels(uint16_t *p_buffer, const uint32_t writeIndex, uint16_t noOfPixels, uint16_t color)\n" +
                "{\n" +
                "    for (uint32_t i = writeIndex; i < (writeIndex + noOfPixels); i++)\n" +
                "    {\n" +
                "        p_buffer[i] = color;\n" +
                "    }\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "static inline void fs_getNextPixelData(fs_pixeldata_info_s *p_pixelDataInfo, uint8_t *p_noOfPixels, uint16_t *p_color)\n" +
                "{\n" +
                "    const uint8_t data = (pixeldata[p_pixelDataInfo->readIndex]);\n" +
                "\n" +
                "    if (data == 0)\n" +
                "    {\n" +
                "        *p_color = customColorPalette565[16];\n" +
                "        *p_noOfPixels = (pixeldata[p_pixelDataInfo->readIndex + 1]);\n" +
                "        p_pixelDataInfo->readIndex += 2;\n" +
                "    }\n" +
                "    else if (data == 0x0F)\n" +
                "    {\n" +
                "        *p_color = customColorPalette565[17];\n" +
                "        *p_noOfPixels = (pixeldata[p_pixelDataInfo->readIndex + 1]);\n" +
                "        p_pixelDataInfo->readIndex += 2;\n" +
                "    }\n" +
                "    else\n" +
                "    {\n" +
                "        *p_noOfPixels = (data & 0xF0) >> 4;\n" +
                "        *p_color = customColorPalette565[data & 0x0F];\n" +
                "        p_pixelDataInfo->readIndex += 1;\n" +
                "    }\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "uint32_t fs_read(uint16_t *p_buffer, const uint32_t bufferLength, fs_pixeldata_info_s *p_pixelDataInfo)\n" +
                "{\n" +
                "    uint32_t writeIndex = 0;\n" +
                "\n" +
                "    if (p_pixelDataInfo->noOfPixelsLeft > 0)\n" +
                "    {\n" +
                "        if (p_pixelDataInfo->noOfPixelsLeft <= bufferLength)\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, p_pixelDataInfo->noOfPixelsLeft, p_pixelDataInfo->colorPixelLeft);\n" +
                "            writeIndex += p_pixelDataInfo->noOfPixelsLeft;\n" +
                "            p_pixelDataInfo->pixelsToRead -= p_pixelDataInfo->noOfPixelsLeft;\n" +
                "            p_pixelDataInfo->noOfPixelsLeft = 0;\n" +
                "        }\n" +
                "        else\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, bufferLength, p_pixelDataInfo->colorPixelLeft);\n" +
                "            writeIndex += bufferLength;\n" +
                "            p_pixelDataInfo->pixelsToRead -= bufferLength;\n" +
                "            p_pixelDataInfo->noOfPixelsLeft -= bufferLength;\n" +
                "\n" +
                "            return writeIndex;\n" +
                "        }\n" +
                "    }\n" +
                "    else\n" +
                "    {\n" +
                "        if ((p_pixelDataInfo->foreColor != foreColorRGB) || (p_pixelDataInfo->backColor != backColorRGB))\n" +
                "        {\n" +
                "            fs_setColorPalette(p_pixelDataInfo->foreColor, p_pixelDataInfo->backColor);\n" +
                "        }\n" +
                "    }\n" +
                "\n" +
                "    while (p_pixelDataInfo->pixelsToRead > 0)\n" +
                "    {\n" +
                "        uint8_t noOfPixels = 0;\n" +
                "        uint16_t color = 0;\n" +
                "        const uint32_t pixelsLeftInBuffer = bufferLength - writeIndex;\n" +
                "\n" +
                "        fs_getNextPixelData(p_pixelDataInfo, &noOfPixels, &color);\n" +
                "\n" +
                "        if (pixelsLeftInBuffer > noOfPixels) /* Pixel data can fit in the buffer */\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, noOfPixels, color);\n" +
                "            writeIndex += noOfPixels;\n" +
                "            p_pixelDataInfo->pixelsToRead -= noOfPixels;\n" +
                "        }\n" +
                "        else if (pixelsLeftInBuffer < noOfPixels) /* Pixels can not fit in the buffer, but some pixels can be added to the buffer */\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, pixelsLeftInBuffer, color);\n" +
                "            writeIndex += pixelsLeftInBuffer;\n" +
                "            p_pixelDataInfo->noOfPixelsLeft = noOfPixels - pixelsLeftInBuffer;\n" +
                "            p_pixelDataInfo->colorPixelLeft = color;\n" +
                "            p_pixelDataInfo->pixelsToRead -= pixelsLeftInBuffer;\n" +
                "\n" +
                "            break;\n" +
                "        }\n" +
                "        else /* Pixels can exactly fit in the buffer */\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, noOfPixels, color);\n" +
                "            writeIndex += noOfPixels;\n" +
                "            p_pixelDataInfo->pixelsToRead -= noOfPixels;\n" +
                "            p_pixelDataInfo->noOfPixelsLeft = 0;\n" +
                "        }\n" +
                "    }\n" +
                "\n" +
                "    return writeIndex;\n" +
                "}\n");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "void fs_transferPixels(fs_pixeldata_info_s *p_pixelDataInfo, void (*transferPixels)(const uint16_t color, const int32_t noOfpixels))\n" +
                "{\n" +
                "    uint32_t pixelsToTransfer = p_pixelDataInfo->pixelsToRead;\n" +
                "\n" +
                "    if ((p_pixelDataInfo->foreColor != foreColorRGB) || (p_pixelDataInfo->backColor != backColorRGB))\n" +
                "    {\n" +
                "        fs_setColorPalette(p_pixelDataInfo->foreColor, p_pixelDataInfo->backColor);\n" +
                "    }\n" +
                "    while (pixelsToTransfer > 0)\n" +
                "    {\n" +
                "        uint8_t noOfPixels = 0;\n" +
                "        uint16_t color = 0;\n" +
                "\n" +
                "        fs_getNextPixelData(p_pixelDataInfo, &noOfPixels, &color);\n" +
                "        transferPixels(color, noOfPixels);\n" +
                "\n" +
                "        if (pixelsToTransfer > noOfPixels)\n" +
                "        {\n" +
                "            pixelsToTransfer -= noOfPixels;\n" +
                "        }\n" +
                "        else\n" +
                "        {\n" +
                "            pixelsToTransfer = 0;\n" +
                "        }\n" +
                "    }\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "uint16_t fs_getPixelColor(const uint32_t foreColor, const uint32_t backColor, const uint8_t pixelValue)\n" +
                "{\n" +
                "    return fs_getColor(foreColor, backColor, pixelValue);\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);
            
            sw.Close();
        }

        private static void CreatePixelDataArray(StreamWriter sw, List<PixelData> pixelDatas)
        {
            sw.WriteLine(
                "const uint8_t pixeldata[] = \n" +
                "{");

            foreach (PixelData pixelData in pixelDatas)
            {
                if ((pixelData.DataLocation.DataLocationType == DataLocationType.Code) && 
                    (pixelData.DataLocation.CompressionType == CompressionType.RLE_Alpha))
                {
                    AddPixelData(sw, pixelData);
                }
            }

            sw.WriteLine(
                "};");
        }

        private static void AddPixelData(StreamWriter sw, PixelData pixelData)
        {
            foreach (DataItemBase dataItem in pixelData.DataItems)
            {
                WriteFileInfoComments(sw, dataItem);

                byte[] imageData = dataItem.Data;
                int maxRowLength = 64;
                int currentRowLength = 0;

                bool isBlackOrWhite = false;

                for (int j = 0; j < imageData.Length; j++)
                {
                    if (currentRowLength == 0)
                    {
                        sw.Write("    ");
                    }

                    if (isBlackOrWhite)
                    {
                        sw.Write(imageData[j] + ", ");
                        isBlackOrWhite = false;
                    }
                    else if (imageData[j] == 0 || imageData[j] == 0x0F)
                    {
                        isBlackOrWhite = true;
                        sw.Write("0x" + imageData[j].ToString("X2") + ",");
                    }
                    else
                    {
                        sw.Write("0x" + imageData[j].ToString("X2") + ", ");
                    }

                    currentRowLength++;

                    if (!isBlackOrWhite && (currentRowLength >= maxRowLength))
                    {
                        sw.WriteLine("");
                        currentRowLength = 0;
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
                        
            sw.WriteLine($"// Compression RLE Alpha, Data length: {dataItem.Data.Length} bytes");
        }

        public static void AddGetRgb565Color(StreamWriter sw, ImageBuilderSettings settings)
        {
            if (settings.PixelDataFormat.RGB565SwapBytes)
            {
                sw.WriteLine(
                    "static inline uint16_t fs_getRGB565(const uint8_t r, const uint8_t g, const uint8_t b)\n" +
                    "{\n" +
                    "    return ((r >> 3) << 3) + ((g >> 2) >> 3) + ((g >> 2) << 13) + ((b >> 3) << 8);\n" +
                    "}");
            }
            else
            {
                sw.WriteLine(
                    "static inline uint16_t fs_getRGB565(const uint8_t r, const uint8_t g, const uint8_t b)\n" +
                    "{\n" +
                    "    return ((r >> 3) << 11) + ((g >> 2) << 5) + (b >> 3);\n" +
                    "}");
            }
        }
    }
}
