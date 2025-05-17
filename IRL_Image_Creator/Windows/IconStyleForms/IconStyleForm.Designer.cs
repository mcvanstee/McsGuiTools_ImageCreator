namespace IRL_Image_Creator.Windows.IconStyleForms
{
    partial class IconStyleForm
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
            tableLayoutPanel1 = new TableLayoutPanel();
            IconStyleListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            panel1 = new Panel();
            CloseButton = new Button();
            DeleteButton = new Button();
            EditButton = new Button();
            NewButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(IconStyleListView, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(674, 431);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // IconStyleListView
            // 
            IconStyleListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            IconStyleListView.FullRowSelect = true;
            IconStyleListView.GridLines = true;
            IconStyleListView.Location = new Point(3, 3);
            IconStyleListView.MultiSelect = false;
            IconStyleListView.Name = "IconStyleListView";
            IconStyleListView.Size = new Size(668, 395);
            IconStyleListView.TabIndex = 1;
            IconStyleListView.UseCompatibleStateImageBehavior = false;
            IconStyleListView.View = View.Details;
            IconStyleListView.SelectedIndexChanged += IconStyleListView_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Prefix";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Back color";
            columnHeader5.Width = 68;
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Colors";
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
            // panel1
            // 
            panel1.Controls.Add(CloseButton);
            panel1.Controls.Add(DeleteButton);
            panel1.Controls.Add(EditButton);
            panel1.Controls.Add(NewButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 401);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(674, 30);
            panel1.TabIndex = 0;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.Location = new Point(596, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(75, 23);
            CloseButton.TabIndex = 5;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(165, 4);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(75, 23);
            DeleteButton.TabIndex = 4;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // EditButton
            // 
            EditButton.Location = new Point(84, 4);
            EditButton.Name = "EditButton";
            EditButton.Size = new Size(75, 23);
            EditButton.TabIndex = 3;
            EditButton.Text = "Edit";
            EditButton.UseVisualStyleBackColor = true;
            EditButton.Click += EditButton_Click;
            // 
            // NewButton
            // 
            NewButton.Location = new Point(3, 4);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(75, 23);
            NewButton.TabIndex = 2;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Click += NewButton_Click;
            // 
            // IconStyleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 431);
            Controls.Add(tableLayoutPanel1);
            Name = "IconStyleForm";
            Text = "Icon Style";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button NewButton;
        private Button EditButton;
        private Button DeleteButton;
        private Button CloseButton;
        private ListView IconStyleListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
    }
}