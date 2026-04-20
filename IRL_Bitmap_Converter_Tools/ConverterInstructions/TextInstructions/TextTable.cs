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

        [XmlArray("")]
        public List<int> ColumnWidths { get; set; } = new();

        [XmlElement]
        public BitmapProperty TranslationProperty { get; set; } = new();

        [XmlElement]
        public bool Translate { get; set; }

        [XmlElement]
        public int NoOfHeaderProperties { get; set; }

        public TextTable() { }

        public void ResetColumnWidths()
        {
            ColumnWidths.Clear();

            for (int i = 0; i < NumberOfColumns + 1; i++)
            {
                ColumnWidths.Add(60);
            }
        }
    }
}
