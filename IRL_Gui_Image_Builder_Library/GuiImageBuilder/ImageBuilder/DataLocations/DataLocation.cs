using System.Xml.Serialization;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations
{
    public enum CompressionType
    {
        None,
        RLE,
        RLE_Alpha
    }

    public enum DataLocationType
    {
        Code,    // Data is embedded in code (e.g., as a byte array)
        File_1,  // Data is stored in an external file (e.g., .bin)
        File_2,   
        File_3,   
        File_4,
        File_5
    }

    [Serializable]
    public class DataLocation
    {
        [XmlElement]
        public int LocationID { get; set; } = 0;

        [XmlElement]
        public string Name { get; set; } = "";

        [XmlElement]
        public DataLocationType DataLocationType { get; set; } = DataLocationType.File_1;

        [XmlElement]
        public CompressionType CompressionType { get; set; } = CompressionType.None;

        public DataLocation()
        {
        }

        public const string DATA_LOCATION_PREFIX = "__DL_";
        public const int DATA_LOCATION_MAX_LOCATIONS = 8;

        public DataLocation(int locationId)
        {
            LocationID = locationId;
        }

        public DataLocation(int locationID, DataLocationType dataLocationType, CompressionType compressionType)
        {
            LocationID = locationID;
            DataLocationType = dataLocationType;
            CompressionType = compressionType;
        }

        public static DataLocation? GetDataLocation(int locationId, List<DataLocation> dataLocations)
        {
            return dataLocations.FirstOrDefault(dl => dl.LocationID == locationId);
        }

        public static int GetNextAvailableId(List<DataLocation> dataLocations)
        {
            int locationId = 0;

            while (dataLocations.Any(dl => dl.LocationID == locationId))
            {
                locationId++;
            }

            return locationId;
        }

        public static int GetDataLocationFromFolderName(string folderName)
        {
            if (folderName.Contains(DATA_LOCATION_PREFIX))
            {
                int idIndex = folderName.IndexOf(DATA_LOCATION_PREFIX) + DATA_LOCATION_PREFIX.Length;
                string idStr = folderName.Substring(idIndex);

                if (int.TryParse(idStr, out int id))
                {
                    return id;
                }
            }

            return -1;
        }

        public static string RemoveDataLocationFromFolderName(string folderName)
        {
            if (folderName.Contains(DATA_LOCATION_PREFIX))
            {
                int idIndex = folderName.IndexOf(DATA_LOCATION_PREFIX);

                return folderName.Substring(0, idIndex);
            }

            return folderName;
        }
    }
}
