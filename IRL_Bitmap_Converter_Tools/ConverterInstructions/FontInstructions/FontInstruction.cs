using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions
{
    [Serializable]
    public class FontInstruction : ConverterInstruction
    {
        [XmlElement]
        public FontFileKeyFormat FontFileKeyFormat { get; set; } = FontFileKeyFormat.Both;

        [XmlArray("")]
        public List<FontBitmap> FontBitmaps { get; set; } = new();

        public FontInstruction() { }
    }
}
