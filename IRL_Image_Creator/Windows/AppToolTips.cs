namespace IRL_Image_Creator.Windows
{
    public static class AppToolTips
    {
        // Tooltips for the MainForm controls
        //
        public const string MainWindow_ImageFileRadioButton = "Create a basic image file with the bitmap data stored in a pixeldata file.\n Generate source files for search bitmap and font info.";
        public const string MainWindow_IncludeFileInfoInImageCheckbox = "Create a single file with the bitmap data and search tree stored in the same file.\nGenerate source files for search bitmap and font info.";
        public const string MainWindow_SingleFileRadioButton = "Create a single pixeldata file for each bitmap";
        public const string MainWindow_CompressedImageRadioButton = "Create a compressed image file with the bitmap data stored in a pixeldata file.\nGenerate source files for search bitmap and font info.";
        public const string MainWindow_SelectUserFolderButton = "Select the folder where the source files will be copied to.\n" +
                "This is only used if 'Copy source files' is checked.";

    }
}
