using System.Diagnostics;

namespace IRL_Image_Creator_Tools.TextImports
{
    public class CsvTextImport
    {
        private readonly string m_FullFilePath;
        private int m_NumberOfColumns = -1;

        public CsvTextImport(string fullFilePath)
        {
            m_FullFilePath = fullFilePath;
        }

        public string[] GetColumnHeaders()
        {
            string[] header = Array.Empty<string>();

            try
            {
                using StreamReader reader = new(m_FullFilePath);
                string? line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    header = line.Split(';');
                }       

                reader.Close();
            }
            catch
            {
                Debug.WriteLine("Error opening input file: " + m_FullFilePath);
            }

            return header;
        }

        public List<string> ReadColumn(int column = 0, int startRow = 0)
        {
            int maxColumns = GetNumberOfColumns();
            var stringsInColumn = new List<string>();

            if ((maxColumns <= 0) && (column >= maxColumns))
            {
                return stringsInColumn;
            }

            try
            {
                using StreamReader reader = new(m_FullFilePath);
                int currentRow = 0;

                while (true)
                {
                    string? line = reader.ReadLine();

                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    else
                    {
                        if (currentRow >= startRow)
                        {
                            if (maxColumns == 1)
                            {
                                stringsInColumn.Add(line);
                            }
                            else
                            {
                                string text = line;

                                string[] splitStr = text.Split(';');
                                stringsInColumn.Add(splitStr[column]);
                            }
                        }
                    }

                    currentRow++;
                }

                reader.Close();
            }
            catch
            {
                Debug.WriteLine("Error opening input file: " + m_FullFilePath);
            }

            return stringsInColumn;
        }

        private int GetNumberOfColumns()
        {
            int columns = -1;

            try
            {
                using StreamReader reader = new(m_FullFilePath);

                string? line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    columns = line.Count(c => c == ';') + 1;
                }

                reader.Close();
            }
            catch
            {
                Debug.WriteLine("Error opening input file: " + m_FullFilePath);
            }

            return columns;
        }

        public int NumberOfColumns
        {
            get
            {
                if (m_NumberOfColumns < 0)
                {
                    m_NumberOfColumns = GetNumberOfColumns();
                }

                return m_NumberOfColumns;
            }
        }
    }
}
