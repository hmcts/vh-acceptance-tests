using System.Drawing;

namespace AcceptanceTests.Common.Data.Helpers
{
    public static class CustomConverters
    {
        public static string ConvertRgbToHex(string rgbCssValue)
        {
            var numbers = rgbCssValue.Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Split(",");
            var r = int.Parse(numbers[0].Trim());
            var g = int.Parse(numbers[1].Trim());
            var b = int.Parse(numbers[2].Trim());
            var rgbColour = Color.FromArgb(r, g, b);
            var hex = "#" + rgbColour.R.ToString("X2") + rgbColour.G.ToString("X2") + rgbColour.B.ToString("X2");
            return hex.ToLower();
        }
    }
}
