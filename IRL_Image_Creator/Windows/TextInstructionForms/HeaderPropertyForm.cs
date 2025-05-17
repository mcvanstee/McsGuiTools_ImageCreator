using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Builder;
using System.ComponentModel;

namespace IRL_Image_Creator.Windows.MainFormModels
{
    public partial class HeaderPropertyForm : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PropertyName { get; set; } = string.Empty;

        public HeaderPropertyForm()
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

        private void OKButton_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection propertyIndices = PropertyListView.SelectedIndices;

            if (propertyIndices.Count > 0)
            {
                int propertyIndex = propertyIndices[0];

                PropertyName = PropertyListView.Items[propertyIndex].Text;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
