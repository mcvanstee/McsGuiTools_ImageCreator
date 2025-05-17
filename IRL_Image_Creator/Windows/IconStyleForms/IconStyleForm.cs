using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;

namespace IRL_Image_Creator.Windows.IconStyleForms
{
    public partial class IconStyleForm : Form
    {
        private readonly Project m_Project;

        public IconStyleForm(Project project)
        {
            InitializeComponent();
            m_Project = project;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshIconStyleListView();
            SetButtonEnabled();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int id = IconStyle.GetNextAvailableId(m_Project.IconStyles);
            IconStyle iconStyle = new IconStyle(id);

            EditIconStyleForm editIconStyleForm = new(iconStyle);
            editIconStyleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editIconStyleForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                m_Project.IconStyles.Add(iconStyle);
                RefreshIconStyleListView();
                Project.Save(m_Project);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (IconStyleListView.SelectedItems.Count == 0)
            {
                return;
            }

            IconStyle iconStyle = (IconStyle)IconStyleListView.SelectedItems[0].Tag;

            EditIconStyleForm editIconStyleForm = new(iconStyle);
            editIconStyleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editIconStyleForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                RefreshIconStyleListView();
                Project.Save(m_Project);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (IconStyleListView.SelectedItems.Count == 0)
            {
                return;
            }

            IconStyle iconStyle = (IconStyle)IconStyleListView.SelectedItems[0].Tag;

            m_Project.IconStyles.Remove(iconStyle);
            RefreshIconStyleListView();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
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

        private void SetButtonEnabled()
        {
            if (IconStyleListView.SelectedItems.Count == 0)
            {
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
            else
            {
                EditButton.Enabled = true;
                DeleteButton.Enabled = true;
            }
        }

        private void IconStyleListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonEnabled();
        }
    }
}
