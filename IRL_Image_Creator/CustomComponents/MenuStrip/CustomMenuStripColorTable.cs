using IRL_Image_Creator.Properties;

namespace System.Windows.Forms
{
    internal class CustomMenuStripColorTable : ProfessionalColorTable
    {
        /// <summary>
        /// Gets the start color of the gradient used in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuStripGradientBegin
        {
            get
            {
                return Settings.Default.MenuStripGradientBegin;
            }
        }

        /// <summary>
        /// Gets the end color of the gradient used in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuStripGradientEnd
        {
            get
            {
                return  Settings.Default.MenuStripGradientEnd;
            }
        }

        /// <summary>
        /// Gets the start color of the gradient used when the top-level menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return Settings.Default.MenuItemPressedGradientBegin;
            }
        }

        /// <summary>
        /// Gets the middle color of the gradient used when the top-level menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuItemPressedGradientMiddle
        {
            get
            {
                return Settings.Default.MenuItemPressedGradientMiddle;
            }
        }

        /// <summary>
        /// Gets the end color of the gradient used when the top-level menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return Settings.Default.MenuItemPressedGradientEnd;
            }
        }

        /// <summary>
        /// Gets the start color of the gradient used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return Settings.Default.MenuItemSelectedGradientBegin;
            }
        }

        /// <summary>
        /// Gets the end color of the gradient used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return Settings.Default.MenuItemSelectedGradientEnd;
            }
        }

        /// <summary>
        /// Gets the color used when the menu item is selected in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuItemSelected
        {
            get
            {
                return Settings.Default.MenuItemSelected;
            }
        }

        /// <summary>
        /// Gets the color used for the menu border in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuBorder
        {
            get
            {
                return Settings.Default.MenuBorder;
            }
        }

        /// <summary>
        /// Gets the color used for the menu item border in the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color MenuItemBorder
        {
            get
            {
                return Settings.Default.MenuItemBorder;
            }
        }

        /// <summary>
        /// Gets the starting color of the gradient used in the image margin of the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color ImageMarginGradientBegin
        {
            get
            {
                return Settings.Default.ImageMarginGradientBegin;
            }
        }

        /// <summary>
        /// Gets the middle color of the gradient used in the image margin of the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return Settings.Default.ImageMarginGradientMiddle;
            }
        }

        /// <summary>
        /// Gets the ending color of the gradient used in the image margin of the System.Windows.Forms.MenuStrip control.
        /// </summary>
        public override Color ImageMarginGradientEnd
        {
            get
            {
                return Settings.Default.ImageMarginGradientEnd;
            }
        }
    }
}
