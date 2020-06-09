using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;

namespace AcceptanceTests.Common.Driver.Settings
{
    public static class SauceLabsDevices
    {
        public static List<Device> AndroidMobiles()
        {
            return new List<Device>
            {
                new Device()
                {
                    AppiumVersion = "1.9.1",
                    DeviceName = "Google Pixel 3a GoogleAPI Emulator",
                    PlatformVersion = "10.0"
                }
            };
        }

        public static List<Device> IOSMobiles()
        {
            return new List<Device>
            {
                new Device()
                {
                    AppiumVersion = "1.17.1",
                    DeviceName = "iPhone 11 Pro Simulator",
                    PlatformVersion = "13.2"
                }
            };
        }

        public static List<Device> AndroidTablets()
        {
            return new List<Device>
            {
                new Device()
                {
                    AppiumVersion = "1.9.1",
                    DeviceName = "Google Pixel C GoogleAPI Emulator",
                    PlatformVersion = "8.1"
                }
            };
        }

        public static List<Device> IOSTablets()
        {
            return new List<Device>
            {
                new Device()
                {
                    AppiumVersion = "1.17.1",
                    DeviceName = "iPad Pro (11 inch) Simulator",
                    PlatformVersion = "13.2"
                }
            };
        }
    }
}
