using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Image_Creator_Tools.BuilderSettings
{
    public class BitmapStyle
    {
        [XmlElement]
        public string FontName { get; set; } = "";

        [XmlElement]
        public float FontSize { get; set; }

        [XmlElement]
        public FontStyle FontStyle { get; set; }

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

        public List<BitmapProperty> BitmapProperties { get; set; } = new();

        public BitmapStyle() {}
        public BitmapStyle(Font font, Color textColor, Color backColor)
        {
            FontName = font.Name;
            FontSize = font.Size;
            FontStyle = font.Style;

            TextColor = textColor;
            BackColor = backColor;
        }
    }
}
