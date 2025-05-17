using System;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    public class BuilderEventArgs : EventArgs
    {
        public string Status { get; }
        public int Progress { get; }

        public BuilderEventArgs(string status, int progress)
        {
            Status = status;
            Progress = progress;
        }
    }
}
