namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic
{
    public class FsbFile
    {
        public uint DataOffset { get; set; }
        public uint DataOffsetCompressed { get; set; }
        public ushort Properties { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }

        public static byte[] GetBytes(FsbFile fsbFile, int sizeOfFileInfo)
        {
            int index = 0;
            byte[] bytes = new byte[sizeOfFileInfo];
            BitConverter.GetBytes(fsbFile.DataOffset).CopyTo(bytes, index);
            index += 4;

            if (sizeOfFileInfo == 9)
            {
                byte properties = (byte)fsbFile.Properties;
                bytes[index] = properties;
                index += 1;
            }
            else if (sizeOfFileInfo == 10)
            {
                ushort properties = fsbFile.Properties;
                BitConverter.GetBytes(properties).CopyTo(bytes, index);
                index += 2;
            }
            else
            {
                // No properties
            }

            BitConverter.GetBytes(fsbFile.Width).CopyTo(bytes, index);
            index += 2;
            BitConverter.GetBytes(fsbFile.Height).CopyTo(bytes, index);

            return bytes;
        }
    }
}
