using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.Converters;
using IRL_Gui_Image_Builder_Library.Exceptions;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Properties;
using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files
{
    public class FsbBuilder
    {
        private readonly ImageBuilderSettings m_builderSettings;
        private readonly BuilderStatusUpdater m_statusUpdater;
        private readonly List<FsbFileInfo> m_files = [];
        public List<DataLocation> DataLocations { get; private set; } = [];

        public readonly int SizeOfFileInfo;
        public uint CRC { get; set; }
        public bool FileSystemBuilt { get; set; } = false;

        public List<FsbFileInfo> FileInfos => m_files;


        public int FileInfoSize
        {
            get
            {
                return m_files.Count * SizeOfFileInfo;
            }
        }

        public FsbBuilder(ImageBuilderSettings builderSettings, BuilderStatusUpdater statusUpdater)
        {
            m_builderSettings = builderSettings;
            m_statusUpdater = statusUpdater;

            if (m_builderSettings.PropertiesUsed == 0)
            {
                SizeOfFileInfo = 8;
            }
            else if (m_builderSettings.PropertiesUsed <= 8)
            {
                SizeOfFileInfo = 9;
            }
            else
            {
                SizeOfFileInfo = 10;
            }
        }

        public bool BuildFileSystem()
        {
            DirectoryInfo directoryInfoRoot = new(FileConstants.GetBmpImportFolder());
            AddAllFiles(directoryInfoRoot);
            CreateFileKeyNames();
            ProcessDataLocations();
            FileSystemBuilt = false;

            if (m_files.Count == 0)
            {
                return FileSystemBuilt;
            }

            bool sortFilePropertiesOK = FsbFilePropertyBuilder.SortAllFilePropertys(m_builderSettings, FileInfos, m_statusUpdater);
            if (!sortFilePropertiesOK)
            {
                return FileSystemBuilt;
            }

            bool hasDuplicateFilenames = HasDuplicateFilenames();

            if (hasDuplicateFilenames)
            {
                throw new ImageBuilderException("Duplicate filenames found in the import folder! Please check the log for more details.");             
            }

            //SortFileInfos();

            FsbFilePropertyBuilder.AddAllFileProperties(m_builderSettings, m_files, m_statusUpdater);

            bool filePropertiesOK = CheckFileProperties();
            if (!filePropertiesOK)
            {
                throw new ImageBuilderException("File properties check failed! Please check the log for more details.");
            }

            SortFileInfos();
            WriteFileNamesToLogFile();

            bool hasDuplicateFileKeys = HasDuplicateFileKeys();
            if (hasDuplicateFileKeys)
            {
                throw new ImageBuilderException("Duplicate filekeys found in the import folder! Please check the log for more details.");
            }

            for (int i = 0; i < m_files.Count; i++)
            {
                m_files[i].FileIndex = i;
            }

            FileSystemBuilt = true;

            return FileSystemBuilt;
        }

        private void SortFileInfos()
        {
            List<FsbFileInfo> sortedFiles = [];

            foreach (DataLocation dataLocation in DataLocations)
            {
                List<FsbFileInfo> filesForDataLocation = m_files.Where(f => f.DataLocation.LocationID == dataLocation.LocationID).ToList();
                filesForDataLocation.Sort((x, y) => x.FileKey.CompareTo(y.FileKey));
                sortedFiles.AddRange(filesForDataLocation);
            }

            m_files.Clear();
            m_files.AddRange(sortedFiles);
        }

        private void AddAllFiles(DirectoryInfo directoryInfo)
        {
            m_statusUpdater.UpdateStatus("Add all Bitmap files");
            DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories();

            AddFilesToFileInfoList(directoryInfo);

            foreach (DirectoryInfo dirInfo in directoryInfos)
            {
                AddAllFiles(dirInfo);
            }
        }

        public void AddPixelData(PixelData pixelData)
        {
            foreach (FsbFileInfo fsbFileInfo in m_files)
            {
                ushort properties = 0;

                foreach (FsbFileProperty fsbFileProperty in fsbFileInfo.FsbFileProperties)
                {
                    properties |= (ushort)(1u << fsbFileProperty.Index);
                }

                if (fsbFileInfo.DataLocation.LocationID == pixelData.DataLocation.LocationID)
                {
                    AddPixelData(pixelData, fsbFileInfo, properties);
                }

                if (fsbFileInfo.IsDummy)
                {
                    fsbFileInfo.FsbFile.UpdateValues(0xFFFFFFFF, properties, 0, 0);
                }
            }
        }

        private void AddPixelData(PixelData pixelData, FsbFileInfo fsbFileInfo, ushort properties)
        {
            CompressionType compression = pixelData.DataLocation.CompressionType;

            if (compression == CompressionType.None && fsbFileInfo.DataLocation.CompressionType == compression)
            {
                AddPixelDataCompressionNone(pixelData, fsbFileInfo, properties);
            }
            else if (compression == CompressionType.RLE && fsbFileInfo.DataLocation.CompressionType == compression)
            {
                AddPixelDataCompressionRLE(pixelData, fsbFileInfo, properties);
            }
            else if (compression == CompressionType.RLE_Alpha && fsbFileInfo.DataLocation.CompressionType == compression)
            {
                AddPixelDataCompressionRLEAlpha(pixelData, fsbFileInfo, properties);
            }
            else
            {
            }
        }

        private void AddPixelDataCompressionNone(PixelData pixelData, FsbFileInfo fsbFileInfo, ushort properties)
        {
            m_statusUpdater.UpdateStatusAndFilesConverted("Converting pixeldata: " + fsbFileInfo.Filename, 1);

            using Bitmap bitmap = new(fsbFileInfo.FilePath, true);
            int writeIndex = pixelData.DataLength;

            byte[] convertedPixelData = PixelDataConverter.GetConvertedPixelData(bitmap, m_builderSettings.PixelDataFormat);
            pixelData.AppendData(convertedPixelData, fsbFileInfo);

            fsbFileInfo.FsbFile.UpdateValues(
                (uint)(writeIndex), properties, (ushort)bitmap.Width, (ushort)bitmap.Height);
        }

        private void AddPixelDataCompressionRLE(PixelData pixelData, FsbFileInfo fsbFileInfo, ushort properties)
        {
            m_statusUpdater.UpdateStatusAndFilesConverted("Converting pixeldata: " + fsbFileInfo.Filename, 1);

            using Bitmap bitmap = new(fsbFileInfo.FilePath, true);
            int writeIndex = pixelData.DataLength;

            byte[] convertedPixelData = PixelDataConverter.GetPixelData_RLE(bitmap, m_builderSettings.PixelDataFormat);
            pixelData.AppendData(convertedPixelData, fsbFileInfo);

            fsbFileInfo.FsbFile.UpdateValues(
                (uint)(writeIndex), properties, (ushort)bitmap.Width, (ushort)bitmap.Height);

            int bytesPerPixel = m_builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? 3 : 2;
            int uncompressedBytes = bitmap.Width * bitmap.Height * bytesPerPixel;
            int compressedBytes = convertedPixelData.Length;
            double compressionRatio = (double)uncompressedBytes / compressedBytes;
            Log.Verbose($"RLE Compression: {fsbFileInfo.Filename} Uncompressed bytes: {uncompressedBytes}" +
                $" Compressed bytes: {compressedBytes} Bytes saved: {uncompressedBytes - compressedBytes} Compression ratio: {compressionRatio:F2}");  
        
            Log.WritePixelDataRLE(convertedPixelData);       
        }

        private void AddPixelDataCompressionRLEAlpha(PixelData pixelData, FsbFileInfo fsbFileInfo, ushort properties)
        {
            m_statusUpdater.UpdateStatusAndFilesConverted("Converting pixeldata: " + fsbFileInfo.Filename, 1);

            using Bitmap bitmap = new(fsbFileInfo.FilePath, true);
            int writeIndex = pixelData.DataLength;

            byte[] convertedPixelData = PixelDataConverter.GetPixelData_RLE_Alpha(bitmap);
            pixelData.AppendData(convertedPixelData, fsbFileInfo);

            fsbFileInfo.FsbFile.UpdateValues(
                (uint)(writeIndex), properties, (ushort)bitmap.Width, (ushort)bitmap.Height);

            int bytesPerPixel = m_builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? 3 : 2;
            int uncompressedBytes = bitmap.Width * bitmap.Height * bytesPerPixel;
            int compressedBytes = convertedPixelData.Length;
            double compressionRatio = (double)uncompressedBytes / compressedBytes;
            Log.Verbose($"RLE Alpha Compression: {fsbFileInfo.Filename} Uncompressed bytes: {uncompressedBytes}" +
                $" Compressed bytes: {compressedBytes} Bytes saved: {uncompressedBytes - compressedBytes} Compression ratio: {compressionRatio:F2}");
        }

        public void AddExternalPixelData(ref byte[] pixelData, uint offset)
        {
            foreach (FsbFileInfo fileInfo in m_files)
            {
                if (fileInfo.IsDummy)
                {
                    fileInfo.FsbFile.ExternalDisplayDataOffset = 0xFFFFFFFF;

                    continue; 
                }

                CompressionType compression = fileInfo.DataLocation.CompressionType;
                using Bitmap bitmap = new(fileInfo.FilePath, true);
                byte[] convertedPixelData = [];

                if (compression == CompressionType.RLE_Alpha)
                {
                    convertedPixelData = PixelDataConverter.GetPixelData_RLE_Alpha(bitmap);
                }
                else
                {
                    PixelDataFormat pixelDataFormat = new()
                    {
                        PixelFormat = PixelFormat.RGB,
                        RGB565SwapBytes = false,
                        PixelFormatRGB = PixelFormatRGB.RGB
                    };

                    convertedPixelData = PixelDataConverter.GetPixelData_RLE(bitmap, pixelDataFormat);
                }

                int currentLength = pixelData.Length;
                Array.Resize(ref pixelData, currentLength + convertedPixelData.Length);
                Array.Copy(convertedPixelData, 0, pixelData, currentLength, convertedPixelData.Length);

                fileInfo.FsbFile.ExternalDisplayDataOffset = offset;
                offset += (uint)convertedPixelData.Length;
            }
        }

        private void AddFilesToFileInfoList(DirectoryInfo directoryInfo)
        {
            FileInfo[] fileInfos = directoryInfo.GetFiles();

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (FileUtils.CanImportFile(fileInfo.Extension))
                {
                    string filename = FileUtils.RemoveExtension(fileInfo.Name);
                    int dataLocationId = GetDataLocationIdFromFileName(fileInfo.FullName);
                    FsbFileInfo fsbFileInfo = new(filename, fileInfo.FullName, dataLocationId);
                    AddFolders(fsbFileInfo, fileInfo);

                    m_files.Add(fsbFileInfo);
                }
            }
        }

        private static int GetDataLocationIdFromFileName(string filename)
        {
            int locationId = -1;

            if (filename.Contains(DataLocation.DATA_LOCATION_PREFIX))
            {
                string locationIdStr = filename.Substring(filename.IndexOf(DataLocation.DATA_LOCATION_PREFIX) + DataLocation.DATA_LOCATION_PREFIX.Length, 1);
                
                if (int.TryParse(locationIdStr, out int parsedLocationId))
                {
                    locationId = parsedLocationId;
                }
            }

            return locationId;
        }

        private static void AddFolders(FsbFileInfo fsbFileInfo, FileInfo sourceFileInfo)
        {
            string? directoryName = sourceFileInfo.DirectoryName;
            string? bmpDirectory = directoryName?.Remove(0, directoryName.LastIndexOf("\\bmps") + 5);

            if (string.IsNullOrEmpty(bmpDirectory))
            {
                return;
            }

            while (!string.IsNullOrEmpty(bmpDirectory))
            {
                if (bmpDirectory.StartsWith('\\'))
                {
                    bmpDirectory = bmpDirectory.Remove(0, 1);
                }

                int index = bmpDirectory.IndexOf('\\'); //LastIndexOf('\\');

                if (index > 0)
                {
                    fsbFileInfo.Folders.Add(bmpDirectory.Substring(0, index));
                    bmpDirectory = bmpDirectory.Remove(0, index);
                }
                else
                {
                    fsbFileInfo.Folders.Add(bmpDirectory);
                    bmpDirectory = "";
                }
            }
        }

        private void CreateFileKeyNames()
        {
            foreach (FsbFileInfo fsbFileInfo in FileInfos)
            {
                string key = "";

                if (fsbFileInfo.Folders.Count == 0)
                {
                    key += fsbFileInfo.Filename.ToUpper();
                }
                else
                {
                    foreach (string folder in fsbFileInfo.Folders)
                    {
                        if (!folder.StartsWith("_"))
                        {
                            string folderKeyName = DataLocation.RemoveDataLocationFromFolderName(folder).ToUpper();
                            key += folderKeyName;
                            key += "_";
                        }
                    }

                    key += fsbFileInfo.Filename.ToUpper();
                }

                key = key.Replace('-', '_');
                key = key.Replace(' ', '_');

                fsbFileInfo.FileKey = key;
            }
        }

        private void ProcessDataLocations()
        {
            foreach (FsbFileInfo fileInfo in FileInfos)
            {
                DataLocation? dataLocationFromSettings = m_builderSettings.DataLocations.FirstOrDefault(dl => dl.LocationID == fileInfo.DataLocation.LocationID);

                if (dataLocationFromSettings == null)
                { 
                    Log.Warning("Data location not found in settings for file: " + fileInfo.Filename + ", using default data location");

                    dataLocationFromSettings = m_builderSettings.DataLocations.FirstOrDefault(dl => dl.LocationID == 0);

                    if (dataLocationFromSettings == null) 
                    { 
                        Log.Error("Default data location with ID 0 not found in settings!"); 
                        throw new ImageBuilderException("Default data location with ID 0 not found in settings!"); 
                    }
                }

                fileInfo.DataLocation = dataLocationFromSettings;
                AddDataLocation(fileInfo.DataLocation);
            }

            // Sort datalocations on location id, to ensure the same order as in the settings
            DataLocations = DataLocations.OrderBy(dl => dl.LocationID).ToList();
        }

        private void AddDataLocation(DataLocation dataLocation)
        {
            bool exists = false;
            foreach (DataLocation dl in DataLocations)
            {
                if (dl.LocationID == dataLocation.LocationID)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                DataLocations.Add(dataLocation);
            }
        }

        private bool CheckFileProperties()
        {
            bool result = true;

            if (m_builderSettings.UseProperties)
            {
                List<string> fileKeys = new();

                foreach (FsbFileInfo fileinfo in FileInfos)
                {
                    if (fileinfo.HasFileProperties)
                    {
                        fileKeys.Add(fileinfo.FileNameWithoutProperties);
                    }
                }

                fileKeys = fileKeys.Distinct().ToList();

                foreach (string key in fileKeys)
                {
                    bool filePropertyOK = CheckFileProperty(key);

                    if (!filePropertyOK)
                    {
                        result = false;

                        break;
                    }
                }
            }

            return result;
        }

        private bool CheckFileProperty(string fileName)
        {
            List<FsbFileInfo> fsbFileInfos = new();

            // Add all fileinfos to the list
            //
            foreach (FsbFileInfo fileinfo in FileInfos)
            {
                if (fileName == fileinfo.FileNameWithoutProperties)
                {
                    fsbFileInfos.Add(fileinfo);
                }
            }

            FsbFileInfo firstFileInfo;

            // Select the first fileinfo
            //
            if (fsbFileInfos[0].FsbFileProperties.Count > 0)
            {
                firstFileInfo = fsbFileInfos[0];
            }
            else
            {
                Log.Error("No files with selected fileproperties found!");

                return false;
            }

            // Check if all properties count are equal.
            //
            foreach (FsbFileInfo fsbFileInfo in fsbFileInfos)
            {
                if (fsbFileInfo.FsbFileProperties.Count != firstFileInfo.FsbFileProperties.Count)
                {
                    Log.Error("Fileproperties, different number of properties: " + firstFileInfo.Filename);

                    return false;
                }
            }

            // Check if all properties are equal on all fileinfos with the same name.
            //
            foreach (FsbFileInfo fsbFileInfo in fsbFileInfos)
            {
                foreach (FsbFileProperty fileProperty in fsbFileInfo.FsbFileProperties)
                {
                    int index = fileProperty.Index;

                    bool hasAllFileProperties = false;

                    foreach (FsbFileProperty property in firstFileInfo.FsbFileProperties)
                    {
                        if (property.Index == index)
                        {
                            hasAllFileProperties = true;

                            break;
                        }
                    }

                    if (!hasAllFileProperties)
                    {
                        Log.Error("Fileproperties, not the same: " + fsbFileInfo.Filename);
                        Log.Error($"Hint: Check if all files with the same name have the same fileproperties, and the same number of fileproperties. File Key: {fileName}");

                        return false;
                    }
                }
            }

            return CheckPropertyValues(fsbFileInfos);
        }

        private bool CheckPropertyValues(List<FsbFileInfo> fsbFileInfos)
        {
            FsbFileInfo rootFileInfo = fsbFileInfos[0];

            List<int> maxNoOfValues = new();
            List<int> activeProperty = new();
            List<Property> propertiesInFileInfos = new();

            foreach (FsbFileProperty fp in rootFileInfo.FsbFileProperties)
            {
                foreach (Property property in m_builderSettings.Properties)
                {
                    if (property.Name == fp.Name)
                    {
                        propertiesInFileInfos.Add(property);
                        maxNoOfValues.Add(property.Max);
                        activeProperty.Add(0);
                    }
                }
            }

            int expectedNoOfFileInfos = 1;
            foreach (int maxValue in maxNoOfValues)
            {
                expectedNoOfFileInfos *= maxValue;
            }

            // Check if files with properties are missing, and insert dummy's
            //
            if (expectedNoOfFileInfos > fsbFileInfos.Count)
            {
                Log.Warning("Files with properties are missing, adding dummy files");

                List<List<PropertyKeys>> propertyKeys = new();
                for (int i = 0; i < expectedNoOfFileInfos; i++)
                {
                    List<PropertyKeys> keys = new();

                    for (int j = 0; j < propertiesInFileInfos.Count; j++)
                    {
                        Property property = propertiesInFileInfos[j];

                        keys.Add(new PropertyKeys(property.Name, property.Alias, activeProperty[j], j));
                    }

                    AddOneToValue(activeProperty, maxNoOfValues);
                    propertyKeys.Add(keys);
                }

                for (int i = 0; i < propertyKeys.Count; i++)
                {
                    if (!IsPropertyValueInFileInfos(propertyKeys[i], fsbFileInfos))
                    {
                        FsbFileInfo dummy = new("", "", -1);
                        string newFileKey = rootFileInfo.FileNameWithoutProperties + "_";

                        foreach (PropertyKeys key in propertyKeys[i])
                        {
                            string keyStr = "_" + key.Name + key.Value.ToString("000");
                            newFileKey += keyStr;

                            FsbFileProperty newFileProperty = new(
                                key.Name, key.Alias, key.Value.ToString(), key.Index);

                            dummy.FsbFileProperties.Add(newFileProperty);
                        }

                        dummy.IsDummy = true;
                        dummy.FileNameWithoutProperties = rootFileInfo.FileNameWithoutProperties;
                        dummy.FileKey = newFileKey;
                        dummy.HasFileProperties = true;

                        FileInfos.Add(dummy);

                        Log.Warning("Add Dummy file: " + newFileKey);
                    }
                }

                m_files.Sort((x, y) => x.FileKey.CompareTo(y.FileKey));
            }
            else if (expectedNoOfFileInfos < fsbFileInfos.Count)
            {
                Log.Error("Properties not defined. Some property values are missing.");

                return false;
            }
            else
            {
            }

            return true;
        }

        private static void AddOneToValue(List<int> values, List<int> maxValues)
        {
            int lastIndex = values.Count - 1;

            for (int i = lastIndex; i >= 0; i--)
            {
                values[i] += 1;

                if (values[i] >= maxValues[i])
                {
                    values[i] = 0;
                }
                else
                {
                    break;
                }
            }
        }

        private static bool IsPropertyValueInFileInfos(List<PropertyKeys> propertyKeys, List<FsbFileInfo> fsbFileInfos)
        {
            for (int i = 0; i < fsbFileInfos.Count; i++)
            {
                FsbFileInfo fsbFileInfo = fsbFileInfos[i];
                bool found = true;

                for (int j = 0; j < fsbFileInfo.FsbFileProperties.Count; j++)
                {
                    FsbFileProperty fsbFileProperty = fsbFileInfo.FsbFileProperties[j];

                    if (fsbFileProperty.Name == propertyKeys[j].Name && fsbFileProperty.Value == propertyKeys[j].Value)
                    {
                        found &= true;
                    }
                    else
                    {
                        found &= false;
                    }
                }

                if (found)
                {
                    return true;
                }
            }

            return false;
        }

        private class PropertyKeys
        {
            public string Name { get; set; }
            public string Alias { get; set; }
            public int Value { get; set; }
            public int Index { get; set; }

            public PropertyKeys(string name, string alias, int value, int index)
            {
                Name = name;
                Alias = alias;
                Value = value;
                Index = index;
            }
        }

        private void WriteFileNamesToLogFile()
        {
            if (!m_builderSettings.LogVerbose)
            {
                return;
            }

            foreach (FsbFileInfo fsbFileInfo in m_files)
            {
                if (fsbFileInfo.IsDummy)
                {
                    Log.Verbose("Missing, Dummy file inserted " + fsbFileInfo.FileKey);
                }
                else
                {
                    Log.Verbose("File: " + fsbFileInfo.Filename);
                }
            }
        }

        private bool HasDuplicateFilenames()
        {
            bool result = false;
            List<string> filenames = new();

            foreach (FsbFileInfo fileInfo in FileInfos)
            {
                filenames.Add(fileInfo.FileKey);
            }

            List<string> uniqueFilenames = filenames.Distinct().ToList();

            if (filenames.Count != uniqueFilenames.Count)
            {
                result = true;
                Log.Error("Duplicate filenames");

                var duplicates = filenames.GroupBy(a => a).SelectMany(ab => ab.Skip(1).Take(1)).ToList();
                foreach (string filename in duplicates)
                {
                    Log.Error("Duplicate: " + filename);
                }
            }

            return result;
        }

        private bool HasDuplicateFileKeys()
        {
            bool result = false;

            List<string> fileKeys = new();

            foreach (FsbFileInfo fileInfo in FileInfos)
            {
                if (fileInfo.HasFileProperties)
                {
                    if (fileInfo.IsRootFileProperty)
                    {
                        string fileNameWithoutProperties = fileInfo.FileKey.Remove(fileInfo.FileKey.IndexOf("__"), fileInfo.FileKey.Length - fileInfo.FileKey.LastIndexOf("__"));
                        fileKeys.Add(fileNameWithoutProperties);
                    }
                }
                else
                {
                    fileKeys.Add(fileInfo.FileKey);
                }
            }

            List<string> uniqueFileKeys = fileKeys.Distinct().ToList();

            if (fileKeys.Count != uniqueFileKeys.Count)
            {
                result = true;
                Log.Error("Duplicate fileKeys");

                var duplicates = fileKeys.GroupBy(a => a).SelectMany(ab => ab.Skip(1).Take(1)).ToList();
                foreach (string fileKey in duplicates)
                {
                    Log.Error("Duplicate: " + fileKey);
                }
            }

            return result;
        }
    }
}

