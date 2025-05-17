using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Image_Creator.Windows.MainFormModels;

namespace IRL_Image_Creator.Windows.TextInstructionForms
{
    public partial class NewTextInstructionForm : Form
    {
        private readonly TextInstruction m_TextInstruction;
        public NewTextInstructionForm(TextInstruction textInstruction)
        {
            m_TextInstruction = textInstruction;

            InitializeComponent();

            TrPropertyTLPanel.Enabled = false;
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Name cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TranslateTextCB.Checked)
            {
                if (PropertyLabel.Text == "")
                {
                    MessageBox.Show("Translation cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            m_TextInstruction.Name = NameTextBox.Text;           

            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TranslationPropertyBtn_Click(object sender, EventArgs e)
        {
            HeaderPropertyForm headerPropertyForm = new();
            headerPropertyForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = headerPropertyForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                try
                {
                    PropertyLabel.Text = headerPropertyForm.PropertyName;
                    m_TextInstruction.Table.TranslationProperty.Name = headerPropertyForm.PropertyName;
                    m_TextInstruction.Table.Translate = TranslateTextCB.Checked;
                }
                catch
                {
                    MessageBox.Show("Error setting translation property", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_TextInstruction.Table.TranslationProperty.Name = "";
                    m_TextInstruction.Table.Translate = false;
                }
            }
            else
            {
                m_TextInstruction.Table.TranslationProperty.Name = "";
                m_TextInstruction.Table.Translate = false;
            }
        }

        private void TranslateTextCB_CheckedChanged(object sender, EventArgs e)
        {
            TrPropertyTLPanel.Enabled = TranslateTextCB.Checked;
        }
    }
}
