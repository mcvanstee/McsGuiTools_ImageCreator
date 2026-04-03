using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Properties;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.ImageFile
{
    public class ExternalImageBuilder
    {
        private readonly ImageBuilderSettings m_builderSettings;
        private readonly FsbBuilder m_fsbBuilder;
        private readonly FontBuilder m_fontBuilder;
        private byte[] m_pixelData = [];

        public ExternalImageBuilder(
            ImageBuilderSettings builderSettings, FsbBuilder fsbBuilder, FontBuilder fontBuilder)
        {
            m_builderSettings = builderSettings;
            m_fsbBuilder = fsbBuilder;
            m_fontBuilder = fontBuilder;
        }

        public void CreateExternalImageFile()
        {
            Log.WriteLine("Starting creation of external image file...");
            string externalPixelDataFilePath = GetExternalDisplayImageFilePath(m_builderSettings);
            using FileStream externalImageFile = new(externalPixelDataFilePath, FileMode.Create, FileAccess.ReadWrite);

            ImageFileHeader imageFileHeader = new(HeaderType.ExternalDisplay, m_builderSettings, m_fsbBuilder, m_fontBuilder);
            imageFileHeader.DataLocationsSize = GetDataLocationsSize();
            imageFileHeader.WriteFileHeader(externalImageFile, 0);
            Log.WriteLine($"External display image file header written. Offset: {externalImageFile.Position}");
            WriteDataLocations(externalImageFile);
            Log.WriteLine($"Data locations written. Offset: {externalImageFile.Position}");
            WriteProperties(externalImageFile);
            WriteFontInfo(externalImageFile);

            uint fileInfoOffset = (uint)externalImageFile.Position;
            uint charInfoOffset = fileInfoOffset + (uint)(m_fsbBuilder.FileInfos.Count * m_fsbBuilder.SizeOfFileInfo);
            uint pixelDataOffset = charInfoOffset + (uint)m_fontBuilder.CharacterInfoSize;

            if (m_fsbBuilder.FileSystemBuilt)
            {
                m_fsbBuilder.AddExternalPixelData(ref m_pixelData, pixelDataOffset);
                WriteFileInfos(externalImageFile);
                Log.WriteLine($"File infos written. Offset: {externalImageFile.Position}");
            }

            int imagePixelDataSize = m_pixelData.Length;

            if (m_fontBuilder.FontsCreated)
            {
                uint offset = pixelDataOffset + (uint)m_pixelData.Length;
                m_fontBuilder.AddExternalPixelData(ref m_pixelData, offset);
                WriteCharInfos(externalImageFile);
                Log.WriteLine($"Character infos written. Offset: {externalImageFile.Position}");
            }

            Log.WriteLine($"Pixel data Offset: {externalImageFile.Position + imagePixelDataSize}");
            Log.WriteLine("External image file creation completed.");

            externalImageFile.Write(m_pixelData);
            externalImageFile.Close();
        }

        private static string GetExternalDisplayImageFilePath(ImageBuilderSettings builderSettings)
        {
            string version = builderSettings.GetVerion().Replace(".", "_");
            string pixelDataFileName = builderSettings.GuiPixelDataFile + "_" + version;
            string pixelDataFilePath = Path.Combine(FileConstants.GetExternalDisplayFolder(), pixelDataFileName);

            return pixelDataFilePath;
        }

        private void WriteDataLocations(FileStream imageFile)
        {
            int noOfDataLocations = m_builderSettings.DataLocations.Count;
            imageFile.Write(BitConverter.GetBytes(noOfDataLocations));

            foreach (DataLocation dataLocation in m_builderSettings.DataLocations)
            {
                imageFile.Write(BitConverter.GetBytes(dataLocation.LocationID));
                imageFile.Write(BitConverter.GetBytes((int)dataLocation.CompressionType));
            }
        }

        private void WriteProperties(FileStream externalImageFile)
        {
            byte[] bytes = new byte[m_builderSettings.PropertiesUsed];
            int index = 0;

            for (int i = 0; i < m_builderSettings.PropertiesUsed; i++)
            {
                Property property = m_builderSettings.Properties[i];

                if (property.Use)
                {
                    bytes[index++] = (byte)property.Max;
                }
            }

            externalImageFile.Write(bytes);
        }

        private void WriteFontInfo(FileStream imageFile)
        {
            foreach (FsFont font in m_fontBuilder.Fonts)
            {
                imageFile.Write(BitConverter.GetBytes(font.CharacterInfos.Count));
            }
        }

        private void WriteCharInfos(FileStream externalImageFile)
        {
            byte[] charSearchData = new byte[m_fontBuilder.CharacterInfoSize];
            m_fontBuilder.CreateCharInfoSearchData(ref charSearchData);
            externalImageFile.Write(charSearchData);
        }

        private void WriteFileInfos(FileStream externalImageFile)
        {
            foreach (FsbFileInfo fileInfo in m_fsbBuilder.FileInfos)
            {
                externalImageFile.Write(FsbFile.GetExternalDisplayBytes(fileInfo.FsbFile, m_fsbBuilder.SizeOfFileInfo));
            }
        }

        private int GetDataLocationsSize()
        {
            int size = sizeof(int);

            size += m_builderSettings.DataLocations.Count * (sizeof(int) + sizeof(int));

            return size;
        }
    }
}
