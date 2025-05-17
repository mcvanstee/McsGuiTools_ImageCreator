using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;

namespace IRL_Image_Creator.Windows.ColorForms
{
    public partial class ColorForm : Form
    {
        private readonly Project m_project;

        public ColorForm(Project project)
        {
            InitializeComponent();
            m_project = project;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshColorListView();
            SetButtonEnabled();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int id = ConverterColor.GetNextAvailableId(m_project.Colors);
            ConverterColor color = new(id);

            EditColorForm editColorForm = new(color);
            editColorForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editColorForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                m_project.Colors.Add(color);
                RefreshColorListView();
                Project.Save(m_project);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (ColorsListView.SelectedItems.Count == 0)
            {
                return;
            }

            ConverterColor color = (ConverterColor)ColorsListView.SelectedItems[0].Tag;

            EditColorForm editColorForm = new(color);
            editColorForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editColorForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                RefreshColorListView();
                Project.Save(m_project);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (ColorsListView.SelectedItems.Count == 0)
            {
                return;
            }

            ConverterColor color = (ConverterColor)ColorsListView.SelectedItems[0].Tag;

            if (MessageBox.Show(this, "Are you sure you want to delete this color?", "Delete Color", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                m_project.Colors.Remove(color);
                RefreshColorListView();
                Project.Save(m_project);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RefreshColorListView()
        {
            ColorsListView.Items.Clear();

            foreach (ConverterColor color in m_project.Colors)
            {
                string[] row =
                {
                    color.Name,
                    CommonUtils.ColorToHexString(color.Color)
                };

                ListViewItem item = new(row)
                {
                    Tag = color
                };
                ColorsListView.Items.Add(item);
            }

            ColorsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void ColorsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonEnabled();
        }

        private void SetButtonEnabled()
        {
            if (ColorsListView.SelectedItems.Count == 0)
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
