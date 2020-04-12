using System;
using AcceptanceTests.Common.Driver.Support;

namespace AcceptanceTests.Common.Data.Time
{
    public class TimeZone
    {
        private readonly bool _runningOnSauceLabs;
        private readonly TargetBrowser _browser;
        public TimeZone(bool runningOnSaucelabs, TargetBrowser browser)
        {
            _runningOnSauceLabs = runningOnSaucelabs;
            _browser = browser;
        }

        public DateTime Adjust(DateTime dateTime)
        {
            if (!_runningOnSauceLabs) return dateTime.ToLocalTime();
            if (_browser == TargetBrowser.Safari ||
                _browser == TargetBrowser.MacChrome ||
                _browser == TargetBrowser.MacFirefox)
            {
                return dateTime.ToUniversalTime().AddHours(1);
            }
            return dateTime.ToUniversalTime();
        }

        public DateTime AdjustForVideoWeb(DateTime dateTime)
        {
            return !_runningOnSauceLabs ? dateTime.ToLocalTime() : dateTime.ToUniversalTime();
        }
    }
}
