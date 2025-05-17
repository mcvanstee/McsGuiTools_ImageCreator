namespace IRL_Image_Creator.Windows.TextInstructionForms
{
    partial class ImportTextForm
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
            OKButton = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            SelectedFileLabel = new Label();
            SelectFileButton = new Button();
            HasHeaderCB = new CheckBox();
            OverwriteCheckBox = new CheckBox();
            ImportFileDialog = new OpenFileDialog();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.Size = new Size(469, 291);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(CancelButton, 0, 0);
            tableLayoutPanel2.Controls.Add(OKButton, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 246);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(469, 45);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            CancelButton.Location = new Point(9, 9);
            CancelButton.Margin = new Padding(9);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 27);
            CancelButton.TabIndex = 0;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            OKButton.Location = new Point(385, 9);
            OKButton.Margin = new Padding(9);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 27);
            OKButton.TabIndex = 1;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Controls.Add(HasHeaderCB, 0, 1);
            tableLayoutPanel3.Controls.Add(OverwriteCheckBox, 0, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(469, 246);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 19.6162052F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80.3838F));
            tableLayoutPanel4.Controls.Add(SelectedFileLabel, 1, 0);
            tableLayoutPanel4.Controls.Add(SelectFileButton, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(469, 30);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // SelectedFileLabel
            // 
            SelectedFileLabel.AutoSize = true;
            SelectedFileLabel.Location = new Point(95, 7);
            SelectedFileLabel.Margin = new Padding(3, 7, 3, 0);
            SelectedFileLabel.Name = "SelectedFileLabel";
            SelectedFileLabel.Size = new Size(12, 15);
            SelectedFileLabel.TabIndex = 0;
            SelectedFileLabel.Text = "-";
            // 
            // SelectFileButton
            // 
            SelectFileButton.Location = new Point(8, 3);
            SelectFileButton.Margin = new Padding(8, 3, 3, 3);
            SelectFileButton.Name = "SelectFileButton";
            SelectFileButton.Size = new Size(75, 23);
            SelectFileButton.TabIndex = 1;
            SelectFileButton.Text = "Select File";
            SelectFileButton.UseVisualStyleBackColor = true;
            SelectFileButton.Click += SelectFileButton_Click;
            // 
            // HasHeaderCB
            // 
            HasHeaderCB.AutoSize = true;
            HasHeaderCB.Location = new Point(8, 37);
            HasHeaderCB.Margin = new Padding(8, 7, 3, 3);
            HasHeaderCB.Name = "HasHeaderCB";
            HasHeaderCB.Size = new Size(123, 19);
            HasHeaderCB.TabIndex = 1;
            HasHeaderCB.Text = "First row is Header";
            HasHeaderCB.UseVisualStyleBackColor = true;
            // 
            // OverwriteCheckBox
            // 
            OverwriteCheckBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            OverwriteCheckBox.AutoSize = true;
            OverwriteCheckBox.Location = new Point(8, 63);
            OverwriteCheckBox.Margin = new Padding(8, 3, 3, 3);
            OverwriteCheckBox.Name = "OverwriteCheckBox";
            OverwriteCheckBox.Size = new Size(147, 24);
            OverwriteCheckBox.TabIndex = 4;
            OverwriteCheckBox.Text = "Overwrite existing keys";
            OverwriteCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImportFileDialog
            // 
            ImportFileDialog.FileName = "openFileDialog1";
            // 
            // ImportTextForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(469, 291);
            Controls.Add(tableLayoutPanel1);
            Name = "ImportTextForm";
            Text = "Import Text";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button CancelButton;
        private Button OKButton;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Label SelectedFileLabel;
        private Button SelectFileButton;
        private OpenFileDialog ImportFileDialog;
        private CheckBox HasHeaderCB;
        private CheckBox OverwriteCheckBox;
    }
}