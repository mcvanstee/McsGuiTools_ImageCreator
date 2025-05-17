using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using System.ComponentModel;

namespace IRL_Image_Creator.Windows.IconStyleForms
{
    public partial class SelectIconStyleForm : Form
    {
        private readonly Project m_Project;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<int> SelectedIconStyleIDs { get; private set; } = new List<int>();

        public SelectIconStyleForm(Project project)
        {
            InitializeComponent();
            m_Project = project;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshIconStyleListView();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (IconStyleListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an icon style.", "No Icon Style Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedIconStyleIDs = IconStyleListView.SelectedItems.Cast<ListViewItem>().Select(item => ((IconStyle)item.Tag).ID).ToList();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RefreshIconStyleListView()
        {
            IconStyleListView.Items.Clear();

            foreach (IconStyle iconStyle in m_Project.IconStyles)
            {
                string margin = $"({iconStyle.Margin.Left} {iconStyle.Margin.Top} {iconStyle.Margin.Right} {iconStyle.Margin.Bottom})";

                string[] row =
                {
                    iconStyle.Name,
                    iconStyle.Prefix,
                    CommonUtils.ColorToHexString(iconStyle.BackColor),
                    GetIconColorRowContent(iconStyle),
                    margin,
                    GetPropertyRowContent(iconStyle)
                };

                ListViewItem item = new(row)
                {
                    Tag = iconStyle
                };
                IconStyleListView.Items.Add(item);
            }

            IconStyleListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private static string GetIconColorRowContent(IconStyle iconStyle)
        {
            string colors = "";

            foreach (ImageSvgColorId svgColor in iconStyle.SvgColors)
            {
                colors += $"({svgColor.Id}, {CommonUtils.ColorToHexString(svgColor.Color)})";
            }

            return colors;
        }

        private static string GetPropertyRowContent(IconStyle iconStyle)
        {
            string propertyRowContent = "";

            foreach (BitmapProperty property in iconStyle.BitmapProperties)
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

            return propertyRowContent;
        }
    }
}
