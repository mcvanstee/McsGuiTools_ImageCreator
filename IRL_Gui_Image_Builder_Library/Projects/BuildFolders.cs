using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;

namespace IRL_Gui_Image_Builder_Library.Projects
{
    public static class BuildFolders
    {
        public static string LogFolderPath(string projectPath) => projectPath + FileConstants.LogFolder;
        public static string BuildFolderPath(string projectPath) => projectPath + FileConstants.BuildFolder;
        public static string BmpImputFolderPath(string projectPath) => projectPath + FileConstants.BmpImportFolder;
        public static string FontInputFolderPath(string projectPath) => projectPath + FileConstants.FontImportFolder;
        public static string SourceFolderPath(string projectPath) => projectPath + FileConstants.SourceFolder;

        public static void ClearBuildFolder(string projectPath)
        {
            FileUtils.ClearDirectory(projectPath + FileConstants.BuildFolder);
            Directory.CreateDirectory(projectPath + FileConstants.SourceFolder);
            FileUtils.ClearDirectory(projectPath + FileConstants.SourceFolder);
        }

        public static void ClearLogFolder(string projectPath)
        {
            FileUtils.ClearDirectory(projectPath + FileConstants.LogFolder);
        }

        private static void CreateFolders(string projectPath)
        {
            //Directory.CreateDirectory(projectPath + FileConstants.LogFolder);
            //Directory.CreateDirectory(projectPath + FileConstants.BmpImportFolder);
            //Directory.CreateDirectory(projectPath + FileConstants.FontImportFolder);
            Directory.CreateDirectory(projectPath + FileConstants.BuildFolder);
            Directory.CreateDirectory(projectPath + FileConstants.SourceFolder);
            //Directory.CreateDirectory(path + FileConstants.ExternalDisplayFolder);
        }
    }
}
