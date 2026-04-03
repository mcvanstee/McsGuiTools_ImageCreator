using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions
{
    [Serializable]
    public class TextInstruction : ConverterInstruction
    {
        [XmlElement]
        public TextTable Table { get; set; } = new();

        [XmlElement]
        public int DataLocationId { get; set; } = 0;
    }
}
