using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas
{
    public class FontDataItem(byte[] data, CharacterInfo characterInfo) : DataItemBase(data, DataType.Font)
    {
        public CharacterInfo CharacterInfo { get; private set; } = characterInfo;
    }
}
