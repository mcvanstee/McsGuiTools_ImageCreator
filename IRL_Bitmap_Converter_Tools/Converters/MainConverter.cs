using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Common_Library.Utils;
using IRL_Bitmap_Converter_Tools.StatusUpdater;
using IRL_Common_Library.Consts;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;

namespace IRL_Bitmap_Converter_Tools.Converters
{
    public static class MainConverter
    {
        public static bool CreateBitmaps(
            List<ConverterInstruction> instructions, List<ConverterFont> fonts,
            List<TextStyle> textStyles, List<FontBitmapStyle> fontBitmapStyles, List<IconStyle> iconStyles,
            int noOfTranslationPropertyValues, string outputFolder, ConverterStatusUpdater statusUpdater)
        {
            bool result = false;

            statusUpdater.ResetValues();
            statusUpdater.UpdateStatusAndProgress("Building Bitmaps", 0);   
            statusUpdater.NoOfInstructions = instructions.Count;

            ClearBuildFolders(outputFolder);

            foreach (ConverterInstruction instruction in instructions)
            {
                result = ProcessInstructions(instruction, textStyles, fontBitmapStyles, iconStyles, fonts, noOfTranslationPropertyValues, outputFolder, statusUpdater);

                if (!result)
                {
                    break;
                }
            }

            statusUpdater.UpdateStatusAndProgress("Finished", 100);

            return result;
        }

        private static bool ProcessInstructions(
            ConverterInstruction instruction, List<TextStyle> textStyles, List<FontBitmapStyle> fontBitmapStyles, List<IconStyle> iconStyles,
            List<ConverterFont> fonts, int noOfTranslationPropertyValues, string outputFolder, ConverterStatusUpdater statusUpdater)
        {
            bool result = true;

            CheckFileKeyPrefix(instruction);
            string instructionName = instruction.Name.Replace(" ", "_");

            if (instruction is FontInstruction fontInstruction)
            {
                string fontPath = outputFolder + FileConstants.FontImportFolder;
                Directory.CreateDirectory(fontPath);

                foreach (FontBitmap fontBitmap in fontInstruction.FontBitmaps)
                {
                    bool bitmapsCreated = FontToBitmapConverter.BuildFontBitmaps(
                        fontBitmap, fontBitmapStyles, fonts, fontPath, fontInstruction.FontFileKeyFormat, statusUpdater);
                    if (!bitmapsCreated)
                    {
                        result = false;
                    }
                }
            }
            else if (instruction is TextInstruction textInstruction)
            {
                statusUpdater.UpdateStatusAndInstructionsConverted($"Text: {instructionName}", 1);

                string bmpPath = outputFolder + FileConstants.BmpImportFolder + FileConstants.ConverterOutputFolder + "\\_" + instructionName;
                Directory.CreateDirectory(bmpPath);

                result = TextConverter.ConvertTextInstructionToBitmaps(textInstruction, textStyles, noOfTranslationPropertyValues, bmpPath);
            }
            else if (instruction is IconInstruction iconInstruction)
            {
                string bmpPath = outputFolder + FileConstants.BmpImportFolder + FileConstants.ConverterOutputFolder + "\\_Icons";
                Directory.CreateDirectory(bmpPath);

                foreach (SvgFileInfo svgFileInfo in iconInstruction.SvgFileInfos)
                {
                    bool iconBitmapsCreated = IconConverter.ConvertSvgToBitmaps(svgFileInfo, iconStyles, statusUpdater, bmpPath);
                    if (!iconBitmapsCreated)
                    {
                        result = false;
                    }
                }
            }
            else
            {
                // Error
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

        public static void ClearBuildFolders(string outputFolder)
        {
            string bmpPath = outputFolder + FileConstants.BmpImportFolder + FileConstants.ConverterOutputFolder;
            string fontPath = outputFolder + FileConstants.FontImportFolder;

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
    }
}

