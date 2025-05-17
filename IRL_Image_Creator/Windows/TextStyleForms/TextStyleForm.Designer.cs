namespace IRL_Image_Creator.Windows.TextStyleForms
{
    partial class TextStyleForm
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
            panel1 = new Panel();
            panel3 = new Panel();
            DeleteButton = new Button();
            EditButton = new Button();
            NewButton = new Button();
            CloseButton = new Button();
            panel2 = new Panel();
            TextStyleListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader9 = new ColumnHeader();
            columnHeader10 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(674, 431);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(DeleteButton);
            panel3.Controls.Add(EditButton);
            panel3.Controls.Add(NewButton);
            panel3.Controls.Add(CloseButton);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 403);
            panel3.Name = "panel3";
            panel3.Size = new Size(674, 28);
            panel3.TabIndex = 1;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(165, 3);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 3;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // EditButton
            // 
            EditButton.Location = new Point(84, 3);
            EditButton.Name = "EditButton";
            EditButton.Size = new Size(75, 23);
            EditButton.TabIndex = 2;
            EditButton.Text = "Edit";
            EditButton.UseVisualStyleBackColor = true;
            EditButton.Click += EditButton_Click;
            // 
            // NewButton
            // 
            NewButton.Location = new Point(3, 3);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(75, 23);
            NewButton.TabIndex = 1;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Click += NewButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.Location = new Point(596, 2);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(75, 23);
            CloseButton.TabIndex = 0;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(TextStyleListView);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(674, 431);
            panel2.TabIndex = 0;
            // 
            // TextStyleListView
            // 
            TextStyleListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader9, columnHeader10, columnHeader2, columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            TextStyleListView.FullRowSelect = true;
            TextStyleListView.GridLines = true;
            TextStyleListView.Location = new Point(0, 0);
            TextStyleListView.MultiSelect = false;
            TextStyleListView.Name = "TextStyleListView";
            TextStyleListView.Size = new Size(674, 431);
            TextStyleListView.TabIndex = 0;
            TextStyleListView.UseCompatibleStateImageBehavior = false;
            TextStyleListView.View = View.Details;
            TextStyleListView.SelectedIndexChanged += TextStyleListView_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 70;
            // 
            // columnHeader9
            // 
            columnHeader9.Text = "Prefix";
            // 
            // columnHeader10
            // 
            columnHeader10.Text = "Project Font";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Font";
            columnHeader2.Width = 70;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Text color";
            columnHeader5.Width = 68;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Back color";
            columnHeader6.Width = 68;
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Margin";
            columnHeader7.Width = 65;
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Property";
            columnHeader8.Width = 200;
            // 
            // TextStyleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 431);
            Controls.Add(panel1);
            Name = "TextStyleForm";
            Text = "Text Style";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Button NewButton;
        private Button CloseButton;
        private ListView TextStyleListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private Button EditButton;
        private ColumnHeader columnHeader9;
        private Button DeleteButton;
        private ColumnHeader columnHeader10;
    }
}