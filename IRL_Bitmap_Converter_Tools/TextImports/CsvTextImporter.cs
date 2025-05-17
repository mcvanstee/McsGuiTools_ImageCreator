using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Common_Library.Utils;
using System.Text;

namespace IRL_Bitmap_Converter_Tools.TextImports
{
    public static class CsvTextImporter
    {
        public static bool StartImport(TextTable table, string fullFilePath, bool headerInFirstRow, bool overWrite)
        {
            int numberOfRows = GetNumberOfRows(fullFilePath);
            int numberOfColumns = GetNumberOfColumns(fullFilePath);

            if (numberOfColumns < 2)
            {
                Log.WriteLine("Error: Impordata columns: " + fullFilePath);

                return false;
            }

            table.NumberOfColumns = numberOfColumns;

            int startRow = 0;

            if (headerInFirstRow)
            {
                AddColumnHeader(fullFilePath, table.Header);
                startRow = 1;
            }

            bool importSuccess = AddText(table.Records, numberOfRows, startRow, fullFilePath, overWrite);
            if (!importSuccess)
            {
                return false;
            }

            bool columnError = false;
            foreach (TextRecord record in table.Records)
            {
                if (record.Text.Count != table.NumberOfColumns - 1)
                {
                    columnError = true;
                }

                if (record.Text.Count > table.NumberOfColumns - 1)
                {
                    table.NumberOfColumns = record.Text.Count;
                }
            }

            if (columnError)
            {
                Log.WriteLine("Error: Column count not equal in: " + fullFilePath);             
            }

            return !columnError;
        }

        private static bool AddText(List<TextRecord> records, int numberOfRows, int startRow, string fullFilePath, bool overWrite)
        {
            bool result = true;

            for (int i = startRow; i < numberOfRows; i++)
            {
                List<string> row = GetRow(fullFilePath, i);

                string key = row[0];
                bool keyExists = (KeyExist(key, records));

                if (keyExists && overWrite)
                {
                    TextRecord? record = GetRecord(key, records);

                    if (record != null)
                    {
                        OverWriteRecord(record, row, true);
                    }
                }
                else if (keyExists)
                {
                    Log.WriteLine("Key already exists: " + key);
                    result = false;
                }
                else
                {
                    AddNewRecord(records, key, row, true);
                }
            }

            return result;
        }

        private static void AddNewRecord(List<TextRecord> records, string key, List<string> row, bool firstColumnIsKey = true)
        {
            TextRecord textRecord = new();
            textRecord.Key = key;

            int start = firstColumnIsKey ? 1 : 0;
            for (int i = start; i < row.Count; i++)
            {
                textRecord.Text.Add(row[i]);
            }

            records.Add(textRecord);
        }

        private static void OverWriteRecord(TextRecord record, List<string> row, bool firstColumnIsKey = true)
        {
            record.Text.Clear();
            int start = firstColumnIsKey ? 1 : 0;

            for (int i = start; i < row.Count; i++)
            {
                record.Text.Add(row[i]);
            }
        }

        private static bool KeyExist(string key, List<TextRecord> records)
        {
            foreach (TextRecord record in records)
            {
                if (record.Key == key)
                {
                    return true;
                }
            }

            return false;
        }

        private static TextRecord? GetRecord(string key, List<TextRecord> records)
        {
            foreach (TextRecord record in records)
            {
                if (record.Key == key)
                {
                    return record;
                }
            }

            return null;
        }

        private static void AddColumnHeader(string fullFilePath, TextRecord header)
        {
            string[] headerRow = Array.Empty<string>();

            try
            {
                using StreamReader reader = new(fullFilePath);
                string? line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    headerRow = line.Split(';');
                }

                reader.Close();
            }
            catch
            {
                Log.WriteLine("Error opening input file: " + fullFilePath);
            }

            header.Text.Clear();
            header.Text.AddRange(headerRow);
        }

        public static int GetNumberOfRows(string fullFilePath)
        {
            int rows = 0;
            try
            {
                using StreamReader reader = new(fullFilePath);
                while (true)
                {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrEmpty(line))
                    {
                        break;
                    }
                    rows++;
                }
                reader.Close();
            }
            catch
            {
                Log.WriteLine("Error opening input file: " + fullFilePath);
            }

            return rows;
        }

        private static List<string> GetRow(string fullFilePath, int row = 0)
        {
            var stringsInRow = new List<string>();
            try
            {
                using StreamReader reader = new(fullFilePath, Encoding.Default);
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
                        if (currentRow == row)
                        {
                            string text = line;
                            string[] splitStr = text.Split(';');
                            stringsInRow.AddRange(splitStr);
                            break;
                        }
                    }
                    currentRow++;
                }
                reader.Close();
            }
            catch
            {
                Log.WriteLine("Error opening input file: " + fullFilePath);
            }
            return stringsInRow;
        }

        public static int GetNumberOfColumns(string fullFilePath)
        {
            int columns = -1;

            try
            {
                using StreamReader reader = new(fullFilePath);

                string? line = reader.ReadLine();

                if (!string.IsNullOrEmpty(line))
                {
                    columns = line.Count(c => c == ';') + 1;
                }

                reader.Close();
            }
            catch
            {
                Log.WriteLine("Error opening input file: " + fullFilePath);
            }

            return columns;
        }
    }
}
