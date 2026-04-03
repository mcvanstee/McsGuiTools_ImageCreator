using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas
{
    public class PixelData
    {
        public int Offset { get; set; } = 0;
        public int DataLength { get; private set; } = 0;
        public DataLocation DataLocation { get; private set; } = new DataLocation();
        public List<DataItemBase> DataItems { get; set; } = [];

        public PixelData(DataLocation dataLocation)
        {
            DataLocation = dataLocation;
        }

        public void AppendData(byte[] dataToAppend, FsbFileInfo fileInfo)
        {
            DataItems.Add(new ImageDataItem(dataToAppend, fileInfo));
            DataLength += dataToAppend.Length;
        }

        public void AppendData(byte[] dataToAppend, CharacterInfo characterInfo)
        {
            DataItems.Add(new FontDataItem(dataToAppend, characterInfo));
            DataLength += dataToAppend.Length;
        }
    }
}
