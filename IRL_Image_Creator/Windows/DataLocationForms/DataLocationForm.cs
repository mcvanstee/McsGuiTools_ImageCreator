using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Image_Creator.Projects;
using IRL_Image_Creator.Windows.Helpers;

namespace IRL_Image_Creator.Windows.DataLocationForms
{
    public partial class DataLocationForm : Form
    {
        private readonly Project m_project;

        public DataLocationForm(Project project)
        {
            InitializeComponent();
            m_project = project;
            SetButtonEnabled();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshDataLocationListView();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (dataLocationListView.SelectedItems.Count == 0)
            {
                return;
            }

            DataLocation dataLocation = (DataLocation)dataLocationListView.SelectedItems[0].Tag;
            EditDataLocationForm editDataLocationForm = new(dataLocation, m_project)
            {
                StartPosition = FormStartPosition.CenterParent
            };

            DialogResult result = editDataLocationForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                RefreshDataLocationListView();
                Project.Save(m_project);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataLocationListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a data location to delete.", "No Data Location Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (dataLocationListView.Items.Count == 1)
            {
                MessageBox.Show("At least one data location must exist.", "Cannot Delete Data Location", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DataLocation dataLocation = (DataLocation)dataLocationListView.SelectedItems[0].Tag;
                m_project.ImageBuilderSettings.DataLocations.Remove(dataLocation);
                RefreshDataLocationListView();
            }
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int id = DataLocation.GetNextAvailableId(m_project.ImageBuilderSettings.DataLocations);
            DataLocation dataLocation = new(id);
            EditDataLocationForm editDataLocationForm = new(dataLocation, m_project)
            {
                StartPosition = FormStartPosition.CenterParent
            };

            DialogResult result = editDataLocationForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                m_project.ImageBuilderSettings.DataLocations.Add(dataLocation);
                m_project.ImageBuilderSettings.DataLocations.Sort((dl1, dl2) => dl1.LocationID.CompareTo(dl2.LocationID));
                RefreshDataLocationListView();
                Project.Save(m_project);
            }
        }

        private void RefreshDataLocationListView()
        {
            dataLocationListView.Items.Clear();

            foreach (DataLocation dataLocation in m_project.ImageBuilderSettings.DataLocations)
            {
                string[] row =
                {
                    dataLocation.LocationID.ToString(),
                    dataLocation.Name,
                    dataLocation.DataLocationType.ToString(),
                    dataLocation.CompressionType.ToString()
                };

                ListViewItem item = new(row)
                {
                    Tag = dataLocation
                };
                dataLocationListView.Items.Add(item);
            }

            ListViewHelper.AutoResizeColumns(dataLocationListView);
        }

        private void SetButtonEnabled()
        {
            bool itemSelected = dataLocationListView.SelectedItems.Count > 0;
            editButton.Enabled = itemSelected;
            deleteButton.Enabled = itemSelected;
        }

        private void DataLocationListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonEnabled();
        }
    }
}
