using IRL_Common_Library.Utils;
using IRL_Image_Creator_Tools.BuilderSettings;
using IRL_Image_Creator_Tools.Enums;
using IRL_Image_Creator_Tools.TextImports;
using IRL_String_To_Bitmap_Converter;
using System.Diagnostics;
using System.Drawing;

namespace IRL_Image_Creator_Tools.Builders
{
    public class TextToBitmapConverter
    {
        private string m_OutputFolder = "";
        private readonly TextBuilderSetting m_Settings;
        private List<string> m_columnNames { get; set; } = new List<string>();

        public TextToBitmapConverter(TextBuilderSetting settings)
        {
            m_Settings = settings;
        }

        public bool ConvertTextFileToBitmaps(BitmapStyle bitmapStyle, string outputFolder)
        {
            if (!File.Exists(m_Settings.FullFilePath))
            {
                return false;
            }

            SetOutputFolder(outputFolder);

            if (Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            CsvTextImport csvTextImport = new(m_Settings.FullFilePath);

            int numberOfColumns = csvTextImport.NumberOfColumns;

            if (m_Settings.ImportFileHasHeader)
            {
                m_columnNames = new List<string>(csvTextImport.GetColumnHeaders());
            }

            List<string> fileKeys = new();

            for (int column = 0; column < numberOfColumns; column++)
            {
                string path = CreatePath(column, numberOfColumns);

                int startRow = m_Settings.ImportFileHasHeader ? 1 : 0;
                List<string> strings = csvTextImport.ReadColumn(column, startRow);

                if (column == 0)
                {
                    fileKeys = GetFileNameKeys(strings);
                }

                if (fileKeys.Count != strings.Count)
                {
                    Debug.WriteLine("Error rows not equal");

                    return false;
                }

                if (!(m_Settings.ImportFileFirstColumnIsKey && column == 0))
                {
                    List<BitmapProperty> properties = new();
                    m_Settings.ColumnProperty.Value = column;
                    properties.Add(m_Settings.ColumnProperty);
                    properties.AddRange(bitmapStyle.BitmapProperties);

                    for (int i = 0; i < fileKeys.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(fileKeys[i]) && !string.IsNullOrEmpty(strings[i]))
                        {
                            string filename = CreateFileNameWithProperties(fileKeys[i], properties);

                            Font font = new(bitmapStyle.FontName, bitmapStyle.FontSize, bitmapStyle.FontStyle);

                            StringToBitmapConverter.GenerateImage(
                                strings[i],
                                font, bitmapStyle.TextColor, bitmapStyle.BackColor,
                                path, filename);
                        }
                        else
                        {
                            Debug.WriteLine("Can not add empty file");
                        }
                    }
                }
            }

            return true;
        }

        private static List<string> GetFileNameKeys(List<string> strings)
        {
            List<string> keys = new();

            foreach (string str in strings)
            {
                keys.Add(CreateFileNameKey(str));
            }

            return keys;
        }

        private static string CreateFileNameKey(string fileName)
        {
            string fileNameKey = fileName.Replace(' ', '_').ToUpper();

            return FileUtils.MakeFileNameValid(fileNameKey);
        }

        private static string CreateFileNameWithProperties(string fileName, List<BitmapProperty> properties)
        {
            string newFileName = fileName;

            bool firstPropertyAdded = false;
            foreach (BitmapProperty property in properties)
            {
                if (!string.IsNullOrEmpty(property.Name))
                {
                    if (firstPropertyAdded)
                    {
                        newFileName = $"{newFileName}_{property.Name}{property.Value}";
                    }
                    else
                    {
                        newFileName = $"{newFileName}@{property.Name}{property.Value}";
                        firstPropertyAdded = true;
                    }
                }
            }

            return newFileName;
        }

        private string CreatePath(int column, int numberOfColumns)
        {
            if (m_Settings.HeaderNameOutputEnum == HeaderNameOutputEnum.None)
            {
                return m_OutputFolder;
            }

            string addToFileKeyPreFix = string.Empty;
            if (m_Settings.HeaderNameOutputEnum == HeaderNameOutputEnum.CreateFolderAddNotToFileKey)
            {
                addToFileKeyPreFix = "_";
            }

            string columnName;
            if (m_columnNames.Count == numberOfColumns)
            {
                columnName = m_columnNames.ElementAt(column);
            }
            else
            {
                columnName = $"Col{column}";
            }

            string path = $"{m_OutputFolder}\\{addToFileKeyPreFix}{columnName}";

            Directory.CreateDirectory(path);

            return path;
        }

        private void SetOutputFolder(string outputFolder)
        {
            if (string.IsNullOrEmpty(outputFolder))
            {
                FileInfo importFileInfo = new(m_Settings.FullFilePath);
                m_OutputFolder = $"{outputFolder}\\{importFileInfo.Name.Replace(importFileInfo.Extension, "")}";
            }
        }
    }
}
