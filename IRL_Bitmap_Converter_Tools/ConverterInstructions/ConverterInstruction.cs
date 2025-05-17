using System.Xml.Serialization;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions
{
    [XmlInclude(typeof(TextInstruction))]
    [XmlInclude(typeof(FontInstruction))]
    [XmlInclude(typeof(IconInstruction))]
    [Serializable]
    public abstract class ConverterInstruction
    {
        [XmlElement]
        public string Name { get; set; } = "";

        [XmlElement]
        public string FileKeyPrefix { get; set; } = "";
    }
}
