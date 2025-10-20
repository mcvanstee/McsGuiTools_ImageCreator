using IRL_Common_Library.Consts;
using IRL_Common_Library.CRC;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.CodeGeneration;
using IRL_Gui_Image_Builder_Library.CodeGeneration.FontSearchFiles;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;
using IRL_Gui_Image_Builder_Library.Converters;
using IRL_Gui_Image_Builder_Library.Exceptions;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.FileSystemBasic;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.FileSystemModels.Fonts;
using IRL_Gui_Image_Builder_Library.Projects;
using System.Drawing;

namespace IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder
{
    public class ImageBuilder
    {
        private struct ImageFileInfo
        {
            public int sizeofVersionNumber;
            public int pixelInfoSize;
            public int fileInfoSize;
            public int charInfoSize;
            public int startDataOffset;
        }

        public static string StartConvertingBmps(
            ImageBuilderSettings builderSettings, List<FSColor> fsColors, BuilderStatusUpdater statusUpdater,
            string projectPath, string userSourcePath)
        {
            string message;

            BuildFolders.ClearBuildFolder(projectPath);

            statusUpdater.ResetValues();
            statusUpdater.UpdateStatusAndProgress("Start Building File System", 0);
            statusUpdater.NoOfFiles = GetNumberOfFilesToConvert(projectPath);

            BitmapFontConverter bitmapFontConverter = new();
            bitmapFontConverter.ConvertFontBitmapsToCharacterBitmaps(BuildFolders.FontInputFolderPath(projectPath));

            if (builderSettings.FileSystemFormat.FileFormat == FileFormat.SingleFile)
            {
                message = SingleFileProcessAllBMPFilesInFolder(builderSettings, projectPath);
                BitmapToCodeGenerator.CreateFiles(builderSettings, projectPath);
            }
            else if (builderSettings.FileSystemFormat.FileFormat == FileFormat.BasicImage)
            {
                message = CreateImageBasicFileSystem(builderSettings, fsColors, statusUpdater, projectPath, userSourcePath);
            }
            else if (builderSettings.FileSystemFormat.FileFormat == FileFormat.OptimizedImage)
            {
                message = CreateImageOptimizedFileSystem(builderSettings, fsColors, statusUpdater, projectPath, userSourcePath);
            }
            else
            {
                message = "Unsupported FileFormat";
            }

            statusUpdater.UpdateStatusAndProgress("Finished", 100);

            return message;
        }

        private static string CreateImageBasicFileSystem(
            ImageBuilderSettings builderSettings, List<FSColor> fsColors, BuilderStatusUpdater statusUpdater,
            string projectPath, string userSourcePath)
        {
            FsbBuilder fsbBuilder = new(builderSettings, projectPath, statusUpdater);
            bool fileSystemBuild = fsbBuilder.BuildBasicFileSystem();

            FontBuilder fontBuilder = new(builderSettings, projectPath, statusUpdater);
            bool fontsCreated = fontBuilder.CreateFonts();

            if (fileSystemBuild || fontsCreated)
            {
                ImageFileInfo imageInfo = GetImageFileInfo(builderSettings, fsbBuilder.FileInfoSize, fontBuilder.CharacterInfoSize);
                byte[] pixelData = [];

                fsbBuilder.AddPixelData(ref pixelData, imageInfo.startDataOffset);
                fontBuilder.AddPixelData(ref pixelData, imageInfo.startDataOffset);
                fontBuilder.CharInfoOffset = imageInfo.fileInfoSize;

                statusUpdater.UpdateStatus("Create Files");
                CreateBasicImageFiles(builderSettings, projectPath, fsbBuilder, fontBuilder, ref pixelData);

                BasicFileSystemCodeGenerator.CreateBasicFileSystemFiles(builderSettings, projectPath, fsbBuilder, fsColors);
                FontCodeGenerator.CreateFontCodeFiles(builderSettings, projectPath, fontBuilder, fsbBuilder.CRC);
                CopySourceFiles(builderSettings, projectPath, userSourcePath);

                string message = WriteResutlToLogFile(fsbBuilder.FsbFileInfos.Count, fontBuilder.Fonts.Count, builderSettings.GetVerion());
                builderSettings.IncrementRevision();

                return message;
            }
            else
            {
                Log.Error("Building filesystem aborted.");
                throw new ImageBuilderException("Error building filesystem, check log file.");
            }
        }

