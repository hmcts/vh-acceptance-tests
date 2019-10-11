using System;
using System.Configuration;
using AcceptanceTests.Driver.Support;

namespace AcceptanceTests.Driver
{
    public class DriverManager
    {
        public BrowserSupport GetTargetBrowser()
        {
            BrowserSupport targetBrowser;
            return Enum.TryParse(ConfigurationManager.AppSettings["TargetBrowser"], true, out targetBrowser) ? targetBrowser : BrowserSupport.Chrome;
        }
    }
}
