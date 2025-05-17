namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.Fonts
{
    public class FsFont
    {
        private List<CharacterInfo> m_characterInfos = new List<CharacterInfo>();
        public ushort FontId { get; private set; }
        public bool IsNumberOnly { get; private set; }
        public string Name { get; private set; }
        public string Path { get; private set; }
        public List<CharacterInfo> CharacterInfos => m_characterInfos;

        public FsFont(ushort fontId, string name, string path, bool isNumberOnly)
        {
            FontId = fontId;
            Name = name;
            Path = path;
            IsNumberOnly = isNumberOnly;
        }
    }
}
