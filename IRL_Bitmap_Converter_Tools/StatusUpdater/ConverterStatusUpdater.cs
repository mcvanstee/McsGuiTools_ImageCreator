namespace IRL_Bitmap_Converter_Tools.StatusUpdater
{
    public class ConverterStatusUpdater
    {
        private string m_status = "";
        private int m_progress;
        private int m_noOfConvertedInstructions;

        public int NoOfInstructions { get; set; }
        public EventHandler<ConverterStatusEventArgs> UpdateConverterStatus;

        public void ResetValues()
        {
            m_noOfConvertedInstructions = 0;
            NoOfInstructions = 0;
            m_progress = 0;
            m_status = "";
        }

        public void UpdateStatusAndProgress(string status, int progress)
        {
            if (progress < 0)
            {
                progress = 0;
            }

            if (status == m_status && progress == m_progress)
            {
                return;
            }

            m_status = status;
            m_progress = progress;

            ConverterStatusEventArgs eventArgs = new(status, progress);
            UpdateConverterStatus?.Invoke(this, eventArgs);
        }

        public void UpdateStatusAndInstructionsConverted(string status, int noOfConvertedInstructions)
        {
            m_noOfConvertedInstructions += noOfConvertedInstructions;
            int progress;

            if (NoOfInstructions == 0 || m_noOfConvertedInstructions == 0)
            {
                progress = 0;
            }
            else if (m_noOfConvertedInstructions >= NoOfInstructions)
            {
                progress = 99;
            }
            else
            {
                progress = (int)(m_noOfConvertedInstructions * 100.0f / NoOfInstructions) - 1;
            }

            UpdateStatusAndProgress(status, progress);
        }

        public void UpdateStatus(string status)
        {
            if (status == m_status)
            {
                return;
            }

            m_status = status;

            ConverterStatusEventArgs eventArgs = new(status, m_progress);
            UpdateConverterStatus?.Invoke(this, eventArgs);
        }
    }
}
