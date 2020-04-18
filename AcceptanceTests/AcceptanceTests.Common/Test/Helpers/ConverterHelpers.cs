using System.Collections.Generic;

namespace AcceptanceTests.Common.Test.Helpers
{
    public static class ConverterHelpers
    {
        private static IEnumerable<string> ConvertStringIntoArray(string options)
        {
            return options.Split(",");
        }
    }
}
