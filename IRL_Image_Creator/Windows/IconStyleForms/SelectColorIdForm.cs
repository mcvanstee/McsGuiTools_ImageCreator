using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Image_Creator.Windows.ColorForms;

namespace IRL_Image_Creator.Windows
{
    public partial class SelectColorIdForm : Form
    {
        private readonly ImageSvgColorId m_IconColorId;
        private int m_SelectedColorID = -1;

        public SelectColorIdForm(ImageSvgColorId iconColorId)
        {
            InitializeComponent();
            m_IconColorId = iconColorId;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            idTextBox.Text = m_IconColorId.Id;
            m_SelectedColorID = m_IconColorId.ConverterColorId;
            colorTextBox.Color = m_IconColorId.Color;

            SetColorLabel();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("Id cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;

            m_IconColorId.Id = idTextBox.Text;
            m_IconColorId.Color = colorTextBox.Color;
            m_IconColorId.ConverterColorId = m_SelectedColorID;

            Close();
        }

        private void SelectColorButton_Click(object sender, EventArgs e)
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;

            SelectColorForm form = new(colors);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ConverterColor selectedColor = ConverterColor.GetColorById(form.SelectedColorID, colors);
                colorTextBox.Color = selectedColor.Color;
                m_SelectedColorID = form.SelectedColorID;
            }
            else
            {
                m_SelectedColorID = -1;
            }

            SetColorLabel() ;
        }

        private void SetColorLabel()
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;
            ConverterColor selectedColor = ConverterColor.GetColorById(m_SelectedColorID, colors);

            if (selectedColor != null)
            {
                colorLabel.Text = $"{selectedColor.Name}";
            }
            else
            {
                colorLabel.Text = "";
            }
        }
    }
}
