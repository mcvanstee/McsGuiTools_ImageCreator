using System.Diagnostics;

namespace IRL_Common_Library.CRC
{
    public static class CRC16
    {
        private const int CrcTableSize = 256;
        private static ushort[] CrcTable = new ushort[CrcTableSize];
        private static bool m_isInitialized = false;

        public static ushort Crc16(ref byte[] buffer, int length)
        {
            ushort crc = 0x0000;

            if (!m_isInitialized)
            {
                InitCrc16Table();
            }

            for (int i = 0; i < length; i++)
            {
                crc = (ushort)((ushort)(crc >> 8) ^ CrcTable[(crc ^ buffer[i]) & 0x00FF]);
            }

            return crc;
        }

        private static void InitCrc16Table()
        {
            for (int i = 0; i < CrcTableSize; i++)
            {
                ushort crc = 0;
                ushort c = (ushort)i;

                for (int j = 0; j < 8; j++)
                {
                    if (((crc ^ c) & 0x0001) > 0)
                    {
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    }
                    else
                    {
                        crc = (ushort)(crc >> 1);
                    }

                    c = (ushort)(c >> 1);
                }

                CrcTable[i] = crc;
            }

            //PrintTable();

            m_isInitialized = true;
        }

        public static void PrintTable()
        {
            const int crcPerLine = 8;
            int i = 0;

            foreach (ushort crc in CrcTable)
            {
                Debug.Write(crc.ToString("X4") + ", ");

                i += 1;

                if (i >= crcPerLine)
                {
                    i = 0;
                    Debug.Write("\n");
                }

            }
        }
    }
}
