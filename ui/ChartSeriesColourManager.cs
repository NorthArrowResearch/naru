using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace naru.ui
{
    public class ChartSeriesColourManager
    {
        // Each unique color key is stored in this dictionary along with the hex colour string
        private Dictionary<string, string> ColorsInUse = new Dictionary<string, string>();
        // The colour palette being used by the manager

        private ChartColorPalette Palette;
        // Random number generater for issuing new colours

        private Random rnd;
        // The min and max integer values for issuing new, random colours. 100-200 should produce bright colours
        private const int CMIN = 100;

        private const int CMAX = 200;

        // Dictionary of colors contained in the official Microsoft charting color palettes
        // https://stackoverflow.com/questions/14204827/ms-chart-for-net-predefined-palettes-color-list
        private Dictionary<ChartColorPalette, List<string>> PaletteColors = new Dictionary<ChartColorPalette, List<string>> {

            {ChartColorPalette.Bright, new List<string> {"#008000", "#0000FF", "#800080", "#00FF00", "#FF00FF", "#008080", "#FFFF00", "#808080", "#00FFFF", "#000080", "#800000", "#FF0000", "#808000", "#C0C0C0", "#FF6347", "#FFE4B5"} },
            {ChartColorPalette.Grayscale, new List<string> {"#C8C8C8", "#BDBDBD", "#B2B2B2", "#A7A7A7", "#9C9C9C", "#919191", "#868686", "#7B7B7B", "#707070", "#656565", "#5A5A5A", "#4F4F4F", "#444444", "#393939", "#2E2E2E", "#232323"} },
            {ChartColorPalette.Excel, new List<string> {"#9999FF", "#993366", "#FFFFCC", "#CCFFFF", "#660066", "#FF8080", "#0066CC", "#CCCCFF", "#000080", "#FF00FF", "#FFFF00", "#00FFFF", "#800080", "#800000", "#008080", "#0000FF"}},
            {ChartColorPalette.Light, new List<string> {"#E6E6FA", "#FFF0F5", "#FFDAB9", "#FFFACD", "#FFE4E1", "#F0FFF0", "#F0F8FF", "#F5F5F5", "#FAEBD7", "#E0FFFF"}},
            {ChartColorPalette.Pastel, new List<string> {"#87CEEB", "#32CD32", "#BA55D3", "#F08080", "#4682B4", "#9ACD32", "#40E0D0", "#FF69B4", "#F0E68C", "#D2B48C", "#8FBC8B", "#6495ED", "#DDA0DD", "#5F9EA0", "#FFDAB9", "#FFA07A"}},
            {ChartColorPalette.EarthTones, new List<string> {"#FF8000", "#B8860B", "#C04000", "#6B8E23", "#CD853F", "#C0C000", "#228B22", "#D2691E", "#808000", "#20B2AA", "#F4A460", "#00C000", "#8FBC8B", "#B22222", "#8B4513", "#C00000"}},
            {ChartColorPalette.SemiTransparent, new List<string> {"#FF0000", "#00FF00", "#0000FF", "#FFFF00", "#00FFFF", "#FF00FF", "#AA7814", "#FF0000", "#00FF00", "#0000FF", "#FFFF00", "#00FFFF", "#FF00FF", "#AA7814", "#647832", "#285A96"}},
            {ChartColorPalette.Berry, new List<string> {"#8A2BE2", "#BA55D3", "#4169E1", "#C71585", "#0000FF", "#8A2BE2", "#DA70D6", "#7B68EE", "#C000C0", "#0000CD", "#800080"}},
            {ChartColorPalette.Chocolate, new List<string> {"#A0522D", "#D2691E", "#8B0000", "#CD853F", "#A52A2A", "#F4A460", "#8B4513", "#C04000", "#B22222", "#B65C3A"}},
            {ChartColorPalette.Fire, new List<string> {"#FFD700", "#FF0000", "#FF1493", "#DC143C", "#FF8C00", "#FF00FF", "#FFFF00", "#FF4500", "#C71585", "#DDE221"}},
            {ChartColorPalette.SeaGreen, new List<string> {"#2E8B57", "#66CDAA", "#4682B4", "#008B8B", "#5F9EA0", "#3CB371", "#48D1CC", "#B0C4DE", "#8FBC8B", "#87CEEB"}},
            {ChartColorPalette.BrightPastel, new List<string> {"#418CF0", "#FCB441", "#E0400A", "#056492", "#BFBFBF", "#1A3B69", "#FFE382", "#129CDD", "#CA6B4B", "#005CDB", "#F3D288", "#506381", "#F1B9A8", "#E0830A", "#7893BE"}}
        };

        /// <summary>
        /// Create a new charting series colour manager
        /// </summary>
        /// <param name="colorPalette">Optional colour palette. Defaults to bright pastel (the default charting palette)</param>
        public ChartSeriesColourManager(ChartColorPalette colorPalette = ChartColorPalette.BrightPastel)
        {
            ColorsInUse = new Dictionary<string, string>();
            Palette = colorPalette;
            rnd = new Random(1);
        }

        /// <summary>
        /// Gets a colour for a unique color key. Passing in the same colour key will always return the same colour for the lifetime of the series manager
        /// </summary>
        /// <param name="colorKey">Unique string that is associated with a colour</param>
        /// <returns></returns>
        /// <remarks>If more colours are needed than the palette has available then "bright" random colours are issued</remarks>
        public Color GetColor(string colorKey)
        {
            if (ColorsInUse.ContainsKey(colorKey))
            {
                return ColorTranslator.FromHtml(ColorsInUse[colorKey]);
            }
            else
            {
                foreach (string sColor in PaletteColors[Palette])
                {
                    if (!ColorsInUse.Values.Contains(sColor))
                    {
                        ColorsInUse[colorKey] = sColor;
                        return ColorTranslator.FromHtml(sColor);
                    }
                }

                // All colours in the palette are in use. Use random "bright" color
                string sRandomColor = string.Empty;
                do
                {
                    sRandomColor = string.Format("#{0}{1}{2}", rnd.Next(CMIN, CMAX).ToString("X"), rnd.Next(CMIN, CMAX).ToString("X"), rnd.Next(CMIN, CMAX).ToString("X"));
                } while (ColorsInUse.Values.Contains(sRandomColor));
                ColorsInUse[colorKey] = sRandomColor;
                return ColorTranslator.FromHtml(sRandomColor);
            }
        }
    }
}