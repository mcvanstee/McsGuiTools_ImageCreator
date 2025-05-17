using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions
{
    [Serializable]
    public class IconStyle
    {
        [XmlElement]
        public int ID { get; set; }

        [XmlElement]
        public string Name { get; set; } = "";

        [XmlElement]
        public string Prefix { get; set; } = "";

        [XmlElement]
        public BitmapMargin Margin { get; set; } = new();

        [XmlIgnore]
        public Color BackColor { get; set; } = Color.White;

        [XmlElement("BackColor")]
        public int BackColorAsArgb
        {
            get => BackColor.ToArgb();
            set => BackColor = Color.FromArgb(value);
        }

        [XmlElement]
        public int BackConverterColorId { get; set; } = -1;

        public List<BitmapProperty> BitmapProperties { get; set; } = new();

        [XmlArray("")]
        public List<ImageSvgColorId> SvgColors { get; set; } = new();

        public IconStyle() { }

        public IconStyle(int id)
        {
            ID = id;
        }

        public static IconStyle GetImageStyleById(int id, List<IconStyle> iconStyles)
        {
            return iconStyles.FirstOrDefault(x => x.ID == id);
        }

        public static int GetNextAvailableId(List<IconStyle> iconStyles)
        {
            int id = 0;

            while (iconStyles.Any(x => x.ID == id))
            {
                id++;
            }

            return id;
        }
    }
}
