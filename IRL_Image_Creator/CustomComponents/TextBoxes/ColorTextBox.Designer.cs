namespace IRL_Image_Creator.CustomComponents.TextBoxes
{
    partial class ColorTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox = new TextBox();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            textBox.Font = new Font("Roboto", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox.Location = new Point(31, 3);
            textBox.MaxLength = 6;
            textBox.Name = "textBox";
            textBox.Size = new Size(84, 23);
            textBox.TabIndex = 0;
            textBox.Text = "FFFFFF";
            textBox.TextChanged += textBox_TextChanged;
            textBox.KeyPress += textBox_KeyPress;
            // 
            // ColorTextBox
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(textBox);
            MinimumSize = new Size(0, 30);
            Name = "ColorTextBox";
            Size = new Size(118, 30);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox;
    }
}
