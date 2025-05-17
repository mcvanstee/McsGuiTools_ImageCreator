using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Windows.ColorForms;

namespace IRL_Image_Creator.Windows.IconStyleForms
{
    public partial class EditIconStyleForm : Form
    {
        private readonly IconStyle m_IconStyle;
        private int m_SelectedBackColorID = -1;

        public EditIconStyleForm(IconStyle iconStyle)
        {
            InitializeComponent();
            m_IconStyle = iconStyle;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            RefreshPropertiesListView();
            RefreshColorsListView();

            NameTB.Text = m_IconStyle.Name;
            PrefixTB.Text = m_IconStyle.Prefix;
            BackColorTB.Color = m_IconStyle.BackColor;
            m_SelectedBackColorID = m_IconStyle.BackConverterColorId;
            LeftMarginTB.Text = m_IconStyle.Margin.Left.ToString();
            TopMarginTB.Text = m_IconStyle.Margin.Top.ToString();
            RightMarginTB.Text = m_IconStyle.Margin.Right.ToString();
            BottomMarginTB.Text = m_IconStyle.Margin.Bottom.ToString();

            SetBackColorLabel();
        }


        private void AddPropertyButton_Click(object sender, EventArgs e)
        {
            SelectTextStylePropertyForm form = new();
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string propertyName = form.PropertyName;
                string valueName = form.ValueName;

                bool propertyExists = false;
                foreach (BitmapProperty property in m_IconStyle.BitmapProperties)
                {
                    if (property.Name == propertyName)
                    {
                        propertyExists = true;
                        break;
                    }
                }

                if (propertyExists)
                {
                    MessageBox.Show(this, "Property already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        int value = int.Parse(valueName);
                        m_IconStyle.BitmapProperties.Add(new BitmapProperty(propertyName, value));
                        RefreshPropertiesListView();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                }
            }
        }

        private void DeletePropertyButton_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = PropertiesListView.SelectedIndices;

            if (indices.Count > 0)
            {
                foreach (ListViewItem listViewItem in indices)
                {
                    BitmapProperty property = (BitmapProperty)listViewItem.Tag;
                    m_IconStyle.BitmapProperties.Remove(property);
                }

                RefreshPropertiesListView();
            }
        }

        private void AddColorButton_Click(object sender, EventArgs e)
        {
            ImageSvgColorId newColor = new ImageSvgColorId("", System.Drawing.Color.White);
            SelectColorIdForm form = new(newColor);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string id = newColor.Id;

                bool idExists = false;
                foreach (ImageSvgColorId svgColor in m_IconStyle.SvgColors)
                {
                    if (svgColor.Id == id)
                    {
                        idExists = true;
                        break;
                    }
                }

                if (idExists)
                {
                    MessageBox.Show(this, "Id already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    m_IconStyle.SvgColors.Add(newColor);
                    RefreshColorsListView();
                }
            }
        }

        private void EditColorButton_Click(object sender, EventArgs e)
        {
            if (ColorListView.SelectedIndices.Count > 0)
            {
                ImageSvgColorId color = (ImageSvgColorId)ColorListView.SelectedItems[0].Tag;

                SelectColorIdForm form = new(color);
                form.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = form.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    RefreshColorsListView();
                }
            }
        }

        private void DeleteColorButton_Click(object sender, EventArgs e)
        {
            if (ColorListView.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem listViewItem in ColorListView.SelectedItems)
                {
                    ImageSvgColorId color = (ImageSvgColorId)listViewItem.Tag;
                    m_IconStyle.SvgColors.Remove(color);
                }

                RefreshColorsListView();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTB.Text))
            {
                MessageBox.Show(this, "Name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (m_IconStyle.SvgColors.Count == 0)
            {
                MessageBox.Show(this, "At least one color is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetIconStyleValues();
            DialogResult = DialogResult.OK;

            Close();
        }

        private void SetIconStyleValues()
        {
            m_IconStyle.Name = NameTB.Text;
            m_IconStyle.Prefix = PrefixTB.Text;
            m_IconStyle.BackColor = BackColorTB.Color;
            m_IconStyle.BackConverterColorId = m_SelectedBackColorID;
            m_IconStyle.Margin.Left = int.Parse(LeftMarginTB.Text);
            m_IconStyle.Margin.Top = int.Parse(TopMarginTB.Text);
            m_IconStyle.Margin.Right = int.Parse(RightMarginTB.Text);
            m_IconStyle.Margin.Bottom = int.Parse(BottomMarginTB.Text);
        }

        private void RefreshPropertiesListView()
        {
            PropertiesListView.Items.Clear();

            foreach (BitmapProperty property in m_IconStyle.BitmapProperties)
            {
                string[] row = { property.Name, property.Value.ToString() };
                ListViewItem item = new(row)
                {
                    Tag = property
                };

                PropertiesListView.Items.Add(item);

            }
        }

        private void RefreshColorsListView()
        {
            ColorListView.Items.Clear();

            foreach (ImageSvgColorId color in m_IconStyle.SvgColors)
            {
                string[] row = { color.Id, CommonUtils.ColorToHexString(color.Color) };
                ListViewItem item = new(row)
                {
                    Tag = color
                };

                ColorListView.Items.Add(item);
            }
        }

        private void SelectBackColorButton_Click(object sender, EventArgs e)
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;

            SelectColorForm form = new(colors);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ConverterColor selectedColor = ConverterColor.GetColorById(form.SelectedColorID, colors);
                BackColorTB.Color = selectedColor.Color;
                m_SelectedBackColorID = selectedColor.ID;
            }
            else
            {
                m_SelectedBackColorID = -1;
            }

            SetBackColorLabel();
        }

        private void SetBackColorLabel()
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;
            ConverterColor selectedColor = ConverterColor.GetColorById(m_SelectedBackColorID, colors);

            if (selectedColor != null)
            {
                backColorLabel.Text = $"{selectedColor.Name}";
            }
            else
            {
                backColorLabel.Text = "";
            }
        }
    }
}
