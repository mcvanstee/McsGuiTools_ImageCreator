namespace IRL_Image_Creator.Windows
{
    partial class SelectColorIdForm
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
            cancelButton = new Button();
            okButton = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            label1 = new Label();
            idTextBox = new TextBox();
            label2 = new Label();
            colorTextBox = new CustomComponents.TextBoxes.ColorTextBox();
            panel1 = new Panel();
            SelectColorButton = new Button();
            colorLabel = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 82.30089F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 17.6991158F));
            tableLayoutPanel1.Size = new Size(448, 254);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(cancelButton, 0, 0);
            tableLayoutPanel2.Controls.Add(okButton, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 211);
            tableLayoutPanel2.Margin = new Padding(3, 2, 3, 2);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(442, 41);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // cancelButton
            // 
            cancelButton.Dock = DockStyle.Left;
            cancelButton.Location = new Point(8, 8);
            cancelButton.Margin = new Padding(8);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(82, 25);
            cancelButton.TabIndex = 0;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // okButton
            // 
            okButton.Dock = DockStyle.Right;
            okButton.Location = new Point(352, 8);
            okButton.Margin = new Padding(8);
            okButton.Name = "okButton";
            okButton.Size = new Size(82, 25);
            okButton.TabIndex = 1;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(label1, 0, 0);
            tableLayoutPanel3.Controls.Add(idTextBox, 0, 1);
            tableLayoutPanel3.Controls.Add(label2, 0, 2);
            tableLayoutPanel3.Controls.Add(panel1, 0, 3);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(442, 203);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(17, 15);
            label1.TabIndex = 0;
            label1.Text = "Id";
            // 
            // idTextBox
            // 
            idTextBox.Location = new Point(3, 18);
            idTextBox.Name = "idTextBox";
            idTextBox.Size = new Size(216, 23);
            idTextBox.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Bottom;
            label2.Location = new Point(3, 53);
            label2.Name = "label2";
            label2.Size = new Size(436, 15);
            label2.TabIndex = 2;
            label2.Text = "Color";
            // 
            // colorTextBox
            // 
            colorTextBox.BackColor = SystemColors.Control;
            colorTextBox.Color = Color.White;
            colorTextBox.Location = new Point(3, 2);
            colorTextBox.MinimumSize = new Size(0, 30);
            colorTextBox.Name = "colorTextBox";
            colorTextBox.Size = new Size(118, 30);
            colorTextBox.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(colorLabel);
            panel1.Controls.Add(SelectColorButton);
            panel1.Controls.Add(colorTextBox);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 68);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(442, 35);
            panel1.TabIndex = 4;
            // 
            // SelectColorButton
            // 
            SelectColorButton.Location = new Point(138, 5);
            SelectColorButton.Name = "SelectColorButton";
            SelectColorButton.Size = new Size(83, 23);
            SelectColorButton.TabIndex = 8;
            SelectColorButton.Text = "Select Color";
            SelectColorButton.UseVisualStyleBackColor = true;
            SelectColorButton.Click += SelectColorButton_Click;
            // 
            // colorLabel
            // 
            colorLabel.AutoSize = true;
            colorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            colorLabel.Location = new Point(227, 9);
            colorLabel.Name = "colorLabel";
            colorLabel.Size = new Size(12, 15);
            colorLabel.TabIndex = 9;
            colorLabel.Text = "-";
            // 
            // SelectColorIdForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(448, 254);
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "SelectColorIdForm";
            Text = "Add Colors";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button cancelButton;
        private Button okButton;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label1;
        private TextBox idTextBox;
        private Label label2;
        private CustomComponents.TextBoxes.ColorTextBox colorTextBox;
        private Panel panel1;
        private Button SelectColorButton;
        private Label colorLabel;
    }
}