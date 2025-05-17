namespace IRL_Common_Library.Utils
{
    static public class Log
    {
        private static StreamWriter? s_streamWriter;

        public static void OpenNewFile(string path)
        {
            s_streamWriter?.Close();
            s_streamWriter = new StreamWriter(path + "\\" + "log.txt");
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

        public static void Warning(string message)
        {
            s_streamWriter?.WriteLine("WARNING " + message);
        }

        public static void Info(string message)
        {
            s_streamWriter?.WriteLine("INFO " + message);
        }
    }
}
