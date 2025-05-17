using IRL_Image_Creator.Projects;

namespace IRL_Image_Creator.Windows.ProjectSettings
{
    public partial class ImportSettingsForm : Form
    {
        private readonly Project m_project;

        public ImportSettingsForm(Project project)
        {
            InitializeComponent();
            m_project = project;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Settings files (*.set)|*.set",
                DefaultExt = "set",
                AddExtension = true
            };

            DialogResult result = openFileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                try
                {
                    ImportExportSettings settings = ImportExportSettings.ImportSettings(openFileDialog.FileName);

                    if (ImportPropertiesCB.Checked && (settings.Properties.Length > 0))
                    {
                        m_project.ImageBuilderSettings.Properties = settings.Properties;
                    }

                    if (ImportTextStylesCB.Checked && (settings.TextStyles.Count > 0))
                    {
                        m_project.TextStyles = settings.TextStyles;
                    }

                    if (ImportFontStylesCB.Checked && (settings.FontBitmapStyles.Count > 0))
                    {
                        m_project.FontBitmapStyles = settings.FontBitmapStyles;
                    }

                    if (ImportIconStylesCB.Checked && (settings.IconStyles.Count > 0))
                    {
                        m_project.IconStyles = settings.IconStyles;
                    }

                    if (ImportColorsCB.Checked && (settings.ConverterColors.Count > 0))
                    {
                        m_project.Colors = settings.ConverterColors;
                    }

                    if (ImportFontsCB.Checked && (settings.ConverterFonts.Count > 0))
                    {
                        m_project.Fonts = settings.ConverterFonts;
                    }

                    Project.Save(m_project);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
