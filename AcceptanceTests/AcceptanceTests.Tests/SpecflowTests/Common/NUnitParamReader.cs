using System;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;

namespace AcceptanceTests.SpecflowTests.Common
{
    public class NUnitParamReader
    {
        public static SutSupport GetTargetApp(string configApp = null)
        {
            var nUnitParam = NUnit.Framework.TestContext.Parameters["TargetApp"];
            Console.WriteLine($"GetTargetApp NUnitParam is: {nUnitParam}");

            SutSupport targetApp = SutSupport.AdminWebsite;

            if (nUnitParam != null)
            {
                targetApp = EnumParser.ParseText<SutSupport>(nUnitParam);
            }
            else
            {
                if (!string.IsNullOrEmpty(configApp))
                {
                    targetApp = EnumParser.ParseText<SutSupport>(configApp);
                }
            }
            Console.WriteLine($"Setting target app to: {targetApp}");
            return targetApp;
        }

        public static BrowserSupport GetTargetBrowser(string configBrowser = null)
        {

            var nUnitParam = NUnit.Framework.TestContext.Parameters["TargetBrowser"];
            Console.WriteLine($"GetTargetApp NUnitParam is: {nUnitParam}");

            BrowserSupport targetBrowser = BrowserSupport.Chrome;

            if (nUnitParam != null)
            {
                targetBrowser = EnumParser.ParseText<BrowserSupport>(nUnitParam);
            }
            else
            {
                if (!string.IsNullOrEmpty(configBrowser))
                {
                    targetBrowser = EnumParser.ParseText<BrowserSupport>(configBrowser);
                }
            }
            Console.WriteLine($"Setting target browser to: {targetBrowser}");
            return targetBrowser;
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
