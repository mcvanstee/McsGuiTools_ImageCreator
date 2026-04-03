namespace IRL_Image_Creator.Windows.IconStyleForms
{
    partial class SelectIconDataLocationForm
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
            selectButton = new Button();
            cancelButton = new Button();
            panel2 = new Panel();
            fontDataLocationComboBox = new ComboBox();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(312, 228);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(selectButton);
            panel1.Controls.Add(cancelButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 198);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(312, 30);
            panel1.TabIndex = 0;
            // 
            // selectButton
            // 
            selectButton.Location = new Point(234, 3);
            selectButton.Name = "selectButton";
            selectButton.Size = new Size(75, 23);
            selectButton.TabIndex = 1;
            selectButton.Text = "Select";
            selectButton.UseVisualStyleBackColor = true;
            selectButton.Click += SelectButton_Click;
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
            // panel2
            // 
            panel2.Controls.Add(fontDataLocationComboBox);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(3, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(306, 192);
            panel2.TabIndex = 1;
            // 
            // fontDataLocationComboBox
            // 
            fontDataLocationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            fontDataLocationComboBox.FormattingEnabled = true;
            fontDataLocationComboBox.Location = new Point(69, 82);
            fontDataLocationComboBox.Name = "fontDataLocationComboBox";
            fontDataLocationComboBox.Size = new Size(170, 23);
            fontDataLocationComboBox.TabIndex = 1;
            fontDataLocationComboBox.SelectedIndexChanged += FontDataLocationComboBox_SelectedIndexChanged;
            // 
            // SelectIconDataLocationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(312, 228);
            Controls.Add(tableLayoutPanel1);
            Name = "SelectIconDataLocationForm";
            Text = "Select Datalocation";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button button2;
        private Button cancelButton;
        private Panel panel2;
        private ComboBox fontDataLocationComboBox;
        private Button selectButton;
    }
}