using IRL_Image_Creator.Projects;

namespace IRL_Image_Creator.Windows.ProjectSettings
{
    public partial class ExportSettingsForm : Form
    {
        private readonly Project m_project;

        public ExportSettingsForm(Project project)
        {
            InitializeComponent();
            m_project = project;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = "Settings files (*.set)|*.set",
                DefaultExt = "set",
                AddExtension = true
            };

            DialogResult result = saveFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                try
                {
                    ImportExportSettings settings = new();

                    if (ExportPropertiesCB.Checked)
                    {
                        settings.Properties = m_project.ImageBuilderSettings.Properties;
                    }

                    if (ExportTextStylesCB.Checked)
                    {
                        settings.TextStyles = m_project.TextStyles;
                    }

                    if (ExportFontStylesCB.Checked)
                    {
                        settings.FontBitmapStyles = m_project.FontBitmapStyles;
                    }

                    if (ExportIconStylesCB.Checked)
                    {
                        settings.IconStyles = m_project.IconStyles;
                    }

                    if (ExportColorsCB.Checked)
                    {
                        settings.ConverterColors = m_project.Colors;
                    }

                    if (ExportFontsCB.Checked)
                    {
                        settings.ConverterFonts = m_project.Fonts;
                    }

                    ImportExportSettings.ExportSettings(settings, saveFileDialog.FileName);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
