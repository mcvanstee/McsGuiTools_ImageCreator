using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using System.IO;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration.Utils
{
    public static class CodeGenegrationUtils
    {
        public static void AddCopyRight(StreamWriter sw)
        {
            sw.WriteLine("/*");
            sw.WriteLine(" *");
            sw.WriteLine(" * @par COPYRIGHT NOTICE:");
            sw.WriteLine(" * Copyright (c) 2024, Marijn van Stee, all rights reserved.");
            sw.WriteLine(" *");
            sw.WriteLine(" */");
            sw.WriteLine("");
        }

        public static void AddHeaderGuardBegin(StreamWriter sw, string fileName)
        {
            string guard = fileName.ToUpper() + "_H_";

            sw.WriteLine("#ifndef " + guard);
            sw.WriteLine("#define " + guard);
            sw.WriteLine("");
        }

        public static void AddHeaderGuardEnd(StreamWriter sw, string fileName)
        {
            string guard = fileName.ToUpper() + "_H_";

            sw.WriteLine("#endif /*" + guard + "*/");
        }

        public static void IncludeStdInt(StreamWriter sw)
        {
            sw.WriteLine("#include <stdint.h>");
        }

        public static void IncludeStdBool(StreamWriter sw)
        {
            sw.WriteLine("#include <stdbool.h>");
        }

        public static void IncludeStdLib(StreamWriter sw)
        {
            sw.WriteLine("#include <stdlib.h>");
        }

        public static void Include(StreamWriter sw, string fileName)
        {
            sw.WriteLine("#include \"" + fileName + ".h\"");
        }

        public static void AddExternCBegin(StreamWriter sw)
        {
            sw.WriteLine("#ifdef __cplusplus");
            sw.WriteLine("extern \"C\" {");
            sw.WriteLine("#endif");
        }

        public static void AddExternCEnd(StreamWriter sw)
        {
            sw.WriteLine("#ifdef __cplusplus");
            sw.WriteLine("}");
            sw.WriteLine("#endif /*__cplusplus*/");
        }

        public static void BlankLine(StreamWriter sw)
        {
            sw.WriteLine("");
        }

        public static void EndOfFile(StreamWriter sw)
        {
            sw.WriteLine("/*** end of file ***/");
        }

        public static void Define(StreamWriter sw, string key, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                sw.WriteLine("#define " + key.ToUpper());
            }
            else
            {
                sw.WriteLine("#define " + key.ToUpper() + " " + value);
            }
        }

        public static void DefineIfNotDefined(StreamWriter sw, string key, string value)
        {
            sw.WriteLine("#ifndef " + key);
            Define(sw, key, value);
            sw.WriteLine("#endif");
        }

        public static void AddFileSearchResultEnum(StreamWriter sw)
        {
            sw.WriteLine("typedef enum");
            sw.WriteLine("{");
            sw.WriteLine("    FILE_SEARCH_OK = 0,");
            sw.WriteLine("    FILE_SEARCH_OUT_OF_BOUNDS = 1,");
            sw.WriteLine("    FILE_SEARCH_PROPERTY_LENGTH = 2,");
            sw.WriteLine("    FILE_SEARCH_PROPERTY_NOT_FOUND = 3,");
            sw.WriteLine("    FILE_SEARCH_DUMMY_NO_DATA = 4,");
            sw.WriteLine("    FILE_SEARCH_ERROR_READING_DATA = 5,");
            sw.WriteLine("} file_search_result_e;");
        }

        public static void AddVersion(StreamWriter sw, ImageBuilderSettings settings)
        {
            Define(sw, "FS_IMAGE_FILE_VERSION_MAJOR ", settings.VersionMajor.ToString() + "u");
            Define(sw, "FS_IMAGE_FILE_VERSION_MINOR ", settings.VersionMinor.ToString() + "u");
            Define(sw, "FS_IMAGE_FILE_VERSION_PATCH ", settings.VersionPatch.ToString() + "u");
            Define(sw, "FS_IMAGE_FILE_VERSION_REVISION ", settings.VersionRevision.ToString() + "u");
        }
    }
}
