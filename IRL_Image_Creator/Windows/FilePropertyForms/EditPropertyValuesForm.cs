using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;

namespace Gui_Image_Builder
{
    public partial class EditPropertyValuesForm : Form
    {
        private const int ColomOffsetSpaces = 10;
        private readonly ImageBuilderSettings m_ImageBuilderSettings;
        private readonly int m_propertyIndex;
        private int m_textBoxIndex = 0;
        private int m_selectedIndex = -1;

        public EditPropertyValuesForm(ImageBuilderSettings imageBuilderSettings, int propertyIndex)
        {
            m_ImageBuilderSettings = imageBuilderSettings;
            m_propertyIndex = propertyIndex;

            InitializeComponent();
        }

        private void EditPropertyValuesForm_Load(object sender, EventArgs e)
        {
            Property property = m_ImageBuilderSettings.Properties[m_propertyIndex];
            PropertyNameA_TB.Text = property.Max.ToString();

            for (int i = 0; i < property.Max; i++)
            {
                string propertyName = property.PropertyValues[i].Name;

                if (string.IsNullOrEmpty(propertyName))
                {
                    propertyName = i.ToString();
                }

                listBox.Items.Add(GetDisplayValue(i, propertyName));
            }

            listBox.SelectedIndex = 0;
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            int itemSelected = listBox.SelectedIndex;
            m_textBoxIndex = itemSelected;
            string itemText = listBox.Items[itemSelected].ToString();
            string valueText = itemText.Remove(0, ColomOffsetSpaces);

            Rectangle r = listBox.GetItemRectangle(itemSelected);

            textBox.Location = new Point((r.X + 5), (r.Y + 20));
            textBox.Size = new Size(r.Width + 4, r.Height - 10);
            textBox.Visible = true;
            textBox.Text = valueText;
            textBox.Focus();
            textBox.SelectAll();
        }

        private void UpdateValue()
        {
            if (m_textBoxIndex != -1 && !string.IsNullOrEmpty(textBox.Text))
            {
                if (ArePropertyValueNamesUnique(m_textBoxIndex, textBox.Text))
                {
                    listBox.Items[m_textBoxIndex] = GetDisplayValue(m_textBoxIndex, textBox.Text);

                    if (m_textBoxIndex < m_ImageBuilderSettings.Properties[m_propertyIndex].Max)
                    {
                        m_ImageBuilderSettings.Properties[m_propertyIndex].PropertyValues[m_textBoxIndex].Name = textBox.Text;
                    }
                }
                else
                {
                    MessageBox.Show("Please enter unique property names. (case insensitive)", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((m_selectedIndex != listBox.SelectedIndex) && (listBox.SelectedIndex != -1))
            {
                m_selectedIndex = listBox.SelectedIndex;
            }

            if (textBox.Visible && (m_selectedIndex != m_textBoxIndex))
            {
                UpdateValue();
                textBox.Visible = false;
                m_textBoxIndex = -1;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                UpdateValue();
                textBox.Visible = false;
            }

            if (e.KeyChar == 27)
            {
                textBox.Visible = false;
            }
        }

        private void PropertyNameA_TB_TextChanged(object sender, EventArgs e)
        {

        }

        private void PropertyNameA_TB_MouseClick(object sender, MouseEventArgs e)
        {
            if (!textBox.Focused && textBox.Visible)
            {
                textBox.Visible = false;
                m_textBoxIndex = -1;
            }
        }

        private void PropertyNameA_TB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                try
                {
                    int value = int.Parse(PropertyNameA_TB.Text);

                    if (value < 2 || value > 254)
                    {
                        errorLabel1.Visible = true;
                    }
                    else
                    {
                        m_ImageBuilderSettings.Properties[m_propertyIndex].Max = value;
                        errorLabel1.Visible = false;
                        UpdatePropertyValues();
                    }
                }
                catch 
                {
                    PropertyNameA_TB.Text = m_ImageBuilderSettings.Properties[m_propertyIndex].Max.ToString();
                    errorLabel1.Visible = true;
                }
            }

            if (e.KeyChar == 27)
            {

            }
        }

        private void UpdatePropertyValues()
        {
            Property property = m_ImageBuilderSettings.Properties[m_propertyIndex];
            int max = property.Max;
            int listBoxCount = listBox.Items.Count; 

            if (listBoxCount < max)
            {
                int itemsToAdd = max - listBoxCount;
                for (int i = 0; i < itemsToAdd; i++)
                {
                    string indexStr = (listBoxCount + i).ToString();
                    property.PropertyValues.Add(new PropertyValue(indexStr, indexStr));
                }

                listBox.Items.Clear();

                for (int i = 0; i < property.Max; i++)
                {
                    string propertyName = property.PropertyValues[i].Name;

                    if (string.IsNullOrEmpty(propertyName))
                    {
                        propertyName = i.ToString();
                    }

                    listBox.Items.Add(GetDisplayValue(i, propertyName));
                }

                listBox.SelectedIndex = 0;
            }
            else if (listBoxCount > max) 
            {
                int itemsToDelete = listBoxCount - max;
                for (int i = 0; i < itemsToDelete; i++)
                {
                    property.PropertyValues.RemoveAt(max);
                    listBox.Items.RemoveAt(max);
                }
            }
            else
            {
            }
        }

        private string GetDisplayValue(int value, string propertyName)
        {
            string columnOffsetSpaces = "";
            
            for (int i = 0; i < ColomOffsetSpaces - 3; i++)
            {
                columnOffsetSpaces += " ";
            }

            return value.ToString("000") + columnOffsetSpaces + propertyName;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool ArePropertyValueNamesUnique(int index, string newName)
        {
            List<string> names = new List<string>();

            for (int i = 0; i < m_ImageBuilderSettings.Properties[m_propertyIndex].PropertyValues.Count; i++) 
            {
                if (i == index) 
                {
                    names.Add(newName.ToUpper());
                }
                else
                {
                    names.Add(m_ImageBuilderSettings.Properties[m_propertyIndex].PropertyValues[i].Name.ToUpper());
                }
            }

            return names.Distinct().Count() == names.Count();
        }
    }
}
