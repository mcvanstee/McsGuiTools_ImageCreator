using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class CrcCodeGenerator
    {
        public static void CreateCrcCode(string projectPath)
        {
            StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + FileConstants.CrcFile + ".c");

            sw.Write(
                "/*\r\n" +
                " *This code is using a part of the LibCRC library by Lammert Bies.\r\n" +
                " *https://www.lammertbies.nl/\r\n" +
                " *https://www.libcrc.org/\r\n" +
                " */");
            CodeGenegrationUtils.BlankLine(sw);
            string licence =
                "/*\r\n * Library: libcrc\r\n * File:    src/crc32.c\r\n * Author:  Lammert Bies\r\n *\r\n * This file is licensed under the MIT License as stated below\r\n *\r\n * Copyright (c) 1999-2016 Lammert Bies\r\n *\r\n * Permission is hereby granted, free of charge, to any person obtaining a copy\r\n * of this software and associated documentation files (the \"Software\"), to deal\r\n * in the Software without restriction, including without limitation the rights\r\n * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell\r\n * copies of the Software, and to permit persons to whom the Software is\r\n * furnished to do so, subject to the following conditions:\r\n *\r\n * The above copyright notice and this permission notice shall be included in all\r\n * copies or substantial portions of the Software.\r\n * \r\n * THE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\r\n * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\r\n * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\r\n * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\r\n * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\r\n * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE\r\n * SOFTWARE.\r\n *\r\n * Description\r\n * -----------\r\n * The source file src/crc32.c contains the routines which are needed to\r\n * calculate a 32 bit CRC value of a sequence of bytes.\r\n */\r\n";
            sw.Write(licence);

            CodeGenegrationUtils.Include(sw, FileConstants.CrcFile);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.Define(sw, "CRC_START_32", "0xFFFFFFFFul");
            CodeGenegrationUtils.BlankLine(sw);
            WriteTable(sw);
            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.BlankLine(sw);

            sw.WriteLine(
                "uint32_t crc_32(const unsigned char *input_str, size_t num_bytes)\r\n" +
                "{\r\n" +
                "    uint32_t crc;\r\n" +
                "    const unsigned char *ptr;\r\n" +
                "    size_t a;\r\n" +
                "\r\n" +
                "    crc = CRC_START_32;\r\n" +
                "    ptr = input_str;\r\n" +
                "\r\n" +
                "    if (ptr != NULL)\r\n" +
                "    {\r\n" +
                "        for (a=0; a < num_bytes; a++)\r\n " +
                "        {\r\n" +
                "            crc = (crc >> 8) ^ crc_tab32[(crc ^ (uint32_t) *ptr++) & 0x000000FFul];\r\n" +
                "        }\r\n" +
                "    }\r\n" +
                "\r\n" +
                "    return (crc ^ 0xFFFFFFFFul);\r\n" +
                "}  /* crc_32 */\r\n" +
                "\r\n" +
                "/*\r\n" +
                " * uint32_t update_crc_32(uint32_t crc, unsigned char c);\r\n" +
                " *\r\n" +
                " * The function update_crc_32() calculates a new CRC-32 value based on the\r\n" +
                " * previous value of the CRC and the next byte of the data to be checked.\r\n" +
                " */\r\n" +
                "\r\n" +
                "uint32_t update_crc_32(uint32_t crc, unsigned char c)\r\n" +
                "{\r\n" +
                "    return (crc >> 8) ^ crc_tab32[(crc ^ (uint32_t)c) & 0x000000FFul];\r\n" +
                "} /* update_crc_32 */\r\n");

            CodeGenegrationUtils.BlankLine(sw);
            CodeGenegrationUtils.EndOfFile(sw);

            sw.Close();
        }

        private static void WriteTable(StreamWriter sw)
        {
            sw.Write("const uint32_t crc_tab32[256] = {\r\n\t0x00000000ul,\r\n\t0x77073096ul,\r\n\t0xEE0E612Cul,\r\n\t0x990951BAul,\r\n\t0x076DC419ul,\r\n\t0x706AF48Ful,\r\n\t0xE963A535ul,\r\n\t0x9E6495A3ul,\r\n\t0x0EDB8832ul,\r\n\t0x79DCB8A4ul,\r\n\t0xE0D5E91Eul,\r\n\t0x97D2D988ul,\r\n\t0x09B64C2Bul,\r\n\t0x7EB17CBDul,\r\n\t0xE7B82D07ul,\r\n\t0x90BF1D91ul,\r\n\t0x1DB71064ul,\r\n\t0x6AB020F2ul,\r\n\t0xF3B97148ul,\r\n\t0x84BE41DEul,\r\n\t0x1ADAD47Dul,\r\n\t0x6DDDE4EBul,\r\n\t0xF4D4B551ul,\r\n\t0x83D385C7ul,\r\n\t0x136C9856ul,\r\n\t0x646BA8C0ul,\r\n\t0xFD62F97Aul,\r\n\t0x8A65C9ECul,\r\n\t0x14015C4Ful,\r\n\t0x63066CD9ul,\r\n\t0xFA0F3D63ul,\r\n\t0x8D080DF5ul,\r\n\t0x3B6E20C8ul,\r\n\t0x4C69105Eul,\r\n\t0xD56041E4ul,\r\n\t0xA2677172ul,\r\n\t0x3C03E4D1ul,\r\n\t0x4B04D447ul,\r\n\t0xD20D85FDul,\r\n\t0xA50AB56Bul,\r\n\t0x35B5A8FAul,\r\n\t0x42B2986Cul,\r\n\t0xDBBBC9D6ul,\r\n\t0xACBCF940ul,\r\n\t0x32D86CE3ul,\r\n\t0x45DF5C75ul,\r\n\t0xDCD60DCFul,\r\n\t0xABD13D59ul,\r\n\t0x26D930ACul,\r\n\t0x51DE003Aul,\r\n\t0xC8D75180ul,\r\n\t0xBFD06116ul,\r\n\t0x21B4F4B5ul,\r\n\t0x56B3C423ul,\r\n\t0xCFBA9599ul,\r\n\t0xB8BDA50Ful,\r\n\t0x2802B89Eul,\r\n\t0x5F058808ul,\r\n\t0xC60CD9B2ul,\r\n\t0xB10BE924ul,\r\n\t0x2F6F7C87ul,\r\n\t0x58684C11ul,\r\n\t0xC1611DABul,\r\n\t0xB6662D3Dul,\r\n\t0x76DC4190ul,\r\n\t0x01DB7106ul,\r\n\t0x98D220BCul,\r\n\t0xEFD5102Aul,\r\n\t0x71B18589ul,\r\n\t0x06B6B51Ful,\r\n\t0x9FBFE4A5ul,\r\n\t0xE8B8D433ul,\r\n\t0x7807C9A2ul,\r\n\t0x0F00F934ul,\r\n\t0x9609A88Eul,\r\n\t0xE10E9818ul,\r\n\t0x7F6A0DBBul,\r\n\t0x086D3D2Dul,\r\n\t0x91646C97ul,\r\n\t0xE6635C01ul,\r\n\t0x6B6B51F4ul,\r\n\t0x1C6C6162ul,\r\n\t0x856530D8ul,\r\n\t0xF262004Eul,\r\n\t0x6C0695EDul,\r\n\t0x1B01A57Bul,\r\n\t0x8208F4C1ul,\r\n\t0xF50FC457ul,\r\n\t0x65B0D9C6ul,\r\n\t0x12B7E950ul,\r\n\t0x8BBEB8EAul,\r\n\t0xFCB9887Cul,\r\n\t0x62DD1DDFul,\r\n\t0x15DA2D49ul,\r\n\t0x8CD37CF3ul,\r\n\t0xFBD44C65ul,\r\n\t0x4DB26158ul,\r\n\t0x3AB551CEul,\r\n\t0xA3BC0074ul,\r\n\t0xD4BB30E2ul,\r\n\t0x4ADFA541ul,\r\n\t0x3DD895D7ul,\r\n\t0xA4D1C46Dul,\r\n\t0xD3D6F4FBul,\r\n\t0x4369E96Aul,\r\n\t0x346ED9FCul,\r\n\t0xAD678846ul,\r\n\t0xDA60B8D0ul,\r\n\t0x44042D73ul,\r\n\t0x33031DE5ul,\r\n\t0xAA0A4C5Ful,\r\n\t0xDD0D7CC9ul,\r\n\t0x5005713Cul,\r\n\t0x270241AAul,\r\n\t0xBE0B1010ul,\r\n\t0xC90C2086ul,\r\n\t0x5768B525ul,\r\n\t0x206F85B3ul,\r\n\t0xB966D409ul,\r\n\t0xCE61E49Ful,\r\n\t0x5EDEF90Eul,\r\n\t0x29D9C998ul,\r\n\t0xB0D09822ul,\r\n\t0xC7D7A8B4ul,\r\n\t0x59B33D17ul,\r\n\t0x2EB40D81ul,\r\n\t0xB7BD5C3Bul,\r\n\t0xC0BA6CADul,\r\n\t0xEDB88320ul,\r\n\t0x9ABFB3B6ul,\r\n\t0x03B6E20Cul,\r\n\t0x74B1D29Aul,\r\n\t0xEAD54739ul,\r\n\t0x9DD277AFul,\r\n\t0x04DB2615ul,\r\n\t0x73DC1683ul,\r\n\t0xE3630B12ul,\r\n\t0x94643B84ul,\r\n\t0x0D6D6A3Eul,\r\n\t0x7A6A5AA8ul,\r\n\t0xE40ECF0Bul,\r\n\t0x9309FF9Dul,\r\n\t0x0A00AE27ul,\r\n\t0x7D079EB1ul,\r\n\t0xF00F9344ul,\r\n\t0x8708A3D2ul,\r\n\t0x1E01F268ul,\r\n\t0x6906C2FEul,\r\n\t0xF762575Dul,\r\n\t0x806567CBul,\r\n\t0x196C3671ul,\r\n\t0x6E6B06E7ul,\r\n\t0xFED41B76ul,\r\n\t0x89D32BE0ul,\r\n\t0x10DA7A5Aul,\r\n\t0x67DD4ACCul,\r\n\t0xF9B9DF6Ful,\r\n\t0x8EBEEFF9ul,\r\n\t0x17B7BE43ul,\r\n\t0x60B08ED5ul,\r\n\t0xD6D6A3E8ul,\r\n\t0xA1D1937Eul,\r\n\t0x38D8C2C4ul,\r\n\t0x4FDFF252ul,\r\n\t0xD1BB67F1ul,\r\n\t0xA6BC5767ul,\r\n\t0x3FB506DDul,\r\n\t0x48B2364Bul,\r\n\t0xD80D2BDAul,\r\n\t0xAF0A1B4Cul,\r\n\t0x36034AF6ul,\r\n\t0x41047A60ul,\r\n\t0xDF60EFC3ul,\r\n\t0xA867DF55ul,\r\n\t0x316E8EEFul,\r\n\t0x4669BE79ul,\r\n\t0xCB61B38Cul,\r\n\t0xBC66831Aul,\r\n\t0x256FD2A0ul,\r\n\t0x5268E236ul,\r\n\t0xCC0C7795ul,\r\n\t0xBB0B4703ul,\r\n\t0x220216B9ul,\r\n\t0x5505262Ful,\r\n\t0xC5BA3BBEul,\r\n\t0xB2BD0B28ul,\r\n\t0x2BB45A92ul,\r\n\t0x5CB36A04ul,\r\n\t0xC2D7FFA7ul,\r\n\t0xB5D0CF31ul,\r\n\t0x2CD99E8Bul,\r\n\t0x5BDEAE1Dul,\r\n\t0x9B64C2B0ul,\r\n\t0xEC63F226ul,\r\n\t0x756AA39Cul,\r\n\t0x026D930Aul,\r\n\t0x9C0906A9ul,\r\n\t0xEB0E363Ful,\r\n\t0x72076785ul,\r\n\t0x05005713ul,\r\n\t0x95BF4A82ul,\r\n\t0xE2B87A14ul,\r\n\t0x7BB12BAEul,\r\n\t0x0CB61B38ul,\r\n\t0x92D28E9Bul,\r\n\t0xE5D5BE0Dul,\r\n\t0x7CDCEFB7ul,\r\n\t0x0BDBDF21ul,\r\n\t0x86D3D2D4ul,\r\n\t0xF1D4E242ul,\r\n\t0x68DDB3F8ul,\r\n\t0x1FDA836Eul,\r\n\t0x81BE16CDul,\r\n\t0xF6B9265Bul,\r\n\t0x6FB077E1ul,\r\n\t0x18B74777ul,\r\n\t0x88085AE6ul,\r\n\t0xFF0F6A70ul,\r\n\t0x66063BCAul,\r\n\t0x11010B5Cul,\r\n\t0x8F659EFFul,\r\n\t0xF862AE69ul,\r\n\t0x616BFFD3ul,\r\n\t0x166CCF45ul,\r\n\t0xA00AE278ul,\r\n\t0xD70DD2EEul,\r\n\t0x4E048354ul,\r\n\t0x3903B3C2ul,\r\n\t0xA7672661ul,\r\n\t0xD06016F7ul,\r\n\t0x4969474Dul,\r\n\t0x3E6E77DBul,\r\n\t0xAED16A4Aul,\r\n\t0xD9D65ADCul,\r\n\t0x40DF0B66ul,\r\n\t0x37D83BF0ul,\r\n\t0xA9BCAE53ul,\r\n\t0xDEBB9EC5ul,\r\n\t0x47B2CF7Ful,\r\n\t0x30B5FFE9ul,\r\n\t0xBDBDF21Cul,\r\n\t0xCABAC28Aul,\r\n\t0x53B39330ul,\r\n\t0x24B4A3A6ul,\r\n\t0xBAD03605ul,\r\n\t0xCDD70693ul,\r\n\t0x54DE5729ul,\r\n\t0x23D967BFul,\r\n\t0xB3667A2Eul,\r\n\t0xC4614AB8ul,\r\n\t0x5D681B02ul,\r\n\t0x2A6F2B94ul,\r\n\t0xB40BBE37ul,\r\n\t0xC30C8EA1ul,\r\n\t0x5A05DF1Bul,\r\n\t0x2D02EF8Dul\r\n};");
        }
    }
}
