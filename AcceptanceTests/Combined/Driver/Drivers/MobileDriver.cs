using System;
using AcceptanceTests.Common.Driver.Capabilities;
using AcceptanceTests.Common.Driver.Support;
using OpenQA.Selenium;

namespace AcceptanceTests.Common.Driver.Drivers
{
    public class MobileDriver
    {
        public static ICapabilities GetMobileDriverCapabilities(BrowserSupport targetBrowser, DeviceSupport device, string scenarioTitle)
        {
            RemoteSessionSettings capabilities = null;
            switch (targetBrowser)
            {
                case BrowserSupport.Safari:
                    capabilities = (RemoteSessionSettings)MobileDriverCapabilities.GetSafariCapabilities(device);
                    break;
                default:
                    var message = $"The device {device} browser {targetBrowser} combination is not supported.";
                    Console.WriteLine(message);
                    throw new NotSupportedException(message);
            }
            capabilities.AddMetadataSetting("name", scenarioTitle);
            capabilities.AddMetadataSetting("build", $"{Environment.GetEnvironmentVariable("Build_DefinitionName")} {Environment.GetEnvironmentVariable("RELEASE_RELEASENAME")}");
            return capabilities;
        }
    }
}
