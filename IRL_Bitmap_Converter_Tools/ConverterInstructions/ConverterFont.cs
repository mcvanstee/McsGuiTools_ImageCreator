using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions
{
    [Serializable]
    public class ConverterFont
    {
        [XmlElement]
        public int ID { get; set; }

        [XmlElement]
        public string Name { get; set; } = "";

        [XmlElement]
        public string FontName { get; set; } = "";

        [XmlElement]
        public float FontSize { get; set; }

        [XmlElement]
        public FontStyle FontStyle { get; set; }

        public ConverterFont() { }

        public ConverterFont(int id)
        {
            ID = id;
        }

        public static ConverterFont GetFontById(int id, List<ConverterFont> fonts)
        {
            return fonts.FirstOrDefault(x => x.ID == id);
        }

        public static int GetNextAvailableId(List<ConverterFont> fonts)
        {
            int id = 0;

            while (fonts.Any(x => x.ID == id))
            {
                id++;
            }

            return id;
        }
    }
}
