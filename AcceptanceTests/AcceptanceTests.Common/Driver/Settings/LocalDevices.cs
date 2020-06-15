using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;

namespace AcceptanceTests.Common.Driver.Settings
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

        public static List<Device> IOSSimulatorTablets()
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

        public static List<Device> IOSRealTablets()
        {
            return new List<Device>
            {
                new Device()
                {
                    DeviceName = "Nick's iPad",
                    PlatformVersion = "13.5",
                    UUID = "00008027-0013588C0A87002E"
                }
            };
        }

    }
}
