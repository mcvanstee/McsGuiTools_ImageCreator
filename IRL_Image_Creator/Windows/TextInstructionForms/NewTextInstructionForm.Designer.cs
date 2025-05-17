namespace IRL_Image_Creator.Windows.TextInstructionForms
{
    partial class NewTextInstructionForm
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
            panel1 = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            NameTextBox = new TextBox();
            label1 = new Label();
            TranslateTextCB = new CheckBox();
            TrPropertyTLPanel = new TableLayoutPanel();
            TranslationPropertyBtn = new Button();
            PropertyLabel = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            TrPropertyTLPanel.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(436, 302);
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
            tableLayoutPanel2.Location = new Point(0, 257);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(436, 45);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            CancelButton.Location = new Point(9, 9);
            CancelButton.Margin = new Padding(9);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 27);
            CancelButton.TabIndex = 4;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            OKButton.Location = new Point(352, 9);
            OKButton.Margin = new Padding(9);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 27);
            OKButton.TabIndex = 3;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel3);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(436, 257);
            panel1.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel4, 0, 0);
            tableLayoutPanel3.Controls.Add(TranslateTextCB, 0, 1);
            tableLayoutPanel3.Controls.Add(TrPropertyTLPanel, 0, 2);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 5;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Size = new Size(436, 257);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 388F));
            tableLayoutPanel4.Controls.Add(NameTextBox, 1, 0);
            tableLayoutPanel4.Controls.Add(label1, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(436, 30);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // NameTextBox
            // 
            NameTextBox.Location = new Point(51, 3);
            NameTextBox.Name = "NameTextBox";
            NameTextBox.Size = new Size(141, 23);
            NameTextBox.TabIndex = 0;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(6, 7);
            label1.Margin = new Padding(3, 7, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // TranslateTextCB
            // 
            TranslateTextCB.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            TranslateTextCB.AutoSize = true;
            TranslateTextCB.Location = new Point(10, 33);
            TranslateTextCB.Margin = new Padding(10, 3, 3, 3);
            TranslateTextCB.Name = "TranslateTextCB";
            TranslateTextCB.Size = new Size(145, 24);
            TranslateTextCB.TabIndex = 1;
            TranslateTextCB.Text = "Mulitple Text Columns";
            TranslateTextCB.UseVisualStyleBackColor = true;
            TranslateTextCB.CheckedChanged += TranslateTextCB_CheckedChanged;
            // 
            // TrPropertyTLPanel
            // 
            TrPropertyTLPanel.ColumnCount = 2;
            TrPropertyTLPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.1100922F));
            TrPropertyTLPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.88991F));
            TrPropertyTLPanel.Controls.Add(TranslationPropertyBtn, 0, 0);
            TrPropertyTLPanel.Controls.Add(PropertyLabel, 1, 0);
            TrPropertyTLPanel.Dock = DockStyle.Fill;
            TrPropertyTLPanel.Location = new Point(0, 60);
            TrPropertyTLPanel.Margin = new Padding(0);
            TrPropertyTLPanel.Name = "TrPropertyTLPanel";
            TrPropertyTLPanel.RowCount = 1;
            TrPropertyTLPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            TrPropertyTLPanel.Size = new Size(436, 30);
            TrPropertyTLPanel.TabIndex = 2;
            // 
            // TranslationPropertyBtn
            // 
            TranslationPropertyBtn.Location = new Point(8, 3);
            TranslationPropertyBtn.Margin = new Padding(8, 3, 3, 3);
            TranslationPropertyBtn.Name = "TranslationPropertyBtn";
            TranslationPropertyBtn.Size = new Size(125, 23);
            TranslationPropertyBtn.TabIndex = 2;
            TranslationPropertyBtn.Text = "Column Property";
            TranslationPropertyBtn.UseVisualStyleBackColor = true;
            TranslationPropertyBtn.Click += TranslationPropertyBtn_Click;
            // 
            // PropertyLabel
            // 
            PropertyLabel.AutoSize = true;
            PropertyLabel.Location = new Point(143, 7);
            PropertyLabel.Margin = new Padding(3, 7, 3, 0);
            PropertyLabel.Name = "PropertyLabel";
            PropertyLabel.Size = new Size(12, 15);
            PropertyLabel.TabIndex = 1;
            PropertyLabel.Text = "-";
            // 
            // NewTextInstructionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(436, 302);
            Controls.Add(tableLayoutPanel1);
            Name = "NewTextInstructionForm";
            Text = "New Text instruction";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            TrPropertyTLPanel.ResumeLayout(false);
            TrPropertyTLPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button CancelButton;
        private Button OKButton;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label1;
        private TextBox NameTextBox;
        private CheckBox TranslateTextCB;
        private TableLayoutPanel TrPropertyTLPanel;
        private Button TranslationPropertyBtn;
        private Label PropertyLabel;
    }
}