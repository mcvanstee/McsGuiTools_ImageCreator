namespace IRL_Image_Creator.Windows.DataLocationForms
{
    partial class DataLocationForm
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
            helpProvider1 = new HelpProvider();
            tableLayoutPanel1 = new TableLayoutPanel();
            dataLocationListView = new ListView();
            nameColumnHeader = new ColumnHeader();
            locationIDColumnHeader = new ColumnHeader();
            locTypeColumnHeader = new ColumnHeader();
            compTypeColumnHeader = new ColumnHeader();
            panel1 = new Panel();
            closeButton = new Button();
            deleteButton = new Button();
            editButton = new Button();
            newButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(dataLocationListView, 0, 0);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.Size = new Size(636, 382);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // dataLocationListView
            // 
            dataLocationListView.Columns.AddRange(new ColumnHeader[] { locationIDColumnHeader, nameColumnHeader, locTypeColumnHeader, compTypeColumnHeader });
            dataLocationListView.Dock = DockStyle.Fill;
            dataLocationListView.FullRowSelect = true;
            dataLocationListView.GridLines = true;
            dataLocationListView.Location = new Point(3, 3);
            dataLocationListView.MultiSelect = false;
            dataLocationListView.Name = "dataLocationListView";
            dataLocationListView.Size = new Size(630, 346);
            dataLocationListView.TabIndex = 0;
            dataLocationListView.UseCompatibleStateImageBehavior = false;
            dataLocationListView.View = View.Details;
            dataLocationListView.SelectedIndexChanged += DataLocationListView_SelectedIndexChanged;
            // 
            // nameColumnHeader
            // 
            nameColumnHeader.Text = "Name";
            // 
            // locationIDColumnHeader
            // 
            locationIDColumnHeader.Text = "Location ID";
            // 
            // locTypeColumnHeader
            // 
            locTypeColumnHeader.Text = "Location Type";
            // 
            // compTypeColumnHeader
            // 
            compTypeColumnHeader.Text = "Compression Type";
            // 
            // panel1
            // 
            panel1.Controls.Add(closeButton);
            panel1.Controls.Add(deleteButton);
            panel1.Controls.Add(editButton);
            panel1.Controls.Add(newButton);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 352);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(636, 30);
            panel1.TabIndex = 0;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.Location = new Point(558, 4);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(75, 23);
            closeButton.TabIndex = 3;
            closeButton.Text = "Close";
            closeButton.UseVisualStyleBackColor = true;
            closeButton.Click += CloseButton_Click;
            // 
            // deleteButton
            // 
            deleteButton.Location = new Point(165, 3);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(75, 23);
            deleteButton.TabIndex = 2;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += DeleteButton_Click;
            // 
            // editButton
            // 
            editButton.Location = new Point(84, 3);
            editButton.Name = "editButton";
            editButton.Size = new Size(75, 23);
            editButton.TabIndex = 1;
            editButton.Text = "Edit";
            editButton.UseVisualStyleBackColor = true;
            editButton.Click += EditButton_Click;
            // 
            // newButton
            // 
            newButton.Location = new Point(3, 3);
            newButton.Name = "newButton";
            newButton.Size = new Size(75, 23);
            newButton.TabIndex = 0;
            newButton.Text = "New";
            newButton.UseVisualStyleBackColor = true;
            newButton.Click += NewButton_Click;
            // 
            // DataLocationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(636, 382);
            Controls.Add(tableLayoutPanel1);
            Name = "DataLocationForm";
            Text = "Data Location";
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private HelpProvider helpProvider1;
        private TableLayoutPanel tableLayoutPanel1;
        private ListView dataLocationListView;
        private Panel panel1;
        private Button newButton;
        private Button closeButton;
        private Button deleteButton;
        private Button editButton;
        private ColumnHeader nameColumnHeader;
        private ColumnHeader locationIDColumnHeader;
        private ColumnHeader locTypeColumnHeader;
        private ColumnHeader compTypeColumnHeader;
    }
}