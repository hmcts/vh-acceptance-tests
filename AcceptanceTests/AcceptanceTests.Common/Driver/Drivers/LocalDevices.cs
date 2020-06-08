using System.Collections.Generic;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public static class LocalDevices
    {
        public static List<Device> AndroidMobiles()
        {
            return new List<Device>
            {
                new Device()
                {
                    DeviceName = "Google_Pixel_3a_GoogleAPI_Emulator",
                }
            };
        }

        public static List<Device> IOSMobiles()
        {
            return new List<Device>
            {
                new Device()
                {
                    DeviceName = "iPhone 11 Pro",
                    PlatformVersion = "13.5"
                }
            };
        }

        public static List<Device> AndroidTablets()
        {
            return new List<Device>
            {
                new Device()
                {
                    DeviceName = "Pixel_C_API_R",
                }
            };
        }

        public static List<Device> IOSTablets()
        {
            return new List<Device>
            {
                new Device()
                {
                    DeviceName = "iPad Pro (11-inch) (2nd generation)",
                    PlatformVersion = "13.5"
                }
            };
        }
    }
}
