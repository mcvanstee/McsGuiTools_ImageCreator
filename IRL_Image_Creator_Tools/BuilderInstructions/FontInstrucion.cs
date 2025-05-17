using IRL_Image_Creator_Tools.BuilderSettings;
using System.Xml.Serialization;

namespace IRL_Image_Creator_Tools.BuilderInstructions
{
    [Serializable]
    public class FontInstrucion : BuilderInstruction
    {
        [XmlElement]
        public BitmapMargin Margin { get; set; } = new();

        [XmlElement]
        public bool MonospaceNumbers { get; set; } = false;

        [XmlArray("")]
        public List<FontBuilderSetting> BuilderSettings { get; set; } = new();

        [XmlArray("")]
        public List<FontBuilderStyle> FontBuilderStyles { get; set; } = new();

        public FontInstrucion() { }
    }
}
