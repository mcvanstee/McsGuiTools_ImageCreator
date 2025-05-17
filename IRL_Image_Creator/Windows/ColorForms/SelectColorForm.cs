using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Common_Library.Utils;
using System.ComponentModel;

namespace IRL_Image_Creator.Windows.ColorForms
{
    public partial class SelectColorForm : Form
    {
        private readonly List<ConverterColor> m_colors;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedColorID { get; private set; } = -1;

        public SelectColorForm(List<ConverterColor> colors)
        {
            InitializeComponent();
            m_colors = colors;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshColorListView();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            if (ColorsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a color.", "No Color Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ConverterColor selectedColor = (ConverterColor)ColorsListView.SelectedItems[0].Tag;

            SelectedColorID = selectedColor.ID;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void RefreshColorListView()
        {
            ColorsListView.Items.Clear();

            foreach (ConverterColor color in m_colors)
            {
                string[] row =
                {
                    color.Name,
                    CommonUtils.ColorToHexString(color.Color)
                };

                ListViewItem item = new(row)
                {
                    Tag = color
                };
                ColorsListView.Items.Add(item);
            }

            ColorsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }
}
