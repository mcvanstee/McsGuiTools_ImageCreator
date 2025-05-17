using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Common_Library.Utils;

namespace IRL_Image_Creator.Windows.IconStyleForms
{
    public partial class EditIconNameForm : Form
    {
        private readonly SvgFileInfo m_svgFileInfo;

        public EditIconNameForm(SvgFileInfo svgFileInfo)
        {
            InitializeComponent();
            m_svgFileInfo = svgFileInfo;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            string iconName = IconNameTextBox.Text;

            if (FileUtils.IsValidFileName(iconName))
            {
                m_svgFileInfo.IconName = iconName;

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Invalid icon name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
