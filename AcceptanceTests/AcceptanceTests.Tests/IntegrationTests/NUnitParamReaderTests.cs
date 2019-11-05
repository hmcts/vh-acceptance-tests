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
        [Category("Local")]
        public void GetTargetAppShouldMatchAppTest(string app)
        {
            var targetApp = NUnitParamReader.GetTargetApp(app);

            Console.WriteLine($"Target app is {targetApp.ToString()}");
            targetApp.Should().Be(EnumParser.ParseText<SutSupport>(app));
        }


        [TestCase("Safari")]
        [TestCase("Firefox")]
        [Category("Local")]
        public void GetTargetBrowserTest(string browser)
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            Console.WriteLine($"Target browser is {targetBrowser.ToString()}");
            targetBrowser.Should().NotBeNull();
        }

        [TestCase(null)]
        [TestCase("Chrome")]
        public void GetTargetChromeOrNullBrowserTest(string browser)
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            Console.WriteLine($"Target browser is {targetBrowser.ToString()}");
            targetBrowser.Should().NotBeNull();
        }

        [TestCase("Safari")]
        [TestCase("Firefox")]
        [Category("Local")]
        public void GetTargetBrowserMatchesBrowserTest(string browser)
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            Console.WriteLine($"Target browser is {targetBrowser.ToString()}");
            targetBrowser.Should().Be(EnumParser.ParseText<BrowserSupport>(browser));
        }

        [TestCase("Chrome")]
        public void GetTargetChromeBrowserMatchesBrowserTest(string browser)
        {
            var targetBrowser = NUnitParamReader.GetTargetBrowser(browser);
            Console.WriteLine($"Target browser is {targetBrowser.ToString()}");
            targetBrowser.Should().Be(EnumParser.ParseText<BrowserSupport>(browser));
        }
    }
}
