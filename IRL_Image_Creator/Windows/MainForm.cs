using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Bitmap_Converter_Tools.StatusUpdater;
using IRL_Common_Library.Consts;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.ImageBuilder.DataLocations;
using IRL_Gui_Image_Builder_Library.GuiImageBuilder.Properties;
using IRL_Image_Creator.Projects;
using IRL_Image_Creator.Windows.ColorForms;
using IRL_Image_Creator.Windows.DataLocationForms;
using IRL_Image_Creator.Windows.FilePropertyForms;
using IRL_Image_Creator.Windows.FontForms;
using IRL_Image_Creator.Windows.FontStyleForms;
using IRL_Image_Creator.Windows.Helpers;
using IRL_Image_Creator.Windows.IconStyleForms;
using IRL_Image_Creator.Windows.ProjectSettings;
using IRL_Image_Creator.Windows.TextStyleForms;
using Svg;
using System.ComponentModel;
using System.Diagnostics;

namespace IRL_Image_Creator.Windows
{
    public partial class MainForm : Form
    {
        // Singelton
        //
        private static MainForm? s_instance;
        private Project m_project = new();
        private readonly BuilderStatusUpdater m_statusUpdater = new();
        private readonly ConverterStatusUpdater m_converterStatusUpdater = new();
        private readonly BindingSource m_imageBuilderSettingsBindingSource = new();
        private Dictionary<string, string> m_iconStyleComboBoxDictionary = [];
        private Dictionary<string, string> m_dataLocationComboBoxDictionary = [];

        private int m_selectedTextInstructionIndex = -1;

        private MainForm()
        {
            InitializeComponent();

            m_statusUpdater.UpdateBuildStatus += OnUpdateBuilderStatus;
            m_converterStatusUpdater.UpdateConverterStatus += OnUpdateConverterStatus;

            AddToolTips();
        }

        private void AddToolTips()
        {
            toolTipMainForm.SetToolTip(SelectUserFolderButton, AppToolTips.MainWindow_SelectUserFolderButton);
        }

        public static MainForm Instance
        {
            get
            {
                s_instance ??= new();

                return s_instance;
            }
        }

        public Property[] Properties => m_project.ImageBuilderSettings.Properties;

        public List<ConverterColor> ConverterColor => m_project.Colors;

        public List<ConverterFont> ConverterFonts => m_project.Fonts;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            panel1.Enabled = false;
            projectToolStripMenuItem.Enabled = false;

            m_imageBuilderSettingsBindingSource.DataSource = m_project.ImageBuilderSettings;

            copySourceFilesCheckBox.DataBindings.Add("Checked", m_imageBuilderSettingsBindingSource, "CopySourceFiles", true, DataSourceUpdateMode.OnPropertyChanged);

            VersionMajorInput.DataBindings.Add("Value", m_imageBuilderSettingsBindingSource, "VersionMajor", true, DataSourceUpdateMode.OnPropertyChanged);
            VersionMinorInput.DataBindings.Add("Value", m_imageBuilderSettingsBindingSource, "VersionMinor", true, DataSourceUpdateMode.OnPropertyChanged);
            VersionPatchInput.DataBindings.Add("Value", m_imageBuilderSettingsBindingSource, "VersionPatch", true, DataSourceUpdateMode.OnPropertyChanged);
            VersionRevisionInput.DataBindings.Add("Value", m_imageBuilderSettingsBindingSource, "VersionRevision", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            Project.Save(m_project);
        }

        private void OnUpdateBuilderStatus(object sender, BuilderEventArgs e)
        {
            statusLabel.Text = e.Status;
            progressBar.Value = e.Progress;
            Application.DoEvents();
        }

        private void OnUpdateConverterStatus(object sender, ConverterStatusEventArgs e)
        {
            statusLabel.Text = e.Status;
            progressBar.Value = e.Progress;
            Application.DoEvents();
        }


        // ## Menu items ##
        //
        //      -- File
        //
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectForm newProjectForm = new();
            newProjectForm.StartPosition = FormStartPosition.CenterParent;
            newProjectForm.ShowDialog(this);

