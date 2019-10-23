using System;
using AcceptanceTests.Driver.Support;

namespace AcceptanceTests.Tests
{
    public class NUnitParamReader
    {
        public static SutSupport GetTargetApp()
        {
            SutSupport targetApp;
            var nUnitParam = NUnit.Framework.TestContext.Parameters["TargetApp"];
            Console.WriteLine($"nUnitParam {nUnitParam}");
            return Enum.TryParse(nUnitParam, true, out targetApp) ? targetApp : SutSupport.AdminWebsite;
        }

        public static BrowserSupport GetTargetBrowser()
        {
            BrowserSupport targetBrowser;
            return Enum.TryParse(NUnit.Framework.TestContext.Parameters["TargetBrowser"], true, out targetBrowser) ? targetBrowser : BrowserSupport.Chrome;
        }

        public static PlatformSupport GetTargetPlatform()
        {
            PlatformSupport targetPlatform;
            return Enum.TryParse(NUnit.Framework.TestContext.Parameters["TargetPlatform"], true, out targetPlatform) ? targetPlatform : PlatformSupport.Desktop;
        }

        internal static DeviceSupport GetTargetDevice()
        {
            DeviceSupport targetDevice;
            return Enum.TryParse(NUnit.Framework.TestContext.Parameters["TargetDevice"], true, out targetDevice) ? targetDevice : DeviceSupport.Pc;
        }
    }
}
