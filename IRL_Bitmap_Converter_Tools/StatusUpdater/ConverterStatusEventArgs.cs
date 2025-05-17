namespace IRL_Bitmap_Converter_Tools.StatusUpdater
{
    public class ConverterStatusEventArgs
    {
        public string Status { get; }
        public int Progress { get; }

        public ConverterStatusEventArgs(string status, int progress)
        {
            Status = status;
            Progress = progress;
        }
    }
}
