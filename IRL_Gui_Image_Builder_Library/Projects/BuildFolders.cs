using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;

namespace IRL_Gui_Image_Builder_Library.Projects
{
    public static class BuildFolders
    {
        public static void ClearBuildFolder()
        {
            FileUtils.ClearDirectory(FileConstants.GetBuildFolder());
            Directory.CreateDirectory(FileConstants.GetSourceFolder());
            FileUtils.ClearDirectory(FileConstants.GetSourceFolder());
            Directory.CreateDirectory(FileConstants.GetExternalDisplayFolder());
            FileUtils.ClearDirectory(FileConstants.GetExternalDisplayFolder());
        }

        public static void ClearLogFolder()
        {
            FileUtils.ClearDirectory(FileConstants.GetLogFolder());
        }
    }
}
