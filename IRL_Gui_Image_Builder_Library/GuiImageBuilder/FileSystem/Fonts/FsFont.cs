using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts
{
    public class FsFont
    {
        private List<CharacterInfo> m_characterInfos = [];
        public ushort FontId { get; private set; }
        public bool IsNumberOnly { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public List<CharacterInfo> CharacterInfos => m_characterInfos;
        public DataLocation DataLocation { get; set; } = new DataLocation();

        public FsFont(ushort fontId, string name, string path, bool isNumberOnly, DataLocation dataLocation)
        {
            FontId = fontId;
            Name = name;
            Path = path;
            IsNumberOnly = isNumberOnly;
            DataLocation = dataLocation;
        }
    }
}
