using System;
using AcceptanceTests.Driver.Support;

namespace AcceptanceTests.Driver.Settings
{
    public class SauceLabsSettings
    {

        public string Username { get; set; }
        public string AccessKey { get; set; }
        public string MobileAccessKey { get; set; }
        public string RemoteServerUrl { get; private set; }
        public bool RunWithSaucelabs => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(AccessKey);

        public string SetRemoteUrl(PlatformSupport platform)
        {
            if (RunWithSaucelabs)
            {
                switch (platform)
                {
                    case PlatformSupport.Desktop:
                        RemoteServerUrl = $"http://{Username}:{AccessKey}@ondemand.eu-central-1.saucelabs.com:80/wd/hub";
                        break;
                    case PlatformSupport.Mobile:
                        RemoteServerUrl = $"http://{Username}:{MobileAccessKey}@ondemand.eu-central-1.saucelabs.com:80/wd/hub";
                        break;
                    default:
                        throw new NotSupportedException($"Plaftorm {platform} is not currently supported");
                }
            }
            return RemoteServerUrl;
        }
    }
}
