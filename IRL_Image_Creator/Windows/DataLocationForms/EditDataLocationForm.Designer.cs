namespace IRL_Image_Creator.Windows.DataLocationForms
{
    partial class EditDataLocationForm
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
            panel1 = new Panel();
            okButton = new Button();
            cancelButton = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            panel2 = new Panel();
            label2 = new Label();
            panel3 = new Panel();
            locationIDLabel = new Label();
            panel4 = new Panel();
            label3 = new Label();
            panel5 = new Panel();
            nameTextBox = new TextBox();
            panel6 = new Panel();
            label1 = new Label();
            panel7 = new Panel();
            locationTypeErrorLabel = new Label();
            locationTypeComboBox = new ComboBox();
            panel8 = new Panel();
            label4 = new Label();
            panel9 = new Panel();
            compressionErrorLabel = new Label();
            compressionComboBox = new ComboBox();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            panel9.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(546, 357);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(okButton);
            panel1.Controls.Add(cancelButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 327);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(546, 30);
            panel1.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            okButton.Location = new Point(468, 4);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 3;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OKButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(3, 4);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += CancelButton_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(panel2, 0, 0);
            tableLayoutPanel2.Controls.Add(panel3, 1, 0);
            tableLayoutPanel2.Controls.Add(panel4, 0, 1);
            tableLayoutPanel2.Controls.Add(panel5, 1, 1);
            tableLayoutPanel2.Controls.Add(panel6, 0, 2);
            tableLayoutPanel2.Controls.Add(panel7, 1, 2);
            tableLayoutPanel2.Controls.Add(panel8, 0, 3);
            tableLayoutPanel2.Controls.Add(panel9, 1, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(546, 327);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(85, 30);
            panel2.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(4, 9);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 0;
            label2.Text = "Location ID";
            // 
            // panel3
            // 
            panel3.Controls.Add(locationIDLabel);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(85, 0);
            panel3.Margin = new Padding(0);
            panel3.Name = "panel3";
            panel3.Size = new Size(461, 30);
            panel3.TabIndex = 1;
            // 
            // locationIDLabel
            // 
            locationIDLabel.AutoSize = true;
            locationIDLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            locationIDLabel.Location = new Point(4, 7);
            locationIDLabel.Name = "locationIDLabel";
            locationIDLabel.Size = new Size(15, 17);
            locationIDLabel.TabIndex = 0;
            locationIDLabel.Text = "0";
            // 
            // panel4
            // 
            panel4.Controls.Add(label3);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(0, 30);
            panel4.Margin = new Padding(0);
            panel4.Name = "panel4";
            panel4.Size = new Size(85, 30);
            panel4.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 8);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 0;
            label3.Text = "Name";
            // 
            // panel5
            // 
            panel5.Controls.Add(nameTextBox);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(85, 30);
            panel5.Margin = new Padding(0);
            panel5.Name = "panel5";
            panel5.Size = new Size(461, 30);
            panel5.TabIndex = 3;
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(4, 5);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(175, 23);
            nameTextBox.TabIndex = 0;
            // 
            // panel6
            // 
            panel6.Controls.Add(label1);
            panel6.Dock = DockStyle.Fill;
            panel6.Location = new Point(0, 60);
            panel6.Margin = new Padding(0);
            panel6.Name = "panel6";
            panel6.Size = new Size(85, 30);
            panel6.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 4);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 0;
            label1.Text = "Location Type";
            // 
            // panel7
            // 
            panel7.Controls.Add(locationTypeErrorLabel);
            panel7.Controls.Add(locationTypeComboBox);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(85, 60);
            panel7.Margin = new Padding(0);
            panel7.Name = "panel7";
            panel7.Size = new Size(461, 30);
            panel7.TabIndex = 5;
            // 
            // locationTypeErrorLabel
            // 
            locationTypeErrorLabel.AutoSize = true;
            locationTypeErrorLabel.Location = new Point(131, 7);
            locationTypeErrorLabel.Name = "locationTypeErrorLabel";
            locationTypeErrorLabel.Size = new Size(30, 15);
            locationTypeErrorLabel.TabIndex = 1;
            locationTypeErrorLabel.Text = "msg";
            // 
            // locationTypeComboBox
            // 
            locationTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            locationTypeComboBox.FormattingEnabled = true;
            locationTypeComboBox.Location = new Point(4, 4);
            locationTypeComboBox.Name = "locationTypeComboBox";
            locationTypeComboBox.Size = new Size(121, 23);
            locationTypeComboBox.TabIndex = 0;
            locationTypeComboBox.SelectedIndexChanged += LocationTypeComboBox_SelectedIndexChanged;
            // 
            // panel8
            // 
            panel8.Controls.Add(label4);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(0, 90);
            panel8.Margin = new Padding(0);
            panel8.Name = "panel8";
            panel8.Size = new Size(85, 30);
            panel8.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 7);
            label4.Name = "label4";
            label4.Size = new Size(77, 15);
            label4.TabIndex = 0;
            label4.Text = "Compression";
            // 
            // panel9
            // 
            panel9.Controls.Add(compressionErrorLabel);
            panel9.Controls.Add(compressionComboBox);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(85, 90);
            panel9.Margin = new Padding(0);
            panel9.Name = "panel9";
            panel9.Size = new Size(461, 30);
            panel9.TabIndex = 7;
            // 
            // compressionErrorLabel
            // 
            compressionErrorLabel.AutoSize = true;
            compressionErrorLabel.Location = new Point(131, 7);
            compressionErrorLabel.Name = "compressionErrorLabel";
            compressionErrorLabel.Size = new Size(30, 15);
            compressionErrorLabel.TabIndex = 9;
            compressionErrorLabel.Text = "msg";
            // 
            // compressionComboBox
            // 
            compressionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            compressionComboBox.FormattingEnabled = true;
            compressionComboBox.Location = new Point(4, 3);
            compressionComboBox.Name = "compressionComboBox";
            compressionComboBox.Size = new Size(121, 23);
            compressionComboBox.TabIndex = 8;
            compressionComboBox.SelectedIndexChanged += CompressionComboBox_SelectedIndexChanged;
            // 
            // EditDataLocationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(546, 357);
            Controls.Add(tableLayoutPanel1);
            Name = "EditDataLocationForm";
            Text = "Data Location";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button okButton;
        private Button cancelButton;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel2;
        private Label label3;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Label label1;
        private Panel panel7;
        private ComboBox locationTypeComboBox;
        private TextBox nameTextBox;
        private Panel panel8;
        private Label label4;
        private Panel panel9;
        private ComboBox compressionComboBox;
        private Label label2;
        private Label locationIDLabel;
        private Label locationTypeErrorLabel;
        private Label compressionErrorLabel;
    }
}