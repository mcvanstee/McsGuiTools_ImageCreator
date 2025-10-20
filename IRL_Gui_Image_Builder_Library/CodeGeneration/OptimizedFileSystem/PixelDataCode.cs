using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.OptimizedFileSystem
{
    public static class PixelDataCode
    {
        public static void CreatePixelDataCode(
            ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder, ref byte[] pixelData)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.PixelDataFile + ".c");

            CodeGenegrationUtils.Include(sw, FileConstants.PixelDataFile);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "const uint8_t pixeldata[] = \n" +
                "{");

            for (int i = 0; i < pixelData.Length; i++)
            {
                if ((i % 64) == 0)
                {
                    sw.Write("    ");
                }

                sw.Write(pixelData[i] + ",");

                if (((i + 1) < pixelData.Length) && ((i + 1) % 64 == 0))
                {
                    sw.WriteLine("");
                }
            }

            sw.WriteLine(
                "\n};");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);

            AddGetRgb565Color(sw, builderSettings);
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
                "static inline void fs_addPixels(uint16_t *restrict p_buffer, const uint32_t writeIndex, uint16_t noOfPixels, uint16_t color)\n" +
                "{\n" +
                "    for (uint32_t i = writeIndex; i < (writeIndex + noOfPixels); i++)\n" +
                "    {\n" +
                "        p_buffer[i] = color;\n" +
                "    }\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "void fs_read(uint16_t *restrict p_buffer, const uint32_t bufferLength, fs_pixeldata_info_s *p_pixelDataInfo)\n" +
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
                "            return;\n" +
                "        }\n" +
                "    }\n" +
                "\n" +
                "    while (p_pixelDataInfo->pixelsToRead > 0)\n" +
                "    {\n" +
                "        const uint16_t noOfPixels = (pixeldata[p_pixelDataInfo->readIndex] + 1);\n" +
                "        const uint8_t pixel = pixeldata[p_pixelDataInfo->readIndex + 1];\n" +
                "        const uint16_t color = fs_getColor(p_pixelDataInfo->foreColor, p_pixelDataInfo->backColor, pixel);\n" +
                "\n" +
                "        if (p_pixelDataInfo->pixelsToRead > noOfPixels)\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, noOfPixels, color);\n" +
                "            writeIndex += noOfPixels;\n" +
                "            p_pixelDataInfo->pixelsToRead -= noOfPixels;\n" +
                "        }\n" +
                "        else if (p_pixelDataInfo->pixelsToRead < noOfPixels)\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, p_pixelDataInfo->pixelsToRead, color);\n" +
                "            writeIndex += p_pixelDataInfo->pixelsToRead;\n" +
                "            p_pixelDataInfo->noOfPixelsLeft = noOfPixels - p_pixelDataInfo->pixelsToRead;\n" +
                "            p_pixelDataInfo->colorPixelLeft = color;\n" +
                "            p_pixelDataInfo->pixelsToRead = 0;\n" +
                "        }\n" +
                "        else\n" +
                "        {\n" +
                "            fs_addPixels(p_buffer, writeIndex, noOfPixels, color);\n" +
                "            writeIndex += noOfPixels;\n" +
                "            p_pixelDataInfo->pixelsToRead = 0;\n" +
                "            p_pixelDataInfo->noOfPixelsLeft = 0;\n" +
                "        }\n" +
                "\n" +
                "        p_pixelDataInfo->readIndex += 2;\n" +
                "    }\n" +
                "}\n");

            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "void fs_transferPixels(fs_pixeldata_info_s *p_pixelDataInfo, void (*transferPixels)(const uint16_t color, const int32_t noOfpixels))\n" +
                "{\n" +
                "    uint32_t pixelsToTransfer = p_pixelDataInfo->pixelsToRead;\n" +
                "\n" +
                "    while (pixelsToTransfer > 0)\n" +
                "    {\n" +
                "        const uint16_t noOfPixels = (pixeldata[p_pixelDataInfo->readIndex] + 1);\n" +
                "        const uint8_t pixel = pixeldata[p_pixelDataInfo->readIndex + 1];\n" +
                "        const uint16_t color = fs_getColor(p_pixelDataInfo->foreColor, p_pixelDataInfo->backColor, pixel);\n" +
                "\n" +
                "        transferPixels(color, noOfPixels);\n" +
                "\n" +
                "        if (pixelsToTransfer > noOfPixels)\n" +
                "        {\n" +
                "            pixelsToTransfer -= noOfPixels;\n" +
                "            p_pixelDataInfo->readIndex += 2;\n" +
                "        }\n" +
                "        else\n" +
                "        {\n" +
                "            pixelsToTransfer = 0;\n" +
                "        }\n" +
                "    }\n" +
                "}");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);
            
            sw.Close();
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
