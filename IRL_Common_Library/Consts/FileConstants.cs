namespace IRL_Common_Library.Consts
{
    public static class FileConstants
    {
        // Folders
        //
        private const string LOG_FOLDER = "log";
        private const string IMPORT_FOLDER = "imports";
        private const string BMP_IMPORT_FOLDER = "bmps";
        private const string FONT_IMPORT_FOLDER = "fonts";
        private const string CONVERTER_OUTPUT_FOLDER = "_bmp_tool";
        private const string BUILD_FOLDER = "build";
        private const string SOURCE_FOLDER = "source";
        private const string EXTERNAL_DISPLAY_FOLDER = "external_display";

        public static string GetLogFolder() => Path.Combine(Environment.CurrentDirectory, LOG_FOLDER);
        public static string GetImportFolder() => Path.Combine(Environment.CurrentDirectory, IMPORT_FOLDER);
        public static string GetBmpImportFolder() => Path.Combine(GetImportFolder(), BMP_IMPORT_FOLDER);
        public static string GetFontImportFolder() => Path.Combine(GetImportFolder(), FONT_IMPORT_FOLDER);
        public static string GetConverterOutputFolder() => Path.Combine(GetBmpImportFolder(), CONVERTER_OUTPUT_FOLDER);
        public static string GetBuildFolder() => Path.Combine(Environment.CurrentDirectory, BUILD_FOLDER);
        public static string GetSourceFolder() => Path.Combine(GetBuildFolder(), SOURCE_FOLDER);
        public static string GetExternalDisplayFolder() => Path.Combine(GetBuildFolder(), EXTERNAL_DISPLAY_FOLDER);


        // Files
        //
        public const string LOG_FILE = "log.txt";
        public const string FILENAME_CRC_FILE = "filename_crc.txt";

        public const string GUI_IMAGE_FILE = "gui_image";
        public const string GUI_SEARCH_TREE_FILE = "gui_search_tree";
        public const string DEFAULT_GUI_PIXEL_DATA_FILE = "gui_pixeldata";
        public const string EXTERNAL_DISPLAY_FILE = "external_dp_pixeldata";
        public const string IMAGE_DEBUG_FILE = "image_debug.txt";


        // C Files
        //
        public const string VERSION_FILE = "fs_version";
        public const string SEARCH_TREE_FILE = "fs_file_search";
        public const string FILE_SYSTEM_FILE = "file_system";
        public const string CRC_FILE = "fs_crc32";
        public const string CHAR_INFO_SEARCH_FILE = "fs_font_search";
        public const string BITMAP_DATA_FILE = "fs_bitmap_data";
        public const string COLOR_FILE = "fs_colors";
        public const string PIXEL_DATA_RLE_A_FILE = "fs_pixeldata_rle_a";
        public const string PIXEL_DATA_RLE_FILE = "fs_pixeldata_rle";


        // C Files crclib
        ///
        public const string CHECKSUM_FILE = "fs_checksum";


        // File extensions
        //
        public const string PROJECT_FILE_EXTENSION = ".giprj";
        public const string IMAGE_FILE_EXTENSION = ".bin";
    }
}
