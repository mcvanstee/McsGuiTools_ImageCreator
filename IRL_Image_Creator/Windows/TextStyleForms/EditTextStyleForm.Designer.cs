namespace IRL_Image_Creator.Windows.TextStyleForms
{
    partial class EditTextStyleForm
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
            CancelButton = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            label4 = new Label();
            TextStylePropertiesListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            tableLayoutPanel3 = new TableLayoutPanel();
            AddTextStylePropertyButton = new Button();
            DeleteTextStylePropertyButton = new Button();
            tableLayoutPanel4 = new TableLayoutPanel();
            SelectBackColorButton = new Button();
            BackColorTB = new CustomComponents.TextBoxes.ColorTextBox();
            MonospaceNumbersCheckBox = new CheckBox();
            BottomMarginTB = new NumericUpDown();
            RightMarginTB = new NumericUpDown();
            TopMarginTB = new NumericUpDown();
            LeftMarginTB = new NumericUpDown();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            tableLayoutPanel5 = new TableLayoutPanel();
            SelectFontButton = new Button();
            SelectedFontLabel = new Label();
            NameTB = new TextBox();
            label3 = new Label();
            PrefixTB = new TextBox();
            label5 = new Label();
            SelectTextColorButton = new Button();
            TextColorTB = new CustomComponents.TextBoxes.ColorTextBox();
            panel10 = new Panel();
            label1 = new Label();
            textColorLabel = new Label();
            backColorLabel = new Label();
            label2 = new Label();
            SelectFontDialog = new FontDialog();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)BottomMarginTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RightMarginTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TopMarginTB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LeftMarginTB).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            panel10.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tableLayoutPanel1.Controls.Add(OKButton, 1, 1);
            tableLayoutPanel1.Controls.Add(CancelButton, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(767, 460);
            tableLayoutPanel1.TabIndex = 16;
            // 
            // OKButton
            // 
            OKButton.Dock = DockStyle.Right;
            OKButton.Location = new Point(689, 433);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 24);
            OKButton.TabIndex = 6;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // CancelButton
            // 
            CancelButton.Dock = DockStyle.Left;
            CancelButton.Location = new Point(3, 433);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 24);
            CancelButton.TabIndex = 7;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(label4, 0, 0);
            tableLayoutPanel2.Controls.Add(TextStylePropertiesListView, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(463, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.Size = new Size(301, 424);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Bottom;
            label4.Location = new Point(3, 11);
            label4.Name = "label4";
            label4.Size = new Size(295, 15);
            label4.TabIndex = 11;
            label4.Text = "Properties";
            // 
            // TextStylePropertiesListView
            // 
            TextStylePropertiesListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2 });
            TextStylePropertiesListView.Dock = DockStyle.Fill;
            TextStylePropertiesListView.FullRowSelect = true;
            TextStylePropertiesListView.GridLines = true;
            TextStylePropertiesListView.Location = new Point(3, 29);
            TextStylePropertiesListView.Name = "TextStylePropertiesListView";
            TextStylePropertiesListView.Size = new Size(295, 352);
            TextStylePropertiesListView.TabIndex = 10;
            TextStylePropertiesListView.UseCompatibleStateImageBehavior = false;
            TextStylePropertiesListView.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Property";
            columnHeader1.Width = 100;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Value";
            columnHeader2.Width = 80;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tableLayoutPanel3.Controls.Add(AddTextStylePropertyButton, 0, 0);
            tableLayoutPanel3.Controls.Add(DeleteTextStylePropertyButton, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 387);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(295, 34);
            tableLayoutPanel3.TabIndex = 12;
            // 
            // AddTextStylePropertyButton
            // 
            AddTextStylePropertyButton.Dock = DockStyle.Fill;
            AddTextStylePropertyButton.Location = new Point(5, 5);
            AddTextStylePropertyButton.Margin = new Padding(5);
            AddTextStylePropertyButton.Name = "AddTextStylePropertyButton";
            AddTextStylePropertyButton.Size = new Size(87, 24);
            AddTextStylePropertyButton.TabIndex = 12;
            AddTextStylePropertyButton.Text = "Add";
            AddTextStylePropertyButton.UseVisualStyleBackColor = true;
            AddTextStylePropertyButton.Click += AddTextStylePropertyButton_Click;
            // 
            // DeleteTextStylePropertyButton
            // 
            DeleteTextStylePropertyButton.Dock = DockStyle.Fill;
            DeleteTextStylePropertyButton.Location = new Point(202, 5);
            DeleteTextStylePropertyButton.Margin = new Padding(5);
            DeleteTextStylePropertyButton.Name = "DeleteTextStylePropertyButton";
            DeleteTextStylePropertyButton.Size = new Size(88, 24);
            DeleteTextStylePropertyButton.TabIndex = 13;
            DeleteTextStylePropertyButton.Text = "Delete";
            DeleteTextStylePropertyButton.UseVisualStyleBackColor = true;
            DeleteTextStylePropertyButton.Click += DeleteTextStylePropertyButton_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel4.Controls.Add(panel10, 0, 2);
            tableLayoutPanel4.Controls.Add(groupBox1, 0, 0);
            tableLayoutPanel4.Controls.Add(groupBox4, 0, 3);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 5;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(460, 430);
            tableLayoutPanel4.TabIndex = 9;
            // 
            // SelectBackColorButton
            // 
            SelectBackColorButton.Location = new Point(142, 102);
            SelectBackColorButton.Name = "SelectBackColorButton";
            SelectBackColorButton.Size = new Size(83, 23);
            SelectBackColorButton.TabIndex = 6;
            SelectBackColorButton.Text = "Select Color";
            SelectBackColorButton.UseVisualStyleBackColor = true;
            SelectBackColorButton.Click += SelectBackColorButton_Click;
            // 
            // BackColorTB
            // 
            BackColorTB.BackColor = SystemColors.Control;
            BackColorTB.Color = Color.White;
            BackColorTB.Location = new Point(12, 99);
            BackColorTB.MinimumSize = new Size(0, 30);
            BackColorTB.Name = "BackColorTB";
            BackColorTB.Size = new Size(118, 30);
            BackColorTB.TabIndex = 5;
            // 
            // MonospaceNumbersCheckBox
            // 
            MonospaceNumbersCheckBox.AutoSize = true;
            MonospaceNumbersCheckBox.Location = new Point(139, 22);
            MonospaceNumbersCheckBox.Name = "MonospaceNumbersCheckBox";
            MonospaceNumbersCheckBox.Size = new Size(140, 19);
            MonospaceNumbersCheckBox.TabIndex = 12;
            MonospaceNumbersCheckBox.Text = "Monospace Numbers";
            MonospaceNumbersCheckBox.UseVisualStyleBackColor = true;
            // 
            // BottomMarginTB
            // 
            BottomMarginTB.Location = new Point(64, 91);
            BottomMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            BottomMarginTB.Name = "BottomMarginTB";
            BottomMarginTB.Size = new Size(42, 23);
            BottomMarginTB.TabIndex = 32;
            // 
            // RightMarginTB
            // 
            RightMarginTB.Location = new Point(64, 68);
            RightMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            RightMarginTB.Name = "RightMarginTB";
            RightMarginTB.Size = new Size(42, 23);
            RightMarginTB.TabIndex = 31;
            // 
            // TopMarginTB
            // 
            TopMarginTB.Location = new Point(64, 45);
            TopMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            TopMarginTB.Name = "TopMarginTB";
            TopMarginTB.Size = new Size(42, 23);
            TopMarginTB.TabIndex = 30;
            // 
            // LeftMarginTB
            // 
            LeftMarginTB.Location = new Point(64, 22);
            LeftMarginTB.Minimum = new decimal(new int[] { 10, 0, 0, int.MinValue });
            LeftMarginTB.Name = "LeftMarginTB";
            LeftMarginTB.Size = new Size(42, 23);
            LeftMarginTB.TabIndex = 29;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(31, 27);
            label12.Name = "label12";
            label12.Size = new Size(27, 15);
            label12.TabIndex = 21;
            label12.Text = "Left";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(31, 50);
            label11.Name = "label11";
            label11.Size = new Size(27, 15);
            label11.TabIndex = 22;
            label11.Text = "Top";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(23, 73);
            label10.Name = "label10";
            label10.Size = new Size(35, 15);
            label10.TabIndex = 23;
            label10.Text = "Right";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(13, 96);
            label9.Name = "label9";
            label9.Size = new Size(47, 15);
            label9.TabIndex = 24;
            label9.Text = "Bottom";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(groupBox2, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 90);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(460, 60);
            tableLayoutPanel5.TabIndex = 10;
            // 
            // SelectFontButton
            // 
            SelectFontButton.Location = new Point(13, 22);
            SelectFontButton.Name = "SelectFontButton";
            SelectFontButton.Size = new Size(75, 23);
            SelectFontButton.TabIndex = 0;
            SelectFontButton.Text = "Select Font";
            SelectFontButton.UseVisualStyleBackColor = true;
            SelectFontButton.Click += SelectFontButton_Click;
            // 
            // SelectedFontLabel
            // 
            SelectedFontLabel.AutoSize = true;
            SelectedFontLabel.Location = new Point(94, 26);
            SelectedFontLabel.Name = "SelectedFontLabel";
            SelectedFontLabel.Size = new Size(36, 15);
            SelectedFontLabel.TabIndex = 1;
            SelectedFontLabel.Text = "None";
            // 
            // NameTB
            // 
            NameTB.Location = new Point(64, 22);
            NameTB.Name = "NameTB";
            NameTB.Size = new Size(214, 23);
            NameTB.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(19, 25);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 8;
            label3.Text = "Name";
            // 
            // PrefixTB
            // 
            PrefixTB.Location = new Point(64, 51);
            PrefixTB.Name = "PrefixTB";
            PrefixTB.Size = new Size(214, 23);
            PrefixTB.TabIndex = 0;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 51);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 0;
            label5.Text = "Prefix";
            // 
            // SelectTextColorButton
            // 
            SelectTextColorButton.Location = new Point(142, 40);
            SelectTextColorButton.Name = "SelectTextColorButton";
            SelectTextColorButton.Size = new Size(83, 23);
            SelectTextColorButton.TabIndex = 5;
            SelectTextColorButton.Text = "Select Color";
            SelectTextColorButton.UseVisualStyleBackColor = true;
            SelectTextColorButton.Click += SelectTextColorButton_Click;
            // 
            // TextColorTB
            // 
            TextColorTB.BackColor = SystemColors.Control;
            TextColorTB.Color = Color.White;
            TextColorTB.Location = new Point(12, 37);
            TextColorTB.MinimumSize = new Size(0, 30);
            TextColorTB.Name = "TextColorTB";
            TextColorTB.Size = new Size(118, 30);
            TextColorTB.TabIndex = 4;
            // 
            // panel10
            // 
            panel10.Controls.Add(groupBox3);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(0, 150);
            panel10.Margin = new Padding(0);
            panel10.Name = "panel10";
            panel10.Size = new Size(460, 140);
            panel10.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 19);
            label1.Name = "label1";
            label1.Size = new Size(60, 15);
            label1.TabIndex = 3;
            label1.Text = "Text Color";
            // 
            // textColorLabel
            // 
            textColorLabel.AutoSize = true;
            textColorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textColorLabel.Location = new Point(82, 19);
            textColorLabel.Name = "textColorLabel";
            textColorLabel.Size = new Size(12, 15);
            textColorLabel.TabIndex = 2;
            textColorLabel.Text = "-";
            // 
            // backColorLabel
            // 
            backColorLabel.AutoSize = true;
            backColorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backColorLabel.Location = new Point(82, 81);
            backColorLabel.Name = "backColorLabel";
            backColorLabel.Size = new Size(12, 15);
            backColorLabel.TabIndex = 4;
            backColorLabel.Text = "-";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 81);
            label2.Name = "label2";
            label2.Size = new Size(64, 15);
            label2.TabIndex = 3;
            label2.Text = "Back Color";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(PrefixTB);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(NameTB);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(454, 84);
            groupBox1.TabIndex = 36;
            groupBox1.TabStop = false;
            groupBox1.Text = "Style Name";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(SelectedFontLabel);
            groupBox2.Controls.Add(SelectFontButton);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Location = new Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(454, 54);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Style Font";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(SelectBackColorButton);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(BackColorTB);
            groupBox3.Controls.Add(backColorLabel);
            groupBox3.Controls.Add(textColorLabel);
            groupBox3.Controls.Add(TextColorTB);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(SelectTextColorButton);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(0, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(460, 140);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "Style Colors";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(MonospaceNumbersCheckBox);
            groupBox4.Controls.Add(BottomMarginTB);
            groupBox4.Controls.Add(LeftMarginTB);
            groupBox4.Controls.Add(RightMarginTB);
            groupBox4.Controls.Add(label9);
            groupBox4.Controls.Add(TopMarginTB);
            groupBox4.Controls.Add(label10);
            groupBox4.Controls.Add(label11);
            groupBox4.Controls.Add(label12);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(3, 293);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(454, 134);
            groupBox4.TabIndex = 38;
            groupBox4.TabStop = false;
            groupBox4.Text = "Margin";
            // 
            // EditTextStyleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(767, 460);
            Controls.Add(tableLayoutPanel1);
            Name = "EditTextStyleForm";
            Text = "Text Style";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)BottomMarginTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)RightMarginTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)TopMarginTB).EndInit();
            ((System.ComponentModel.ISupportInitialize)LeftMarginTB).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            panel10.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button OKButton;
        private Button CancelButton;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private ListView TextStylePropertiesListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private TableLayoutPanel tableLayoutPanel3;
        private Button AddTextStylePropertyButton;
        private Button DeleteTextStylePropertyButton;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label3;
        private CustomComponents.TextBoxes.ColorTextBox BackColorTB;
        private TextBox NameTB;
        private CustomComponents.TextBoxes.ColorTextBox TextColorTB;
        private TableLayoutPanel tableLayoutPanel5;
        private Button SelectFontButton;
        private Label SelectedFontLabel;
        private Label textColorLabel;
        private Label label2;
        private FontDialog SelectFontDialog;
        private NumericUpDown BottomMarginTB;
        private NumericUpDown RightMarginTB;
        private NumericUpDown TopMarginTB;
        private NumericUpDown LeftMarginTB;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private CheckBox MonospaceNumbersCheckBox;
        private Label label5;
        private TextBox PrefixTB;
        private Button SelectTextColorButton;
        private Panel panel10;
        private Label label1;
        private Button SelectBackColorButton;
        private Label backColorLabel;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
    }
}