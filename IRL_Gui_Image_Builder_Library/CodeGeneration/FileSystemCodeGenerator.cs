using IRL_Gui_Image_Builder_Library.CodeGeneration.PixelDataFiles;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;

namespace IRL_Gui_Image_Builder_Library.CodeGeneration
{
    public static class FileSystemCodeGenerator
    {
        public static void CreateCodeFiles(
            ImageBuilderSettings builderSettings, FsbBuilder fsbBuilder, List<PixelData> pixelDatas)
        {
            if (!fsbBuilder.FileSystemBuilt)
            {
                return;
            }

            if (builderSettings.UseProperties)
            {
                FSPropertiesHeaderFileCodeGenerator.CreateFileKeyHeader(builderSettings, fsbBuilder);
                FSPropertiesCFileCodeGenerator.CreateFileSystemCFile(builderSettings, fsbBuilder);
            }
            else
            {
                FSHeaderFileCodeGenerator.CreateFileKeyHeader(builderSettings, fsbBuilder);
                FSCFileCodeGenerator.CreateFileSystemCFile(builderSettings, fsbBuilder);
            }

            foreach (PixelData pixelData in pixelDatas)
            {
                if ((pixelData.DataLocation.DataLocationType == DataLocationType.Code) &&
                    (pixelData.DataLocation.CompressionType == CompressionType.RLE_Alpha))
                {
                    PixelDataHeader.CreatePixelDataHeader(builderSettings, fsbBuilder);
                    PixelDataCode.CreatePixelDataCode(builderSettings, pixelDatas);

                    break;
                }
            }

            foreach (PixelData pixelData in pixelDatas)
            {
                if ((pixelData.DataLocation.DataLocationType == DataLocationType.Code) &&
                    (pixelData.DataLocation.CompressionType == CompressionType.RLE))
                {
                    PixelDataRleHeader.CreatePixelDataHeader(builderSettings);
                    PixelDataRleCode.CreatePixelDataCode(builderSettings, pixelDatas);

                    break;
                }
            }
        }
    }
}
