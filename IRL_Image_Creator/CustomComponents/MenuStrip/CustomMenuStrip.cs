using System.ComponentModel;
using IRL_Image_Creator.Properties;

namespace System.Windows.Forms
{
    public partial class CustomMenuStrip : MenuStrip
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of System.Windows.Forms.CustomMenuStrip.
        /// </summary>
        public CustomMenuStrip()
        {
            InitializeComponent();

            this.RenderMode = ToolStripRenderMode.Professional;
            this.Renderer = new ToolStripProfessionalRenderer(new CustomMenuStripColorTable());
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ForeColor of the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuStripForeColor")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuStripForeColor
        {
            get { return Settings.Default.MenuStripForeColor; }
            set
            {
                Settings.Default.MenuStripForeColor = value;
                this.ForeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the start color of the gradient used in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuStripGradientBegin")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuStripGradientBegin
        {
            get { return Settings.Default.MenuStripGradientBegin; }
            set { Settings.Default.MenuStripGradientBegin = value; }
        }

        /// <summary>
        /// Gets or sets the end color of the gradient used in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuStripGradientEnd")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuStripGradientEnd
        {
            get { return Settings.Default.MenuStripGradientEnd; }
            set { Settings.Default.MenuStripGradientEnd = value; }
        }

        /// <summary>
        /// Gets or sets the start color of the gradient used when the top-level menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuItemPressedGradientBegin")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuItemPressedGradientBegin
        {
            get { return Settings.Default.MenuStripGradientEnd; }
            set { Settings.Default.MenuStripGradientEnd = value; }
        }

        /// <summary>
        /// Gets or sets the middle color of the gradient used when the top-level menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuItemPressedGradientMiddle")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuItemPressedGradientMiddle
        {
            get { return Settings.Default.MenuStripGradientEnd; }
            set { Settings.Default.MenuStripGradientEnd = value; }
        }

        /// <summary>
        /// Gets or sets the end color of the gradient used when the top-level menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuItemPressedGradientEnd")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuItemPressedGradientEnd
        {
            get { return Settings.Default.MenuStripGradientEnd; }
            set { Settings.Default.MenuStripGradientEnd = value; }
        }

        /// <summary>
        /// Gets or sets the end color of the gradient used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuItemSelectedGradientBegin")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuItemSelectedGradientBegin
        {
            get { return Settings.Default.MenuItemSelectedGradientBegin; }
            set { Settings.Default.MenuItemSelectedGradientBegin = value; }
        }

        /// <summary>
        /// Gets or sets the end color of the gradient used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuItemSelectedGradientEnd")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuItemSelectedGradientEnd
        {
            get { return Settings.Default.MenuItemSelectedGradientEnd; }
            set { Settings.Default.MenuItemSelectedGradientEnd = value; }
        }

        /// <summary>
        /// Gets or sets the color used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuItemSelected")]
        [Description("The color used when the menu item is selected in the System.Windows.Forms.MenuStrip control.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuItemSelected
        {
            get { return Settings.Default.MenuItemSelected; }
            set { Settings.Default.MenuItemSelected = value; }
        }

        /// <summary>
        /// Gets or sets the color used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuBorder")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuBorder
        {
            get { return Settings.Default.MenuBorder; }
            set { Settings.Default.MenuBorder = value; }
        }

        /// <summary>
        /// Gets or sets the color used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("MenuItemBorder")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color MenuItemBorder
        {
            get { return Settings.Default.MenuItemBorder; }
            set { Settings.Default.MenuItemBorder = value; }
        }

        /// <summary>
        /// Gets or sets the starting color of the gradient used in the image margin of the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("ImageMarginGradientBegin")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ImageMarginGradientBegin
        {
            get { return Settings.Default.ImageMarginGradientBegin; }
            set { Settings.Default.ImageMarginGradientBegin = value; }
        }

        /// <summary>
        /// Gets or sets the middle color of the gradient used in the image margin of the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("ImageMarginGradientMiddle")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ImageMarginGradientMiddle
        {
            get { return Settings.Default.ImageMarginGradientMiddle; }
            set { Settings.Default.ImageMarginGradientMiddle = value; }
        }

        /// <summary>
        /// Gets or sets the ending color of the gradient used in the image margin of the System.Windows.Forms.MenuStrip control.
        /// </summary>
        [Category("Style")]
        [DisplayName("ImageMarginGradientEnd")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color ImageMarginGradientEnd
        {
            get { return Settings.Default.ImageMarginGradientEnd; }
            set { Settings.Default.ImageMarginGradientEnd = value; }
        }
        #endregion
    }
}
