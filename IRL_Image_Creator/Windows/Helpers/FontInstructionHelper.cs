using IRL_Bitmap_Converter_Tools.ConverterInstructions.FontInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Image_Creator.Projects;
using IRL_Image_Creator.Windows.FontStyleForms;
using IRL_Image_Creator.Windows.FontForms;

namespace IRL_Image_Creator.Windows.Helpers
{
    public static class FontInstructionHelper
    {
        public static void RefreshFontListView(Project project, ListView fontsListView)
        {
            fontsListView.Items.Clear();

            FontInstruction instruction = GetFontInstruction(project.Instructions);

            if (instruction == null)
            {
                instruction = new();
                project.Instructions.Add(instruction);
            }

            foreach (FontBitmap fontBitmap in instruction.FontBitmaps)
            {
                string fontName = GetConverterFontName(project, fontBitmap.FontConverterId);
                string font = $"{fontBitmap.FontName}, {fontBitmap.FontSize}, {fontBitmap.FontStyle}";
                string styleRowContent = GetStyleRowContent(project.FontBitmapStyles, fontBitmap);

                string[] row =
                {
                    fontName,
                    font,
                    styleRowContent
                };

                ListViewItem listViewItem = new(row);
                listViewItem.Tag = fontBitmap;
                fontsListView.Items.Add(listViewItem);
            }

            fontsListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public static void AddFont(Project project, ListView fontsListView, FontDialog fontDialog, Form owner)
        {
            SelectFontForm selectFontForm = new("", 12, FontStyle.Regular, project.Fonts);
            selectFontForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult selectFontResult = selectFontForm.ShowDialog(owner);

            if (selectFontResult == DialogResult.OK)
            {
                FontBitmap fontBitmap = new()
                {
                    FontName = selectFontForm.FontName,
                    FontSize = selectFontForm.FontSize,
                    FontStyle = selectFontForm.FontStyle,
                    FontConverterId = selectFontForm.SelectedFontID
                };

                FontInstruction instruction = GetFontInstruction(project.Instructions);
                instruction.FontBitmaps.Add(fontBitmap);

                RefreshFontListView(project, fontsListView);
            }
        }

        public static void EditFont(Project project, ListView fontsListView, FontDialog fontDialog, Form owner)
        {
            if (fontsListView.SelectedIndices.Count == 1)
            {
                FontBitmap fontBitmap = (FontBitmap)fontsListView.SelectedItems[0].Tag;

                SelectFontForm selectFontForm = new(fontBitmap.FontName, fontBitmap.FontSize, fontBitmap.FontStyle, project.Fonts);
                selectFontForm.StartPosition = FormStartPosition.CenterParent;
                DialogResult selectFontResult = selectFontForm.ShowDialog(owner);

                if (selectFontResult == DialogResult.OK)
                {
                    fontBitmap.FontName = selectFontForm.FontName;
                    fontBitmap.FontSize = selectFontForm.FontSize;
                    fontBitmap.FontStyle = selectFontForm.FontStyle;
                    fontBitmap.FontConverterId = selectFontForm.SelectedFontID;

                    RefreshFontListView(project, fontsListView);
                }
            }
            else if (fontsListView.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Please select only one font to edit.", "Multiple Fonts Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Please select a font to edit.", "No Font Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void DeleteFont(Project project, ListView fontsListView, Form owner)
        {
            DialogResult dialogResult = MessageBox.Show(owner, "Are you sure you want to delete the selected fonts?", "Delete Fonts", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            if (fontsListView.SelectedIndices.Count > 0)
            {
                FontInstruction instruction = GetFontInstruction(project.Instructions);

                foreach (ListViewItem selectedItem in fontsListView.SelectedItems)
                {
                    FontBitmap fontBitmap = (FontBitmap)selectedItem.Tag;
                    if (fontBitmap != null)
                    {
                        instruction.FontBitmaps.Remove(fontBitmap);
                    }
                }

                RefreshFontListView(project, fontsListView);
            }
        }

        public static void SelectFontStyles(Project project, ListView fontsListView, Form owner)
        {
            if (fontsListView.SelectedItems.Count > 0)
            {
                SelectFontStyleForm selectFontStyleForm = new(project);
                selectFontStyleForm.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = selectFontStyleForm.ShowDialog(owner);

                if (result == DialogResult.OK)
                {
                    foreach (ListViewItem selectedItem in fontsListView.SelectedItems)
                    {
                        FontBitmap fontBitmap = (FontBitmap)selectedItem.Tag;
                        if (fontBitmap != null)
                        {
                            fontBitmap.FontBitmapStyleIds = selectFontStyleForm.SelectedFontStyleIDs;
                        }
                    }
                }

                RefreshFontListView(project, fontsListView);
            }
        }

        public static FontInstruction GetFontInstruction(List<ConverterInstruction> converterInstructions)
        {
            return (FontInstruction)converterInstructions.FirstOrDefault(x => x is FontInstruction);
        }

        private static string GetStyleRowContent(List<FontBitmapStyle> fontBitmapStyles, FontBitmap fontBitmap)
        {
            string styleRowContent = "";

            foreach (int styleId in fontBitmap.FontBitmapStyleIds)
            {
                FontBitmapStyle style = FontBitmapStyle.GetFontStyleById(styleId, fontBitmapStyles);

                if (style != null)
                {
                    styleRowContent += $"{style.Name}, ";
                }
            }

            return styleRowContent.TrimEnd(',', ' ');
        }

        public static string GetConverterFontName(Project project, int fontConverterId)
        {
            if (fontConverterId == -1)
            {
                return "Custom";
            }

            ConverterFont font = ConverterFont.GetFontById(fontConverterId, project.Fonts);
            if (font != null) 
            {
                return font.Name;
            }

            return "Custom";
        }
    }
}