            if (newProjectForm.DialogResult == DialogResult.OK)
            {
                m_project = Project.Create(newProjectForm.ProjectName, newProjectForm.ProjectPath);
                m_imageBuilderSettingsBindingSource.DataSource = m_project.ImageBuilderSettings;

                UpdateFormName();
                UpdateProjectChanged();
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog.Filter = $"GI project files | *{FileConstants.PROJECT_FILE_EXTENSION}";
            FileDialog.Multiselect = false;
            DialogResult result = FileDialog.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                FileInfo fileInfo = new(FileDialog.FileName);
                m_project = Project.Open(fileInfo);
                m_imageBuilderSettingsBindingSource.DataSource = m_project.ImageBuilderSettings;

                UpdateFormName();
                UpdateProjectChanged();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_project.Name))
            {
                Debug.WriteLine("Save as");
                // TODO save as
            }
            else
            {
                Project.Save(m_project);
            }
        }

        private void UpdateFormName()
        {
            Text = $"IRL Image Creator - {m_project.Name}";
        }


        //      -- Project
        //
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new();
            aboutForm.StartPosition = FormStartPosition.CenterParent;
            aboutForm.ShowDialog(this);
        }

        private void dataLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataLocationForm dataLocationForm = new(m_project);
            dataLocationForm.StartPosition = FormStartPosition.CenterParent;
            dataLocationForm.ShowDialog(this);

            UpdateDataLocationDictionary();
            SetFontDataLocationComboBoxValues();
            SetTextDataLocationComboBoxValues();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilePropertyForm filePropertyForm = new(m_project);
            filePropertyForm.StartPosition = FormStartPosition.CenterParent;
            filePropertyForm.ShowDialog(this);
        }

        private void textStylesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextStyleForm textStyleForm = new(m_project);
            textStyleForm.StartPosition = FormStartPosition.CenterParent;
            textStyleForm.ShowDialog(this);
        }

        private void fontStylesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontStyleForm fontStyleForm = new(m_project);
            fontStyleForm.StartPosition = FormStartPosition.CenterParent;
            fontStyleForm.ShowDialog(this);
        }

        private void iconStylesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IconStyleForm iconStyleForm = new(m_project);
            iconStyleForm.StartPosition = FormStartPosition.CenterParent;
            iconStyleForm.ShowDialog(this);
            SetIconStyleComboBoxValues();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportSettingsForm importSettingsForm = new(m_project);
            importSettingsForm.StartPosition = FormStartPosition.CenterParent;
            importSettingsForm.ShowDialog();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSettingsForm exportSettingsForm = new(m_project);
            exportSettingsForm.StartPosition = FormStartPosition.CenterParent;
            exportSettingsForm.ShowDialog();
        }

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorForm colorForm = new(m_project);
            colorForm.StartPosition = FormStartPosition.CenterParent;
            colorForm.ShowDialog();

            Project.UpdateTextStyleColors(m_project);
            Project.UpdateFontStyleColors(m_project);
            Project.UpdateIconStyleColors(m_project);
            Project.Save(m_project);
        }

        private void fontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontForm fontForm = new(m_project);
            fontForm.StartPosition = FormStartPosition.CenterParent;
            fontForm.ShowDialog();

            Project.UpdateTextStyleFonts(m_project);
            Project.UpdateFontInstructionFonts(m_project);
            Project.Save(m_project);

            FontInstructionHelper.RefreshFontListView(m_project, FontsListView);
        }


        // ## Main Tab ##
        //

        private void UpdateProjectChanged()
        {
            UpdateDataLocationDictionary();
            SetFontDataLocationComboBoxValues();
            SetTextDataLocationComboBoxValues();

            if (m_project.ImageBuilderSettings.PixelDataFormat.PixelFormat == PixelFormat.RGB565)
            {
                RGB24RadioButton.Checked = false;
                RGB565RadioButton.Checked = true;
            }
            else
            {
                RGB24RadioButton.Checked = true;
                RGB565RadioButton.Checked = false;
            }

            SwapBytesCheckBox.Checked = m_project.ImageBuilderSettings.PixelDataFormat.RGB565SwapBytes;
            RGBOrderComboBox.SelectedIndex = (int)m_project.ImageBuilderSettings.PixelDataFormat.PixelFormatRGB;

            pixelDataTextBox.Text = m_project.ImageBuilderSettings.GuiPixelDataFile;
            SourceCopyToFolderLabel.Text = m_project.UserSourceFolder;

            if (!copySourceFilesCheckBox.Checked)
            {
                SelectUserFolderButton.Enabled = false;
            }

            m_imageBuilderSettingsBindingSource.ResetBindings(false);
            m_selectedTextInstructionIndex = -1;

            TextInstructionHelper.InitTextInstructionForm(textInstructionListView);
            TextInstructionHelper.RefreshTextInstructionList(textInstructionListView, m_project);
            TextInstructionHelper.ListViewSelectFirst(textInstructionListView, ref m_selectedTextInstructionIndex);

            SetFontKeyRadioButtons();
            SetIconStyleComboBoxValues();

            panel1.Enabled = true;
            projectToolStripMenuItem.Enabled = true;

            FontInstructionHelper.UpdateFontDataLocationComboBoxValue(m_project, fontDataLocationComboBox);
            FontInstructionHelper.RefreshFontListView(m_project, FontsListView);
            FontsListViewSetButtons();

            IconInstructionHelper.RefreshImageListView(m_project, IconListView);
            IconListViewSetButtons();

            TextInstructionListViewSetStateButtons();

            if (Debugger.IsAttached)
            {
                m_project.ImageBuilderSettings.LogVerbose = true;
            }
        }



        // ## Bitmap and Image builder ##
        //

        private void RGB565RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (RGB565RadioButton.Checked)
            {
                RGB24RadioButton.Checked = false;
                m_project.ImageBuilderSettings.PixelDataFormat.PixelFormat = PixelFormat.RGB565;
            }
        }

        private void RGB24RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (RGB24RadioButton.Checked)
            {
                RGB565RadioButton.Checked = false;
                m_project.ImageBuilderSettings.PixelDataFormat.PixelFormat = PixelFormat.RGB;
            }
        }

        private void SwapBytesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_project.ImageBuilderSettings.PixelDataFormat.RGB565SwapBytes = SwapBytesCheckBox.Checked;
        }

        private void RGBOrderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_project.ImageBuilderSettings.PixelDataFormat.PixelFormatRGB = (PixelFormatRGB)RGBOrderComboBox.SelectedIndex;
        }

        private void SelectUserFolderButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(m_project.UserSourceFolder))
            {
                folderBrowserDialog.SelectedPath = m_project.UserSourceFolder;
            }

            folderBrowserDialog.ShowDialog(this);

            if (Directory.Exists(folderBrowserDialog.SelectedPath))
            {
                m_project.UserSourceFolder = folderBrowserDialog.SelectedPath;
                SourceCopyToFolderLabel.Text = m_project.UserSourceFolder;
            }
        }

        private void pixelDataTextBox_TextChanged(object sender, EventArgs e)
        {
            if (FileUtils.IsValidFileName(pixelDataTextBox.Text))
            {
                m_project.ImageBuilderSettings.GuiPixelDataFile = pixelDataTextBox.Text;
                pixelDataTextBox.ForeColor = SystemColors.WindowText;
            }
            else
            {
                pixelDataTextBox.ForeColor = Color.Red;
            }
        }

        private void copySourceFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            m_project.ImageBuilderSettings.CopySourceFiles = copySourceFilesCheckBox.Checked;
            SelectUserFolderButton.Enabled = copySourceFilesCheckBox.Checked;
        }

        private void CreateBitmapsButton_Click(object sender, EventArgs e)
        {
            ImageBuilderHelper.CreateBitmaps(m_project, m_converterStatusUpdater, this);
        }

        private void CreateImageButton_Click(object sender, EventArgs e)
        {
            ImageBuilderHelper.CreateImage(m_project, m_imageBuilderSettingsBindingSource, m_statusUpdater, this);
        }

        private void BuildAllButton_Click(object sender, EventArgs e)
        {
            ImageBuilderHelper.BuildAll(
                m_project, m_imageBuilderSettingsBindingSource,
                m_converterStatusUpdater, m_statusUpdater, this);
        }


        // ## Text instructions ##
        //

        private void AddTextInstructionBtn_Click(object sender, EventArgs e)
        {
            TextInstructionHelper.AddTextInstruction(m_project, textInstructionListView, this);
        }

        private void DeleteTextInstructionBtn_Click(object sender, EventArgs e)
        {
            TextInstructionHelper.DeleteTextInstruction(m_project, textInstructionListView);
        }

        private void ImportTextBtn_Click(object sender, EventArgs e)
        {
            TextInstructionHelper.ImportText(m_project, textInstructionListView, textToConvertListView, this);
        }

        private void AddTextStyleButton_Click(object sender, EventArgs e)
        {
            TextInstructionHelper.AddTextStyle(m_project, textInstructionListView, textToConvertListView, this);
        }

        private void DeleteTextStyleButton_Click(object sender, EventArgs e)
        {
            TextInstructionHelper.DeleteTextStyle(m_project, textInstructionListView, textToConvertListView, this);
        }

        private void TextInstructionListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {

        }

        private void TextInstructionListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Text instruction selection changed");
            TextInstructionHelper.TextInstructionListViewSelectionChanged(
                ref m_selectedTextInstructionIndex, m_project, textInstructionListView, textToConvertListView, InputTextFileGroupBox);
            TextInstructionListViewSetStateButtons();
            TextToConvertListViewSetStateButtons();
            UpdateTextDataLocationComboBoxValue();
        }

        private void TextToConvertListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextToConvertListViewSetStateButtons();
        }

        private void TextDataLocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (textInstructionListView.Items.Count == 0 || m_selectedTextInstructionIndex == -1) 
            {
                return;
            }

            ListViewItem selectedItem = textInstructionListView.Items[m_selectedTextInstructionIndex];
            TextInstruction textInstruction = (TextInstruction)m_project.Instructions.Find(x => x.Name == selectedItem.Text);

            if (textInstruction == null)
            {
                return;
            }

            try
            {
                int value = int.Parse(textDataLocationComboBox.SelectedValue.ToString());
                textInstruction.DataLocationId = value;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);

                return;
            }
        }

        private void UpdateTextDataLocationComboBoxValue()
        {
            ListViewItem selectedItem = textInstructionListView.Items[m_selectedTextInstructionIndex];
            TextInstruction textInstruction = (TextInstruction)m_project.Instructions.Find(x => x.Name == selectedItem.Text);

            if (textInstruction == null)
            {
                return;
            }

            textDataLocationComboBox.SelectedValue = textInstruction.DataLocationId.ToString();
        }

        private void textToConvertListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            if (textInstructionListView.SelectedItems.Count == 0)
            {
                return;
            }

            ListViewItem selectedItem = textInstructionListView.SelectedItems[0];
            TextInstruction textInstruction = (TextInstruction)m_project.Instructions.Find(x => x.Name == selectedItem.Text);

            if (textInstruction.Table.ColumnWidths.Count != textInstruction.Table.NumberOfColumns + 1)
            {
                return;
            }

            if (textToConvertListView.Columns.Count != textInstruction.Table.ColumnWidths.Count)
            {
                return;
            }

            if ((textInstruction.Table.ColumnWidths[e.ColumnIndex] != textToConvertListView.Columns[e.ColumnIndex].Width) &&
                (textToConvertListView.Columns[e.ColumnIndex].Width != 60))
            {
                Debug.WriteLine($"Column width changed: ColumnIndex={e.ColumnIndex}, NewWidth={textToConvertListView.Columns[e.ColumnIndex].Width}");
                textInstruction.Table.ColumnWidths[e.ColumnIndex] = textToConvertListView.Columns[e.ColumnIndex].Width;
                Project.Save(m_project);
            }
        }

        private void TextToConvertListViewSetStateButtons()
        {
            if (m_selectedTextInstructionIndex != -1)
            {
                AddTextStyleButton.Enabled = true;
                DeleteTextStyleButton.Enabled = true;
            }
            else
            {
                AddTextStyleButton.Enabled = false;
                DeleteTextStyleButton.Enabled = false;
            }
        }

        private void TextInstructionListViewSetStateButtons()
        {
            if (m_selectedTextInstructionIndex != -1)
            {
                DeleteTextInstructionBtn.Enabled = true;
                TextInfoGroupBox.Enabled = true;
                InputTextFileGroupBox.Enabled = true;
            }
            else
            {
                DeleteTextInstructionBtn.Enabled = false;
                TextInfoGroupBox.Enabled = false;
                InputTextFileGroupBox.Enabled = false;
            }
        }

        private void SetTextDataLocationComboBoxValues()
        {
            textDataLocationComboBox.DataSource = new BindingSource(m_dataLocationComboBoxDictionary, null);
            textDataLocationComboBox.DisplayMember = "Value";
            textDataLocationComboBox.ValueMember = "Key";
        }


        // ## Font instructions ##
        //

        private void AddFontButton_Click(object sender, EventArgs e)
        {
            FontInstructionHelper.AddFont(m_project, FontsListView, FontDialog, this);
        }

        private void EditFontButton_Click(object sender, EventArgs e)
        {
            FontInstructionHelper.EditFont(m_project, FontsListView, FontDialog, this);
        }

        private void DeleteFontButton_Click(object sender, EventArgs e)
        {
            FontInstructionHelper.DeleteFont(m_project, FontsListView, this);
        }

        private void SelectFontStyleButton_Click(object sender, EventArgs e)
        {
            FontInstructionHelper.SelectFontStyles(m_project, FontsListView, this);
        }

        private void FontsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontsListViewSetButtons();
        }

        private void FontsListViewSetButtons()
        {
            if (FontsListView.SelectedItems.Count > 0)
            {
                SelectFontStyleButton.Enabled = true;
                EditFontButton.Enabled = true;
                DeleteFontButton.Enabled = true;
            }
            else
            {
                SelectFontStyleButton.Enabled = false;
                EditFontButton.Enabled = false;
                DeleteFontButton.Enabled = false;
            }
        }

        private void SetFontKeyRadioButtons()
        {
            FontInstruction fontInstruction = FontInstructionHelper.GetFontInstruction(m_project.Instructions);

            if (fontInstruction == null)
            {
                FontKeyBothRB.Checked = true;

                return;
            }
            else
            {
                if (fontInstruction.FontFileKeyFormat == FontFileKeyFormat.ProjectFontName)
                {
                    FontKeyProjectFnRB.Checked = true;
                }
                else if (fontInstruction.FontFileKeyFormat == FontFileKeyFormat.FontName)
                {
                    FontKeyFontnameRB.Checked = true;
                }
                else
                {
                    FontKeyBothRB.Checked = true;
                }
            }
        }

        private void FontKeyProjectFnRB_CheckedChanged(object sender, EventArgs e)
        {
            FontInstruction fontInstruction = FontInstructionHelper.GetFontInstruction(m_project.Instructions);

            if ((fontInstruction != null) && FontKeyProjectFnRB.Checked)
            {
                fontInstruction.FontFileKeyFormat = FontFileKeyFormat.ProjectFontName;
            }
        }

        private void FontKeyFontnameRB_CheckedChanged(object sender, EventArgs e)
        {
            FontInstruction fontInstruction = FontInstructionHelper.GetFontInstruction(m_project.Instructions);

            if ((fontInstruction != null) && FontKeyFontnameRB.Checked)
            {
                fontInstruction.FontFileKeyFormat = FontFileKeyFormat.FontName;
            }
        }

        private void FontKeyBothRB_CheckedChanged(object sender, EventArgs e)
        {
            FontInstruction fontInstruction = FontInstructionHelper.GetFontInstruction(m_project.Instructions);

            if ((fontInstruction != null) && FontKeyBothRB.Checked)
            {
                fontInstruction.FontFileKeyFormat = FontFileKeyFormat.Both;
            }
        }

        private void FontDataLocationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            FontInstruction fontInstruction = FontInstructionHelper.GetFontInstruction(m_project.Instructions);

            if (fontInstruction == null)
            {
                return;
            }

            try
            {
                int value = int.Parse(fontDataLocationComboBox.SelectedValue.ToString());
                fontInstruction.DataLocationId = value;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);

                return;
            }
        }

        private void SetFontDataLocationComboBoxValues()
        {
            fontDataLocationComboBox.DataSource = new BindingSource(m_dataLocationComboBoxDictionary, null);
            fontDataLocationComboBox.DisplayMember = "Value";
            fontDataLocationComboBox.ValueMember = "Key";
        }

        // ## Icon instructions ##
        //

        private void AddIconButton_Click(object sender, EventArgs e)
        {
            IconInstructionHelper.AddImages(m_project, FileDialog, IconListView, this);
        }

        private void DeleteIconButton_Click(object sender, EventArgs e)
        {
            IconInstructionHelper.DeleteImages(m_project, IconListView, this);
        }

        private void EditIconNameButton_Click(object sender, EventArgs e)
        {
            EditIconNameForm editIconNameForm = new((SvgFileInfo)IconListView.SelectedItems[0].Tag);
            editIconNameForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = editIconNameForm.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                Project.Save(m_project);
                IconInstructionHelper.RefreshImageListView(m_project, IconListView);
            }
        }

        private void SelectIconStyleButton_Click(object sender, EventArgs e)
        {
            IconInstructionHelper.AddIconStyle(m_project, IconListView, this);
        }

        private void SetIconStyleComboBoxValues()
        {
            m_iconStyleComboBoxDictionary.Clear();
            m_iconStyleComboBoxDictionary.Add("0", "None");

            foreach (IconStyle iconStyle in m_project.IconStyles)
            {
                m_iconStyleComboBoxDictionary.Add((iconStyle.ID + 1).ToString(), iconStyle.Name);
            }

            IconStyleComboBox.DataSource = new BindingSource(m_iconStyleComboBoxDictionary, null);
            IconStyleComboBox.DisplayMember = "Value";
            IconStyleComboBox.ValueMember = "Key";
        }

        private void IconStyleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetIconStyleToSvgPreview();
        }

        private void SetIconStyleToSvgPreview()
        {
            string key = ((KeyValuePair<string, string>)IconStyleComboBox.SelectedItem).Key;
            if (key == "0")
            {
                return;
            }

            try
            {
                int iconStyleId = int.Parse(key) - 1;
                IconStyle iconStyle = IconStyle.GetImageStyleById(iconStyleId, m_project.IconStyles);

                IconInstructionHelper.ApplyImageStyeToSvG(iconStyle, IconListView, svgViewer, svgXMLViewer);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        private void IconListViewSetButtons()
        {
            if (IconListView.SelectedItems.Count > 0)
            {
                DeleteImagesButton.Enabled = true;
                EditIconNameButton.Enabled = true;
                SelectIconStyleButton.Enabled = true;
                selectDataLocationButton.Enabled = true;
            }
            else
            {
                SelectIconStyleButton.Enabled = false;
                EditIconNameButton.Enabled = false;
                DeleteImagesButton.Enabled = false;
                selectDataLocationButton.Enabled = false;
            }
        }

        private void IconListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            IconListViewSetButtons();

            ListView.SelectedIndexCollection indices = IconListView.SelectedIndices;

            if (indices.Count == 0)
            {
                return;
            }

            try
            {
                SvgFileInfo svgFileInfo = (SvgFileInfo)IconListView.SelectedItems[0].Tag;

                SvgDocument svgDocument = SvgDocument.FromSvg<SvgDocument>(svgFileInfo.SvgString);
                IconInstructionHelper.RenderSvg(svgDocument, svgViewer);
                svgXMLViewer.Text = svgFileInfo.SvgString;

                SetIconStyleToSvgPreview();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void SelectDataLocationButton_Click(object sender, EventArgs e)
        {
            IconInstructionHelper.SelectIconDataLocation(m_project, IconListView, this);
        }


        // ## Form general methods ##
        //
        private void UpdateDataLocationDictionary()
        {
            m_dataLocationComboBoxDictionary.Clear();

            foreach (DataLocation dataLocation in m_project.ImageBuilderSettings.DataLocations)
            {
                string locationName = "";
                if (string.IsNullOrEmpty(dataLocation.Name))
                {
                    locationName = dataLocation.DataLocationType.ToString();
                }
                else
                {
                    locationName = dataLocation.Name;
                }

                m_dataLocationComboBoxDictionary.Add(
                    dataLocation.LocationID.ToString(),
                    $"{dataLocation.LocationID.ToString()} - {locationName}");
            }
        }

        private void TextDataLocationComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("Text data location combo box selected value changed");
        }
    }
}
