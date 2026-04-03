namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas
{
    public abstract class DataItemBase(byte[] data, DataType type) 
    {
        public DataType Type { get; } = type;
        public byte[] Data { get; private set; } = data;
    }
}
