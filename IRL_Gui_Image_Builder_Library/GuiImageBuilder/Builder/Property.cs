namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    public class Property
    {
        public bool Use { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public int Max { get; set; }
        public List<PropertyValue> PropertyValues { get; set; } = new();

        public Property()
        {

        }

        public Property(string name)
        {
            Name = name;
        }
    }
}
