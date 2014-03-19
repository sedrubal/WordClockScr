using System.Drawing;
using System;

namespace WordClockScr
{
    /// <summary>
    /// A static class with tools-functions
    /// </summary>
    public static class Tools
    {
        /// <summary>
        /// Returns a color, which ARGB-values are between the color1 and color2. You can manipulate the color with "percent": 0.0 -> returns color1; 1.0 -> returns color2;
        /// </summary>
        /// <param name="color1">the first color</param>
        /// <param name="color2">the second color</param>
        /// <param name="percent">the precentage wighting to color1 or color2</param>
        /// <returns>a color between color1 and color2 with the weighting of the "percent"-value</returns>
        public static Color GetWeightedColor(Color color1, Color color2, double percent)
        {
            return Color.FromArgb((int)(color1.A + (color2.A - color1.A) * percent), (int)(color1.R + (color2.R - color1.R) * percent), (int)(color1.G + (color2.G - color1.G) * percent), (int)(color1.B + (color2.B - color1.B) * percent));
        }
    }
}

    
namespace WordClockScr.tools.Animations
{
    /// <summary>
    /// Enum to set the current Fading-Effect (Fading in, out or do nothing)
    /// </summary>
    public enum FadeMode
    {
        nothing,
        fadeIn,
        fadeOut
    }
}