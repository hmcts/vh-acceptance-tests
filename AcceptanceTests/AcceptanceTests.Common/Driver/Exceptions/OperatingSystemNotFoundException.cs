using System;
using AcceptanceTests.Common.Driver.Enums;

namespace AcceptanceTests.Common.Driver.Exceptions
{
    [Serializable]
    public class OperatingSystemNotFoundException : Exception
    {
        public OperatingSystemNotFoundException()
        {

        }

        public OperatingSystemNotFoundException(string message) : 
            base($"Invalid option {message}. Options are: '{TargetOS.Windows}, {TargetOS.MacOs}, {TargetOS.Android}, {TargetOS.iOS} or {TargetOS.Samsung}")
        {

        }
    }
}
