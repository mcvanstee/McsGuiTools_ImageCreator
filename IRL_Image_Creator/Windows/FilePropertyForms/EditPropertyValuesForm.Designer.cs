namespace Gui_Image_Builder
{
    partial class EditPropertyValuesForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.PropertyNameA_TB = new System.Windows.Forms.TextBox();
            this.listBox = new System.Windows.Forms.ListBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.errorLabel1 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(325, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "No of Properties";
            // 
            // PropertyNameA_TB
            // 
            this.PropertyNameA_TB.Location = new System.Drawing.Point(325, 24);
            this.PropertyNameA_TB.Name = "PropertyNameA_TB";
            this.PropertyNameA_TB.Size = new System.Drawing.Size(143, 23);
            this.PropertyNameA_TB.TabIndex = 2;
            this.PropertyNameA_TB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.PropertyNameA_TB_MouseClick);
            this.PropertyNameA_TB.TextChanged += new System.EventHandler(this.PropertyNameA_TB_TextChanged);
            this.PropertyNameA_TB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PropertyNameA_TB_KeyPress);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.ItemHeight = 15;
            this.listBox.Location = new System.Drawing.Point(2, 24);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(299, 334);
            this.listBox.TabIndex = 3;
            this.listBox.SelectedIndexChanged += new System.EventHandler(this.listBox_SelectedIndexChanged);
            this.listBox.DoubleClick += new System.EventHandler(this.listBox_DoubleClick);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(2, 360);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(299, 23);
            this.textBox.TabIndex = 4;
            this.textBox.Visible = false;
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name";
            // 
            // errorLabel1
            // 
            this.errorLabel1.AutoSize = true;
            this.errorLabel1.ForeColor = System.Drawing.Color.Red;
            this.errorLabel1.Location = new System.Drawing.Point(324, 48);
            this.errorLabel1.Name = "errorLabel1";
            this.errorLabel1.Size = new System.Drawing.Size(204, 15);
            this.errorLabel1.TabIndex = 7;
            this.errorLabel1.Text = "Please enter a value between [2 - 254]";
            this.errorLabel1.Visible = false;
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(446, 361);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // EditPropertyValuesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 396);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.errorLabel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.PropertyNameA_TB);
            this.Controls.Add(this.label2);
            this.Name = "EditPropertyValuesForm";
            this.Text = "Edit Property Values";
            this.Load += new System.EventHandler(this.EditPropertyValuesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PropertyNameA_TB;
        private System.Windows.Forms.ListView PropertyValuesListview;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label errorLabel1;
        private System.Windows.Forms.Button closeButton;
    }
}