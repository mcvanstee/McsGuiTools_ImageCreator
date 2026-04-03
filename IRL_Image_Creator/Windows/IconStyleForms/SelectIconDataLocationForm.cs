using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Image_Creator.Projects;

namespace IRL_Image_Creator.Windows.IconStyleForms
{
    public partial class SelectIconDataLocationForm : Form
    {
        private readonly Project m_project;
        public int SelectedDataLocationId { get; private set; } = -1;
        private Dictionary<string, string> m_dataLocationComboBoxDictionary = [];

        public SelectIconDataLocationForm(Project project)
        {
            InitializeComponent();
            m_project = project;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateDataLocationDictionary();
            fontDataLocationComboBox.DataSource = new BindingSource(m_dataLocationComboBoxDictionary, null);
            fontDataLocationComboBox.DisplayMember = "Value";
            fontDataLocationComboBox.ValueMember = "Key";
            if (fontDataLocationComboBox.Items.Count > 0)
            {
                fontDataLocationComboBox.SelectedIndex = 0;
            }
        }

        private void FontDataLocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int value = int.Parse(fontDataLocationComboBox.SelectedValue.ToString());
                SelectedDataLocationId = value;
            }
            catch (Exception)
            {
                SelectedDataLocationId = -1;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UpdateDataLocationDictionary()
        {
            m_dataLocationComboBoxDictionary.Clear();

            foreach (DataLocation dataLocation in m_project.ImageBuilderSettings.DataLocations)
            {
                string locationName = "";
                if (string.IsNullOrEmpty(dataLocation.Name))
                {
                    locationName = dataLocation.DataLocationType.ToString();
                }
                else
                {
                    locationName = dataLocation.Name;
                }

                m_dataLocationComboBoxDictionary.Add(
                    dataLocation.LocationID.ToString(),
                    $"{dataLocation.LocationID.ToString()} - {locationName}");
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
