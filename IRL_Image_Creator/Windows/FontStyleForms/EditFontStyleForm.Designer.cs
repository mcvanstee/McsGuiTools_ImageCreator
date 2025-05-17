namespace IRL_Image_Creator.Windows.FontStyleForms
{
    partial class EditFontStyleForm
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
            OKButton = new Button();
            CancelButton = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            panel9 = new Panel();
            BottomMarginTB = new NumericUpDown();
            RightMarginTB = new NumericUpDown();
            TopMarginTB = new NumericUpDown();
            LeftMarginTB = new NumericUpDown();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            MonospaceNumbersCheckBox = new CheckBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            label1 = new Label();
            NameTextBox = new TextBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            label2 = new Label();
            panel1 = new Panel();
            textColorLabel = new Label();
            SelectTextColorButton = new Button();
            TextColorTB = new CustomComponents.TextBoxes.ColorTextBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            label3 = new Label();
            panel2 = new Panel();
            backColorLabel = new Label();
            SelectBackColorButton = new Button();
            BackColorTB = new CustomComponents.TextBoxes.ColorTextBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BottomMarginTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RightMarginTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TopMarginTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LeftMarginTB).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            panel2.SuspendLayout();
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
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(619, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(OKButton, 1, 0);
            tableLayoutPanel2.Controls.Add(CancelButton, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 420);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(619, 30);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // OKButton
            // 
            OKButton.Dock = DockStyle.Right;
            OKButton.Location = new Point(541, 3);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 24);
            OKButton.TabIndex = 9;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Dock = DockStyle.Left;
            CancelButton.Location = new Point(3, 3);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 24);
            CancelButton.TabIndex = 8;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(panel9, 0, 4);
            tableLayoutPanel3.Controls.Add(MonospaceNumbersCheckBox, 0, 3);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel6, 0, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(619, 420);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // panel9
            // 
            panel9.Controls.Add(BottomMarginTB);
            panel9.Controls.Add(RightMarginTB);
            panel9.Controls.Add(TopMarginTB);
            panel9.Controls.Add(LeftMarginTB);
            panel9.Controls.Add(label13);
            panel9.Controls.Add(label12);
            panel9.Controls.Add(label11);
            panel9.Controls.Add(label10);
            panel9.Controls.Add(label9);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(3, 143);
            panel9.Name = "panel9";
            panel9.Size = new Size(613, 274);
            panel9.TabIndex = 14;
            // 
            // BottomMarginTB
            // 
            BottomMarginTB.Location = new Point(64, 94);
            BottomMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            BottomMarginTB.Name = "BottomMarginTB";
            BottomMarginTB.Size = new Size(42, 23);
            BottomMarginTB.TabIndex = 32;
            // 
            // RightMarginTB
            // 
            RightMarginTB.Location = new Point(64, 71);
            RightMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            RightMarginTB.Name = "RightMarginTB";
            RightMarginTB.Size = new Size(42, 23);
            RightMarginTB.TabIndex = 31;
            // 
            // TopMarginTB
            // 
            TopMarginTB.Location = new Point(64, 48);
            TopMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            TopMarginTB.Name = "TopMarginTB";
            TopMarginTB.Size = new Size(42, 23);
            TopMarginTB.TabIndex = 30;
            // 
            // LeftMarginTB
            // 
            LeftMarginTB.Location = new Point(64, 25);
            LeftMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            LeftMarginTB.Name = "LeftMarginTB";
            LeftMarginTB.Size = new Size(42, 23);
            LeftMarginTB.TabIndex = 29;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(3, 9);
            label13.Name = "label13";
            label13.Size = new Size(45, 15);
            label13.TabIndex = 20;
            label13.Text = "Margin";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(13, 30);
            label12.Name = "label12";
            label12.Size = new Size(27, 15);
            label12.TabIndex = 21;
            label12.Text = "Left";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(13, 53);
            label11.Name = "label11";
            label11.Size = new Size(27, 15);
            label11.TabIndex = 22;
            label11.Text = "Top";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(13, 76);
            label10.Name = "label10";
            label10.Size = new Size(35, 15);
            label10.TabIndex = 23;
            label10.Text = "Right";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(13, 99);
            label9.Name = "label9";
            label9.Size = new Size(47, 15);
            label9.TabIndex = 24;
            label9.Text = "Bottom";
            // 
            // MonospaceNumbersCheckBox
            // 
            MonospaceNumbersCheckBox.AutoSize = true;
            MonospaceNumbersCheckBox.Location = new Point(8, 114);
            MonospaceNumbersCheckBox.Margin = new Padding(8, 9, 3, 3);
            MonospaceNumbersCheckBox.Name = "MonospaceNumbersCheckBox";
            MonospaceNumbersCheckBox.Size = new Size(140, 19);
            MonospaceNumbersCheckBox.TabIndex = 13;
            MonospaceNumbersCheckBox.Text = "Monospace Numbers";
            MonospaceNumbersCheckBox.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.2561178F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.74388F));
            tableLayoutPanel4.Controls.Add(label1, 0, 0);
            tableLayoutPanel4.Controls.Add(NameTextBox, 1, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(613, 29);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 7);
            label1.Margin = new Padding(3, 7, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(72, 3);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(243, 23);
            NameTextBox.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.6316643F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.36834F));
            tableLayoutPanel5.Controls.Add(label2, 0, 0);
            tableLayoutPanel5.Controls.Add(panel1, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 35);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(619, 35);
            tableLayoutPanel5.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 9);
            label2.Margin = new Padding(3, 9, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 0;
            label2.Text = "Text Color";
            // 
            // panel1
            // 
            panel1.Controls.Add(textColorLabel);
            panel1.Controls.Add(SelectTextColorButton);
            panel1.Controls.Add(TextColorTB);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(72, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(547, 35);
            panel1.TabIndex = 1;
            // 
            // textColorLabel
            // 
            textColorLabel.AutoSize = true;
            textColorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textColorLabel.Location = new Point(229, 9);
            textColorLabel.Name = "textColorLabel";
            textColorLabel.Size = new Size(12, 15);
            textColorLabel.TabIndex = 7;
            textColorLabel.Text = "-";
            // 
            // SelectTextColorButton
            // 
            SelectTextColorButton.Location = new Point(140, 5);
            SelectTextColorButton.Name = "SelectTextColorButton";
            SelectTextColorButton.Size = new Size(83, 23);
            SelectTextColorButton.TabIndex = 6;
            SelectTextColorButton.Text = "Select Color";
            SelectTextColorButton.UseVisualStyleBackColor = true;
            SelectTextColorButton.Click += SelectTextColorButton_Click;
            // 
            // TextColorTB
            // 
            TextColorTB.BackColor = SystemColors.Control;
            TextColorTB.Color = Color.White;
            TextColorTB.Location = new Point(3, 2);
            TextColorTB.MinimumSize = new Size(0, 30);
            TextColorTB.Name = "TextColorTB";
            TextColorTB.Size = new Size(118, 30);
            TextColorTB.TabIndex = 5;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.6316643F));
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 88.36834F));
            tableLayoutPanel6.Controls.Add(label3, 0, 0);
            tableLayoutPanel6.Controls.Add(panel2, 1, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(0, 70);
            tableLayoutPanel6.Margin = new Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel6.Size = new Size(619, 35);
            tableLayoutPanel6.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 9);
            label3.Margin = new Padding(3, 9, 3, 0);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 0;
            label3.Text = "Back Color";
            // 
            // panel2
            // 
            panel2.Controls.Add(backColorLabel);
            panel2.Controls.Add(SelectBackColorButton);
            panel2.Controls.Add(BackColorTB);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(72, 0);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Size = new Size(547, 35);
            panel2.TabIndex = 1;
            // 
            // backColorLabel
            // 
            backColorLabel.AutoSize = true;
            backColorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backColorLabel.Location = new Point(229, 9);
            backColorLabel.Name = "backColorLabel";
            backColorLabel.Size = new Size(12, 15);
            backColorLabel.TabIndex = 8;
            backColorLabel.Text = "-";
            // 
            // SelectBackColorButton
            // 
            SelectBackColorButton.Location = new Point(140, 5);
            SelectBackColorButton.Name = "SelectBackColorButton";
            SelectBackColorButton.Size = new Size(83, 23);
            SelectBackColorButton.TabIndex = 7;
            SelectBackColorButton.Text = "Select Color";
            SelectBackColorButton.UseVisualStyleBackColor = true;
            SelectBackColorButton.Click += SelectBackColorButton_Click;
            // 
            // BackColorTB
            // 
            BackColorTB.BackColor = SystemColors.Control;
            BackColorTB.Color = Color.White;
            BackColorTB.Location = new Point(3, 2);
            BackColorTB.MinimumSize = new Size(0, 30);
            BackColorTB.Name = "BackColorTB";
            BackColorTB.Size = new Size(118, 30);
            BackColorTB.TabIndex = 6;
            // 
            // EditFontStyleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(619, 450);
            Controls.Add(tableLayoutPanel1);
            Name = "EditFontStyleForm";
            Text = "Font Style";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panel9.ResumeLayout(false);
            panel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)BottomMarginTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)RightMarginTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)TopMarginTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)LeftMarginTB).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button CancelButton;
        private Button OKButton;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label1;
        private TextBox NameTextBox;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private Label label2;
        private Label label3;
        private CustomComponents.TextBoxes.ColorTextBox TextColorTB;
        private CustomComponents.TextBoxes.ColorTextBox BackColorTB;
        private CheckBox MonospaceNumbersCheckBox;
        private Panel panel9;
        private NumericUpDown BottomMarginTB;
        private NumericUpDown RightMarginTB;
        private NumericUpDown TopMarginTB;
        private NumericUpDown LeftMarginTB;
        private Label label13;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Panel panel1;
        private Button SelectTextColorButton;
        private Label textColorLabel;
        private Panel panel2;
        private Button SelectBackColorButton;
        private Label backColorLabel;
    }
}