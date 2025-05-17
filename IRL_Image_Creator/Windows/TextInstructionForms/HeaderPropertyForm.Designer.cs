namespace IRL_Image_Creator.Windows.MainFormModels
{
    partial class HeaderPropertyForm
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
            OKButton = new Button();
            label1 = new Label();
            PropertyListView = new ListView();
            NameHeader = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            CancelButton = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(PropertyListView, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(419, 283);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // OKButton
            // 
            OKButton.Dock = DockStyle.Right;
            OKButton.Location = new Point(338, 0);
            OKButton.Margin = new Padding(0);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 24);
            OKButton.TabIndex = 4;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Bottom;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(413, 15);
            label1.TabIndex = 0;
            label1.Text = "Property";
            // 
            // PropertyListView
            // 
            PropertyListView.Columns.AddRange(new ColumnHeader[] { NameHeader, columnHeader2 });
            PropertyListView.Dock = DockStyle.Fill;
            PropertyListView.FullRowSelect = true;
            PropertyListView.GridLines = true;
            PropertyListView.Location = new Point(3, 27);
            PropertyListView.Name = "PropertyListView";
            PropertyListView.Size = new Size(413, 223);
            PropertyListView.TabIndex = 1;
            PropertyListView.UseCompatibleStateImageBehavior = false;
            PropertyListView.View = View.Details;
            // 
            // NameHeader
            // 
            NameHeader.Text = "Property";
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Name";
            columnHeader2.Width = 120;
            // 
            // CancelButton
            // 
            CancelButton.Dock = DockStyle.Left;
            CancelButton.Location = new Point(0, 0);
            CancelButton.Margin = new Padding(0);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 24);
            CancelButton.TabIndex = 3;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(OKButton, 1, 0);
            tableLayoutPanel2.Controls.Add(CancelButton, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 256);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(413, 24);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // HeaderPropertyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 283);
            Controls.Add(tableLayoutPanel1);
            Name = "HeaderPropertyForm";
            Text = "Header Property";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private ListView PropertyListView;
        public ColumnHeader NameHeader;
        private ColumnHeader columnHeader2;
        private Button CancelButton;
        private Button OKButton;
        private TableLayoutPanel tableLayoutPanel2;
    }
}