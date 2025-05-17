using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Image_Creator.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRL_Image_Creator.Windows.FontForms
{
    public partial class FontForm : Form
    {
        private readonly Project m_project;

        public FontForm(Project project)
        {
            InitializeComponent();
            m_project = project;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshFontListView();
            SetButtonEnabled();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int id = ConverterFont.GetNextAvailableId(m_project.Fonts);
            ConverterFont font = new(id);

            EditFontForm editFontForm = new(font);
            editFontForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editFontForm.ShowDialog(this);

            if (result == DialogResult.OK) 
            {
                m_project.Fonts.Add(font);
                RefreshFontListView();
                Project.Save(m_project);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (FontsListView.SelectedItems.Count == 0)
            {
                return;
            }

            ConverterFont font = (ConverterFont)FontsListView.SelectedItems[0].Tag;

            EditFontForm editFontForm = new(font);
            editFontForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editFontForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                RefreshFontListView();
                Project.Save(m_project);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (FontsListView.SelectedItems.Count == 0)
            {
                return;
            }

            ConverterFont font = (ConverterFont)FontsListView.SelectedItems[0].Tag;

            if (MessageBox.Show(this, "Are you sure you want to delete this font?", "Delete Font", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                m_project.Fonts.Remove(font);
                RefreshFontListView();
                Project.Save(m_project);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RefreshFontListView()
        {
            FontsListView.Items.Clear();

            foreach (ConverterFont font in m_project.Fonts)
            {
                string[] row =
                {
                    font.Name,
                    font.FontName,
                    font.FontSize.ToString(),
                    font.FontStyle.ToString()
                };

                ListViewItem item = new(row)
                {
                    Tag = font
                };
                FontsListView.Items.Add(item);
            }

            FontsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void FontsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonEnabled();
        }

        private void SetButtonEnabled()
        {
            if (FontsListView.SelectedItems.Count == 0)
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
