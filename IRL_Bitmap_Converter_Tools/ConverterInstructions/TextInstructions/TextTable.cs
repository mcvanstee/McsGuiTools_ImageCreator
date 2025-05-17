using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions
{
    public class TextTable
    {
        [XmlElement]
        public TextRecord Header { get; set; } = new();

        [XmlElement]
        public int NumberOfColumns { get; set; }

        [XmlArray("")]
        public List<TextRecord> Records { get; set; } = new();

        [XmlElement]
        public BitmapProperty TranslationProperty { get; set; } = new();

        [XmlElement]
        public bool Translate { get; set; }

        public TextTable() { }
    }
}
