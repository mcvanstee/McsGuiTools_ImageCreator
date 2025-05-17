namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic
{
    public class FsbFileProperty
    {
        public string Name { get; }
        public string Alias { get; }
        public int Value { get; }
        public int Index { get; }

        public FsbFileProperty(string name, string alias, string value, int index)
        {
            Name = name;
            Alias = alias;
            Index = index;

            try
            {
                int val = int.Parse(value);
                Value = val;
            }
            catch
            {
                Value = -1;
            }
        }
    }
}
