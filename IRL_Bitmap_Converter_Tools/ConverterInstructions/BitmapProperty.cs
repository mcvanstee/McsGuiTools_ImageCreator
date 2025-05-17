using System.Xml.Serialization;

namespace IRL_Bitmap_Converter_Tools.ConverterInstructions
{
    [Serializable]
    public class BitmapProperty
    {
        [XmlElement]
        public string Name { get; set; } = "";

        [XmlElement]
        public string Description { get; set; } = "";

        [XmlElement]
        public int Value { get; set; }

        public BitmapProperty() { }

        public BitmapProperty(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public BitmapProperty(string name, string description, int value)
        {
            Name = name;
            Description = description;
            Value = value;
        }
    }
}
