using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions
{
    [Serializable]
    public class ConverterColor
    {
        [XmlElement]
        public int ID { get; set; }

        [XmlElement]
        public string Name { get; set; } = "";

        [XmlIgnore]
        public Color Color { get; set; } = Color.White;

        [XmlElement("Color")]
        public int BackColorAsArgb
        {
            get => Color.ToArgb();
            set => Color = Color.FromArgb(value);
        }

        public ConverterColor() { }

        public ConverterColor(int id)
        {
            ID = id;
        }

        public static ConverterColor GetColorById(int id, List<ConverterColor> colors)
        {
            return colors.FirstOrDefault(x => x.ID == id);
        }

        public static int GetNextAvailableId(List<ConverterColor> colors)
        {
            int id = 0;

            while (colors.Any(x => x.ID == id))
            {
                id++;
            }

            return id;
        }
    }
}
