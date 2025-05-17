using System.Drawing;
using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions
{
    [Serializable]
    public class FontBitmapStyle
    {
        [XmlElement]
        public int ID { get; set; }

        [XmlElement]
        public string Name { get; set; } = "";

        [XmlElement]
        public BitmapMargin Margin { get; set; } = new();

        [XmlElement]
        public bool MonospaceNumbers { get; set; }

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

        public FontBitmapStyle() { }

        public FontBitmapStyle(int id)
        {
            ID = id;
        }

        public static FontBitmapStyle GetFontStyleById(int id, List<FontBitmapStyle> fontStyles)
        {
            return fontStyles.FirstOrDefault(x => x.ID == id);
        }

        public static int GetNextAvailableId(List<FontBitmapStyle> fontStyles)
        {
            int id = 0;

            while (fontStyles.Any(x => x.ID == id))
            {
                id++;
            }

            return id;
        }
    }
}
