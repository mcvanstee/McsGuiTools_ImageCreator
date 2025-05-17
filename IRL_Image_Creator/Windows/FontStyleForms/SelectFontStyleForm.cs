using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;
using System.ComponentModel;
using System.Data;

namespace IRL_Image_Creator.Windows.FontStyleForms
{
    public partial class SelectFontStyleForm : Form
    {
        private readonly Project m_Project;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> SelectedFontStyleIDs { get; private set; } = new List<int>();

        public SelectFontStyleForm(Project project)
        {
            InitializeComponent();
            m_Project = project;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshFontStyleListView();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (FontStyleListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a font style.", "No Font Style Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedFontStyleIDs = FontStyleListView.SelectedItems.Cast<ListViewItem>().Select(item => ((FontBitmapStyle)item.Tag).ID).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RefreshFontStyleListView()
        {
            FontStyleListView.Items.Clear();

            foreach (FontBitmapStyle fontStyle in m_Project.FontBitmapStyles)
            {
                string marginRowContent = $"({fontStyle.Margin.Left}; {fontStyle.Margin.Top}; {fontStyle.Margin.Right}; {fontStyle.Margin.Bottom};)";
                string monospaceRowContent = fontStyle.MonospaceNumbers ? "Yes" : "No";

                string[] row =
                {
                    fontStyle.Name,
                    CommonUtils.ColorToHexString(fontStyle.TextColor),
                    CommonUtils.ColorToHexString(fontStyle.BackColor),
                    marginRowContent,
                    monospaceRowContent
                };

                ListViewItem item = new(row);
                item.Tag = fontStyle;
                FontStyleListView.Items.Add(item);
            }
        }
    }
}
