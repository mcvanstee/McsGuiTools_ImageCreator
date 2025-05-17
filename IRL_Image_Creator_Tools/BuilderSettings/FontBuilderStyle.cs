using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Image_Creator_Tools.BuilderSettings
{
    [Serializable]
    public class FontBuilderStyle
    {
        [XmlIgnore]
        public Color TextColor { get; set; } = Color.Black;

        [XmlIgnore]
        public Color BackColor { get; set; } = Color.White;

        [XmlElement("TextColor")]
        public int TextColorAsArgb
        {
            get { return TextColor.ToArgb(); }
            set { TextColor = Color.FromArgb(value); }
        }

        [XmlElement("BackColor")]
        public int BackColorAsArgb
        {
            get { return BackColor.ToArgb(); }
            set { BackColor = Color.FromArgb(value); }
        }

        public FontBuilderStyle() { }
    }
}
