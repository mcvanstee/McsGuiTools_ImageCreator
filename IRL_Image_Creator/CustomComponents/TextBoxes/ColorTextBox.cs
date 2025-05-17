using System.ComponentModel;

namespace IRL_Image_Creator.CustomComponents.TextBoxes
{
    [DefaultEvent(nameof(TextChanged))]
    public partial class ColorTextBox : UserControl
    {
        private Color m_color = Color.White;
        private bool m_textBoxError;
        public event EventHandler? ColorChanged;

        public ColorTextBox()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            BackColor = SystemColors.Control;
        }

        //[Browsable(true)]
        //public new event EventHandler? TextChanged
        //{
        //    add => textBox.TextChanged += value;
        //    remove => textBox.TextChanged -= value;
        //}

        //[Browsable(true)]
        //public new string Text
        //{
        //    get => textBox.Text;
        //    set => textBox.Text = value;
        //}

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color Color
        {
            get => m_color;
            set
            {
                if (m_color != value)
                {
                    m_color = value;

                    string colorStr = m_color.R.ToString("X2") + m_color.G.ToString("X2") + m_color.B.ToString("X2");
                    textBox.Text = colorStr;
                }
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox.Text.Length < 6)
            {
                m_textBoxError = true;
                Invalidate();
                return;
            }

            string rStr = textBox.Text.Substring(0, 2);
            string gStr = textBox.Text.Substring(2, 2);
            string bStr = textBox.Text.Substring(4, 2);

            try
            {
                int r = Convert.ToInt32(rStr, 16);
                int g = Convert.ToInt32(gStr, 16);
                int b = Convert.ToInt32(bStr, 16);

                m_color = Color.FromArgb(r, g, b);
                m_textBoxError = false;
                textBox.Text = textBox.Text.ToUpper();

                OnColorChanged();
            }
            catch
            {
                m_textBoxError = true;
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            textBox.BackColor = BackColor;
            textBox.ForeColor = ForeColor;

            base.OnPaint(pe);

            int x = textBox.Location.X - 30;
            int y = textBox.Location.Y + ((textBox.Height - 20) / 2);
            pe.Graphics.FillRectangle(new SolidBrush(m_color), x, y, 20, 20);

            if (m_textBoxError)
            {
                using Pen pen = new Pen(Color.FromArgb(255, 0, 0), 4);
                pe.Graphics.DrawRectangle(pen, textBox.Location.X, textBox.Location.Y, textBox.Width, textBox.Height);
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (!(c == '\b' || ('0' <= c && c <= '9') || ('A' <= c && c <= 'F') || ('a' <= c && c <= 'f')))
            {
                e.Handled = true;
            }
        }

        private void OnColorChanged()
        {
            ColorChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
