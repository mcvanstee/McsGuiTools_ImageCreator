namespace IRL_Image_Creator.Windows.ProjectSettings
{
    partial class ExportSettingsForm
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
            ExportButton = new Button();
            CancelButton = new Button();
            panel2 = new Panel();
            ExportColorsCB = new CheckBox();
            ExportFontsCB = new CheckBox();
            ExportFontStylesCB = new CheckBox();
            ExportIconStylesCB = new CheckBox();
            ExportTextStylesCB = new CheckBox();
            ExportPropertiesCB = new CheckBox();
            saveFileDialog = new SaveFileDialog();
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
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(ExportButton, 1, 0);
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
            // ExportButton
            // 
            ExportButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExportButton.Location = new Point(256, 3);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(75, 23);
            ExportButton.TabIndex = 1;
            ExportButton.Text = "Save";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += ExportButton_Click;
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
            panel2.Controls.Add(ExportColorsCB);
            panel2.Controls.Add(ExportFontsCB);
            panel2.Controls.Add(ExportFontStylesCB);
            panel2.Controls.Add(ExportIconStylesCB);
            panel2.Controls.Add(ExportTextStylesCB);
            panel2.Controls.Add(ExportPropertiesCB);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(334, 201);
            panel2.TabIndex = 1;
            // 
            // ExportColorsCB
            // 
            ExportColorsCB.AutoSize = true;
            ExportColorsCB.Location = new Point(12, 137);
            ExportColorsCB.Name = "ExportColorsCB";
            ExportColorsCB.Size = new Size(96, 19);
            ExportColorsCB.TabIndex = 5;
            ExportColorsCB.Text = "Export Colors";
            ExportColorsCB.UseVisualStyleBackColor = true;
            // 
            // ExportFontsCB
            // 
            ExportFontsCB.AutoSize = true;
            ExportFontsCB.Location = new Point(12, 112);
            ExportFontsCB.Name = "ExportFontsCB";
            ExportFontsCB.Size = new Size(91, 19);
            ExportFontsCB.TabIndex = 4;
            ExportFontsCB.Text = "Export Fonts";
            ExportFontsCB.UseVisualStyleBackColor = true;
            // 
            // ExportFontStylesCB
            // 
            ExportFontStylesCB.AutoSize = true;
            ExportFontStylesCB.Location = new Point(12, 87);
            ExportFontStylesCB.Name = "ExportFontStylesCB";
            ExportFontStylesCB.Size = new Size(119, 19);
            ExportFontStylesCB.TabIndex = 3;
            ExportFontStylesCB.Text = "Export Font Styles";
            ExportFontStylesCB.UseVisualStyleBackColor = true;
            // 
            // ExportIconStylesCB
            // 
            ExportIconStylesCB.AutoSize = true;
            ExportIconStylesCB.Location = new Point(12, 62);
            ExportIconStylesCB.Name = "ExportIconStylesCB";
            ExportIconStylesCB.Size = new Size(118, 19);
            ExportIconStylesCB.TabIndex = 2;
            ExportIconStylesCB.Text = "Export Icon Styles";
            ExportIconStylesCB.UseVisualStyleBackColor = true;
            // 
            // ExportTextStylesCB
            // 
            ExportTextStylesCB.AutoSize = true;
            ExportTextStylesCB.Location = new Point(12, 37);
            ExportTextStylesCB.Name = "ExportTextStylesCB";
            ExportTextStylesCB.Size = new Size(116, 19);
            ExportTextStylesCB.TabIndex = 1;
            ExportTextStylesCB.Text = "Export Text Styles";
            ExportTextStylesCB.UseVisualStyleBackColor = true;
            // 
            // ExportPropertiesCB
            // 
            ExportPropertiesCB.AutoSize = true;
            ExportPropertiesCB.Location = new Point(12, 12);
            ExportPropertiesCB.Name = "ExportPropertiesCB";
            ExportPropertiesCB.Size = new Size(115, 19);
            ExportPropertiesCB.TabIndex = 0;
            ExportPropertiesCB.Text = "Export Properties";
            ExportPropertiesCB.UseVisualStyleBackColor = true;
            // 
            // ExportSettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(334, 231);
            Controls.Add(tableLayoutPanel1);
            Name = "ExportSettingsForm";
            Text = "Export Settings";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button ExportButton;
        private Button CancelButton;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel2;
        private CheckBox ExportFontStylesCB;
        private CheckBox ExportIconStylesCB;
        private CheckBox ExportTextStylesCB;
        private CheckBox ExportPropertiesCB;
        private SaveFileDialog saveFileDialog;
        private CheckBox ExportColorsCB;
        private CheckBox ExportFontsCB;
    }
}