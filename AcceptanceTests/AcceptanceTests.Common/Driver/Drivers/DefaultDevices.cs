using System.IO;
using System.Linq;
using AcceptanceTests.Common.Driver.Enums;
using Castle.Core.Internal;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public static class DefaultDevices
    {
        public static string GetAppiumVersion(TargetDevice targetDevice, TargetOS targetOS)
        {
            return GetDevice(targetDevice, targetOS).AppiumVersion;
        }

        public static string GetPlatformVersion(TargetDevice targetDevice, TargetOS targetOS)
        {
            return GetDevice(targetDevice, targetOS).PlatformVersion;
        }

        public static string GetTargetDeviceName(TargetDevice targetDevice, TargetOS targetOS)
        {
            return GetDevice(targetDevice, targetOS).DeviceName;
        }

        public static bool IsMobileOrTablet(TargetDevice targetDevice)
        {
            return targetDevice == TargetDevice.Mobile || targetDevice == TargetDevice.Tablet;
        }

        public static bool DeviceNameHasNotBeenSet(string deviceName)
        {
            return deviceName.IsNullOrEmpty();
        }

        private static SauceLabsDevice GetDevice(TargetDevice targetDevice, TargetOS targetOS)
        {
            return targetDevice switch
            {
                TargetDevice.Mobile when targetOS == TargetOS.Android => SauceLabsDevices.AndroidMobiles().First(),
                TargetDevice.Mobile when targetOS == TargetOS.iOS => SauceLabsDevices.IOSMobiles().First(),
                TargetDevice.Tablet when targetOS == TargetOS.Android => SauceLabsDevices.AndroidTablets().First(),
                TargetDevice.Tablet when targetOS == TargetOS.iOS => SauceLabsDevices.IOSTablets().First(),
                _ => throw new InvalidDataException(
                    $"No device exists with OS '{targetOS}' and Device '{targetDevice}'")
            };
        }
    }
}
