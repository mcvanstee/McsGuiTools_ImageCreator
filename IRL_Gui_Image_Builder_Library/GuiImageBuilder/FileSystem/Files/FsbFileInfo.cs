using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files
{
    public class FsbFileInfo
    {
        public string Filename { get; }
        public string FilePath { get; }
        public string FileKey { get; set; }
        public List<string> Folders { get; } = [];
        public int FileIndex { get; set; }
        public FsbFile FsbFile { get; } = new FsbFile();
        public bool IsDummy { get; set; }
        public DataLocation DataLocation { get; set; } = new DataLocation();


        // File Properties 
        //
        public string FileNameWithoutProperties { get; set; }
        public bool HasFileProperties { get; set; }
        public bool IsRootFileProperty
        {
            get
            {
                foreach (FsbFileProperty fsbFileProperty in FsbFileProperties)
                {
                    if (fsbFileProperty.Value != 0)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        public List<FsbFileProperty> FsbFileProperties { get; } = new List<FsbFileProperty>();

        public FsbFileInfo(string filename, string filePath, int dataLocationId)
        {
            Filename = filename;
            FilePath = filePath;
            DataLocation.LocationID = dataLocationId;
        }
    }
}
