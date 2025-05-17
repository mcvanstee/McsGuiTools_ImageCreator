using System.Drawing;

namespace IRL_Common_Library.Utils
{
    public static class CommonUtils
    {
        public static string ColorToHexString(Color color)
        {
            return "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
    }
}
