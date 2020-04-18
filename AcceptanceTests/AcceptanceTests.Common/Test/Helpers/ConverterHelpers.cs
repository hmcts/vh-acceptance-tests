using System.Collections.Generic;

namespace AcceptanceTests.Common.Test.Helpers
{
    public static class ConverterHelpers
    {
        public static IEnumerable<string> ConvertStringIntoArray(string options)
        {
            return options.Split(",");
        }
    }
}
