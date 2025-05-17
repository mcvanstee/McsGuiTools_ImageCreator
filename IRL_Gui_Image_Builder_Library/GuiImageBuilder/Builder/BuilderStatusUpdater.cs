namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    public class BuilderStatusUpdater
    {
        private string m_status = "";
        private int m_progress;
        private int m_noOfConvertedFiles;

        public int NoOfFiles;
        public EventHandler<BuilderEventArgs> UpdateBuildStatus;

        public void ResetValues()
        {
            m_noOfConvertedFiles = 0;
            NoOfFiles = 0;
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

            BuilderEventArgs eventArgs = new(status, progress);
            UpdateBuildStatus?.Invoke(this, eventArgs);
        }

        public void UpdateStatusAndFilesConverted(string status, int noOfConvertedFiles)
        {
            m_noOfConvertedFiles += noOfConvertedFiles;
            int progress;

            if (NoOfFiles == 0 || m_noOfConvertedFiles == 0)
            {
                progress = 0;
            }
            else if (m_noOfConvertedFiles >= NoOfFiles)
            {
                progress = 99;
            }
            else
            {
                progress = (int)(m_noOfConvertedFiles * 100.0f / NoOfFiles) - 1;
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

            BuilderEventArgs eventArgs = new(m_status, m_progress);
            UpdateBuildStatus?.Invoke(this, eventArgs);
        }
    }
}
