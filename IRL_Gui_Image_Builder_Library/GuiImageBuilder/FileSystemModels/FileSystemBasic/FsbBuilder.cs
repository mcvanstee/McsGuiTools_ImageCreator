using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.Projects;
using System.Drawing;
using IRL_Common_Library.Consts;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic
{
    public class FsbBuilder
    {
        private readonly string m_projectPath;
        private readonly ImageBuilderSettings m_builderSettings;
        private readonly BuilderStatusUpdater m_statusUpdater;
        private readonly List<FsbFileInfo> m_files = new List<FsbFileInfo>();

        public readonly int SizeOfFileInfo;
        public uint CRC { get; set; }

        public List<FsbFileInfo> FsbFileInfos => m_files;
        public int FileInfoSize
        {
            get
            {
                return m_files.Count * SizeOfFileInfo;
            }
        }

        public byte[] FileInfoSearchData
        {
            get
            {
                byte[] data = new byte[FileInfoSize];
                int writeIndex = 0;

                foreach (FsbFileInfo fileInfo in m_files)
                {
                    FsbFile.GetBytes(fileInfo.FsbFile, SizeOfFileInfo).CopyTo(data, writeIndex);
                    writeIndex += SizeOfFileInfo;
                }

                return data;
            }
        }

        public FsbBuilder(ImageBuilderSettings builderSettings, string projectPath, BuilderStatusUpdater statusUpdater)
        {
            m_projectPath = projectPath;
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
            DirectoryInfo directoryInfoRoot = new(BuildFolders.BmpImputFolderPath(m_projectPath));
            AddAllFiles(directoryInfoRoot);
            CreateFileKeyNames();

            bool sortFilePropertiesOK = FsbFilePropertyBuilder.SortAllFilePropertys(m_builderSettings, FsbFileInfos, m_statusUpdater);
            if (!sortFilePropertiesOK)
            {
                return false;
            }

            bool hasDuplicateFilenames = HasDuplicateFilenames();

            if (m_files.Count == 0 || hasDuplicateFilenames)
            {
                return false;
            }

            m_files.Sort((x, y) => x.FileKey.CompareTo(y.FileKey));

            FsbFilePropertyBuilder.AddAllFileProperties(m_builderSettings, m_files, m_statusUpdater);

            bool filePropertiesOK = CheckFileProperties();
            if (!filePropertiesOK)
            {
                return false;
            }

            WriteFileNamesToDebugFile();

            bool hasDuplicateFileKeys = HasDuplicateFileKeys();
            if (hasDuplicateFileKeys)
            {
                return false;
            }

            for (int i = 0; i < m_files.Count; i++)
            {
                m_files[i].FileIndex = i;
            }

            return true;
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

        public void ConvertAllFilesPixelData(ref byte[] pixelData, int pixelDataOffset, ref byte[] externalDisplayPixelData)
        {
            foreach (FsbFileInfo fsbFileInfo in m_files)
            {
                ushort properties = 0;

                foreach (FsbFileProperty fsbFileProperty in fsbFileInfo.FsbFileProperties)
                {
                    properties |= (ushort)(1u << fsbFileProperty.Index);
                }

                if (fsbFileInfo.IsDummy)
                {
                    FsbFile file = fsbFileInfo.FsbFile;
                    file.DataOffset = 0xFFFFFFFF;
                    file.Properties = properties;
                    file.Width = 0;
                    file.Height = 0;
                }
                else
                {
                    m_statusUpdater.UpdateStatusAndFilesConverted("Converting pixeldata: " + fsbFileInfo.Filename, 1);

                    using Bitmap bitmap = new(fsbFileInfo.FilePath, true);

                    int writeIndex = pixelData.Length;

                    byte[] convertedPixelData = PixelDataConverter.GetConvertedPixelData_1(bitmap, m_builderSettings.PixelDataFormat);
                    ArrayUtils.AppendToArray(ref pixelData, convertedPixelData);

                    AddExternalDisplayPixelData(ref externalDisplayPixelData, bitmap);

                    FsbFile file = fsbFileInfo.FsbFile;
                    file.DataOffset = (uint)(writeIndex + pixelDataOffset);
                    file.Properties = properties;
                    file.Width = (ushort)bitmap.Width;
                    file.Height = (ushort)bitmap.Height;

                    bitmap.Dispose();
                }
            }
        }

        private static void AddExternalDisplayPixelData(ref byte[] externalDisplayPixelData, Bitmap bitmap)
        {
            PixelDataFormat externDixplayFormat = new();
            externDixplayFormat.PixelFormat = PixelFormat.RGB;
            byte[] bitmapPixelData = PixelDataConverter.GetConvertedPixelData_1(bitmap, externDixplayFormat);
            ArrayUtils.AppendToArray(ref externalDisplayPixelData, bitmapPixelData);
        }

        private void AddFilesToFileInfoList(DirectoryInfo directoryInfo)
        {
            FileInfo[] fileInfos = directoryInfo.GetFiles();

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (FileUtils.CanImportFile(fileInfo.Extension))
                {
                    string filename = FileUtils.RemoveExtension(fileInfo.Name);
                    FsbFileInfo fsbFileInfo = new(filename, fileInfo.FullName);
                    AddFolders(fsbFileInfo, fileInfo);

                    m_files.Add(fsbFileInfo);
                }
            }
        }

        private static void AddFolders(FsbFileInfo fsbFileInfo, FileInfo sourceFileInfo)
        {
            string directoryName = sourceFileInfo.DirectoryName;
            string bmpDirectory = directoryName.Remove(0, directoryName.LastIndexOf("\\bmps") + 5);  //"\\BMP"

            if (string.IsNullOrEmpty(bmpDirectory))
            {
                return;
            }

            while (!string.IsNullOrEmpty(bmpDirectory))
            {
                if (bmpDirectory.StartsWith("\\"))
                {
                    bmpDirectory = bmpDirectory.Remove(0, 1);

                }

                int index = bmpDirectory.LastIndexOf("\\");

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
            foreach (FsbFileInfo fsbFileInfo in FsbFileInfos)
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
                            key += folder.ToUpper();
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

        //private void AddAllFileProperties()
        //{
        //    if (m_builderSettings.UseProperties)
        //    {
        //        foreach (FsbFileInfo fileinfo in FsbFileInfos)
        //        {
        //            AddProperties(fileinfo);
        //        }
        //    }
        //}

        //private void AddProperties(FsbFileInfo fileinfo)
        //{
        //    if (!fileinfo.FileKey.Contains("__"))
        //    {
        //        fileinfo.HasFileProperties = false;

        //        return;
        //    }

        //    int indexProperties = fileinfo.FileKey.IndexOf("__");
        //    fileinfo.FileNameWithoutProperties = fileinfo.FileKey.Remove(indexProperties, fileinfo.FileKey.Length - indexProperties);
        //    fileinfo.HasFileProperties = true;

        //    string propertiesFilename = fileinfo.FileKey.Remove(0, (fileinfo.FileKey.LastIndexOf("__") + 1));

        //    int propertyIndex = 0;
        //    foreach (Property property in m_builderSettings.Properties)
        //    {
        //        if (property.Use)
        //        {
        //            int propertyNameIndex = propertiesFilename.IndexOf("_" + property.Name);

        //            if (propertyNameIndex >= 0)
        //            {
        //                propertyNameIndex += 1;
        //                int propertyValueIndex = propertyNameIndex + 1;

        //                string propertyName = propertiesFilename[propertyNameIndex].ToString();
        //                string propertyValue = GetPropertyValue(propertiesFilename, propertyValueIndex);

        //                FsbFileProperty fileProperty = new FsbFileProperty(propertyName, property.Alias, propertyValue, propertyIndex);
        //                fileinfo.FsbFileProperties.Add(fileProperty);
        //            }
        //        }

        //        propertyIndex += 1;
        //    }
        //}

        //private static string GetPropertyValue(string fileName, int propertyValueIndex) 
        //{
        //    string value = fileName.Substring(propertyValueIndex, 3);

        //    return value;
        //}

        private bool CheckFileProperties()
        {
            bool result = true;

            if (m_builderSettings.UseProperties)
            {
                List<string> fileKeys = new();

                foreach (FsbFileInfo fileinfo in FsbFileInfos)
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
            foreach (FsbFileInfo fileinfo in FsbFileInfos)
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
                        FsbFileInfo dummy = new("", "");
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

                        FsbFileInfos.Add(dummy);

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

        private void WriteFileNamesToDebugFile()
        {
            if (!m_builderSettings.CreateDebugFiles)
            {
                return;
            }

            StreamWriter sw = new(BuildFolders.LogFolderPath(m_projectPath) + "\\FileNames" + ".txt");

            foreach (FsbFileInfo fsbFileInfo in m_files)
            {
                if (fsbFileInfo.IsDummy)
                {
                    sw.WriteLine("Missing, Dummy file inserted " + fsbFileInfo.FileKey);
                }
                else
                {
                    sw.WriteLine(fsbFileInfo.Filename);
                }
            }

            sw.Close();
        }

        private bool HasDuplicateFilenames()
        {
            bool result = false;
            List<string> filenames = new();

            foreach (FsbFileInfo fileInfo in FsbFileInfos)
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

            foreach (FsbFileInfo fileInfo in FsbFileInfos)
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
