using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.Converters;
using IRL_Bitmap_Converter_Tools.StatusUpdater;
using IRL_Common_Library.Consts;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using IRL_Image_Creator.Projects;
using System.Diagnostics;
using IRL_Gui_Image_Builder_Library.Projects;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.CodeGeneration.Utils;

namespace IRL_Image_Creator.Windows.Helpers
{
    public static class ImageBuilderHelper
    {
        public static void CreateBitmaps(Project project, ConverterStatusUpdater converterStatusUpdater, Form owner)
        {
            owner.Enabled = false;
            BuildFolders.ClearLogFolder(project.ProjectFolder);
            Log.OpenNewFile(BuildFolders.LogFolderPath(project.ProjectFolder));

            bool success = StartCreateBitmaps(project, converterStatusUpdater);

            Log.CloseFile();
            owner.Enabled = true;

            if (!success)
            {
                const string caption = "Result";
                MessageBox.Show(owner, "Error creating bitmaps", caption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                // Open File explorer
                //
                string buildFolder = $"{project.ProjectFolder}{FileConstants.LogFolder}";
                Process.Start("explorer.exe", @buildFolder);
            }
        }

        public static void CreateImage(
            Project project, BindingSource imageBuilderSettingsBindingSource, BuilderStatusUpdater statusUpdater, Form owner)
        {
            owner.Enabled = false;
            BuildFolders.ClearLogFolder(project.ProjectFolder);
            Log.OpenNewFile(BuildFolders.LogFolderPath(project.ProjectFolder));

            StartCreateImage(project, imageBuilderSettingsBindingSource, statusUpdater, owner);

            Log.CloseFile();
            owner.Enabled = true;
        }

        public static void BuildAll(
            Project project, BindingSource imageBuilderSettingsBindingSource, 
            ConverterStatusUpdater converterStatusUpdater, BuilderStatusUpdater statusUpdater, Form owner)
        {
            if (project.ImageBuilderSettings.FileSystemFormat.FileFormat == FileFormat.SingleFile)
            {
                StartCreateImage(project, imageBuilderSettingsBindingSource, statusUpdater, owner);
            }
            else
            {

                owner.Enabled = false;
                BuildFolders.ClearLogFolder(project.ProjectFolder);
                Log.OpenNewFile(BuildFolders.LogFolderPath(project.ProjectFolder));

                bool bitmapsCreated = StartCreateBitmaps(project, converterStatusUpdater);

                if (bitmapsCreated)
                {
                    StartCreateImage(project, imageBuilderSettingsBindingSource, statusUpdater, owner);
                }

                Log.CloseFile();
                owner.Enabled = true;

                if (!bitmapsCreated)
                {
                    const string caption = "Result";
                    MessageBox.Show(owner, "Error creating bitmaps", caption,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    // Open File explorer
                    //
                    string buildFolder = $"{project.ProjectFolder}{FileConstants.LogFolder}";
                    Process.Start("explorer.exe", @buildFolder);
                }
            }
        }

        private static void StartCreateImage(Project project, BindingSource imageBuilderSettingsBindingSource, BuilderStatusUpdater statusUpdater, Form owner)
        {
            Project.Save(project);

            List<FSColor> fsColors = GetFSColors(project.Colors);
            project.ImageBuilderSettings.UseProperties = project.ImageBuilderSettings.Properties.Length > 0;
            string message = ImageBuilder.StartConvertingBmps(
                project.ImageBuilderSettings, fsColors, statusUpdater,
                project.ProjectFolder, project.UserSourceFolder);

            // Update version
            //
            imageBuilderSettingsBindingSource.ResetBindings(false);
            Project.Save(project);

            const string caption = "Result";
            MessageBox.Show(owner, message, caption,
            MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Open File explorer
            //
            string buildFolder = $"{project.ProjectFolder}{FileConstants.BuildFolder}";
            Process.Start("explorer.exe", @buildFolder);
        }

        private static bool StartCreateBitmaps(Project project, ConverterStatusUpdater converterStatusUpdater)
        {
            Project.Save(project);

            int numberOfTranslations = 0;

            foreach (ConverterInstruction instruction in project.Instructions)
            {
                if (instruction is TextInstruction textInstruction)
                {
                    if (textInstruction.Table.Translate)
                    {
                        numberOfTranslations =
                            project.ImageBuilderSettings.Properties.FirstOrDefault(
                                x => x.Name == textInstruction.Table.TranslationProperty.Name)?.PropertyValues.Count ?? 0;
                    }
                }
            }

            return MainConverter.CreateBitmaps(
                project.Instructions, project.Fonts, project.TextStyles, project.FontBitmapStyles, project.IconStyles,
                numberOfTranslations, project.ProjectFolder, converterStatusUpdater);
        }

        private static List<FSColor> GetFSColors(List<ConverterColor> colors)
        {
            List<FSColor> fsColors = new();

            foreach (ConverterColor color in colors)
            {
                int colorRGB = color.Color.ToArgb() & 0xFFFFFF;
                fsColors.Add(new FSColor(color.Name, colorRGB));
            }

            return fsColors;
        }
    }
}
