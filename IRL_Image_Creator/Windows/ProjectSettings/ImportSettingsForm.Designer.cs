namespace IRL_Image_Creator.Windows.ProjectSettings
{
    partial class ImportSettingsForm
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
            ImportButton = new Button();
            CancelButton = new Button();
            panel2 = new Panel();
            ImportFontStylesCB = new CheckBox();
            ImportIconStylesCB = new CheckBox();
            ImportTextStylesCB = new CheckBox();
            ImportPropertiesCB = new CheckBox();
            openFileDialog = new OpenFileDialog();
            ImportFontsCB = new CheckBox();
            ImportColorsCB = new CheckBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(334, 231);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(ImportButton, 1, 0);
            tableLayoutPanel2.Controls.Add(CancelButton, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 201);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(334, 30);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // ImportButton
            // 
            ImportButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ImportButton.Location = new Point(256, 3);
            ImportButton.Name = "ImportButton";
            ImportButton.Size = new Size(75, 23);
            ImportButton.TabIndex = 1;
            ImportButton.Text = "Open";
            ImportButton.UseVisualStyleBackColor = true;
            ImportButton.Click += ImportButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(3, 3);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 0;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(ImportColorsCB);
            panel2.Controls.Add(ImportFontsCB);
            panel2.Controls.Add(ImportFontStylesCB);
            panel2.Controls.Add(ImportIconStylesCB);
            panel2.Controls.Add(ImportTextStylesCB);
            panel2.Controls.Add(ImportPropertiesCB);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(334, 201);
            panel2.TabIndex = 1;
            // 
            // ImportFontStylesCB
            // 
            ImportFontStylesCB.AutoSize = true;
            ImportFontStylesCB.Location = new Point(12, 87);
            ImportFontStylesCB.Name = "ImportFontStylesCB";
            ImportFontStylesCB.Size = new Size(122, 19);
            ImportFontStylesCB.TabIndex = 3;
            ImportFontStylesCB.Text = "Import Font Styles";
            ImportFontStylesCB.UseVisualStyleBackColor = true;
            // 
            // ImportIconStylesCB
            // 
            ImportIconStylesCB.AutoSize = true;
            ImportIconStylesCB.Location = new Point(12, 62);
            ImportIconStylesCB.Name = "ImportIconStylesCB";
            ImportIconStylesCB.Size = new Size(121, 19);
            ImportIconStylesCB.TabIndex = 2;
            ImportIconStylesCB.Text = "Import Icon Styles";
            ImportIconStylesCB.UseVisualStyleBackColor = true;
            // 
            // ImportTextStylesCB
            // 
            ImportTextStylesCB.AutoSize = true;
            ImportTextStylesCB.Location = new Point(12, 37);
            ImportTextStylesCB.Name = "ImportTextStylesCB";
            ImportTextStylesCB.Size = new Size(119, 19);
            ImportTextStylesCB.TabIndex = 1;
            ImportTextStylesCB.Text = "Import Text Styles";
            ImportTextStylesCB.UseVisualStyleBackColor = true;
            // 
            // ImportPropertiesCB
            // 
            ImportPropertiesCB.AutoSize = true;
            ImportPropertiesCB.Location = new Point(12, 12);
            ImportPropertiesCB.Name = "ImportPropertiesCB";
            ImportPropertiesCB.Size = new Size(118, 19);
            ImportPropertiesCB.TabIndex = 0;
            ImportPropertiesCB.Text = "Import Properties";
            ImportPropertiesCB.UseVisualStyleBackColor = true;
            // 
            // openFileDialog
            // 
            openFileDialog.FileName = "openFileDialog1";
            // 
            // ImportFontsCB
            // 
            ImportFontsCB.AutoSize = true;
            ImportFontsCB.Location = new Point(12, 112);
            ImportFontsCB.Name = "ImportFontsCB";
            ImportFontsCB.Size = new Size(94, 19);
            ImportFontsCB.TabIndex = 4;
            ImportFontsCB.Text = "Import Fonts";
            ImportFontsCB.UseVisualStyleBackColor = true;
            // 
            // ImportColorsCB
            // 
            ImportColorsCB.AutoSize = true;
            ImportColorsCB.Location = new Point(12, 137);
            ImportColorsCB.Name = "ImportColorsCB";
            ImportColorsCB.Size = new Size(99, 19);
            ImportColorsCB.TabIndex = 5;
            ImportColorsCB.Text = "Import Colors";
            ImportColorsCB.UseVisualStyleBackColor = true;
            // 
            // ImportSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 231);
            Controls.Add(tableLayoutPanel1);
            Name = "ImportSettingsForm";
            Text = "Import Settings";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button ImportButton;
        private Button CancelButton;
        private Panel panel2;
        private CheckBox ImportFontStylesCB;
        private CheckBox ImportIconStylesCB;
        private CheckBox ImportTextStylesCB;
        private CheckBox ImportPropertiesCB;
        private OpenFileDialog openFileDialog;
        private CheckBox ImportColorsCB;
        private CheckBox ImportFontsCB;
    }
}