using IRL_Common_Library.Consts;

namespace IRL_Image_Creator.Windows
{
    public partial class NewProjectForm : Form
    {
        public string ProjectName => ProjectNameTB.Text;
        public string ProjectPath => ProjectPathLabel.Text;

        public NewProjectForm()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectNameTB.Text))
            {
                MessageBox.Show(this, "Please enter a valid project name");
            }
            else if (!Directory.Exists(ProjectPathLabel.Text))
            {
                MessageBox.Show(this, "Please select a project folder");
            }
            else
            {
                DialogResult = DialogResult.OK;

                Close();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void SelectPathButton_Click(object sender, EventArgs e)
        {
            DialogResult result = ProjectFolderDialog.ShowDialog(this);
            
            if (result == DialogResult.OK && !string.IsNullOrEmpty(ProjectFolderDialog.SelectedPath))
            {
                bool exist = Directory.EnumerateFiles(ProjectFolderDialog.SelectedPath, $"*{FileConstants.ProjectFileExtension}").Any();

                if (exist)
                {
                    MessageBox.Show(this, "A project already exists in this folder");
                }
                else
                {
                    ProjectPathLabel.Text = ProjectFolderDialog.SelectedPath;
                }
            }
        }
    }
}
