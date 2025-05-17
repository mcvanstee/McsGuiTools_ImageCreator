using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Image_Creator.Windows.ColorForms;

namespace IRL_Image_Creator.Windows.FontStyleForms
{
    public partial class EditFontStyleForm : Form
    {
        private readonly FontBitmapStyle m_FontBitmapStyle;
        private int m_SelectedTextColorID = -1;
        private int m_SelectedBackColorID = -1;

        public EditFontStyleForm(FontBitmapStyle fontBitmapStyle)
        {
            m_FontBitmapStyle = fontBitmapStyle;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NameTextBox.Text = m_FontBitmapStyle.Name;
            TextColorTB.Color = m_FontBitmapStyle.TextColor;
            BackColorTB.Color = m_FontBitmapStyle.BackColor;
            m_SelectedTextColorID = m_FontBitmapStyle.TextConverterColorId;
            m_SelectedBackColorID = m_FontBitmapStyle.BackConverterColorId;
            MonospaceNumbersCheckBox.Checked = m_FontBitmapStyle.MonospaceNumbers;
            LeftMarginTB.Text = m_FontBitmapStyle.Margin.Left.ToString();
            TopMarginTB.Text = m_FontBitmapStyle.Margin.Top.ToString();
            RightMarginTB.Text = m_FontBitmapStyle.Margin.Right.ToString();
            BottomMarginTB.Text = m_FontBitmapStyle.Margin.Bottom.ToString();

            SetTextColorLabel();
            SetBackColorLabel();
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

            m_FontBitmapStyle.Name = NameTextBox.Text;
            m_FontBitmapStyle.TextColor = TextColorTB.Color;
            m_FontBitmapStyle.BackColor = BackColorTB.Color;
            m_FontBitmapStyle.TextConverterColorId = m_SelectedTextColorID;
            m_FontBitmapStyle.BackConverterColorId = m_SelectedBackColorID;
            m_FontBitmapStyle.MonospaceNumbers = MonospaceNumbersCheckBox.Checked;
            m_FontBitmapStyle.Margin.Left = int.Parse(LeftMarginTB.Text);
            m_FontBitmapStyle.Margin.Top = int.Parse(TopMarginTB.Text);
            m_FontBitmapStyle.Margin.Right = int.Parse(RightMarginTB.Text);
            m_FontBitmapStyle.Margin.Bottom = int.Parse(BottomMarginTB.Text);

            DialogResult = DialogResult.OK;

            Close();
        }


        private void SelectTextColorButton_Click(object sender, EventArgs e)
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;

            SelectColorForm form = new(colors);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ConverterColor selectedColor = ConverterColor.GetColorById(form.SelectedColorID, colors);
                TextColorTB.Color = selectedColor.Color;
                m_SelectedTextColorID = selectedColor.ID;
            }
            else
            {
                m_SelectedTextColorID = -1;
            }

            SetTextColorLabel();
        }

        private void SelectBackColorButton_Click(object sender, EventArgs e)
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;

            SelectColorForm form = new(colors);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ConverterColor selectedColor = ConverterColor.GetColorById(form.SelectedColorID, colors);
                BackColorTB.Color = selectedColor.Color;
                m_SelectedBackColorID = selectedColor.ID;
            }
            else
            {
                m_SelectedBackColorID = -1;
            }

            SetBackColorLabel();
        }

        private void SetTextColorLabel()
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;
            ConverterColor selectedColor = ConverterColor.GetColorById(m_SelectedTextColorID, colors);

            if (selectedColor != null)
            {
                textColorLabel.Text = $"{selectedColor.Name}";
            }
            else
            {
                textColorLabel.Text = "";
            }
        }

        private void SetBackColorLabel()
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;
            ConverterColor selectedColor = ConverterColor.GetColorById(m_SelectedBackColorID, colors);

            if (selectedColor != null)
            {
                backColorLabel.Text = $"{selectedColor.Name}";
            }
            else
            {
                backColorLabel.Text = "";
            }
        }
    }
}
