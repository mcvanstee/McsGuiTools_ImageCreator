using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using System.Xml.Serialization;

namespace IRL_Image_Creator.Windows.ProjectSettings
{
    [Serializable]
    public class ImportExportSettings
    {
        [XmlArray("")]
        public List<TextStyle> TextStyles { get; set; } = new();

        [XmlArray("")]
        public List<FontBitmapStyle> FontBitmapStyles { get; set; } = new();

        [XmlArray("")]
        public List<IconStyle> IconStyles { get; set; } = new();

        [XmlArray("")]
        public Property[] Properties { get; set; } = Array.Empty<Property>();

        [XmlArray("")]
        public List<ConverterColor> ConverterColors { get; set; } = new();

        [XmlArray("")]
        public List<ConverterFont> ConverterFonts { get; set; } = new();

        public ImportExportSettings() { }

        public static void ExportSettings(ImportExportSettings settings, string filePath)
        {
            using FileStream stream = new(filePath, FileMode.Create, FileAccess.Write);
            XmlSerializer serializer = new(settings.GetType());
            serializer.Serialize(stream, settings);
            stream.Close();
        }

        public static ImportExportSettings ImportSettings(string filePath)
        {
            ImportExportSettings settings = new();

            if (File.Exists(filePath))
            {
                using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read);
                XmlSerializer serializer = new(settings.GetType());
                settings = (ImportExportSettings)serializer.Deserialize(stream);
            }

            return settings;
        }
    }
}
