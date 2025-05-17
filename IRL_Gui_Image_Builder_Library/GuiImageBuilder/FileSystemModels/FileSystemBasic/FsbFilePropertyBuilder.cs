using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic
{
    public static class FsbFilePropertyBuilder
    {
        public static bool SortAllFilePropertys(ImageBuilderSettings builderSettings, List<FsbFileInfo> fsbFileInfos, BuilderStatusUpdater statusUpdater)
        {
            if (!builderSettings.UseProperties)
            {
                return true;
            }

            statusUpdater.UpdateStatus("Pre process file properties");

            bool result = true;
            List<char> upperChars = GetUpperChars();

            foreach (FsbFileInfo fsbFileInfo in fsbFileInfos)
            {
                if (HasFileProperties(fsbFileInfo.FileKey))
                {
                    ReplaceFilePropertyCharacter(fsbFileInfo);

                    if (!SortFileProperties(fsbFileInfo, upperChars))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        public static void AddAllFileProperties(ImageBuilderSettings builderSettings, List<FsbFileInfo> fsbFileInfos, BuilderStatusUpdater statusUpdater)
        {
            if (builderSettings.UseProperties)
            {
                statusUpdater.UpdateStatus("Add properties");

                foreach (FsbFileInfo fileinfo in fsbFileInfos)
                {
                    AddProperties(builderSettings, fileinfo);
                }
            }
        }

        private static void AddProperties(ImageBuilderSettings builderSettings, FsbFileInfo fileinfo)
        {
            if (!fileinfo.FileKey.Contains("__"))
            {
                fileinfo.HasFileProperties = false;

                return;
            }

            int indexProperties = fileinfo.FileKey.IndexOf("__");
            fileinfo.FileNameWithoutProperties = fileinfo.FileKey.Remove(indexProperties, fileinfo.FileKey.Length - indexProperties);
            fileinfo.HasFileProperties = true;

            string propertiesFilename = fileinfo.FileKey.Remove(0, fileinfo.FileKey.LastIndexOf("__") + 1);

            int propertyIndex = 0;
            foreach (Property property in builderSettings.Properties)
            {
                if (property.Use)
                {
                    int propertyNameIndex = propertiesFilename.IndexOf("_" + property.Name);

                    if (propertyNameIndex >= 0)
                    {
                        propertyNameIndex += 1;
                        int propertyValueIndex = propertyNameIndex + 1;

                        string propertyName = propertiesFilename[propertyNameIndex].ToString();
                        string propertyValue = GetPropertyValue(propertiesFilename, propertyValueIndex);

                        FsbFileProperty fileProperty = new(propertyName, property.Alias, propertyValue, propertyIndex);
                        fileinfo.FsbFileProperties.Add(fileProperty);
                    }
                }

                propertyIndex += 1;
            }
        }

        private static string GetPropertyValue(string fileName, int propertyValueIndex)
        {
            string value = fileName.Substring(propertyValueIndex, 3);

            return value;
        }

        private static bool SortFileProperties(FsbFileInfo fsbFileInfo, List<char> upperChars)
        {
            bool result = true;

            string newFileKey = fsbFileInfo.FileKey.Substring(0, fsbFileInfo.FileKey.IndexOf("__") + 1);
            string propertiesFilename = fsbFileInfo.FileKey.Remove(0, fsbFileInfo.FileKey.LastIndexOf("__") + "__".Length);

            foreach (char c in upperChars)
            {
                if (propertiesFilename.Contains(c))
                {
                    int numberOfSameProperties = propertiesFilename.Split(c).Length - 1;
                    if (numberOfSameProperties > 1)
                    {
                        Log.Error("File " + fsbFileInfo.Filename + " has property " + c + " defined multiple times");

                        result = false;
                    }
                    else
                    {
                        bool addLeadingZerosResult = AddLeadingZerosToPropertyValue(propertiesFilename, ref newFileKey, c);
                        if (!addLeadingZerosResult)
                        {
                            result = false;
                        }
                    }
                }
            }

            fsbFileInfo.FileKey = newFileKey;

            return result;
        }

        private static bool AddLeadingZerosToPropertyValue(string propertiesFilename, ref string newFileKey, char c)
        {
            bool result = true;
            int keyPosition = propertiesFilename.IndexOf(c);

            if (keyPosition > -1)
            {
                string valueString = "";

                if (!char.IsDigit(propertiesFilename[keyPosition + 1]))
                {
                    Log.Error("Unvalid property value: " + propertiesFilename);

                    return false;
                }

                for (int j = keyPosition + 1; j < propertiesFilename.Length; j++)
                {
                    char number = propertiesFilename[j];
                    if (char.IsDigit(number))
                    {
                        valueString += number;
                    }
                    else
                    {
                        break;
                    }
                }

                if (valueString.Length > 0)
                {
                    int value = int.Parse(valueString);

                    if (value >= 0 && value < 1000)
                    {
                        string newPropertyValue = "_" + c + value.ToString().PadLeft(3, '0');
                        newFileKey += newPropertyValue;
                    }
                    else
                    {
                        Log.Error("Property value must be 0 - 999");

                        result = false;
                    }
                }
            }

            return result;
        }

        private static List<char> GetUpperChars()
        {
            List<char> upperChars = new();

            for (int i = 65; i <= 90; i++)
            {
                upperChars.Add((char)i);
            }

            return upperChars;
        }

        private static bool HasFileProperties(string fileKey)
        {
            if (fileKey.Contains("__") ||
                fileKey.Contains("@") ||
                fileKey.Contains("#") ||
                fileKey.Contains("$"))
            {
                return true;
            }

            return false;
        }

        private static void ReplaceFilePropertyCharacter(FsbFileInfo fsbFileInfo)
        {
            if (fsbFileInfo.FileKey.Contains('@'))
            {
                fsbFileInfo.FileKey = fsbFileInfo.FileKey.Replace("@", "__");
            }

            if (fsbFileInfo.FileKey.Contains('#'))
            {
                fsbFileInfo.FileKey = fsbFileInfo.FileKey.Replace("#", "__");
            }

            if (fsbFileInfo.FileKey.Contains('$'))
            {
                fsbFileInfo.FileKey = fsbFileInfo.FileKey.Replace("$", "__");
            }

            if (fsbFileInfo.FileKey.Contains("___"))
            {
                fsbFileInfo.FileKey = fsbFileInfo.FileKey.Replace("___", "__");
            }
        }
    }
}
