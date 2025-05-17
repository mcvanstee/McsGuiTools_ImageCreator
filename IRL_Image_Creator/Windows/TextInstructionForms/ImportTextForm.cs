using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Bitmap_Converter_Tools.TextImports;

namespace IRL_Image_Creator.Windows.TextInstructionForms
{
    public partial class ImportTextForm : Form
    {
        private readonly TextInstruction m_TextInstruction;

        public ImportTextForm(TextInstruction textInstruction)
        {
            m_TextInstruction = textInstruction;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetNoFileSelected();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (ImportFileDialog.FileName == "")
            {
                MessageBox.Show("No file selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool importResult = CsvTextImporter.StartImport(m_TextInstruction.Table, SelectedFileLabel.Text, HasHeaderCB.Checked, OverwriteCheckBox.Checked);

            if (importResult)
            {
                MessageBox.Show("Import successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Import failed, check log file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
            }

            Close();
        }

        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ImportFileDialog = new OpenFileDialog
            {
                Filter = "CSV Files|*.csv",
                Title = "Select a CSV File"
            };
            DialogResult dialogResult = ImportFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                SelectedFileLabel.Text = ImportFileDialog.FileName;
                HasHeaderCB.Enabled = true;
                OverwriteCheckBox.Enabled = true;
                OKButton.Enabled = true;
            }
            else
            {
                SetNoFileSelected();
            }
        }

        private void SetNoFileSelected()
        {
            SelectedFileLabel.Text = "No file selected";
            HasHeaderCB.Enabled = false;
            OverwriteCheckBox.Enabled = false;
            OKButton.Enabled = false;
        }
    }
}
