using System.Runtime.InteropServices;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Exceptions;

namespace AcceptanceTests.Common.Driver.Settings
{
    public static class DetectOS
    {
        public static TargetOS GetCurrentOS()
        {
            if (OperatingSystem.IsWindows())
            {
                return TargetOS.Windows;
            }

            if (OperatingSystem.IsMacOS())
            {
                return TargetOS.MacOs;
            }

            if (OperatingSystem.IsLinux())
            {
                return TargetOS.Windows;
            }

            throw new OperatingSystemNotFoundException("Unable to determine current Operating System");
        }

        private static class OperatingSystem
        {
            public static bool IsWindows() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            public static bool IsMacOS() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            public static bool IsLinux() =>
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
    }
}
