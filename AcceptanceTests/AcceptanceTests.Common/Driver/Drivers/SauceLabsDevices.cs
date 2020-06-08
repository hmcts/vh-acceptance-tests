using System.Collections.Generic;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public static class SauceLabsDevices
    {
        public static List<SauceLabsDevice> AndroidMobiles()
        {
            return new List<SauceLabsDevice>
            {
                new SauceLabsDevice()
                {
                    AppiumVersion = "1.9.1",
                    DeviceName = "Google Pixel 3a GoogleAPI Emulator",
                    PlatformVersion = "10.0"
                }
            };
        }

        public static List<SauceLabsDevice> IOSMobiles()
        {
            return new List<SauceLabsDevice>
            {
                new SauceLabsDevice()
                {
                    AppiumVersion = "1.17.1",
                    DeviceName = "iPhone 11 Pro Simulator",
                    PlatformVersion = "13.2"
                }
            };
        }

        public static List<SauceLabsDevice> AndroidTablets()
        {
            return new List<SauceLabsDevice>
            {
                new SauceLabsDevice()
                {
                    AppiumVersion = "1.9.1",
                    DeviceName = "Google Pixel C GoogleAPI Emulator",
                    PlatformVersion = "8.1"
                }
            };
        }

        public static List<SauceLabsDevice> IOSTablets()
        {
            return new List<SauceLabsDevice>
            {
                new SauceLabsDevice()
                {
                    AppiumVersion = "1.17.1",
                    DeviceName = "iPad Pro (11 inch) Simulator",
                    PlatformVersion = "13.2"
                }
            };
        }
    }
}
