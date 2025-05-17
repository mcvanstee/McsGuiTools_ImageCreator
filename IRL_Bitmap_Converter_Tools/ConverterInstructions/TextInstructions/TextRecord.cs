using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions
{
    public class TextRecord
    {
        [XmlElement]
        public string Key { get; set; } = "";

        [XmlArray("")]
        public List<string> Text { get; set; } = new();

        [XmlArray("")]
        public List<int> TextStyleIDs { get; set; } = new();

        public TextRecord() { }
        public TextRecord(string key, string text)
        {
            Key = key;
            Text.Add(text);
        }
    }
}
