using IRL_Bitmap_Converter_Tools.ConverterInstructions.IconInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Common_Library.Utils;
using IRL_Image_Creator.Projects;
using IRL_Image_Creator.Windows.IconStyleForms;
using Svg;
using System.Xml;

namespace IRL_Image_Creator.Windows.Helpers
{
    public static class IconInstructionHelper
    {
        public static void AddImages(Project project, OpenFileDialog fileDialog, ListView iconListView, Form owner)
        {
            fileDialog.Filter = $"Vector Graphics (*.svg)|*.svg";
            fileDialog.Multiselect = true;
            DialogResult result = fileDialog.ShowDialog(owner);

            List<string> duplicates = new();

            if (result == DialogResult.OK)
            {
                IconInstruction instruction = GetIconInstruction(project.Instructions);
                if (instruction == null)
                {
                    instruction = new();
                    project.Instructions.Add(instruction);
                }

                foreach (string fullFilePath in fileDialog.FileNames)
                {
                    FileInfo fileInfo = new(fullFilePath);

                    if (instruction.SvgFileInfos.Exists(x => x.Filename == FileUtils.RemoveExtension(fileInfo.Name)))
                    {
                        duplicates.Add(fileInfo.Name);
                    }
                    else
                    {
                        string fileName = FileUtils.RemoveExtension(fileInfo.Name);

                        XmlDocument xmlDoc = new()
                        {
                            XmlResolver = null
                        };
                        xmlDoc.Load(fullFilePath);

                        SvgFileInfo svgFileInfo = new(fileName, xmlDoc.InnerXml);
                        instruction.SvgFileInfos.Add(svgFileInfo);
                    }
                }

                RefreshImageListView(project, iconListView);

                if (duplicates.Count > 0)
                {
                    string message = "The following files already exists in the list:\n\n";

                    foreach (string duplicate in duplicates)
                    {
                        message += $"{duplicate}\n";
                    }

                    const string caption = "Duplicates";
                    MessageBox.Show(message, caption,
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public static void DeleteImages(Project project, ListView iconListView, Form owner)
        {
            DialogResult dialogResult = MessageBox.Show(owner, "Are you sure you want to delete the selected images?", "Delete Images", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            if (iconListView.SelectedIndices.Count > 0)
            {
                IconInstruction instruction = GetIconInstruction(project.Instructions);

                foreach (ListViewItem selectedItem in iconListView.SelectedItems)
                {
                    SvgFileInfo svgFileInfo = (SvgFileInfo)selectedItem.Tag;
                    if (svgFileInfo != null)
                    {
                        instruction.SvgFileInfos.Remove(svgFileInfo);
                    }
                }
            }

            RefreshImageListView(project, iconListView);
        }

        public static void AddIconStyle(Project project, ListView iconListView, Form owner)
        {
            if (iconListView.SelectedItems.Count > 0)
            {
                SelectIconStyleForm selectIconStyleForm = new(project);
                selectIconStyleForm.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = selectIconStyleForm.ShowDialog(owner);

                if (result == DialogResult.OK)
                {
                    foreach (ListViewItem selectedItem in iconListView.SelectedItems)
                    {
                        SvgFileInfo svgFileInfo = (SvgFileInfo)selectedItem.Tag;

                        if (svgFileInfo != null)
                        {
                            svgFileInfo.ImageBitmapStyleIds = selectIconStyleForm.SelectedIconStyleIDs;
                        }
                    }
                }

                RefreshImageListView(project, iconListView);
            }
        }

        public static void RefreshImageListView(Project project, ListView imagesListView)
        {
            imagesListView.Items.Clear();

            IconInstruction instruction = GetIconInstruction(project.Instructions);

            if (instruction == null)
            {
                instruction = new();
                project.Instructions.Add(instruction);
            }

            foreach (SvgFileInfo svgFileInfo in instruction.SvgFileInfos)
            {
                string styleRowContent = GetStyleRowContent(project.IconStyles, svgFileInfo);
                string[] row =
                {
                    svgFileInfo.Filename,
                    svgFileInfo.IconName,
                    styleRowContent
                };

                ListViewItem listViewItem = new(row);
                listViewItem.Tag = svgFileInfo;
                imagesListView.Items.Add(listViewItem);
            }

            imagesListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


        public static void RenderSvg(SvgDocument svgDoc, PictureBox svgViewer)
        {
            if (OperatingSystem.IsWindows())
            {
                svgViewer.Image?.Dispose();
            }

            svgViewer.Image = svgDoc.Draw();
        }

        public static void ApplyImageStyeToSvG(IconStyle style, ListView iconListView, PictureBox svgViewer, RichTextBox svgXMLViewer)
        {

            ListView.SelectedIndexCollection svgImages = iconListView.SelectedIndices;

            if (svgImages.Count == 0)
            {
                return;
            }

            XmlDocument xmlDoc = new();

            string svg = svgXMLViewer.Text;
            if (!svg.Contains("svg\"><rect"))
            {
                svg = svg.Replace("svg\">", "svg\"><rect width=\"100%\" height=\"100%\" fill=\"red\"/>");
            }

            xmlDoc.LoadXml(svg);

            foreach (ImageSvgColorId svgColor in style.SvgColors)
            {
                IRL_Bitmap_Converter_Tools.Converters.IconConverter.SetAttributesColorWithId(xmlDoc, svgColor.Id, svgColor.Color);
            }

            IRL_Bitmap_Converter_Tools.Converters.IconConverter.SetColorsBackground(xmlDoc, style.BackColor);
            svgXMLViewer.Text = xmlDoc.InnerXml;

            SvgDocument svgDoc = SvgDocument.FromSvg<SvgDocument>(svgXMLViewer.Text);
            RenderSvg(svgDoc, svgViewer);
        }

        private static IconInstruction GetIconInstruction(List<ConverterInstruction> instructions)
        {
            return instructions.FirstOrDefault(i => i is IconInstruction) as IconInstruction;
        }

        private static string GetStyleRowContent(List<IconStyle> styles, SvgFileInfo svgFileInfo)
        {
            string styleRowContent = "";

            foreach (int styleId in svgFileInfo.ImageBitmapStyleIds)
            {
                IconStyle style = IconStyle.GetImageStyleById(styleId, styles);

                if (style != null)
                {
                    styleRowContent += style.Name + ", ";
                }
            }

            return styleRowContent.TrimEnd(',', ' ');
        }
    }
}
