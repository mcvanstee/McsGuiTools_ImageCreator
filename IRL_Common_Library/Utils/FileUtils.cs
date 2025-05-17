using System.Text.RegularExpressions;

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
            string fullFilePath = $"{path}\\{fileName}{extension}";

            if (!File.Exists(fullFilePath))
            {
                return fullFilePath;
            }

            int number = 1;
            while (true)
            {
                fullFilePath = $"{path}\\{fileName}({number}){extension}";

                if (!File.Exists(fullFilePath))
                {
                    return fullFilePath;
                }

                number++;
            }
        }

        public static string GetTopFolder(string path)
        {
            string result = path;
            int filenameIndex = path.LastIndexOf("\\");

            if (path[path.Length - 4] == '.')
            {
                result = path.Remove(filenameIndex, path.Length - filenameIndex);
            }

            int folderIndex = result.LastIndexOf("\\");

            result = result.Remove(0, folderIndex + 1);

            return result;
        }

        public static bool IsValidFileName(string filename)
        {
            char[] charDot = new char[] { '.' };
            char[] invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).ToArray();
            invalidChars = invalidChars.Concat(charDot).ToArray();

            return filename == string.Join("", filename.Split(invalidChars));
        }

        public static bool IsValidFileName(this string expression, bool platformIndependent)
        {
            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }
            return (Regex.IsMatch(expression, sPattern, RegexOptions.CultureInvariant));
        }

        public static string MakeFileNameValid(string filename)
        {
            char[] charDot = { '.' };
            char[] invalidChars = Path.GetInvalidFileNameChars().Concat(Path.GetInvalidPathChars()).ToArray();
            invalidChars = invalidChars.Concat(charDot).ToArray();

            return string.Join("", filename.Split(invalidChars));
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
