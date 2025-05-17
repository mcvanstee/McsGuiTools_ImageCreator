using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions
{
    public class FontBitmap
    {
        [XmlElement]
        public string FontName { get; set; } = "";

        [XmlElement]
        public float FontSize { get; set; }

        [XmlElement]
        public FontStyle FontStyle { get; set; }

        [XmlElement]
        public int FontConverterId { get; set; } = -1;

        [XmlArray("")]
        public List<int> FontBitmapStyleIds { get; set; } = new();

        public FontBitmap() { }

        public FontBitmap(Font font)
        {
            FontName = font.Name;
            FontSize = font.Size;
            FontStyle = font.Style;
        }
    }
}
