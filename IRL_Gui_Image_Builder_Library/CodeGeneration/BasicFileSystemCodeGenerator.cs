using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class BasicFileSystemCodeGenerator
    {
        public static void CreateBasicFileSystemFiles(
            ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder, List<FSColor> fsColors)
        {
            if (builderSettings.UseProperties)
            {
                FSPropertiesHeaderFileCodeGenerator.CreateFileKeyHeader(builderSettings, projectPath, fsbBuilder);
                FSPropertiesCFileCodeGenerator.CreateFileSystemCFile(builderSettings, projectPath, fsbBuilder);
            }
            else
            {
                FSHeaderFileCodeGenerator.CreateFileKeyHeader(builderSettings, projectPath, fsbBuilder);
                FSCFileCodeGenerator.CreateFileSystemCFile(builderSettings, projectPath, fsbBuilder);
            }

            FSColorHeaderGenerator.CreateColorHeader(fsColors, projectPath);
            VersionHeaderGenerator.CreateVersionHeader(builderSettings, projectPath);
            CrcHeaderGenerator.CreateCrcHeader(projectPath);
            CrcCodeGenerator.CreateCrcCode(projectPath);
        }
    }
}
