using System;
using AcceptanceTests.Driver.Support;
using OpenQA.Selenium;

namespace AcceptanceTests.Driver.Capabilities
{
    public class MobileDriverCapabilities
    {
        public static ICapabilities GetSafariCapabilities(DeviceSupport device)
        {
            var capabilities = new RemoteSessionSettings();

            switch (device)
            {
                case DeviceSupport.Iphone:
                    capabilities.AddMetadataSetting("browser", "iphone");
                    capabilities.AddMetadataSetting("version", "8.0");
                    capabilities.AddMetadataSetting("deviceOrientation", "portrait");
                    capabilities.AddMetadataSetting("platform", "iOS 13.1.2");
                    break;
                case DeviceSupport.Ipad:
                    capabilities.AddMetadataSetting("browser", "ipad");
                    capabilities.AddMetadataSetting("version", "10.2");
                    capabilities.AddMetadataSetting("deviceOrientation", "landscape");
                    capabilities.AddMetadataSetting("platform", "iOS 13.1.2");
                    break;
                default:
                    var message = $"The device {device} is not supported.";
                    Console.WriteLine(message);
                    throw new NotSupportedException(message);
            }
            
            return capabilities;
        }
    }
}
