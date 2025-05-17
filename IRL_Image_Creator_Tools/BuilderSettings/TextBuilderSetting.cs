using IRL_Image_Creator_Tools.Enums;
using System.Xml.Serialization;

namespace IRL_Image_Creator_Tools.BuilderSettings
{
    [Serializable]
    public class TextBuilderSetting
    {
        [XmlElement]
        public string FullFilePath { get; set; } = "";

        [XmlElement]
        public bool ImportFileHasHeader { get; set; }

        [XmlElement]
        public bool ImportFileFirstColumnIsKey { get; set; }

        [XmlElement]
        public BitmapMargin Padding { get; set; } = new();

        [XmlElement]
        public HeaderNameOutputEnum HeaderNameOutputEnum { get; set; } = HeaderNameOutputEnum.None;

        [XmlElement]
        public BitmapProperty ColumnProperty { get; set; } = new();

        public TextBuilderSetting() { }

        public TextBuilderSetting(string fullFilePath)
        {
            FullFilePath = fullFilePath;
        }

        //public TextBuilderSetting(
        //    string fullFilePath, List<string> columnNames, bool importFileHasHeader, bool importFileFirstColumnIsKey)
        //{
        //    FullFilePath = fullFilePath;
        //    //OutputFolder = outputFolder;
        //    ColumnNames = columnNames;
        //    ImportFileHasHeader = importFileHasHeader;
        //    ImportFileFirstColumnIsKey = importFileFirstColumnIsKey;
        //}
    }
}
