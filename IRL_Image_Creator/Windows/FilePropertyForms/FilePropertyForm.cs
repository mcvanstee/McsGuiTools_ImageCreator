using Gui_Image_Builder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
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

namespace IRL_Image_Creator.Windows.FilePropertyForms
{
    public partial class FilePropertyForm : Form
    {
        private readonly Project m_project;

        public FilePropertyForm(Project project)
        {
            m_project = project;
            InitializeComponent();

            RefreshPropertiesListView();
        }

        private void RefreshPropertiesListView()
        {
            PropertiesListView.Items.Clear();

            foreach (Property property in m_project.ImageBuilderSettings.Properties)
            {
                string values = "";

                foreach (PropertyValue propertyValue in property.PropertyValues)
                {
                    values += $"({propertyValue.Name}, {propertyValue.Value}) ";
                }

                if (property.Use)
                {
                    string[] row = { property.Name, property.Alias, values };
                    ListViewItem listViewItem = new(row);
                    listViewItem.Tag = property;

                    PropertiesListView.Items.Add(listViewItem);
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            EditPropertyNamesForm editPropertyNamesForm = new(m_project.ImageBuilderSettings);
            editPropertyNamesForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editPropertyNamesForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                RefreshPropertiesListView();
                Project.Save(m_project);
            }
        }
    }
}
