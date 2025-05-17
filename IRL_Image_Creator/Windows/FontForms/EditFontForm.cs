using IRL_Bitmap_Converter_Tools.ConverterInstructions;

namespace IRL_Image_Creator.Windows.FontForms
{
    public partial class EditFontForm : Form
    {
        private readonly ConverterFont m_Font;
        private ConverterFont m_SelectedFont;

        public EditFontForm(ConverterFont converterFont)
        {
            InitializeComponent();
            m_Font = converterFont;
            m_SelectedFont = new()
            {
                Name = m_Font.Name,
                FontName = m_Font.FontName,
                FontSize = m_Font.FontSize,
                FontStyle = m_Font.FontStyle
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NameTextBox.Text = m_Font.Name;
            SetSelctedFontLabel(m_Font.Name, m_Font.FontSize, m_Font.FontStyle);
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
                MessageBox.Show(this, "Name is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(m_SelectedFont.FontName))
            {
                MessageBox.Show(this, "Font is not selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            m_Font.Name = NameTextBox.Text;
            m_Font.FontName = m_SelectedFont.FontName;
            m_Font.FontSize = m_SelectedFont.FontSize;
            m_Font.FontStyle = m_SelectedFont.FontStyle;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void SelectFontButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_Font.FontName))
            {
                fontDialog.Font = new Font(m_Font.FontName, m_Font.FontSize, m_Font.FontStyle, GraphicsUnit.Pixel);
            }

            DialogResult result = fontDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                m_SelectedFont.FontName = fontDialog.Font.Name;
                m_SelectedFont.FontSize = (float)Math.Round(fontDialog.Font.Size);
                m_SelectedFont.FontStyle = fontDialog.Font.Style;

                SetSelctedFontLabel(m_SelectedFont.FontName, m_SelectedFont.FontSize, m_SelectedFont.FontStyle);
            }
        }

        private void SetSelctedFontLabel(string fontName, float fontSize, FontStyle fontStyle)
        {
            if (string.IsNullOrEmpty(fontName))
            {
                FontLabel.Text = "No font selected";
            }
            else
            {
                FontLabel.Text = $"{fontName} {fontSize} {fontStyle}";
            }
        }
    }
}
