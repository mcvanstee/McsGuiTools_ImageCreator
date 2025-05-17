using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions
{
    public class IconInstruction : ConverterInstruction
    {
        [XmlArray("")]
        public List<SvgFileInfo> SvgFileInfos { get; set; } = new();
    }
}
