using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Bitmap_Converter_Tools.Exeptions;
using IRL_Bitmap_Converter_Tools.StatusUpdater;
using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;

namespace IRL_Bitmap_Converter_Tools.Converters
{
    public static class MainConverter
    {
        public static bool CreateBitmaps(
            List<ConverterInstruction> instructions, List<ConverterFont> fonts,
            List<TextStyle> textStyles, List<FontBitmapStyle> fontBitmapStyles, List<IconStyle> iconStyles,
            List<DataLocation> dataLocations, ConverterStatusUpdater statusUpdater)
        {
            bool result = false;

            statusUpdater.ResetValues();
            statusUpdater.UpdateStatusAndProgress("Building Bitmaps", 0);   
            statusUpdater.NoOfInstructions = instructions.Count;

            ClearBuildFolders();

            Log.WriteLine("Starting bitmap conversion...");
            Log.WriteLine("Clearing build folders...");
            Log.WriteLine($"Output Folder: {FileConstants.GetBuildFolder()}");
            Log.WriteLine($"Number of Instructions: {instructions.Count}");

            int noOfFontInstructionsConverted = 0;
            int noOfTextInstructionsConverted = 0;
            int noOfIconInstructionsConverted = 0;

            foreach (ConverterInstruction instruction in instructions)
            {
                result = ProcessInstructions(instruction, textStyles, fontBitmapStyles, iconStyles, fonts, dataLocations, statusUpdater);

                if (!result)
                {
                    Log.Error($"Error in converting instruction: {instruction.Name}");
                    break;
                }
                else
                {
                    if (instruction is FontInstruction)
                    {
                        noOfFontInstructionsConverted++;
                    }
                    else if (instruction is TextInstruction)
                    {
                        noOfTextInstructionsConverted++;
                    }
                    else if (instruction is IconInstruction)
                    {
                        noOfIconInstructionsConverted++;
                    }
                }
            }

            statusUpdater.UpdateStatusAndProgress("Finished", 100);
            Log.WriteLine($"Font Instructions Converted: {noOfFontInstructionsConverted}");
            Log.WriteLine($"Text Instructions Converted: {noOfTextInstructionsConverted}");
            Log.WriteLine($"Icon Instructions Converted: {noOfIconInstructionsConverted}");
            Log.WriteLine("Bitmap conversion finished...");

            return result;
        }

        private static bool ProcessInstructions(
            ConverterInstruction instruction, List<TextStyle> textStyles, List<FontBitmapStyle> fontBitmapStyles, List<IconStyle> iconStyles,
            List<ConverterFont> fonts, List<DataLocation> dataLocations, ConverterStatusUpdater statusUpdater)
        {
            bool result = true;

            CheckFileKeyPrefix(instruction);
            string instructionName = instruction.Name.Replace(" ", "_");

            bool bitmapMask = false;
            string dataLocationStr = "";

            if (instruction is FontInstruction fontInstruction)
            {
                result = ProcessFontInstruction(fontInstruction, dataLocations, fontBitmapStyles, fonts, statusUpdater);
            }
            else if (instruction is TextInstruction textInstruction)
            {
                result = ProcessTextInstruction(textInstruction, dataLocations, textStyles, statusUpdater);
            }
            else if (instruction is IconInstruction iconInstruction)
            {
                result = ProcessIconInstruction(iconInstruction, dataLocations, iconStyles, statusUpdater);
            }
            else
            {
                Log.Error($"Unknown instruction type: {instruction.GetType().Name}");
                throw new ConverterException($"Unknown instruction type: {instruction.GetType().Name}");
            }

            return result;
        }

        private static bool ProcessFontInstruction(
            FontInstruction fontInstruction, List<DataLocation> dataLocations, List<FontBitmapStyle> fontBitmapStyles,
            List<ConverterFont> fonts, ConverterStatusUpdater statusUpdater)
        {
            bool result = true;

            string instructionName = fontInstruction.Name.Replace(" ", "_");
            DataLocation? dataLocation = DataLocation.GetDataLocation(fontInstruction.DataLocationId, dataLocations);

            if (dataLocation == null)
            {
                Log.Error($"Data location with ID {fontInstruction.DataLocationId} not found for font instruction: {instructionName}");
                throw new ConverterException($"Data location with ID {fontInstruction.DataLocationId} not found for font instruction: {instructionName}");
            }

            bool bitmapMask = dataLocation.CompressionType == CompressionType.RLE_Alpha;
            string dataLocationStr = $"{DataLocation.DATA_LOCATION_PREFIX}{dataLocation.LocationID}";

            string fontPath = FileConstants.GetFontImportFolder();
            Directory.CreateDirectory(fontPath);

            foreach (FontBitmap fontBitmap in fontInstruction.FontBitmaps)
            {
                bool bitmapsCreated = FontToBitmapConverter.BuildFontBitmaps(
                    fontBitmap, fontBitmapStyles, bitmapMask, fonts, dataLocationStr, fontPath, fontInstruction.FontFileKeyFormat, statusUpdater);
                if (!bitmapsCreated)
                {
                    result = false;
                }
            }

            return result;
        }

