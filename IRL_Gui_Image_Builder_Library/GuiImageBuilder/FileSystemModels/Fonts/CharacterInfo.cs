using IRL_Common_Library.Utils;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.Fonts
{
    public class CharacterInfo
    {
        public FsFont Font { get; private set; }
        public string FilePath { get; private set; }
        public string FileExtension { get; private set; }
        public byte ASSCI { get; private set; }
        public uint DataOffset { get; set; }
        public uint DataSize { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }

        public CharacterInfo(FsFont font, byte assci, string filepath, string fileExtension)
        {
            Font = font;
            ASSCI = assci;
            FilePath = filepath;
            FileExtension = fileExtension;
        }

        public static byte GetCharValue(string filename)
        {
            string filenameNoExtension = FileUtils.RemoveExtension(filename);
            string valueStr = filenameNoExtension.Remove(0, filenameNoExtension.LastIndexOf("_") + 1);

            return byte.Parse(valueStr);
        }

        public static byte[] GetBytes(CharacterInfo characterInfo)
        {
            byte[] bytes = new byte[GetSize()];
            byte width = (byte)characterInfo.Width;
            byte height = (byte)characterInfo.Height;

            BitConverter.GetBytes(characterInfo.DataOffset).CopyTo(bytes, 0);
            bytes[4] = width;
            bytes[5] = height;

            return bytes;
        }

        public static int GetSize()
        {
            return 6;
        }
    }
}
