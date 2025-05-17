using System.Configuration;

namespace IRL_Image_Creator.Windows
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            emailContentLabel.Text = AppInfo.Email;
            versionContentLabel.Text = AppInfo.Version;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
