using System.IO;
using System.Linq;
using AcceptanceTests.Common.Driver.Enums;
using Castle.Core.Internal;

namespace AcceptanceTests.Common.Driver.Settings
{
    public static class DefaultDevices
    {
        public static string GetAppiumVersion(TargetDevice targetDevice, TargetOS targetOS, bool runningOnSauceLabs, bool realDevice)
        {
            return runningOnSauceLabs ? GetSauceLabsDevice(targetDevice, targetOS, realDevice).AppiumVersion : GetLocalDevice(targetDevice, targetOS, realDevice).AppiumVersion;
        }

        public static string GetPlatformVersion(TargetDevice targetDevice, TargetOS targetOS, bool runningOnSauceLabs, bool realDevice)
        {
            return runningOnSauceLabs ? GetSauceLabsDevice(targetDevice, targetOS, realDevice).PlatformVersion : GetLocalDevice(targetDevice, targetOS, realDevice).PlatformVersion;
        }

        public static string GetTargetDeviceName(TargetDevice targetDevice, TargetOS targetOS, bool runningOnSauceLabs, bool realDevice)
        {
            return runningOnSauceLabs ? GetSauceLabsDevice(targetDevice, targetOS, realDevice).DeviceName : GetLocalDevice(targetDevice, targetOS, realDevice).DeviceName;
        }

        public static string GetUUID()
        {
            return LocalDevices.IOSRealTablets().First().UUID;
        }

        public static bool IsMobileOrTablet(TargetDevice targetDevice)
        {
            return targetDevice == TargetDevice.Mobile || targetDevice == TargetDevice.Tablet;
        }

        public static bool DeviceNameHasNotBeenSet(string deviceName)
        {
            return deviceName.IsNullOrEmpty();
        }

        private static Device GetLocalDevice(TargetDevice targetDevice, TargetOS targetOS, bool realDevice)
        {
            return targetDevice switch
            {
                TargetDevice.Mobile when targetOS == TargetOS.Android => LocalDevices.AndroidMobiles().First(),
                TargetDevice.Mobile when targetOS == TargetOS.iOS => LocalDevices.IOSMobiles().First(),
                TargetDevice.Tablet when targetOS == TargetOS.Android => LocalDevices.AndroidTablets().First(),
                TargetDevice.Tablet when targetOS == TargetOS.iOS => realDevice ? LocalDevices.IOSRealTablets().First() : LocalDevices.IOSSimulatorTablets().First(),
                _ => throw new InvalidDataException(
                    $"No device exists with OS '{targetOS}' and Device '{targetDevice}'")
            };
        }

        private static Device GetSauceLabsDevice(TargetDevice targetDevice, TargetOS targetOS, bool realDevice)
        {
            return targetDevice switch
            {
                TargetDevice.Mobile when targetOS == TargetOS.Android => SauceLabsDevices.AndroidMobiles().First(),
                TargetDevice.Mobile when targetOS == TargetOS.iOS => SauceLabsDevices.IOSMobiles().First(),
                TargetDevice.Tablet when targetOS == TargetOS.Android => SauceLabsDevices.AndroidTablets().First(),
                TargetDevice.Tablet when targetOS == TargetOS.iOS => realDevice ? SauceLabsDevices.IOSRealTablets().First() : SauceLabsDevices.IOSSimulatorTablets().First(),
                _ => throw new InvalidDataException(
                    $"No device exists with OS '{targetOS}' and Device '{targetDevice}'")
            };
        }
    }
}
