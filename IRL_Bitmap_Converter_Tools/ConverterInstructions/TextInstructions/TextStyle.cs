using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions
{
    [Serializable]
    public class TextStyle
    {
        [XmlElement]
        public int ID { get; set; }

        [XmlElement]
        public string Description { get; set; } = "";

        [XmlElement]
        public string Prefix { get; set; } = "";

        [XmlElement]
        public string FontName { get; set; } = "";

        [XmlElement]
        public float FontSize { get; set; }

        [XmlElement]
        public FontStyle FontStyle { get; set; }

        [XmlIgnore]
        public Color TextColor { get; set; } = Color.Black;

        [XmlIgnore]
        public Color BackColor { get; set; } = Color.White;

        [XmlElement("TextColor")]
        public int TextColorAsArgb
        {
            get => TextColor.ToArgb();
            set => TextColor = Color.FromArgb(value);
        }

        [XmlElement("BackColor")]
        public int BackColorAsArgb
        {
            get => BackColor.ToArgb();
            set => BackColor = Color.FromArgb(value);
        }

        [XmlElement]
        public int BackConverterColorId { get; set; } = -1;

        [XmlElement]
        public int TextConverterColorId { get; set; } = -1;

        [XmlElement]
        public int FontConverterId { get; set; } = -1;

        [XmlElement]
        public BitmapMargin Margin { get; set; } = new();

        public List<BitmapProperty> BitmapProperties { get; set; } = new();

        public TextStyle() { }

        public TextStyle(int id)
        {
            ID = id;
        }

        public static TextStyle GetTextStyleById(int id, List<TextStyle> textStyles)
        {
            return textStyles.FirstOrDefault(x => x.ID == id);
        }

        public static int GetNextAvailableId(List<TextStyle> textStyles)
        {
            int id = 0;

            while (textStyles.Any(x => x.ID == id))
            {
                id++;
            }

            return id;
        }
    }
}
