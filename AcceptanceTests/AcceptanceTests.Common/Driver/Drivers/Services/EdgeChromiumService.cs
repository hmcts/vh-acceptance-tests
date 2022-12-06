using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium.Edge;

namespace AcceptanceTests.Common.Driver.Drivers.Services
{
    public class EdgeChromiumService : IDriverService
    {
        private static EdgeDriverService _edgeService;

        Uri IDriverService.Start()
        {
            _edgeService = EdgeDriverService.CreateDefaultService(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"msedgedriver.exe");
            _edgeService.UseVerboseLogging = true;
            _edgeService.Start();
            return _edgeService.ServiceUrl;
        }

        void IDriverService.Stop()
        {
            _edgeService?.Dispose();
        }
    }
}
