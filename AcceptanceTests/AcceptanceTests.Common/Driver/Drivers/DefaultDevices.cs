using System.IO;
using System.Linq;
using AcceptanceTests.Common.Driver.Enums;
using Castle.Core.Internal;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public static class DefaultDevices
    {
        public static string GetAppiumVersion(TargetDevice targetDevice, TargetOS targetOS, bool runningOnSauceLabs)
        {
            return runningOnSauceLabs ? GetSauceLabsDevice(targetDevice, targetOS).AppiumVersion : GetLocalDevice(targetDevice, targetOS).AppiumVersion;
        }

        public static string GetPlatformVersion(TargetDevice targetDevice, TargetOS targetOS, bool runningOnSauceLabs)
        {
            return runningOnSauceLabs ? GetSauceLabsDevice(targetDevice, targetOS).PlatformVersion : GetLocalDevice(targetDevice, targetOS).PlatformVersion;
        }

        public static string GetTargetDeviceName(TargetDevice targetDevice, TargetOS targetOS, bool runningOnSauceLabs)
        {
            return runningOnSauceLabs ? GetSauceLabsDevice(targetDevice, targetOS).DeviceName : GetLocalDevice(targetDevice, targetOS).DeviceName;
        }

        public static bool IsMobileOrTablet(TargetDevice targetDevice)
        {
            return targetDevice == TargetDevice.Mobile || targetDevice == TargetDevice.Tablet;
        }

        public static bool DeviceNameHasNotBeenSet(string deviceName)
        {
            return deviceName.IsNullOrEmpty();
        }

        private static Device GetLocalDevice(TargetDevice targetDevice, TargetOS targetOS)
        {
            return targetDevice switch
            {
                TargetDevice.Mobile when targetOS == TargetOS.Android => LocalDevices.AndroidMobiles().First(),
                TargetDevice.Mobile when targetOS == TargetOS.iOS => LocalDevices.IOSMobiles().First(),
                TargetDevice.Tablet when targetOS == TargetOS.Android => LocalDevices.AndroidTablets().First(),
                TargetDevice.Tablet when targetOS == TargetOS.iOS => LocalDevices.IOSTablets().First(),
                _ => throw new InvalidDataException(
                    $"No device exists with OS '{targetOS}' and Device '{targetDevice}'")
            };
        }

        private static Device GetSauceLabsDevice(TargetDevice targetDevice, TargetOS targetOS)
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
