using System.Xml.Serialization;
using IRL_Common_Library.Consts;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    [Serializable]
    public class ImageBuilderSettings
    {
        private const int NoMaxProperties = 16;

        [XmlElement]
        public uint VersionMajor { get; set; } = 1;

        [XmlElement]
        public uint VersionMinor { get; set; }

        [XmlElement]
        public uint VersionPatch { get; set; }

        [XmlElement]
        public uint VersionRevision { get; set; }

        [XmlElement]
        public string GuiPixelDataFile { get; set; } = FileConstants.DefaultGuiPixelDataFile;

        [XmlElement]
        public bool CreateDebugFiles { get; set; }

        [XmlElement]
        public bool CopySourceFiles { get; set; }

        [XmlElement]
        public FileSystemFormat FileSystemFormat { get; set; } = new();

        [XmlElement]
        public PixelDataFormat PixelDataFormat { get; set; } = new();

        [XmlElement]
        public bool FontDataInImage { get; set; }

        [XmlElement]
        public bool UseProperties { get; set; }

        [XmlArray("")]
        public Property[] Properties { get; set; } = new Property[NoMaxProperties];

        public ImageBuilderSettings()
        {
            string propertyNames = "ABCDEFGHIJKLMNOP";

            for (int i = 0; i < NoMaxProperties; i++)
            {
                Properties[i] = new Property(propertyNames.Substring(i, 1));
                Properties[i].Use = false;
                Properties[i].Alias = propertyNames.Substring(i, 1);
                Properties[i].Max = 2;

                Properties[i].PropertyValues.Add(new PropertyValue("0", "0"));
                Properties[i].PropertyValues.Add(new PropertyValue("1", "1"));
            }
        }

        public int PropertiesUsed
        {
            get
            {
                int properiesUsed = 0;

                foreach (Property property in Properties)
                {
                    if (property.Use)
                    {
                        properiesUsed++;
                    }
                }

                return properiesUsed;
            }
        }

        public void IncrementRevision()
        {
            VersionRevision++;
        }

        public string GetVerion()
        {
            return VersionMajor + "." + VersionMinor + "." + VersionPatch + "." + VersionRevision;
        }
    }

    public enum FileFormat
    {
        SingleFile,
        BasicImage
    }

    [Serializable]
    public class FileSystemFormat
    {
        [XmlElement]
        public FileFormat FileFormat { get; set; } = FileFormat.BasicImage;

        [XmlElement]
        public bool SingleFileIncludeWidthHeight { get; set; }

        [XmlElement]
        public bool SeparateSearchTreeFromData { get; set; } = true;
    }

    public enum PixelFormat
    {
        RGB,
        RGB565
    }

    public enum PixelFormatRGB
    {
        RGB,
        BGR
    }

    [Serializable]
    public class PixelDataFormat
    {
        [XmlElement]
        public PixelFormat PixelFormat { get; set; } = PixelFormat.RGB565;

        [XmlElement]
        public PixelFormatRGB PixelFormatRGB { get; set; } = PixelFormatRGB.RGB;

        [XmlElement]
        public bool RGB565SwapBytes { get; set; }
    }
}
