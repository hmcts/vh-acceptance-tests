using System.Collections.Generic;
using AcceptanceTests.Common.Driver.Configuration;
using AcceptanceTests.Common.Driver.Enums;
using AcceptanceTests.Common.Driver.Support;
using FluentAssertions;

namespace AcceptanceTests.Common.Driver.Drivers
{
    internal abstract class IDriverValidator
    {
        internal abstract void Validate(DriverOptions options);
    }

    internal class DriverValidator
    {
        public void Validate(DriverOptions options)
        {
            var deviceTypes = new Dictionary<TargetDevice, IDriverValidator>
            {
                {TargetDevice.Desktop, new DesktopValidator()},
                {TargetDevice.Tablet, new TabletValidator()},
                {TargetDevice.Mobile, new MobileValidator()}
            };
            deviceTypes[options.TargetDevice].Validate(options);
        }

        private class DesktopValidator : IDriverValidator
        {
            internal override void Validate(DriverOptions options)
            {
                options.TargetOS.ToString().Should().BeOneOf(TargetOS.Windows.ToString(), TargetOS.MacOs.ToString());
                options.TargetBrowser.Should().NotBeNull();
                options.TargetBrowserVersion.Should().NotBeNull();
            }
        }

        private class TabletValidator : IDriverValidator
        {
            internal override void Validate(DriverOptions options)
            {
                options.TargetOS.ToString().Should().BeOneOf(TargetOS.Android.ToString(), TargetOS.iOS.ToString());
                options.TargetBrowser.Should().NotBeNull();
                options.TargetDeviceName.Should().NotBeNullOrEmpty();
                options.TargetDeviceOrientation.Should().NotBeNull();
            }
        }

        private class MobileValidator : IDriverValidator
        {
            internal override void Validate(DriverOptions options)
            {
                options.TargetOS.ToString().Should().BeOneOf(TargetOS.Android.ToString(), TargetOS.iOS.ToString(), TargetOS.Samsung.ToString());
                options.TargetBrowser.Should().NotBeNull();
                options.TargetDeviceName.Should().NotBeNullOrEmpty();
                options.TargetDeviceOrientation.Should().NotBeNull();
            }
        }
    }
}
