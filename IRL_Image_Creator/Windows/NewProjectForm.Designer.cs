namespace IRL_Image_Creator.Windows
{
    partial class NewProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ProjectFolderDialog = new FolderBrowserDialog();
            label1 = new Label();
            ProjectNameTB = new TextBox();
            label2 = new Label();
            ProjectPathLabel = new Label();
            CreateButton = new Button();
            CancelButton = new Button();
            SelectPathButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 11);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 0;
            label1.Text = "Project Name:";
            // 
            // ProjectNameTB
            // 
            ProjectNameTB.Location = new Point(9, 29);
            ProjectNameTB.Name = "ProjectNameTB";
            ProjectNameTB.Size = new Size(167, 23);
            ProjectNameTB.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 68);
            label2.Name = "label2";
            label2.Size = new Size(34, 15);
            label2.TabIndex = 2;
            label2.Text = "Path:";
            // 
            // ProjectPathLabel
            // 
            ProjectPathLabel.AutoSize = true;
            ProjectPathLabel.Location = new Point(46, 68);
            ProjectPathLabel.Name = "ProjectPathLabel";
            ProjectPathLabel.Size = new Size(0, 15);
            ProjectPathLabel.TabIndex = 3;
            // 
            // CreateButton
            // 
            CreateButton.Location = new Point(399, 196);
            CreateButton.Name = "CreateButton";
            CreateButton.Size = new Size(75, 23);
            CreateButton.TabIndex = 4;
            CreateButton.Text = "Create";
            CreateButton.UseVisualStyleBackColor = true;
            CreateButton.Click += CreateButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(12, 196);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 5;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // SelectPathButton
            // 
            SelectPathButton.Location = new Point(9, 86);
            SelectPathButton.Name = "SelectPathButton";
            SelectPathButton.Size = new Size(75, 23);
            SelectPathButton.TabIndex = 6;
            SelectPathButton.Text = "Select Path";
            SelectPathButton.UseVisualStyleBackColor = true;
            SelectPathButton.Click += SelectPathButton_Click;
            // 
            // NewProjectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(486, 232);
            Controls.Add(SelectPathButton);
            Controls.Add(CancelButton);
            Controls.Add(CreateButton);
            Controls.Add(ProjectPathLabel);
            Controls.Add(label2);
            Controls.Add(ProjectNameTB);
            Controls.Add(label1);
            Name = "NewProjectForm";
            Text = "New Project";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FolderBrowserDialog ProjectFolderDialog;
        private Label label1;
        private TextBox ProjectNameTB;
        private Label label2;
        private Label ProjectPathLabel;
        private Button CreateButton;
        private Button CancelButton;
        private Button SelectPathButton;
    }
}