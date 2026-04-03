using IRL_Common_Library.Consts;
using IRL_Common_Library.CRC;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.CodeGeneration;
using IRL_Gui_Image_Builder_Library.CodeGeneration.FontSearchFiles;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.Converters;
using IRL_Gui_Image_Builder_Library.Exceptions;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Files;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystem.Fonts;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.ImageFile;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.PixelDatas;
using IRL_Gui_Image_Builder_Library.Projects;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder
{
    public class ImageBuilder
    {
        public static string StartConvertingBmps(
            ImageBuilderSettings builderSettings, List<FSColor> fsColors, BuilderStatusUpdater statusUpdater,  string userSourcePath)
        {
            string message = "";

            BuildFolders.ClearBuildFolder();

            statusUpdater.ResetValues();
            statusUpdater.UpdateStatusAndProgress("Start Building File System", 0);
            statusUpdater.NoOfFiles = GetNumberOfFilesToConvert();

            BitmapFontConverter bitmapFontConverter = new();
            bitmapFontConverter.ConvertFontBitmapsToCharacterBitmaps(FileConstants.GetFontImportFolder());
            message = CreateFileSystem(builderSettings, fsColors, statusUpdater, userSourcePath);
            statusUpdater.UpdateStatusAndProgress("Finished", 100);

            return message;
        }

        private static string CreateFileSystem(
            ImageBuilderSettings builderSettings, List<FSColor> fsColors, BuilderStatusUpdater statusUpdater, string userSourcePath)
        {
            FsbBuilder fsbBuilder = new(builderSettings, statusUpdater);
            bool fileSystemBuild = fsbBuilder.BuildFileSystem();

            FontBuilder fontBuilder = new(builderSettings, statusUpdater);
            bool fontsCreated = fontBuilder.CreateFonts();

            List<PixelData> pixelDatas = [];

            if (fileSystemBuild || fontsCreated)
            {
                AddPixelDatas(fsbBuilder, fontBuilder, pixelDatas);
                statusUpdater.UpdateStatus("Create Files");

                CreateImageFiles(builderSettings, fsbBuilder, fontBuilder, pixelDatas);
                
                FileSystemCodeGenerator.CreateCodeFiles(builderSettings, fsbBuilder, pixelDatas);
                FSColorHeaderGenerator.CreateColorHeader(fsColors);
                VersionHeaderGenerator.CreateVersionHeader(builderSettings);
                CrcHeaderGenerator.CreateCrcHeader();
                CrcCodeGenerator.CreateCrcCode();
                FontCodeGenerator.CreateFontCodeFiles(builderSettings, fontBuilder, fsbBuilder.CRC);
                CopySourceFiles(builderSettings, userSourcePath);

                ExternalImageBuilder externalImage = new(builderSettings, fsbBuilder, fontBuilder);
                externalImage.CreateExternalImageFile();

                string message = WriteResutlToLogFile(fsbBuilder.FileInfos.Count, fontBuilder.Fonts.Count, builderSettings.GetVerion());
                builderSettings.IncrementRevision();

                return message;
            }
            else
            {
                Log.Error("Building filesystem aborted.");
                throw new ImageBuilderException("Error building filesystem, check log file.");
            }
        }

        private static void AddPixelDatas(FsbBuilder fsbBuilder, FontBuilder fontBuilder, List<PixelData> pixelDatas)
        {            
            foreach (DataLocation dataLocation in fsbBuilder.DataLocations)
            {
                //int offset = (dataLocation.DataLocationType == DataLocationType.File) ? ImageFileHeader.IMAGE_FILE_HEADER_SIZE : 0;
                PixelData pixelData = new(dataLocation);
                //{
                //    Offset = offset,
                //    DataLocation = dataLocation
                //};

                fsbBuilder.AddPixelData(pixelData);
                pixelDatas.Add(pixelData);
            }

            foreach (DataLocation dataLocation in fontBuilder.DataLocations)
            {
                if (pixelDatas.Exists(pd => pd.DataLocation == dataLocation))
                {
                    PixelData pixelData = pixelDatas.Find(pd => pd.DataLocation == dataLocation)!;
                    fontBuilder.AddPixelData(pixelData);
                    pixelDatas.Add(pixelData);
                }
                else
                {
                    //int offset = (dataLocation.DataLocationType == DataLocationType.File) ? ImageFileHeader.IMAGE_FILE_HEADER_SIZE : 0;
                    PixelData pixelData = new(dataLocation);

                    fontBuilder.AddPixelData(pixelData);
                    pixelDatas.Add(pixelData);
                }
            }
        }

        private static void CreateImageFiles(
                ImageBuilderSettings builderSettings, FsbBuilder fsbBuilder, FontBuilder fontBuilder, List<PixelData> pixelDatas)
        {
            foreach (PixelData pixelData in pixelDatas)
            {
                if (pixelData.DataLocation.DataLocationType == DataLocationType.File_1)
                {
                    pixelData.Offset = ImageFileHeader.IMAGE_FILE_HEADER_SIZE;
                    CreateImageFile(builderSettings, fsbBuilder, fontBuilder, pixelData);
                }
            }
        }

        private static void CreateImageFile(
            ImageBuilderSettings builderSettings, FsbBuilder fsbBuilder, FontBuilder fontBuilder, PixelData pixelData)
        {
            uint crc = 0;
            string pixelDataFilePath = GetImageFilePath(builderSettings);

            using FileStream imageFile = new(pixelDataFilePath, FileMode.Create, FileAccess.ReadWrite);

            ImageFileHeader imageFileHeader = new(HeaderType.Basic, builderSettings, fsbBuilder, fontBuilder);
            crc = imageFileHeader.WriteFileHeader(imageFile, crc);

            Log.WriteLine("Pixel data offset: " + imageFile.Position);

            foreach (DataItemBase dataItem in pixelData.DataItems)
            {
                byte[] dataBytes = dataItem.Data;
                crc = CRC32.GetCrc32Accumulate(crc, ref dataBytes, dataBytes.Length);
                imageFile.Write(dataBytes);
            }

            fsbBuilder.CRC = crc;

            Log.WriteLine("CRC: " + crc);

            imageFile.Write(BitConverter.GetBytes(crc));
            imageFile.Close();
        }

        public static string GetFileName(string filePath)
        {
            int filenameIndex = filePath.LastIndexOf("\\");
            string filename = filePath.Remove(filePath.Length - 4, 4);
            filename = filename.Remove(0, filenameIndex + 1);

            return filename;
        }

        private static string WriteResutlToLogFile(int noOfFiles, int noOfFonts, string version)
        {
            string message = noOfFiles.ToString() + " File(s) and " + noOfFonts + " Font(s) added";
            Log.WriteLine("Build successful");
            Log.WriteLine("Build V" + version);
            Log.WriteLine(message);

            return message;
        }

        private static string GetImageFilePath(ImageBuilderSettings builderSettings)
        {
            string version = builderSettings.GetVerion().Replace(".", "_");
            string pixelDataFileName = builderSettings.GuiPixelDataFile + "_" + version + FileConstants.IMAGE_FILE_EXTENSION;
            string pixelDataFilePath = Path.Combine(FileConstants.GetBuildFolder(), pixelDataFileName);

            return pixelDataFilePath;
        }

        private static int GetNumberOfFilesToConvert()
        {
            int numberOfFiles = 0;

            DirectoryInfo directoryInfoRoot = new(FileConstants.GetBmpImportFolder());
            DirectoryInfo[] directoryInfos = directoryInfoRoot.GetDirectories();

            numberOfFiles += directoryInfoRoot.GetFiles().Length;
            foreach (DirectoryInfo dirInfo in directoryInfos)
            {
                numberOfFiles += dirInfo.GetFiles().Length;
            }

            DirectoryInfo fontRootDir = new(FileConstants.GetFontImportFolder());
            numberOfFiles += 95 * fontRootDir.GetDirectories().Length;

            return numberOfFiles;
        }

        private static void CopySourceFiles(ImageBuilderSettings builderSettings, string userSourceFolder)
        {
            if (!builderSettings.CopySourceFiles ||
                string.IsNullOrEmpty(userSourceFolder) ||
                !Directory.Exists(userSourceFolder))
            {
                return;
            }

            string sourceFolderPath = FileConstants.GetSourceFolder();

            try
            {
                FileUtils.CopyFile(
                    Path.Combine(sourceFolderPath, FileConstants.SEARCH_TREE_FILE + ".h"),
                    Path.Combine(userSourceFolder, FileConstants.SEARCH_TREE_FILE + ".h"));
                FileUtils.CopyFile(
                    Path.Combine(sourceFolderPath, FileConstants.SEARCH_TREE_FILE + ".c"),
                    Path.Combine(userSourceFolder, FileConstants.SEARCH_TREE_FILE + ".c"));
                FileUtils.CopyFile(
                    Path.Combine(sourceFolderPath, FileConstants.CHAR_INFO_SEARCH_FILE + ".h"),
                    Path.Combine(userSourceFolder, FileConstants.CHAR_INFO_SEARCH_FILE + ".h"));
                FileUtils.CopyFile(
                    Path.Combine(sourceFolderPath, FileConstants.CHAR_INFO_SEARCH_FILE + ".c"),
                    Path.Combine(userSourceFolder, FileConstants.CHAR_INFO_SEARCH_FILE + ".c"));
            }
            catch (Exception ex)
            {
                Log.Error("Copying source files." + ex.Message);
            }
        }
    }
}

