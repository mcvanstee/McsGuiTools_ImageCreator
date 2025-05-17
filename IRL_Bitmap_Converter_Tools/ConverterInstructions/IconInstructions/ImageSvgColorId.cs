using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions
{
    public class ImageSvgColorId
    {
        public string Id { get; set; } = "";

        [XmlIgnore]
        public Color Color { get; set; } = Color.White;

        [XmlElement("Color")]
        public int ColorAsArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        [XmlElement]
        public int ConverterColorId { get; set; } = -1;

        public ImageSvgColorId()
        {
        }

        public ImageSvgColorId(string id, Color color)
        {
            Id = id;
            Color = color;
        }
    }
}
