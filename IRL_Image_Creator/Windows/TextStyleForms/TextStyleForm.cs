using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;
using IRL_Image_Creator.Windows.Helpers;

namespace IRL_Image_Creator.Windows.TextStyleForms
{
    public partial class TextStyleForm : Form
    {
        private readonly Project m_Project;
        public TextStyleForm(Project project)
        {
            m_Project = project;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshTextStyleListView();
            SetButtonEnabled();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            int id = TextStyle.GetNextAvailableId(m_Project.TextStyles);
            TextStyle textStyle = new TextStyle(id);

            EditTextStyleForm editTextStyleForm = new(textStyle);
            editTextStyleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editTextStyleForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                m_Project.TextStyles.Add(textStyle);
                RefreshTextStyleListView();
                Project.Save(m_Project);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (TextStyleListView.SelectedItems.Count == 0)
            {
                return;
            }

            TextStyle textStyle = (TextStyle)TextStyleListView.SelectedItems[0].Tag;

            EditTextStyleForm editTextStyleForm = new(textStyle);
            editTextStyleForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editTextStyleForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                RefreshTextStyleListView();
                Project.Save(m_Project);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (TextStyleListView.SelectedItems.Count == 0)
            {
                return;
            }

            TextStyle textStyle = (TextStyle)TextStyleListView.SelectedItems[0].Tag;

            m_Project.TextStyles.Remove(textStyle);
            RefreshTextStyleListView();
        }

        private void RefreshTextStyleListView()
        {
            TextStyleListView.Items.Clear();

            foreach (TextStyle textStyle in m_Project.TextStyles)
            {
                string propertyRowContent = GetPropertyRowContent(textStyle);
                string font = $"{textStyle.FontName}, {textStyle.FontSize}, {textStyle.FontStyle}";
                string marginRowContent = $"({textStyle.Margin.Left}; {textStyle.Margin.Top}; {textStyle.Margin.Right}; {textStyle.Margin.Bottom};)";
                string converterFontName = FontInstructionHelper.GetConverterFontName(m_Project, textStyle.FontConverterId);

                string[] row = 
                { 
                    textStyle.Description, 
                    textStyle.Prefix,
                    converterFontName,
                    font,
                    CommonUtils.ColorToHexString(textStyle.TextColor), 
                    CommonUtils.ColorToHexString(textStyle.BackColor),
                    marginRowContent, 
                    propertyRowContent, 
                };

                ListViewItem listViewItem = new(row);
                listViewItem.Tag = textStyle;
                TextStyleListView.Items.Add(listViewItem);
            }

            TextStyleListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private static string GetPropertyRowContent(TextStyle textStyle)
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

            return propertyRowContent;
        }

        private void TextStyleListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtonEnabled();
        }

        private void SetButtonEnabled()
        {
            if (TextStyleListView.SelectedItems.Count == 0)
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
    }
}
