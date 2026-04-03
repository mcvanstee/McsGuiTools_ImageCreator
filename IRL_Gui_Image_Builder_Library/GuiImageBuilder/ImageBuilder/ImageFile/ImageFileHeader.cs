using IRL_Common_Library.CRC;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.ImageFile
{
    public enum HeaderType
    {
        Basic = 0,
        Optimized = 1,
        ExternalDisplay = 3
    }

    public class ImageFileHeader
    {
        public const int IMAGE_FILE_HEADER_SIZE = 50;

        private readonly ImageBuilderSettings m_builderSettings;
        private readonly FontBuilder m_fontBuilder;
        private readonly FsbBuilder m_fsbBuilder;

        public HeaderType HeaderType { get; set; }       // 0 = basic, 1 = optimized, 3 = external display
        public int HeaderSize { get; private set; }      // size of header in bytes, data offset = headerSize
        public uint VersionMajor { get; set; }           // major
        public uint VersionMinor { get; set; }           // minor
        public uint VersionPatch { get; set; }           // patch
        public uint VersionRevision { get; set; }        // revision
        public int PixelDataFormat { get; set; }         // pixel format enum
        public int PixelDataFormatRGB { get; set; }      // RGB format enum
        public int FileInfoSize { get; set; }
        public int CharInfoSize { get; set; }
        public int DataLocationsSize { get; set; }

        public ImageFileHeader(HeaderType headerType, ImageBuilderSettings builderSettings, FsbBuilder fsbBuilder, FontBuilder fontBuilder)
        {
            HeaderType = headerType;
            HeaderSize = IMAGE_FILE_HEADER_SIZE;
            m_builderSettings = builderSettings;
            m_fontBuilder = fontBuilder;
            m_fsbBuilder = fsbBuilder;

            VersionMajor = builderSettings.VersionMajor;
            VersionMinor = builderSettings.VersionMinor;
            VersionPatch = builderSettings.VersionPatch;
            VersionRevision = builderSettings.VersionRevision;
            PixelDataFormat = (int)m_builderSettings.PixelDataFormat.PixelFormat;
            PixelDataFormatRGB = (int)m_builderSettings.PixelDataFormat.PixelFormatRGB;
            FileInfoSize = m_fsbBuilder.FileInfoSize;
            CharInfoSize = m_fontBuilder.CharacterInfoSize;
        }

        public uint WriteFileHeader(FileStream imageFile, uint crc)
        {
            byte[] headerBytes = new byte[HeaderSize];
            headerBytes[0] = (byte)HeaderType;
            headerBytes[1] = (byte)HeaderSize;
            BitConverter.GetBytes(VersionMajor).CopyTo(headerBytes, 2);
            BitConverter.GetBytes(VersionMinor).CopyTo(headerBytes, 6);
            BitConverter.GetBytes(VersionPatch).CopyTo(headerBytes, 10);
            BitConverter.GetBytes(VersionRevision).CopyTo(headerBytes, 14);

            if (HeaderType == HeaderType.Basic)
            {
                BitConverter.GetBytes(PixelDataFormat).CopyTo(headerBytes, 18);
                BitConverter.GetBytes(PixelDataFormatRGB).CopyTo(headerBytes, 22);
                BitConverter.GetBytes(FileInfoSize).CopyTo(headerBytes, 26);
                BitConverter.GetBytes(CharInfoSize).CopyTo(headerBytes, 30);
                
                imageFile.Write(headerBytes);
                crc = CRC32.GetCrc32Accumulate(crc, ref headerBytes, headerBytes.Length);
            }
            else // HeaderType ExternalDisplay
            {
                headerBytes[18] = (byte)m_fsbBuilder.SizeOfFileInfo;
                headerBytes[19] = (byte)CharacterInfo.GetSize();
                BitConverter.GetBytes(FileInfoSize).CopyTo(headerBytes, 20);
                BitConverter.GetBytes(CharInfoSize).CopyTo(headerBytes, 24);
                BitConverter.GetBytes(DataLocationsSize).CopyTo(headerBytes, 28);
                BitConverter.GetBytes(m_fsbBuilder.FileInfos.Count).CopyTo(headerBytes, 32);
                BitConverter.GetBytes(m_fontBuilder.Fonts.Count).CopyTo(headerBytes, 36);
                BitConverter.GetBytes(m_builderSettings.PropertiesUsed).CopyTo(headerBytes, 40);

                imageFile.Write(headerBytes);
            }

            return crc;
        }
    }
}
