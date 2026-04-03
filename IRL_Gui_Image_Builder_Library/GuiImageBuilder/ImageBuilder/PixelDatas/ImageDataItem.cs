using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas
{
    public class ImageDataItem(byte[] data, FsbFileInfo fileInfo) : DataItemBase(data, DataType.Bitmap)
    {
        public FsbFileInfo FileInfo { get; private set; } = fileInfo;
    }
}
