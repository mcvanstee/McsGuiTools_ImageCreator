namespace IRL_Image_Creator.Windows.FilePropertyForms
{
    partial class FilePropertyForm
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
            panel1 = new Panel();
            panel3 = new Panel();
            EditButton = new Button();
            CloseButton = new Button();
            panel2 = new Panel();
            panelWithBorder2 = new CustomComponents.Panels.PanelWithBorder();
            PropertiesListView = new ListView();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panelWithBorder2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(674, 431);
            panel1.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Controls.Add(EditButton);
            panel3.Controls.Add(CloseButton);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 403);
            panel3.Name = "panel3";
            panel3.Size = new Size(674, 28);
            panel3.TabIndex = 1;
            // 
            // EditButton
            // 
            EditButton.Location = new Point(4, 2);
            EditButton.Name = "EditButton";
            EditButton.Size = new Size(75, 22);
            EditButton.TabIndex = 1;
            EditButton.Text = "Edit";
            EditButton.UseVisualStyleBackColor = true;
            EditButton.Click += EditButton_Click;
            // 
            // CloseButton
            // 
            CloseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CloseButton.Location = new Point(595, 2);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(75, 22);
            CloseButton.TabIndex = 0;
            CloseButton.Text = "Close";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(panelWithBorder2);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(674, 431);
            panel2.TabIndex = 0;
            // 
            // panelWithBorder2
            // 
            panelWithBorder2.BorderColor = Color.Gray;
            panelWithBorder2.Controls.Add(PropertiesListView);
            panelWithBorder2.Dock = DockStyle.Fill;
            panelWithBorder2.Location = new Point(0, 0);
            panelWithBorder2.Name = "panelWithBorder2";
            panelWithBorder2.Padding = new Padding(1);
            panelWithBorder2.Size = new Size(674, 431);
            panelWithBorder2.TabIndex = 3;
            // 
            // PropertiesListView
            // 
            PropertiesListView.BorderStyle = BorderStyle.None;
            PropertiesListView.Columns.AddRange(new ColumnHeader[] { columnHeader3, columnHeader4, columnHeader5 });
            PropertiesListView.Dock = DockStyle.Fill;
            PropertiesListView.FullRowSelect = true;
            PropertiesListView.GridLines = true;
            PropertiesListView.Location = new Point(1, 1);
            PropertiesListView.MultiSelect = false;
            PropertiesListView.Name = "PropertiesListView";
            PropertiesListView.Size = new Size(672, 429);
            PropertiesListView.TabIndex = 1;
            PropertiesListView.UseCompatibleStateImageBehavior = false;
            PropertiesListView.View = View.Details;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Property";
            columnHeader3.Width = 75;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Name";
            columnHeader4.Width = 150;
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Values";
            columnHeader5.Width = 500;
            // 
            // FilePropertyForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(674, 431);
            Controls.Add(panel1);
            Name = "FilePropertyForm";
            Text = "File Properties";
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panelWithBorder2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Button CloseButton;
        private CustomComponents.Panels.PanelWithBorder panelWithBorder2;
        private ListView PropertiesListView;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private Button EditButton;
    }
}