namespace IRL_Common_Library.Utils
{
    public static class FileUtils
    {
        public static string RemoveExtension(string filename)
        {
            int indexOfDot = filename.LastIndexOf(".");
            if (indexOfDot != -1)
            {
                return filename.Remove(indexOfDot, 4);
            }

            return filename;
        }

        public static string CreateUniqeFileName(string path, string fileName, string extension)
        {
            string fullFilePath = Path.Combine(path, fileName + extension);

            if (!File.Exists(fullFilePath))
            {
                return fullFilePath;
            }

            int number = 1;
            while (true)
            {
                fullFilePath = Path.Combine(path, $"{fileName}({number}){extension}");

                if (!File.Exists(fullFilePath))
                {
                    return fullFilePath;
                }

                number++;
            }
        }

        public static bool IsValidFileName(string filename)
        {
            char[] charDot = new char[] { '.' };
            char[] invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).ToArray();
            invalidChars = invalidChars.Concat(charDot).ToArray();

            return filename == string.Join("", filename.Split(invalidChars));
        }

        public static bool CanImportFile(string extension)
        {
            return (extension == ".bmp" || extension == ".jpg" || extension == ".png");
        }

        public static void CopyFile(string source, string destination)
        {
            try
            {
                File.Replace(source, destination, null);
            }
            catch (FileNotFoundException)
            {
                File.Copy(source, destination);
            }
        }

        public static bool ClearDirectory(string directoryPath)
        {
            DirectoryInfo directoryInfo = new(directoryPath);

            foreach (FileInfo file in directoryInfo.EnumerateFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo dir in directoryInfo.EnumerateDirectories())
            {
                dir.Delete(true);
            }

            return IsDirectoryEmpty(directoryPath);
        }

        public static bool IsDirectoryEmpty(string directoryPath)
        {
            DirectoryInfo directoryInfo = new(directoryPath);

            return directoryInfo.GetFileSystemInfos().Length == 0;
        }
    }
}
