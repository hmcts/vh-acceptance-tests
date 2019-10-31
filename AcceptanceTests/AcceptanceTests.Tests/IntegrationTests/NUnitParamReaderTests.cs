using System;
using AcceptanceTests.Driver.Support;
using AcceptanceTests.Model;
using FluentAssertions;
using NUnit.Framework;

namespace AcceptanceTests.Tests.IntegrationTests
{
    public class NUnitParamReaderTests
    {
        [TestCase(null)]
        [TestCase("AdminWebsite")]
        [TestCase("ServiceWebsite")]
        public void GetTargetAppTest(string app)
        {
            var targetApp = NUnitParamReader.GetTargetApp(app);
            Console.WriteLine($"Target app is {targetApp.ToString()}");
            targetApp.Should().NotBeNull();
        }

        [TestCase("AdminWebsite")]
        [TestCase("ServiceWebsite")]
        public void GetTargetAppShouldMatchAppTest(string app)
        {
            var targetApp = NUnitParamReader.GetTargetApp(app);
            Console.WriteLine($"Target app is {targetApp.ToString()}");
            targetApp.Should().Be(EnumParser.ParseText<SutSupport>(app));
        }

        [TestCase(null)]
        [TestCase("Chrome")]
        [TestCase("Safari")]
        [TestCase("Firefox")]
        public void GetTargetBrowserTest(string browser)
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            Console.WriteLine($"Target browser is {targetBrowser.ToString()}");
            targetBrowser.Should().NotBeNull();
        }

        [TestCase("Chrome")]
        [TestCase("Safari")]
        [TestCase("Firefox")]
        public void GetTargetBrowserMatchesBrowserTest(string browser)
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            Console.WriteLine($"Target browser is {targetBrowser.ToString()}");
            targetBrowser.Should().Be(EnumParser.ParseText<BrowserSupport>(browser));
        }
    }
}
