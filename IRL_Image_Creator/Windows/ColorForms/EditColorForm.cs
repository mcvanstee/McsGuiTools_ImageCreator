using IRL_Bitmap_Converter_Tools.ConverterInstructions;

namespace IRL_Image_Creator.Windows.ColorForms
{
    public partial class EditColorForm : Form
    {
        private readonly ConverterColor m_ConverterColor;

        public EditColorForm(ConverterColor converterColor)
        {
            InitializeComponent();
            m_ConverterColor = converterColor;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NameTextBox.Text = m_ConverterColor.Name;
            ColorTB.Color = m_ConverterColor.Color;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Name cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_ConverterColor.Name = NameTextBox.Text;
            m_ConverterColor.Color = ColorTB.Color;

            DialogResult = DialogResult.OK;

            Close();
        }
    }
}
