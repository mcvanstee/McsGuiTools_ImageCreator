namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    public class PropertyValue
    {
        public string Name { get; set; } = string.Empty;
        public string Alias { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public PropertyValue()
        {

        }

        public PropertyValue(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
