using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using System.ComponentModel;

namespace IRL_Image_Creator.Windows.FontForms
{
    public partial class SelectFontForm : Form
    {
        private readonly List<ConverterFont> m_fonts;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedFontID { get; private set; } = -1;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string FontName { get; private set; } = "";
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float FontSize { get; private set; } = 12;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FontStyle FontStyle { get; private set; } = FontStyle.Regular;

        public SelectFontForm(string fontName, float fontSize, FontStyle fontStyle, List<ConverterFont> fonts)
        {
            InitializeComponent();
            FontName = fontName;
            FontSize = fontSize;
            FontStyle = fontStyle;
            m_fonts = fonts;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshFontListView();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CustomFontButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(FontName))
            {
                fontDialog.Font = new Font(FontName, FontSize, FontStyle, GraphicsUnit.Pixel);
            }

            DialogResult result = fontDialog.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                FontName = fontDialog.Font.Name;
                FontSize = (float)Math.Round(fontDialog.Font.Size);
                FontStyle = fontDialog.Font.Style;

                SelectedFontID = -1;

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (FontsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a font.", "No Font Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ConverterFont selectedFont = (ConverterFont)FontsListView.SelectedItems[0].Tag;

            SelectedFontID = selectedFont.ID;
            FontName = selectedFont.FontName;
            FontSize = selectedFont.FontSize;
            FontStyle = selectedFont.FontStyle;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void RefreshFontListView()
        {
            FontsListView.Items.Clear();

            foreach (ConverterFont font in m_fonts)
            {
                string[] row =
                {
                    font.Name,
                    font.FontName,
                    font.FontSize.ToString(),
                    font.FontStyle.ToString()
                };

                ListViewItem item = new(row)
                {
                    Tag = font
                };
                FontsListView.Items.Add(item);
            }

            FontsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
