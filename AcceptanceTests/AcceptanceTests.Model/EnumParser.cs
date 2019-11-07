using System;
using System.Globalization;

namespace AcceptanceTests.Model
{
    public static class EnumParser
    {
        public static T ParseText<T>(string text)
        {
            var formattedText = FormatText(text);
            T parsedText;
            try
            {
                parsedText = (T)Enum.Parse(typeof(T), formattedText, true);
            }
            catch (Exception)
            {
                var message = $"{text} is not supported.";
                Console.WriteLine(message);
                throw new NotSupportedException(message);
            }
            return parsedText;
        }

        private static string FormatText(string text)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(text.ToLowerInvariant()).Replace(" ", "");

        }
    }
}
