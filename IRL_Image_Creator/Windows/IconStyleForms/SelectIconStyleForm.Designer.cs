namespace IRL_Image_Creator.Windows.IconStyleForms
{
    partial class SelectIconStyleForm
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
            tableLayoutPanel2 = new TableLayoutPanel();
            CancelButton = new Button();
            SelectButton = new Button();
            IconStyleListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(IconStyleListView, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(674, 431);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(SelectButton, 1, 0);
            tableLayoutPanel2.Controls.Add(CancelButton, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 401);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(674, 30);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(3, 3);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 2;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // SelectButton
            // 
            SelectButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SelectButton.Location = new Point(596, 3);
            SelectButton.Name = "SelectButton";
            SelectButton.Size = new Size(75, 23);
            SelectButton.TabIndex = 3;
            SelectButton.Text = "Select";
            SelectButton.UseVisualStyleBackColor = true;
            SelectButton.Click += SelectButton_Click;
            // 
            // IconStyleListView
            // 
            IconStyleListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader5, columnHeader6, columnHeader7, columnHeader8 });
            IconStyleListView.FullRowSelect = true;
            IconStyleListView.GridLines = true;
            IconStyleListView.Location = new Point(3, 3);
            IconStyleListView.Name = "IconStyleListView";
            IconStyleListView.Size = new Size(668, 395);
            IconStyleListView.TabIndex = 2;
            IconStyleListView.UseCompatibleStateImageBehavior = false;
            IconStyleListView.View = View.Details;
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
            // SelectIconStyleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 431);
            Controls.Add(tableLayoutPanel1);
            Name = "SelectIconStyleForm";
            Text = "Select Icon Style";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button CancelButton;
        private Button SelectButton;
        private ListView IconStyleListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
    }
}