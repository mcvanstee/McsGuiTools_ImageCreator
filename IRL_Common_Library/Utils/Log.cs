namespace IRL_Common_Library.Utils
{
    static public class Log
    {
        private static StreamWriter? s_streamWriter;
        private static bool s_verbose;

        public static void OpenNewFile(string path, bool verbose)
        {
            s_streamWriter?.Close();
            string logFilePath = Path.Combine(path, "log.txt");
            s_streamWriter = new StreamWriter(logFilePath);
            s_verbose = verbose;
        }

        public static void CloseFile()
        {
            s_streamWriter?.Close();
        }

        public static void Error(string message)
        {
            s_streamWriter?.WriteLine("ERROR " + message);
        }

        public static void WriteLine(string message)
        {
            s_streamWriter?.WriteLine(message);
        }

        public static void Verbose(string message)
        {
            if (s_verbose)
            {
                s_streamWriter?.WriteLine(message);
            }
        }

        public static void Warning(string message)
        {
            s_streamWriter?.WriteLine("WARNING " + message);
        }

        public static void Info(string message)
        {
            s_streamWriter?.WriteLine("INFO " + message);
        }

        public static void WritePixelDataRLE(byte[] data)
        {
            string line = string.Empty;
            for (int i = 0; i < data.Length; i += 3)
            {
                if (i % 30 == 0)
                {
                    s_streamWriter?.WriteLine(line);
                    line = string.Empty;
                }
                else
                {
                    ushort pixelData = BitConverter.ToUInt16(data, i + 1);
                    string pixelDataHex = pixelData.ToString("X4");
                    line += $"{data[i]} {pixelDataHex}; ";
                }
            }
        }
    }
}