        private static bool ProcessTextInstruction(
            TextInstruction textInstruction, List<DataLocation> dataLocations, 
            List<TextStyle> textStyles, ConverterStatusUpdater statusUpdater)
        {
            string instructionName = "_" + textInstruction.Name.Replace(" ", "_");
            DataLocation? dataLocation = DataLocation.GetDataLocation(textInstruction.DataLocationId, dataLocations);

            if (dataLocation == null)
            {
                Log.Error($"Data location with ID {textInstruction.DataLocationId} not found for text instruction: {instructionName}");
                throw new ConverterException($"Data location with ID {textInstruction.DataLocationId} not found for text instruction: {instructionName}");
            }

            bool bitmapMask = dataLocation.CompressionType == CompressionType.RLE_Alpha;
            string dataLocationStr = $"{DataLocation.DATA_LOCATION_PREFIX}{dataLocation.LocationID}";

            statusUpdater.UpdateStatusAndInstructionsConverted($"Text: {instructionName}", 1);

            string bmpPath = Path.Combine(FileConstants.GetConverterOutputFolder(), instructionName);

            if (!string.IsNullOrEmpty(dataLocationStr))
            {
                bmpPath += dataLocationStr;
            }

            Directory.CreateDirectory(bmpPath);

            return TextConverter.ConvertTextInstructionToBitmaps(textInstruction, textStyles, bitmapMask, bmpPath);
        }

        private static bool ProcessIconInstruction(
            IconInstruction iconInstruction, List<DataLocation> dataLocations, List<IconStyle> iconStyles, ConverterStatusUpdater statusUpdater)
        {
            bool result = true;

            foreach (SvgFileInfo svgFileInfo in iconInstruction.SvgFileInfos)
            {
                string instructionName = svgFileInfo.Filename.Replace(" ", "_");
                DataLocation dataLocation = GetDataLocationById(svgFileInfo.DataLocationId, dataLocations, svgFileInfo.Filename);
                bool bitmapMask = dataLocation.CompressionType == CompressionType.RLE_Alpha;
                string dataLocationStr = $"{DataLocation.DATA_LOCATION_PREFIX}{dataLocation.LocationID}";
                string bmpPath = Path.Combine(FileConstants.GetConverterOutputFolder(), "_Icons" + dataLocationStr);
   
                Directory.CreateDirectory(bmpPath);

                bool iconBitmapsCreated = IconConverter.ConvertSvgToBitmaps(svgFileInfo, iconStyles, bitmapMask, statusUpdater, bmpPath);
                if (!iconBitmapsCreated)
                {
                    result = false;
                }
            }

            return result;
        }

        private static void CheckFileKeyPrefix(ConverterInstruction instructions)
        {
            if (string.IsNullOrEmpty(instructions.FileKeyPrefix))
            {
                return;
            }

            instructions.FileKeyPrefix = instructions.FileKeyPrefix.Trim();
            instructions.FileKeyPrefix = instructions.FileKeyPrefix.Replace(" ", "_");
            instructions.FileKeyPrefix = instructions.FileKeyPrefix.Trim('_');
        }

        public static void ClearBuildFolders()
        {
            string bmpPath = FileConstants.GetConverterOutputFolder();
            string fontPath = FileConstants.GetFontImportFolder();

            if (Directory.Exists(bmpPath))
            {
                FileUtils.ClearDirectory(bmpPath);
            }
            else
            {
                Directory.CreateDirectory(bmpPath);
            }

            if (Directory.Exists(fontPath))
            {
                FileUtils.ClearDirectory(fontPath);
            }
            else
            {
                Directory.CreateDirectory(fontPath);
            }
        }

        private static DataLocation GetDataLocationById(int dataLocationId, List<DataLocation> dataLocations, string instructionName)
        {
            DataLocation dataLocation = DataLocation.GetDataLocation(dataLocationId, dataLocations);

            if (dataLocation == null)
            {
                Log.Error($"Data location with ID {dataLocationId} not found for instruction: {instructionName}");
                throw new ConverterException($"Data location with ID {dataLocationId} not found for instruction: {instructionName}");
            }

            return dataLocation;
        }
    }
}

