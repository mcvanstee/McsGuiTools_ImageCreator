using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;

namespace IRL_Image_Creator.Windows.FontStyleForms
{
    public partial class FontStyleForm : Form
    {
        private readonly Project m_Project;

        public FontStyleForm(Project project)
        {
            m_Project = project;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshFontStyleListView();
            SetButtonEnabled();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int id = FontBitmapStyle.GetNextAvailableId(m_Project.FontBitmapStyles);
            FontBitmapStyle fontStyle = new FontBitmapStyle(id);

            EditFontStyleForm editFontStyleForm = new(fontStyle);
            editFontStyleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editFontStyleForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                m_Project.FontBitmapStyles.Add(fontStyle);
                RefreshFontStyleListView();
                Project.Save(m_Project);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (FontStyleListView.SelectedItems.Count == 0)
            {
                return;
            }

            FontBitmapStyle fontStyle = (FontBitmapStyle)FontStyleListView.SelectedItems[0].Tag;

            EditFontStyleForm editFontStyleForm = new(fontStyle);
            editFontStyleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editFontStyleForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                RefreshFontStyleListView();
                Project.Save(m_Project);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (FontStyleListView.SelectedItems.Count == 0)
            {
                return;
            }

            FontBitmapStyle fontStyle = (FontBitmapStyle)FontStyleListView.SelectedItems[0].Tag;

            m_Project.FontBitmapStyles.Remove(fontStyle);
            RefreshFontStyleListView();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
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

            FontStyleListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void FontStyleListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonEnabled();
        }

        private void SetButtonEnabled()
        {
            if (FontStyleListView.SelectedItems.Count == 0)
            {
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
            {
                EditButton.Enabled = true;
                DeleteButton.Enabled = true;

            }
        }
    }
}
