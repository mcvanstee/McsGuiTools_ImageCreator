namespace IRL_Image_Creator.Windows
{
    partial class TestForm
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
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            textListView = new ListView();
            panel1.SuspendLayout();
            panel4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1032, 755);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(200, 755);
            panel2.TabIndex = 0;
            // 
            // panel3
            // 
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(832, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(200, 755);
            panel3.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Controls.Add(textListView);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(200, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(632, 755);
            panel4.TabIndex = 2;
            // 
            // textListView
            // 
            textListView.FullRowSelect = true;
            textListView.GridLines = true;
            textListView.Location = new Point(19, 226);
            textListView.Name = "textListView";
            textListView.Size = new Size(585, 455);
            textListView.TabIndex = 0;
            textListView.UseCompatibleStateImageBehavior = false;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1032, 755);
            Controls.Add(panel1);
            Name = "TestForm";
            Text = "TestForm";
            panel1.ResumeLayout(false);
            panel4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private ListView textListView;
    }
}