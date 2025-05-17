using IRL_Bitmap_Converter_Tools.ConverterInstructions;
using IRL_Bitmap_Converter_Tools.ConverterInstructions.TextInstructions;
using IRL_Common_Library.Utils;
using IRL_Gui_Image_Builder_Library.Projects;
using IRL_Image_Creator.Projects;
using IRL_Image_Creator.Properties;
using IRL_Image_Creator.Windows.TextInstructionForms;
using IRL_Image_Creator.Windows.TextStyleForms;

namespace IRL_Image_Creator.Windows.Helpers
{
    public static class TextInstructionHelper
    {
        public static void InitTextInstructionForm(ListView listView)
        {
            ImageList imageList = new();
            imageList.Images.Add("TextFile", Resources.TextFile);
            listView.SmallImageList = imageList;
        }

        public static void RefreshTextToConvertListView(TextInstruction textInstruction, ListView listView, List<TextStyle> textStyles)
        {
            SetListViewHeader(textInstruction, listView);

            foreach (TextRecord record in textInstruction.Table.Records)
            {
                string styleRowContent = GetStyleRowContent(textStyles, record);

                int rowSize = textInstruction.Table.NumberOfColumns + 1;
                string[] row = new string[rowSize];
                row[0] = record.Key;

                for (int i = 0; i < record.Text.Count; i++)
                {
                    row[i + 1] = record.Text[i];
                }

                row[rowSize - 1] = styleRowContent;

                ListViewItem listViewItem = new(row);
                listViewItem.Tag = record;

                listView.Items.Add(listViewItem);
            }

            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public static void RefreshTextInstructionList(ListView listView, Project project)
        {
            listView.Items.Clear();

            foreach (ConverterInstruction instruction in project.Instructions)
            {
                if (instruction.GetType() == typeof(TextInstruction))
                {
                    ListViewItem lvi = new(instruction.Name, "TextFile");
                    listView.Items.Add(lvi);
                }
            }
        }

        public static void ListViewSelectFirst(ListView listView)
        {
            if (listView.Items.Count > 0)
            {
                listView.Items[0].Selected = true;
            }
        }

        public static void TextInstructionListViewSelectionChanged(
            ref int currentSelectedIndex, Project project, ListView textInstructionListView, ListView textToConvertListView, GroupBox inputTextFileGroupBox)
        {
            if (textInstructionListView.SelectedItems.Count > 0)
            {
                int selectedIndex = textInstructionListView.SelectedItems[0].Index;

                if ((currentSelectedIndex == -1) || (currentSelectedIndex != selectedIndex))
                {
                    currentSelectedIndex = selectedIndex;
                }
                else
                {
                    return;
                }

                ListViewItem selectedItem = textInstructionListView.SelectedItems[0];
                TextInstruction textInstruction = (TextInstruction)project.Instructions.Find(x => x.Name == selectedItem.Text);

                if (textInstruction != null)
                {
                    inputTextFileGroupBox.Text = textInstruction.Name;
                    RefreshTextToConvertListView(textInstruction, textToConvertListView, project.TextStyles);
                }
            }
        }

        public static void AddTextInstruction(Project project, ListView textInstructionListView, Form owner)
        {
            TextInstruction textInstruction = new();
            NewTextInstructionForm newTextInstructionForm = new(textInstruction);
            newTextInstructionForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = newTextInstructionForm.ShowDialog(owner);

            if (result == DialogResult.OK)
            {
                project.Instructions.Add(textInstruction);
                RefreshTextInstructionList(textInstructionListView, project);
            }
        }

        public static void DeleteTextInstructionk(Project project, ListView textInstructionListView)
        {
            if (textInstructionListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = textInstructionListView.SelectedItems[0];
                string name = selectedItem.Text;

                for (int i = 0; i < project.Instructions.Count; i++)
                {
                    if (project.Instructions[i].Name == name)
                    {
                        project.Instructions.RemoveAt(i);

                        break;
                    }
                }

                RefreshTextInstructionList(textInstructionListView, project);
            }
        }

        public static void ImportText(Project project, ListView textInstructionListView, ListView textToConvertListView, Form owner)
        {
            if (textInstructionListView.SelectedItems.Count > 0)
            {
                Log.OpenNewFile(BuildFolders.LogFolderPath(project.ProjectFolder));

                ListViewItem selectedItem = textInstructionListView.SelectedItems[0];

                TextInstruction textInstruction = (TextInstruction)project.Instructions.Find(x => x.Name == selectedItem.Text);
                if (textInstruction != null)
                {
                    ImportTextForm importTextForm = new(textInstruction);
                    importTextForm.StartPosition = FormStartPosition.CenterParent;
                    importTextForm.ShowDialog(owner);

                    if (importTextForm.DialogResult == DialogResult.OK)
                    {
                        Project.Save(project);
                        RefreshTextInstructionList(textInstructionListView, project);
                        RefreshTextToConvertListView(textInstruction, textToConvertListView, project.TextStyles);
                    }
                    else if (importTextForm.DialogResult == DialogResult.Abort)
                    {
                        string fullFilePath = Project.GetFullFilePath(project);
                        FileInfo fileInfo = new(fullFilePath);
                        project = Project.Open(fileInfo);
                    }
                    else
                    {
                    }
                }

                Log.CloseFile();
            }
        }

        public static void AddTextStyle(
            Project project, ListView textInstructionListView, ListView textToConvertListView, Form owner)
        {
            if (textToConvertListView.SelectedItems.Count > 0)
            {
                SelectTextStyleForm selectTextStyleForm = new(project);
                selectTextStyleForm.StartPosition = FormStartPosition.CenterParent;
                DialogResult result = selectTextStyleForm.ShowDialog(owner);

                if (result == DialogResult.OK)
                {
                    foreach (ListViewItem item in textToConvertListView.SelectedItems)
                    {
                        TextRecord record = (TextRecord)item.Tag;
                        if (record != null)
                        {
                            record.TextStyleIDs = selectTextStyleForm.SelectedTextStyleIDs;
                        }
                    }
                }
            }

            if (textInstructionListView.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = textInstructionListView.SelectedItems[0];
                TextInstruction textInstruction = (TextInstruction)project.Instructions.Find(x => x.Name == selectedItem.Text);
                RefreshTextToConvertListView(textInstruction, textToConvertListView, project.TextStyles);
            }
        }

        public static void DeleteTextStyle(
            Project project, ListView textInstructionListView, ListView textToConvertListView, Form owner)
        {
            if ((textToConvertListView.SelectedItems.Count > 0) && (textInstructionListView.SelectedItems.Count > 0))
            {
                DialogResult result = MessageBox.Show(
                    owner,
                    "Are you sure you want to delete the selected text style?",
                    "Delete Text Style",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }

                ListViewItem selectedItem = textInstructionListView.SelectedItems[0];
                TextInstruction textInstruction = (TextInstruction)project.Instructions.Find(x => x.Name == selectedItem.Text);

                foreach (ListViewItem item in textToConvertListView.SelectedItems)
                {
                    TextRecord record = (TextRecord)item.Tag;
                    textInstruction?.Table.Records.Remove(record);
                }

                ResetTextToConvertListView(textToConvertListView);
                RefreshTextInstructionList(textInstructionListView, project);

                Project.Save(project);
            }
        }

        public static void ResetTextToConvertListView(ListView listView)
        {
            listView.Items.Clear();
            listView.Columns.Clear();

            listView.Columns.Add("Key");
            listView.Columns.Add("Text");
            listView.Columns.Add("Style");
        }

        private static string GetStyleRowContent(List<TextStyle> textStyles, TextRecord record)
        {
            string styleRowContent = "";
            foreach (int styleId in record.TextStyleIDs)
            {
                TextStyle style = TextStyle.GetTextStyleById(styleId, textStyles);

                if (style != null)
                {
                    styleRowContent += $"{style.Description}, ";
                }
            }

            return styleRowContent.TrimEnd(',', ' ');
        }

        private static void SetListViewHeader(TextInstruction textInstruction, ListView listView)
        {
            if (textInstruction.Table.Translate)
            {
                listView.Items.Clear();
                listView.Columns.Clear();

                if (textInstruction.Table.Header.Text.Count == 0)
                {
                    listView.Columns.Add("Key");

                    for (int i = 1; i < textInstruction.Table.NumberOfColumns; i++)
                    {
                        listView.Columns.Add($"Column {i}");
                    }
                }
                else
                {
                    foreach (string header in textInstruction.Table.Header.Text)
                    {
                        listView.Columns.Add(header);
                    }
                }

                listView.Columns.Add("Style");
            }
            else
            {
                ResetTextToConvertListView(listView);
            }
        }
    }
}
