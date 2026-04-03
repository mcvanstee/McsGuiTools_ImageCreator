using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Image_Creator.Projects;

namespace IRL_Image_Creator.Windows.DataLocationForms
{
    public partial class EditDataLocationForm : Form
    {
        private DataLocation m_dataLocation;
        private Project m_project;
        private Dictionary<string, string> m_locationComboboxDictionary = [];
        private Dictionary<string, string> m_compressionComboboxDictionary = [];

        public EditDataLocationForm(DataLocation dataLocation, Project project)
        {
            InitializeComponent();
            m_dataLocation = dataLocation;
            m_project = project;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            locationTypeErrorLabel.Text = "";
            compressionErrorLabel.Text = "";

            SetLocationTypeComboBoxValues();
            SetCompressionTypeComboBoxValues();
            // Initialize form fields with existing data location values
            locationIDLabel.Text = m_dataLocation.LocationID.ToString();
            nameTextBox.Text = m_dataLocation.Name;
            locationTypeComboBox.SelectedValue = m_dataLocation.DataLocationType.ToString();
            compressionComboBox.SelectedValue = m_dataLocation.CompressionType.ToString();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                // Save changes
                m_dataLocation.Name = nameTextBox.Text;
                m_dataLocation.DataLocationType = Enum.Parse<DataLocationType>(locationTypeComboBox.SelectedValue.ToString());
                m_dataLocation.CompressionType = Enum.Parse<CompressionType>(compressionComboBox.SelectedValue.ToString());

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please correct the errors in the form.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetLocationTypeComboBoxValues()
        {
            m_locationComboboxDictionary.Clear();
            //bool codeDataLocationExist = CodeDataLocationExists();
            
            //if (codeDataLocationExist && m_dataLocation.DataLocationType != DataLocationType.Code)
            //{
            //    locationTypeComboBox.Enabled = false;
            //    locationTypeErrorLabel.Text = "Only one Code Data Location is allowed.";
            //}
            m_locationComboboxDictionary.Add("Code File", "Code");
            m_locationComboboxDictionary.Add("Data File 1", "File_1");

            // Set the ComboBox data source
            locationTypeComboBox.DataSource = new BindingSource(m_locationComboboxDictionary, null);
            locationTypeComboBox.DisplayMember = "Key";
            locationTypeComboBox.ValueMember = "Value";
        }

        private void SetCompressionTypeComboBoxValues()
        {
            m_compressionComboboxDictionary.Clear();
            m_compressionComboboxDictionary.Add("None", CompressionType.None.ToString());
            m_compressionComboboxDictionary.Add("RLE", CompressionType.RLE.ToString());
            m_compressionComboboxDictionary.Add("RLE Gamma", CompressionType.RLE_Alpha.ToString());

            //if (m_dataLocation.DataLocationType == DataLocationType.Code)
            //{
            //    compressionComboBox.Enabled = false;
            //}

            // Set the ComboBox data source
            compressionComboBox.DataSource = new BindingSource(m_compressionComboboxDictionary, null);
            compressionComboBox.DisplayMember = "Key";
            compressionComboBox.ValueMember = "Value";
        }

        private bool ValidateForm()
        {
            bool valid = true;

            return valid;
        }

        private bool CodeDataLocationExists()
        {
            foreach (DataLocation dl in m_project.ImageBuilderSettings.DataLocations)
            {
                if (dl.DataLocationType == DataLocationType.Code)
                {
                    return true;
                }
            }

            return false;
        }

        private void LocationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataLocationType dataLocationType;
            try
            {
                dataLocationType = Enum.Parse<DataLocationType>(locationTypeComboBox.SelectedValue.ToString());
            }
            catch
            {
                return;
            }

            //bool codeDataLocationExist = CodeDataLocationExists();

            //if (codeDataLocationExist)
            //{
            //    locationTypeErrorLabel.Text = "Code Data Location already exists.";
            //}

            //if (dataLocationType == DataLocationType.Code)
            //{
            //    compressionComboBox.SelectedValue = CompressionType.RLE_Alpha.ToString();
            //    compressionComboBox.Enabled = false;
            //}
            //else
            //{
            //    compressionComboBox.Enabled = true;
            //}
        }

        private void CompressionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