        private static string CreateImageOptimizedFileSystem(
            ImageBuilderSettings builderSettings, List<FSColor> fsColors, BuilderStatusUpdater statusUpdater,
            string projectPath, string userSourcePath)
        {
            FsbBuilder fsbBuilder = new(builderSettings, projectPath, statusUpdater);
            bool fileSystemBuild = fsbBuilder.BuildOptimizedFileSystem();

            FontBuilder fontBuilder = new(builderSettings, projectPath, statusUpdater);
            bool fontsCreated = fontBuilder.CreatOptimizedFonts();

            if (fileSystemBuild || fontsCreated)
            {
                ImageFileInfo imageInfo = GetImageFileInfo(builderSettings, fsbBuilder.FileInfoSize, fontBuilder.CharacterInfoSize);
                byte[] codeStoredPixelData = [];
                byte[] fileStoredPixelData = [];

                fsbBuilder.AddOptimizedPixelData(ref codeStoredPixelData, 0);
                fsbBuilder.AddPixelData(ref fileStoredPixelData, imageInfo.startDataOffset);
                fontBuilder.AddOptimizedPixelData(ref codeStoredPixelData, 0);

                statusUpdater.UpdateStatus("Create Files");
                CreateCodeStoredImageFiles(
                    builderSettings, projectPath, fsbBuilder, fontBuilder, ref fileStoredPixelData);

                BasicFileSystemCodeGenerator.CreateOptimizedFileSystemFiles(
                    builderSettings, projectPath, fsbBuilder, fsColors, ref codeStoredPixelData);

                FontCodeGenerator.CreateFontCodeFiles(builderSettings, projectPath, fontBuilder, fsbBuilder.CRC);
                CopySourceFiles(builderSettings, projectPath, userSourcePath);

                CreateExternalDisplayImageFileCompressed(builderSettings, projectPath, 
                    ref codeStoredPixelData, ref fileStoredPixelData);

                string message = WriteResutlToLogFile(fsbBuilder.FsbFileInfos.Count, fontBuilder.Fonts.Count, builderSettings.GetVerion());
                builderSettings.IncrementRevision();

                return message;
            }
            else
            {
                Log.Error("Building filesystem aborted.");
                throw new ImageBuilderException("Error building filesystem, check log file.");
            }
        }

        private static string SingleFileProcessAllBMPFilesInFolder(ImageBuilderSettings builderSettings, string projectPath)
        {
            string[] files = Directory.GetFiles(projectPath + FileConstants.BmpImportFolder);

            foreach (string filePath in files)
            {
                string extension = filePath.Substring(filePath.IndexOf('.'));

                if (FileUtils.CanImportFile(extension))
                {
                    Bitmap bitmap = new(filePath, true);

                    string filename = GetFileName(filePath);

                    FileStream outputFile = new(projectPath + FileConstants.BuildFolder + "\\" + filename, FileMode.Create, FileAccess.Write);
                    if (builderSettings.FileSystemFormat.SingleFileIncludeWidthHeight)
                    {
                        outputFile.Write(BitConverter.GetBytes((ushort)bitmap.Width), 0, sizeof(ushort));
                        outputFile.Write(BitConverter.GetBytes((ushort)bitmap.Height), 0, sizeof(ushort));
                    }

                    ConvertAndWritePixelData(bitmap, outputFile, builderSettings);

                    outputFile.Close();
                }
            }

            if (files.Length == 0)
            {
                const string message = "BMP import folder empty!";
                Log.WriteLine("No files converted " + message);

                return message;
            }
            else
            {
                string message = files.Length.ToString() + " file(s) converted.";
                Log.WriteLine(message);

                return message;
            }
        }

