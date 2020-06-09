using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Settings;
using FluentAssertions;

namespace AcceptanceTests.Common.Driver.Drivers
{
    internal abstract class IDriverValidator
    {
        internal abstract void ValidateDriverOptions(DriverOptions options);
        internal abstract void ValidateSauceLabsOptions(SauceLabsOptions options);
    }

    internal class DriverSetupValidator
    {
        public static void ValidateDriver(DriverOptions options)
        {
            var deviceTypes = new Dictionary<TargetDevice, IDriverValidator>
            {
                {TargetDevice.Desktop, new DesktopValidator()},
                {TargetDevice.Tablet, new TabletValidator()},
                {TargetDevice.Mobile, new MobileValidator()}
            };
            deviceTypes[options.TargetDevice].ValidateDriverOptions(options);
        }

        public static void ValidateSauceLabs(TargetDevice targetDevice, SauceLabsOptions options)
        {
            var deviceTypes = new Dictionary<TargetDevice, IDriverValidator>
            {
                {TargetDevice.Desktop, new DesktopValidator()},
                {TargetDevice.Tablet, new TabletValidator()},
                {TargetDevice.Mobile, new MobileValidator()}
            };
            deviceTypes[targetDevice].ValidateSauceLabsOptions(options);
        }

        private class DesktopValidator : IDriverValidator
        {
            internal override void ValidateDriverOptions(DriverOptions options)
            {
                options.TargetOS.ToString().Should().BeOneOf(TargetOS.Windows.ToString(), TargetOS.MacOs.ToString());
                options.TargetBrowser.Should().NotBeNull();
            }

            internal override void ValidateSauceLabsOptions(SauceLabsOptions options)
            {
                GeneralSettingsAreSet(options);
                options.BrowserVersion.Should().NotBeNull();
                options.SeleniumVersion.Should().NotBeNull();
            }
        }

        private class TabletValidator : IDriverValidator
        {
            internal override void ValidateDriverOptions(DriverOptions options)
            {
                options.TargetOS.ToString().Should().BeOneOf(TargetOS.Android.ToString(), TargetOS.iOS.ToString());
                options.TargetBrowser.Should().NotBeNull();
                options.TargetDeviceName.Should().NotBeNullOrEmpty();
                options.TargetDeviceOrientation.Should().NotBeNull();
                VerifyPlatformVersion(options.PlatformVersion, options.TargetOS);
            }

            internal override void ValidateSauceLabsOptions(SauceLabsOptions options)
            {
                GeneralSettingsAreSet(options);
                options.AppiumVersion.Should().NotBeNullOrEmpty();
            }
        }

        private class MobileValidator : IDriverValidator
        {
            internal override void ValidateDriverOptions(DriverOptions options)
            {
                options.TargetOS.ToString().Should().BeOneOf(TargetOS.Android.ToString(), TargetOS.iOS.ToString(), TargetOS.Samsung.ToString());
                options.TargetBrowser.Should().NotBeNull();
                options.TargetDeviceName.Should().NotBeNullOrEmpty();
                options.TargetDeviceOrientation.Should().NotBeNull();
                VerifyPlatformVersion(options.PlatformVersion, options.TargetOS);
            }

            internal override void ValidateSauceLabsOptions(SauceLabsOptions options)
            {
                GeneralSettingsAreSet(options);
                options.AppiumVersion.Should().NotBeNullOrEmpty();
            }
        }

        private static void GeneralSettingsAreSet(SauceLabsOptions options)
        {
            options.CommandTimeoutInSeconds.Should().BeGreaterThan(0);
            options.IdleTimeoutInSeconds.Should().BeGreaterThan(0);
            options.MaxDurationInSeconds.Should().BeGreaterThan(0);
            options.Name.Should().NotBeNullOrEmpty();
            options.Timezone.Should().NotBeNullOrEmpty();
        }

        private static void VerifyPlatformVersion(string platformVersion, TargetOS targetOS)
        {
            if (targetOS == TargetOS.iOS)
            { 
                platformVersion.Should().NotBeNullOrEmpty();
            }
        }
    }
}
