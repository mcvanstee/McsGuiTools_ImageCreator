namespace IRL_Image_Creator.Windows.ColorForms
{
    partial class ColorForm
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
            ColorsListView = new ListView();
            columnHeader1 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            tableLayoutPanel2 = new TableLayoutPanel();
            DeleteButton = new Button();
            EditButton = new Button();
            NewButton = new Button();
            CloseButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(ColorsListView, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(613, 415);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // ColorsListView
            // 
            ColorsListView.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader5 });
            ColorsListView.Dock = DockStyle.Fill;
            ColorsListView.FullRowSelect = true;
            ColorsListView.GridLines = true;
            ColorsListView.Location = new Point(3, 3);
            ColorsListView.MultiSelect = false;
            ColorsListView.Name = "ColorsListView";
            ColorsListView.Size = new Size(607, 379);
            ColorsListView.TabIndex = 2;
            ColorsListView.UseCompatibleStateImageBehavior = false;
            ColorsListView.View = View.Details;
            ColorsListView.SelectedIndexChanged += ColorsListView_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Name";
            columnHeader1.Width = 80;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Color";
            columnHeader5.Width = 80;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 5;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutPanel2.Controls.Add(DeleteButton, 2, 0);
            tableLayoutPanel2.Controls.Add(EditButton, 1, 0);
            tableLayoutPanel2.Controls.Add(NewButton, 0, 0);
            tableLayoutPanel2.Controls.Add(CloseButton, 4, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 385);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(613, 30);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // DeleteButton
            // 
            DeleteButton.Location = new Point(163, 3);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(74, 23);
            DeleteButton.TabIndex = 5;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Click += DeleteButton_Click;
            // 
            // EditButton
            // 
            EditButton.Location = new Point(83, 3);
            EditButton.Name = "EditButton";
            EditButton.Size = new Size(74, 23);
            EditButton.TabIndex = 4;
            EditButton.Text = "Edit";
            EditButton.UseVisualStyleBackColor = true;
            EditButton.Click += EditButton_Click;
            // 
            // NewButton
            // 
            NewButton.Location = new Point(3, 3);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(74, 23);
            NewButton.TabIndex = 3;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Click += NewButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.Location = new Point(536, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(74, 23);
            CloseButton.TabIndex = 6;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // ColorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(613, 415);
            Controls.Add(tableLayoutPanel1);
            Name = "ColorForm";
            Text = "Colors";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button NewButton;
        private Button EditButton;
        private Button DeleteButton;
        private Button CloseButton;
        private ListView ColorsListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader5;
    }
}