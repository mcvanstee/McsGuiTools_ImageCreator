using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Image_Creator_Tools.BuilderSettings
{
    [Serializable]
    public class FontBuilderSetting
    {
        [XmlElement]
        public string FontName { get; set; } = "";

        [XmlElement]
        public float FontSize { get; set; }

        [XmlElement]
        public FontStyle FontStyle { get; set; }

        public FontBuilderSetting() { }

        public FontBuilderSetting(Font font) 
        { 
            FontName = font.Name;
            FontSize = font.Size;
            FontStyle = font.Style;
        }
    }
}
