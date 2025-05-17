using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;

namespace Gui_Image_Builder
{
    public partial class EditPropertyNamesForm : Form
    {
        private readonly ImageBuilderSettings m_imageBuilderSettings;

        public EditPropertyNamesForm(ImageBuilderSettings imageBuilderSettings)
        {
            m_imageBuilderSettings = imageBuilderSettings;
            InitializeComponent();

            InitNameTextBoxes();
            InitCheckBoxes();
        }

        private void InitNameTextBoxes()
        {
            int index = 0;
            PropertyNameA_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameB_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameC_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameD_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameE_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameF_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameG_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameH_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;

            PropertyNameI_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameJ_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameK_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameL_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameM_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameN_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameO_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
            PropertyNameP_TB.Text = m_imageBuilderSettings.Properties[index++].Alias;
        }

        private void InitCheckBoxes()
        {
            int index = 0;
            UseProperty_A_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_B_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_C_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_D_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_E_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_F_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_G_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_H_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;

            UseProperty_I_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_J_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_K_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_L_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_M_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_N_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_O_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
            UseProperty_P_CB.Checked = m_imageBuilderSettings.Properties[index++].Use;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (ArePropertyNameUnique())
            {
                int index = 0;
                SetPropertyName(index++, PropertyNameA_TB.Text);
                SetPropertyName(index++, PropertyNameB_TB.Text);
                SetPropertyName(index++, PropertyNameC_TB.Text);
                SetPropertyName(index++, PropertyNameD_TB.Text);
                SetPropertyName(index++, PropertyNameE_TB.Text);
                SetPropertyName(index++, PropertyNameF_TB.Text);
                SetPropertyName(index++, PropertyNameG_TB.Text);
                SetPropertyName(index++, PropertyNameH_TB.Text);

                SetPropertyName(index++, PropertyNameI_TB.Text);
                SetPropertyName(index++, PropertyNameJ_TB.Text);
                SetPropertyName(index++, PropertyNameK_TB.Text);
                SetPropertyName(index++, PropertyNameL_TB.Text);
                SetPropertyName(index++, PropertyNameM_TB.Text);
                SetPropertyName(index++, PropertyNameN_TB.Text);
                SetPropertyName(index++, PropertyNameO_TB.Text);
                SetPropertyName(index++, PropertyNameP_TB.Text);

                index = 0;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_A_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_B_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_C_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_D_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_E_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_F_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_G_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_H_CB.Checked;

                m_imageBuilderSettings.Properties[index++].Use = UseProperty_I_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_J_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_K_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_L_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_M_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_N_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_O_CB.Checked;
                m_imageBuilderSettings.Properties[index++].Use = UseProperty_P_CB.Checked;

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter unique property names. (case insensitive)", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditValuesPropertyAButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(0);
        }

        private void EditValuesPropertyBButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(1);
        }

        private void EditValuesPropertyCButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(2);
        }

        private void EditValuesPropertyDButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(3);
        }

        private void EditValuesPropertyEButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(4);
        }

        private void EditValuesPropertyFButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(5);
        }

        private void EditValuesPropertyGButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(6);
        }

        private void EditValuesPropertyHButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(7);
        }

        private void EditValuesPropertyIButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(8);
        }

        private void EditValuesPropertyJButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(9);
        }

        private void EditValuesPropertyKButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(10);
        }

        private void EditValuesPropertyLButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(11);
        }

        private void EditValuesPropertyMButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(12);
        }

        private void EditValuesPropertyNButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(13);
        }

        private void EditValuesPropertyOButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(14);
        }

        private void EditValuesPropertyPButton_Click(object sender, EventArgs e)
        {
            ShowEditPropertyValuesForm(15);
        }

        private void ShowEditPropertyValuesForm(int index)
        {
            EditPropertyValuesForm valuesForm = new(m_imageBuilderSettings, index);
            valuesForm.StartPosition = FormStartPosition.CenterParent;
            valuesForm.ShowDialog(this);
        }

        private bool ArePropertyNameUnique()
        {
            List<string> propertyNames = new()
            {
                PropertyNameA_TB.Text.ToUpper(),
                PropertyNameB_TB.Text.ToUpper(),
                PropertyNameC_TB.Text.ToUpper(),
                PropertyNameD_TB.Text.ToUpper(),
                PropertyNameE_TB.Text.ToUpper(),
                PropertyNameF_TB.Text.ToUpper(),
                PropertyNameG_TB.Text.ToUpper(),
                PropertyNameH_TB.Text.ToUpper(),
                PropertyNameI_TB.Text.ToUpper(),
                PropertyNameJ_TB.Text.ToUpper(),
                PropertyNameK_TB.Text.ToUpper(),
                PropertyNameL_TB.Text.ToUpper(),
                PropertyNameM_TB.Text.ToUpper(),
                PropertyNameN_TB.Text.ToUpper(),
                PropertyNameO_TB.Text.ToUpper(),
                PropertyNameP_TB.Text.ToUpper()
            };

            return propertyNames.Distinct().Count() == propertyNames.Count();
        }

        private void SetPropertyName(int index, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                m_imageBuilderSettings.Properties[index].Alias = m_imageBuilderSettings.Properties[index].Name;
            }
            else
            {
                m_imageBuilderSettings.Properties[index].Alias = name;
            }
        }
    }
}
