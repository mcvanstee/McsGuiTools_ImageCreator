using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Image_Creator.Windows.ColorForms;
using IRL_Image_Creator.Windows.FontForms;

namespace IRL_Image_Creator.Windows.TextStyleForms
{
    public partial class EditTextStyleForm : Form
    {
        private readonly TextStyle m_TextStyle;
        private int m_SelectedTextColorID = -1;
        private int m_SelectedBackColorID = -1;
        private int m_SelectedFontID = -1;

        public EditTextStyleForm(TextStyle textStyle)
        {
            m_TextStyle = textStyle;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            NameTB.Text = m_TextStyle.Description;
            PrefixTB.Text = m_TextStyle.Prefix;
            TextColorTB.Color = m_TextStyle.TextColor;
            BackColorTB.Color = m_TextStyle.BackColor;
            m_SelectedTextColorID = m_TextStyle.TextConverterColorId;
            m_SelectedBackColorID = m_TextStyle.BackConverterColorId;
            m_SelectedFontID = m_TextStyle.FontConverterId;
            LeftMarginTB.Text = m_TextStyle.Margin.Left.ToString();
            TopMarginTB.Text = m_TextStyle.Margin.Top.ToString();
            RightMarginTB.Text = m_TextStyle.Margin.Right.ToString();
            BottomMarginTB.Text = m_TextStyle.Margin.Bottom.ToString();

            RefreshTextStylePropertiesListView();
            SetSelctedFontLabel();
            SetTextColorLabel();
            SetBackColorLabel();
        }

        private void SelectFontButton_Click(object sender, EventArgs e)
        {
            SelectFontForm form = new(
                m_TextStyle.FontName, m_TextStyle.FontSize, m_TextStyle.FontStyle,
                MainForm.Instance.ConverterFonts);
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                m_TextStyle.FontName = form.FontName;
                m_TextStyle.FontSize = form.FontSize;
                m_TextStyle.FontStyle = form.FontStyle;
                m_SelectedFontID = form.SelectedFontID;

                SetSelctedFontLabel();
            }
        }

        private void SetSelctedFontLabel()
        {
            if (string.IsNullOrEmpty(m_TextStyle.FontName))
            {
                SelectedFontLabel.Text = "No font selected";
            }
            else if (m_SelectedFontID != -1)
            {
                ConverterFont converterFont = ConverterFont.GetFontById(m_SelectedFontID, MainForm.Instance.ConverterFonts);

                SelectedFontLabel.Text = $"{m_TextStyle.FontName} {m_TextStyle.FontSize} {m_TextStyle.FontStyle} (Project Font: {converterFont.Name})";
            }
            else
            {
                SelectedFontLabel.Text = $"{m_TextStyle.FontName} {m_TextStyle.FontSize} {m_TextStyle.FontStyle}";
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
                MessageBox.Show(this, "Name is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(m_TextStyle.FontName))
            {
                MessageBox.Show(this, "Font is not selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetTextStyleValues();
            DialogResult = DialogResult.OK;

            Close();
        }

        private void SetTextStyleValues()
        {
            m_TextStyle.Description = NameTB.Text;
            m_TextStyle.Prefix = PrefixTB.Text;
            m_TextStyle.TextColor = TextColorTB.Color;
            m_TextStyle.BackColor = BackColorTB.Color;
            m_TextStyle.TextConverterColorId = m_SelectedTextColorID;
            m_TextStyle.BackConverterColorId = m_SelectedBackColorID;
            m_TextStyle.FontConverterId = m_SelectedFontID;
            m_TextStyle.Margin.Left = int.Parse(LeftMarginTB.Text);
            m_TextStyle.Margin.Top = int.Parse(TopMarginTB.Text);
            m_TextStyle.Margin.Right = int.Parse(RightMarginTB.Text);
            m_TextStyle.Margin.Bottom = int.Parse(BottomMarginTB.Text);
        }

        private void AddTextStylePropertyButton_Click(object sender, EventArgs e)
        {
            SelectTextStylePropertyForm form = new();
            form.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = form.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string propertyName = form.PropertyName;
                string valueName = form.ValueName;

                bool propertyExists = false;
                foreach (BitmapProperty propertyValue in m_TextStyle.BitmapProperties)
                {
                    if (propertyValue.Name == propertyName)
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
                        m_TextStyle.BitmapProperties.Add(new BitmapProperty(propertyName, value));
                        RefreshTextStylePropertiesListView();
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                        throw;
                    }
                }
            }
        }

        private void DeleteTextStylePropertyButton_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = TextStylePropertiesListView.SelectedIndices;

            if (indices.Count > 0)
            {
                int selectedIndex = indices[0];
                m_TextStyle.BitmapProperties.RemoveAt(selectedIndex);
                RefreshTextStylePropertiesListView();
            }
        }

        private void RefreshTextStylePropertiesListView()
        {
            TextStylePropertiesListView.Items.Clear();

            foreach (BitmapProperty propertyValue in m_TextStyle.BitmapProperties)
            {
                string[] row = 
                { 
                    propertyValue.Name, 
                    propertyValue.Value.ToString()
                };
                ListViewItem item = new(row)
                {
                    Tag = propertyValue
                };

                TextStylePropertiesListView.Items.Add(item);
            }
        }

        private void SelectTextColorButton_Click(object sender, EventArgs e)
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;

            SelectColorForm form = new(colors);
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog(this);

            if (form.DialogResult == DialogResult.OK)
            {
                ConverterColor selectedColor = ConverterColor.GetColorById(form.SelectedColorID, colors);
                TextColorTB.Color = selectedColor.Color;
                m_SelectedTextColorID = selectedColor.ID;
            }
            else
            {
                m_SelectedTextColorID = -1;
            }

            SetTextColorLabel();
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

        private void SetTextColorLabel()
        {
            List<ConverterColor> colors = MainForm.Instance.ConverterColor;
            ConverterColor selectedColor = ConverterColor.GetColorById(m_SelectedTextColorID, colors);

            if (selectedColor != null)
            {
                textColorLabel.Text = $"{selectedColor.Name}";
            }
            else
            {
                textColorLabel.Text = "";
            }
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
