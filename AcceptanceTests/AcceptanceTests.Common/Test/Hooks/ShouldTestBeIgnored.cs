using AcceptanceTests.Common.Driver.Enums;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Common.Test.Hooks
{
    public class ShouldTestBeIgnored
    {
        private ScenarioInfo _test;
        private TargetBrowser _browser;
        private TargetDevice _device;
        private TargetOS _os;

        public ShouldTestBeIgnored ForTest(ScenarioInfo test)
        {
            _test = test;
            return this;
        }

        public ShouldTestBeIgnored WithBrowser(TargetBrowser browser)
        {
            _browser = browser;
            return this;
        }

        public ShouldTestBeIgnored WithDevice(TargetDevice device)
        {
            _device = device;
            return this;
        }

        public ShouldTestBeIgnored WithOS(TargetOS os)
        {
            _os = os;
            return this;
        }

        public void Check()
        {
            foreach (var tag in _test.Tags)
            {
                CheckIfDeviceIgnored(tag, _device, _test.Title);
                CheckIfBrowserIgnored(tag, _browser, _test.Title);
                CheckIfOsIgnored(tag, _os, _test.Title);
            }
        }

        private static void CheckIfDeviceIgnored(string tag, TargetDevice device, string title)
        {

            if (tag.Equals("NotDesktop") && device == TargetDevice.Desktop ||
                tag.Equals("NotMobile") && device == TargetDevice.Mobile ||
                tag.Equals("NotTablet") && device == TargetDevice.Tablet ||
                tag.Equals("DesktopOnly") && device != TargetDevice.Desktop ||
                tag.Equals("MobileOnly") && device != TargetDevice.Mobile ||
                tag.Equals("TabletOnly") && device != TargetDevice.Tablet)
            {
                Assert.Ignore($"Ignoring the '{title}' test on {device}, as this functionality is not supported for this device.");
            }
        }

        private static void CheckIfBrowserIgnored(string tag, TargetBrowser browser, string title)
        {

            if (tag.Equals("NotChrome") && browser == TargetBrowser.Chrome ||
                tag.Equals("NotEdge") && browser == TargetBrowser.Edge ||
                tag.Equals("NotEdgeChromium") && browser == TargetBrowser.EdgeChromium ||
                tag.Equals("NotFirefox") && browser == TargetBrowser.Firefox ||
                tag.Equals("NotIE") && browser == TargetBrowser.Ie11 ||
                tag.Equals("NotSafari") && browser == TargetBrowser.Safari ||
                tag.Equals("ChromeOnly") && browser != TargetBrowser.Chrome ||
                tag.Equals("EdgeOnly") && browser != TargetBrowser.Edge ||
                tag.Equals("EdgeChromiumOnly") && browser != TargetBrowser.EdgeChromium ||
                tag.Equals("FirefoxOnly") && browser != TargetBrowser.Firefox ||
                tag.Equals("IEOnly") && browser != TargetBrowser.Ie11 ||
                tag.Equals("SafariOnly") && browser != TargetBrowser.Safari)
            {
                Assert.Ignore($"Ignoring the '{title}' test on {browser}, as this functionality is not supported for this browser.");
            }
        }

        private static void CheckIfOsIgnored(string tag, TargetOS os, string title)
        {
            if (tag.Equals("NotWindows") && os == TargetOS.Windows ||
                tag.Equals("NotMacOS") && os == TargetOS.MacOs ||
                tag.Equals("NotAndroid") && os == TargetOS.Android ||
                tag.Equals("NotIOS") && os == TargetOS.iOS ||
                tag.Equals("WindowsOnly") && os != TargetOS.Windows ||
                tag.Equals("MacOSOnly") && os != TargetOS.MacOs ||
                tag.Equals("AndroidOnly") && os != TargetOS.Android ||
                tag.Equals("IOSOnly") && os != TargetOS.iOS)
            {
                Assert.Ignore($"Ignoring the '{title}' test on {os}, as this functionality is not supported for this device type.");
            }
        }
    }
}
