using IRL_Image_Creator_Tools.BuilderSettings;
using System.Xml.Serialization;

namespace IRL_Image_Creator_Tools.BuilderInstructions
{
    [Serializable]
    public class TextInstruction : BuilderInstruction
    {
        [XmlElement]
        public TextBuilderSetting BuilderSetting { get; set; } = new();

        [XmlArray("")]
        public List<BitmapStyle> Styles { get; set; } = new();
    }
}