        private static void ConvertAndWritePixelData(Bitmap bitmap, FileStream fileStream, ImageBuilderSettings builderSettings)
        {
            byte[] convertedPixelData = PixelDataConverter.GetConvertedPixelData(bitmap, builderSettings.PixelDataFormat);
            fileStream.Write(convertedPixelData);
        }

        public static string GetFileName(string filePath)
        {
            int filenameIndex = filePath.LastIndexOf("\\");
            string filename = filePath.Remove(filePath.Length - 4, 4);
            filename = filename.Remove(0, filenameIndex + 1);

            return filename;
        }

        private static void CreateBasicImageFiles(
            ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder, FontBuilder fontBuilder, ref byte[] pixelData)
        {
            bool writeFontData = fontBuilder.CharacterInfoSize > 0;
            string version = builderSettings.GetVerion().Replace(".", "_");
            string pixelDataFileName = builderSettings.GuiPixelDataFile + "_" + version;
            string pixelDataFilePath = BuildFolders.BuildFolderPath(projectPath) + "\\" + pixelDataFileName;

            using FileStream imageFile = new(pixelDataFilePath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter fileSearchLog = new(BuildFolders.LogFolderPath(projectPath) + "\\image_debug.txt");

            uint crc = WriteVersionNumberToImageFile(imageFile, builderSettings);
            AddPixelDataInfo(imageFile, builderSettings);

            if (!builderSettings.FileSystemFormat.SeparateSearchTreeFromData)
            {
                crc = AddBitmapSearchInfoToImageFile(fsbBuilder, imageFile, crc, fileSearchLog, builderSettings.CreateDebugFiles);
            }

            if (writeFontData && builderSettings.FontDataInImage)
            {
                fontBuilder.CreateCharInfoSearchData();
                crc = AddFontSearchInfoToImageFile(fontBuilder, imageFile, crc, fileSearchLog, builderSettings.CreateDebugFiles);
            }

            Log.WriteLine("Pixeldata offset: " + imageFile.Position);

            crc = CRC32.GetCrc32Accumulate(crc, ref pixelData, pixelData.Length);
            fsbBuilder.CRC = crc;
            imageFile.Write(pixelData);

            Log.WriteLine("CRC: " + crc);

            imageFile.Write(BitConverter.GetBytes(crc));
            imageFile.Close();
            fileSearchLog?.Close();
        }

        private static string WriteResutlToLogFile(int noOfFiles, int noOfFonts, string version)
        {
            string message = noOfFiles.ToString() + " File(s) and " + noOfFonts + " Font(s) added";
            Log.WriteLine("Build successful");
            Log.WriteLine("Build V" + version);
            Log.WriteLine(message);

            return message;
        }

        private static void CreateCodeStoredImageFiles(
            ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder, FontBuilder fontBuilder, ref byte[] dataFilePixelData)
        {
            bool writeFontData = fontBuilder.CharacterInfoSize > 0;
            string version = builderSettings.GetVerion().Replace(".", "_");
            string pixelDataFileName = builderSettings.GuiPixelDataFile + "_" + version;
            string pixelDataFilePath = BuildFolders.BuildFolderPath(projectPath) + "\\" + pixelDataFileName;

            using FileStream imageFile = new(pixelDataFilePath, FileMode.Create, FileAccess.ReadWrite);

            uint crc = WriteVersionNumberToImageFile(imageFile, builderSettings);
            AddPixelDataInfo(imageFile, builderSettings);

            Log.WriteLine("Pixeldata offset: " + imageFile.Position);

            crc = CRC32.GetCrc32Accumulate(crc, ref dataFilePixelData, dataFilePixelData.Length);
            fsbBuilder.CRC = crc;
            imageFile.Write(dataFilePixelData);

            Log.WriteLine("CRC: " + crc);

            imageFile.Write(BitConverter.GetBytes(crc));
        }

        private static void CreateExternalDisplayImageFileCompressed(
            ImageBuilderSettings builderSettings, string projectPath, 
            ref byte[] optimizedPixelData, ref byte[] pixelData)
        {
            string version = builderSettings.GetVerion().Replace(".", "_");
            string filePath = $"{BuildFolders.BuildFolderPath(projectPath)}\\{FileConstants.ExternalDisplayFile}_{version}";

            using FileStream externalDisplayImageFile = new(filePath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter fileSearchLog = new(BuildFolders.LogFolderPath(projectPath) + "\\image_debug.txt");

            WriteVersionNumberToImageFile(externalDisplayImageFile, builderSettings);
            AddPixelDataInfo(externalDisplayImageFile, builderSettings);

            externalDisplayImageFile.Write(pixelData);
            uint startOffset = (uint)externalDisplayImageFile.Position;
            externalDisplayImageFile.Write(optimizedPixelData);
            externalDisplayImageFile.Write(BitConverter.GetBytes(startOffset));

            //externalDisplayImageFile.Write(BitConverter.GetBytes(fsbBuilder.CRC));
            externalDisplayImageFile.Close();
        }

        private static void CreateExternalDisplayImageFile(ImageBuilderSettings builderSettings, string projectPath, FsbBuilder fsbBuilder, FontBuilder fontBuilder, ref byte[] pixelData)
        {
            bool writeFontData = fontBuilder.CharacterInfoSize > 0;
            string version = builderSettings.GetVerion().Replace(".", "_");
            string filePath = $"{BuildFolders.BuildFolderPath(projectPath)}\\{FileConstants.ExternalDisplayFile}_{version}";

            using FileStream externalDisplayImageFile = new(filePath, FileMode.Create, FileAccess.ReadWrite);
            using StreamWriter fileSearchLog = new(BuildFolders.LogFolderPath(projectPath) + "\\image_debug.txt");

            WriteVersionNumberToImageFile(externalDisplayImageFile, builderSettings);

            if (!builderSettings.FileSystemFormat.SeparateSearchTreeFromData)
            {
                AddBitmapSearchInfoToImageFile(fsbBuilder, externalDisplayImageFile, 0, fileSearchLog, false);
            }

            if (writeFontData && builderSettings.FontDataInImage)
            {
                AddFontSearchInfoToImageFile(fontBuilder, externalDisplayImageFile, 0, fileSearchLog, false);
            }

            externalDisplayImageFile.Write(pixelData);
            externalDisplayImageFile.Write(BitConverter.GetBytes(fsbBuilder.CRC));
            externalDisplayImageFile.Close();
        }

        private static uint WriteVersionNumberToImageFile(FileStream imageFile, ImageBuilderSettings builderSettings)
        {
            byte[] versionMajor = (BitConverter.GetBytes(builderSettings.VersionMajor));
            byte[] versionMinor = (BitConverter.GetBytes(builderSettings.VersionMinor));
            byte[] versionPatch = (BitConverter.GetBytes(builderSettings.VersionPatch));
            byte[] versionRevision = (BitConverter.GetBytes(builderSettings.VersionRevision));

            imageFile.Write(versionMajor);
            imageFile.Write(versionMinor);
            imageFile.Write(versionPatch);
            imageFile.Write(versionRevision);

            uint crc = CRC32.GetCrc32Accumulate(0, ref versionMajor, versionMajor.Length);
            crc = CRC32.GetCrc32Accumulate(crc, ref versionMinor, versionMinor.Length);
            crc = CRC32.GetCrc32Accumulate(crc, ref versionPatch, versionPatch.Length);
            crc = CRC32.GetCrc32Accumulate(crc, ref versionRevision, versionRevision.Length);

            return crc;
        }

        private static void AddPixelDataInfo(FileStream imageFile, ImageBuilderSettings builderSettings)
        {
            byte[] bytesPerPixel = new byte[1];
            byte[] pixelFormat = new byte[1];
            byte[] compressed = new byte[2];

            compressed[0] = builderSettings.FileSystemFormat.CompressBasicImagePixelData ? (byte)1 : (byte)0;
            compressed[1] = (byte)((builderSettings.FileSystemFormat.FileFormat == FileFormat.OptimizedImage) ? 1 : 0);
            bytesPerPixel[0] = builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB ? (byte)3 : (byte)2;           

            if (builderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB)
            {
                if (builderSettings.PixelDataFormat.PixelFormatRGB == PixelFormatRGB.BGR)
                {
                    pixelFormat[0] = 1; // BGR
                }
                else
                {
                    pixelFormat[0] = 0; // RGB
                }
            }
            else
            {
                if (builderSettings.PixelDataFormat.RGB565SwapBytes)
                {
                    pixelFormat[0] = 1; // RGB565 with swapped bytes
                }
                else
                {
                    pixelFormat[0] = 0; // RGB
                }
            }

            imageFile.Write(bytesPerPixel, 0, bytesPerPixel.Length);
            imageFile.Write(pixelFormat, 0, pixelFormat.Length);
            imageFile.Write(compressed, 0, compressed.Length);
        }

        private static uint AddBitmapSearchInfoToImageFile(
            FsbBuilder fsbBuilder, FileStream imageFile, uint crc, StreamWriter fileSearchLog, bool writeDebugFile)
        {
            byte[] fileSearchData = fsbBuilder.FileInfoSearchData;

            if (writeDebugFile)
            {
                for (int i = 0; i < fileSearchData.Length; i += fsbBuilder.SizeOfFileInfo)
                {
                    if (fsbBuilder.SizeOfFileInfo == 8)
                    {
                        fileSearchLog.WriteLine("(" + i + ") " + BitConverter.ToUInt32(fileSearchData, i) + " " + BitConverter.ToUInt16(fileSearchData, i + 4) + " " + BitConverter.ToUInt16(fileSearchData, i + 6));
                    }
                    else if (fsbBuilder.SizeOfFileInfo == 9)
                    {
                        fileSearchLog.WriteLine("(" + i + ") " + BitConverter.ToUInt32(fileSearchData, i) + " " + fileSearchData[i + 4] + " " + BitConverter.ToUInt16(fileSearchData, i + 5) + " " + BitConverter.ToUInt16(fileSearchData, i + 7));
                    }
                    else if (fsbBuilder.SizeOfFileInfo == 10)
                    {
                        fileSearchLog.WriteLine("(" + i + ") " + BitConverter.ToUInt32(fileSearchData, i) + " " + BitConverter.ToUInt16(fileSearchData, i + 4) + " " + BitConverter.ToUInt16(fileSearchData, i + 6) + " " + BitConverter.ToUInt16(fileSearchData, i + 8));
                    }
                    else
                    {
                        Log.Error("Writing image_debug.txt");
                    }
                }
            }

            crc = CRC32.GetCrc32Accumulate(crc, ref fileSearchData, fileSearchData.Length);
            imageFile.Write(fileSearchData);

            return crc;
        }

        private static uint AddFontSearchInfoToImageFile(
            FontBuilder fontBuilder, FileStream imageFile, uint crc, StreamWriter fileSearchLog, bool writeDebugFiles)
        {
            if (writeDebugFiles)
            {
                for (int i = 0; i < fontBuilder.CharInfoSearchData.Length; i += 6)
                {
                    fileSearchLog?.WriteLine("(" + (imageFile.Position + i).ToString() + ") " + BitConverter.ToInt32(fontBuilder.CharInfoSearchData, i) + " " + fontBuilder.CharInfoSearchData[i + 4] + " " + fontBuilder.CharInfoSearchData[i + 5]);
                }
            }

            Log.WriteLine("Font info offset: " + imageFile.Position);

            byte[] charSearchData = fontBuilder.CharInfoSearchData;
            crc = CRC32.GetCrc32Accumulate(crc, ref charSearchData, charSearchData.Length);
            imageFile.Write(fontBuilder.CharInfoSearchData);

            return crc;
        }

        private static int GetNumberOfFilesToConvert(string projectPath)
        {
            int numberOfFiles = 0;

            DirectoryInfo directoryInfoRoot = new(BuildFolders.BmpImputFolderPath(projectPath));
            DirectoryInfo[] directoryInfos = directoryInfoRoot.GetDirectories();

            numberOfFiles += directoryInfoRoot.GetFiles().Length;
            foreach (DirectoryInfo dirInfo in directoryInfos)
            {
                numberOfFiles += dirInfo.GetFiles().Length;
            }

            DirectoryInfo fontRootDir = new(BuildFolders.FontInputFolderPath(projectPath));
            numberOfFiles += (95 * fontRootDir.GetDirectories().Length);

            return numberOfFiles;
        }

        private static void CopySourceFiles(ImageBuilderSettings builderSettings, string projectPath, string userSourceFolder)
        {
            if (!builderSettings.CopySourceFiles ||
                string.IsNullOrEmpty(userSourceFolder) ||
                !Directory.Exists(userSourceFolder))
            {
                return;
            }

            string sourceFolderPath = BuildFolders.SourceFolderPath(projectPath);

            try
            {
                FileUtils.CopyFile(
                    sourceFolderPath + "\\" + FileConstants.SearchTreeFile + ".h",
                    userSourceFolder + "\\" + FileConstants.SearchTreeFile + ".h");
                FileUtils.CopyFile(
                    sourceFolderPath + "\\" + FileConstants.SearchTreeFile + ".c",
                    userSourceFolder + "\\" + FileConstants.SearchTreeFile + ".c");

                FileUtils.CopyFile(
                    sourceFolderPath + "\\" + FileConstants.CharInfoSearchFile + ".h",
                    userSourceFolder + "\\" + FileConstants.CharInfoSearchFile + ".h");
                FileUtils.CopyFile(
                    sourceFolderPath + "\\" + FileConstants.CharInfoSearchFile + ".c",
                    userSourceFolder + "\\" + FileConstants.CharInfoSearchFile + ".c");
            }
            catch (Exception ex)
            {
                Log.Error("Copying source files." + ex.Message);
            }
        }

        private static ImageFileInfo GetImageFileInfo(ImageBuilderSettings builderSettings, int fileInfoSize, int charInfoSize)
        {
            int sizeofVersionNumberBytes = sizeof(uint) * 4;
            int pixelInfoSizeBytes = 4;
            int fileInfoSizeBytes = builderSettings.FileSystemFormat.SeparateSearchTreeFromData ? 0 : fileInfoSize;
            int charInfoSizeBytes = builderSettings.FontDataInImage ? charInfoSize : 0;
            int startDataOffset = sizeofVersionNumberBytes + pixelInfoSizeBytes + charInfoSizeBytes + fileInfoSizeBytes;

            return new ImageFileInfo
            {
                sizeofVersionNumber = sizeofVersionNumberBytes,
                pixelInfoSize = pixelInfoSizeBytes,
                fileInfoSize = fileInfoSizeBytes,
                charInfoSize = charInfoSizeBytes,
                startDataOffset = startDataOffset
            };
        }
    }
}




//private static void CompressImageFile(FsbBuilder fsbBuilder, FontBuilder fontBuilder, ref byte[] pixelData, string pixelDataFilePath, string projectPath)
//{
//    using FileStream compressedFile = new(
//        pixelDataFilePath + ".comp",
//        FileMode.Create, FileAccess.ReadWrite);

//    uint sizeCompressedData = 0;
//    const uint startDataOffset = 18;
//    uint writeIndex = 0;

//    foreach (FsbFileInfo fileInfo in fsbBuilder.FsbFileInfos)
//    {
//        uint legthBytes = (uint)(fileInfo.FsbFile.Width * fileInfo.FsbFile.Height * 3);
//        sizeCompressedData = CompressBitmapPixelData(fileInfo.FsbFile.DataOffset - startDataOffset, legthBytes, ref pixelData, compressedFile);
//        fileInfo.FsbFile.DataOffset = writeIndex;

//        writeIndex += sizeCompressedData;
//    }

//    foreach (FsFont font in fontBuilder.Fonts)
//    {
//        foreach (CharacterInfo charInfo in font.CharacterInfos)
//        {
//            uint legthBytes = (uint)(charInfo.Width * charInfo.Height * 3);
//            sizeCompressedData = CompressBitmapPixelData(charInfo.DataOffset - startDataOffset, legthBytes, ref pixelData, compressedFile);
//            charInfo.DataOffset = writeIndex;

//            writeIndex += sizeCompressedData;
//        }
//    }

//    compressedFile.Position = 0;

//    StreamWriter sw = new(BuildFolders.SourceFolderPath(projectPath) + "\\" + "comprdata" + ".c");


//    for (int i = 0; i < compressedFile.Length; i++)
//    {
//        sw.Write(compressedFile.ReadByte() + ",");
//        if ((i + 1) % 64 == 0)
//        {
//            sw.WriteLine("");
//        }
//    }

//    sw.Close();

//    compressedFile.Close();
//}

//private static uint CompressBitmapPixelData(uint offset, uint lengthBytes, ref byte[] pixelData, FileStream compressedFile)
//{
//    ushort currentPixel = 0xFFFF;
//    ushort pixelCount = 0;
//    uint totalSize = 0;

//    for (uint i = offset; i < (offset + lengthBytes); i += 3)
//    {
//        byte pixel = pixelData[i];

//        if (pixel == currentPixel)
//        {
//            if (pixelCount < 256)
//            {
//                pixelCount++;
//            }
//            else
//            {
//                AddPixelInfo(compressedFile, (byte)(pixelCount - 1), (byte)currentPixel, ref totalSize);

//                currentPixel = pixel;
//                pixelCount = 1;
//            }
//        }
//        else if (currentPixel == 0xFFFF)
//        {
//            currentPixel = pixel;
//            pixelCount = 1;
//        }
//        else
//        {
//            AddPixelInfo(compressedFile, (byte)(pixelCount - 1), (byte)currentPixel, ref totalSize);

//            currentPixel = pixel;
//            pixelCount = 1;
//        }
//    }

//    AddPixelInfo(compressedFile, (byte)(pixelCount - 1), (byte)currentPixel, ref totalSize);

//    return totalSize;
//}

//private static void AddPixelInfo(FileStream compressedFile, byte pixelCount, byte colorCode, ref uint size)
//{
//    compressedFile.WriteByte(pixelCount);
//    compressedFile.WriteByte(colorCode);
//    size += 2;
//}


//private static uint CompressBitmapPixelData(uint offset, uint lengthBytes, ref byte[] pixelData, FileStream compressedFile)
//{
//    ushort currentPixel = pixelData[offset];
//    byte pixelCount = 1;
//    uint totalSize = 0;

//    for (uint i = offset + 3; i < (offset + lengthBytes); i += 3)
//    {
//        byte pixel = pixelData[i];

//        if (pixel == currentPixel)
//        {
//            if (pixelCount < 0xFF)
//            {
//                pixelCount++;
//            }
//            else
//            {
//                compressedFile.WriteByte(pixelCount);
//                compressedFile.WriteByte(currentPixel);
//                totalSize += 2; // 1 byte for count, 1 byte for pixel value

//                currentPixel = pixel;
//                pixelCount = 1;

//            }
//        }
//        else
//        {
//            compressedFile.WriteByte(pixelCount);
//            compressedFile.WriteByte(currentPixel);
//            totalSize += 2;

//            currentPixel = pixel;
//            pixelCount = 1;
//        }
//    }

//    compressedFile.WriteByte(pixelCount);
//    compressedFile.WriteByte(currentPixel);
//    totalSize += 2; // Last pixel count and value

//    return totalSize;
//}