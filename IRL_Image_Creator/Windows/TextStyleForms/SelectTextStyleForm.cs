using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;
using System.ComponentModel;
using System.Data;
using System.Globalization;

namespace IRL_Image_Creator.Windows.TextStyleForms
{
    public partial class SelectTextStyleForm : Form
    {
        private readonly Project m_Project;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> SelectedTextStyleIDs { get; private set; } = new List<int>();

        public SelectTextStyleForm(Project project)
        {
            m_Project = project;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshTextStyleListView();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (TextStyleListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a text style.", "No Text Style Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedTextStyleIDs = TextStyleListView.SelectedItems.Cast<ListViewItem>().Select(item => ((TextStyle)item.Tag).ID).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RefreshTextStyleListView()
        {
            TextStyleListView.Items.Clear();

            foreach (TextStyle textStyle in m_Project.TextStyles)
            {
                string propertyRowContent = "";

                foreach (BitmapProperty property in textStyle.BitmapProperties)
                {
                    if (string.IsNullOrEmpty(property.Description))
                    {
                        propertyRowContent += $"({property.Name}, {property.Value}) ";
                    }
                    else
                    {
                        propertyRowContent += $"({property.Description}, {property.Value}) ";
                    }
                }

                string marginRowContent = $"({textStyle.Margin.Left}; {textStyle.Margin.Top}; {textStyle.Margin.Right}; {textStyle.Margin.Bottom};)";

                string[] row = { textStyle.Description, textStyle.FontName,
                    textStyle.FontSize.ToString(CultureInfo.InvariantCulture), textStyle.FontStyle.ToString(),
                    CommonUtils.ColorToHexString(textStyle.TextColor), CommonUtils.ColorToHexString(textStyle.BackColor),
                    marginRowContent, propertyRowContent, textStyle.Prefix };

                ListViewItem listViewItem = new(row);
                listViewItem.Tag = textStyle;
                TextStyleListView.Items.Add(listViewItem);
            }
        }
    }
}
