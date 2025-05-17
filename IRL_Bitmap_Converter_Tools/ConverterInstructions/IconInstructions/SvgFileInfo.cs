using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions
{
    [Serializable]
    public class SvgFileInfo
    {
        [XmlElement]
        public string Filename { get; set; } = "";

        [XmlElement]
        public string IconName { get; set; } = "";

        [XmlElement]
        public string SvgString { get; set; } = "";

        [XmlArray("")]
        public List<int> ImageBitmapStyleIds { get; set; } = new();

        public SvgFileInfo()
        {
        }

        public SvgFileInfo(string filename, string svgString)
        {
            Filename = filename;
            IconName = filename;
            SvgString = svgString;
        }
    }
}
