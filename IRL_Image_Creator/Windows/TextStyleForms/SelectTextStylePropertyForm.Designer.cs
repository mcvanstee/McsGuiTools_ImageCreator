namespace IRL_Image_Creator.Windows
{
    partial class SelectTextStylePropertyForm
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
            label1 = new Label();
            PropertyListView = new ListView();
            NameHeader = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            ValueListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            CancelButton = new Button();
            OKButton = new Button();
            label2 = new Label();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(PropertyListView, 0, 1);
            tableLayoutPanel1.Controls.Add(ValueListView, 1, 1);
            tableLayoutPanel1.Controls.Add(CancelButton, 0, 2);
            tableLayoutPanel1.Controls.Add(OKButton, 1, 2);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 53F));
            tableLayoutPanel1.Size = new Size(401, 327);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Bottom;
            label1.Location = new Point(3, 12);
            label1.Name = "label1";
            label1.Size = new Size(194, 20);
            label1.TabIndex = 0;
            label1.Text = "Property";
            // 
            // PropertyListView
            // 
            PropertyListView.Columns.AddRange(new ColumnHeader[] { NameHeader, columnHeader2 });
            PropertyListView.Dock = DockStyle.Fill;
            PropertyListView.FullRowSelect = true;
            PropertyListView.GridLines = true;
            PropertyListView.Location = new Point(3, 36);
            PropertyListView.Margin = new Padding(3, 4, 3, 4);
            PropertyListView.Name = "PropertyListView";
            PropertyListView.Size = new Size(194, 234);
            PropertyListView.TabIndex = 1;
            PropertyListView.UseCompatibleStateImageBehavior = false;
            PropertyListView.View = View.Details;
            PropertyListView.MouseClick += PropertyListView_MouseClick;
            // 
            // NameHeader
            // 
            NameHeader.Text = "Property";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Name";
            columnHeader2.Width = 90;
            // 
            // ValueListView
            // 
            ValueListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader3 });
            ValueListView.Dock = DockStyle.Fill;
            ValueListView.FullRowSelect = true;
            ValueListView.GridLines = true;
            ValueListView.Location = new Point(203, 36);
            ValueListView.Margin = new Padding(3, 4, 3, 4);
            ValueListView.Name = "ValueListView";
            ValueListView.Size = new Size(195, 234);
            ValueListView.TabIndex = 2;
            ValueListView.UseCompatibleStateImageBehavior = false;
            ValueListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Value";
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Name";
            columnHeader3.Width = 100;
            // 
            // CancelButton
            // 
            CancelButton.Dock = DockStyle.Left;
            CancelButton.Location = new Point(9, 285);
            CancelButton.Margin = new Padding(9, 11, 9, 11);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(86, 31);
            CancelButton.TabIndex = 3;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // OKButton
            // 
            OKButton.Dock = DockStyle.Right;
            OKButton.Location = new Point(306, 285);
            OKButton.Margin = new Padding(9, 11, 9, 11);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(86, 31);
            OKButton.TabIndex = 4;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Bottom;
            label2.Location = new Point(203, 12);
            label2.Name = "label2";
            label2.Size = new Size(195, 20);
            label2.TabIndex = 5;
            label2.Text = "Value";
            // 
            // SelectTextStylePropertyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(401, 327);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(419, 374);
            MinimumSize = new Size(419, 374);
            Name = "SelectTextStylePropertyForm";
            Text = "Select Property";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private ListView PropertyListView;
        private ListView ValueListView;
        private Button CancelButton;
        private Button OKButton;
        private Label label2;
        public ColumnHeader NameHeader;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader3;
    }
}