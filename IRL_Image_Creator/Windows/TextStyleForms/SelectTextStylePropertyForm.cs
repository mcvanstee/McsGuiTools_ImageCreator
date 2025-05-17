using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using System.ComponentModel;

namespace IRL_Image_Creator.Windows
{
    public partial class SelectTextStylePropertyForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PropertyName { get; set; } = string.Empty;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ValueName { get; set; } = string.Empty;

        public SelectTextStylePropertyForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (Property property in MainForm.Instance.Properties)
            {
                if (property.Use)
                {
                    string[] row = { property.Name, property.Alias };
                    ListViewItem item = new(row)
                    {
                        Tag = property
                    };

                    PropertyListView.Items.Add(item);
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection propertyIndices = PropertyListView.SelectedIndices;
            ListView.SelectedIndexCollection valueIndices = ValueListView.SelectedIndices;

            if (propertyIndices.Count > 0 && valueIndices.Count > 0)
            {
                int propertyIndex = propertyIndices[0];
                int valueIndex = valueIndices[0];

                PropertyName = PropertyListView.Items[propertyIndex].Text;
                ValueName = ValueListView.Items[valueIndex].Text;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void PropertyListView_MouseClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection indices = PropertyListView.SelectedIndices;
            ValueListView.Items.Clear();

            if (indices.Count > 0)
            {
                int selectedIndex = indices[0];

                string propertyName = PropertyListView.Items[selectedIndex].Text;

                foreach (Property property in MainForm.Instance.Properties)
                {
                    if (property.Name == propertyName)
                    {
                        foreach (PropertyValue value in property.PropertyValues)
                        {
                            string[] row = { value.Value, value.Name };
                            ListViewItem item = new(row)
                            {
                                Tag = value
                            };

                            ValueListView.Items.Add(item);
                        }
                    }
                }
            }
        }
    }
}
