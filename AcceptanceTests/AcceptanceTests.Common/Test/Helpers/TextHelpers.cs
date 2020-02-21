using System;

namespace AcceptanceTests.Common.Test.Helpers
{
    public static class TextHelpers
    {
        public static string RemoveSpacesOnSafari(string text)
        {
            text = RemoveWhitespace(text);
            text = RemoveTabs(text);
            text = RemoveNewLines(text);
            text = RemoveDoubleWhiteSpaces(text);
            return text;
        }

        private static string RemoveWhitespace(string text)
        {
            return text.Trim();
        }

        private static string RemoveTabs(string text)
        {
            return text.Replace("\t", " ");
        }

        private static string RemoveNewLines(string text)
        {
            return text.Replace("\n", " ");
        }

        private static string RemoveDoubleWhiteSpaces(string text)
        {
            while (text.IndexOf("  ", StringComparison.Ordinal) >= 0)
            {
                text = text.Replace("  ", " ");
            }
            return text;
        }
    }
}
