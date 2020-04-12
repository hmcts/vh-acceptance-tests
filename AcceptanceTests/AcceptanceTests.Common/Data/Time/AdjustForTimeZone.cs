using System;
using AcceptanceTests.Common.Driver.Support;

namespace AcceptanceTests.Common.Data.Time
{
    public class TimeZone
    {
        private readonly bool _runningOnSauceLabs;
        private readonly TargetBrowser _browser;
        private readonly bool _isVideoWeb;
        public TimeZone(bool runningOnSaucelabs, TargetBrowser browser, bool isVideoWeb = false)
        {
            _runningOnSauceLabs = runningOnSaucelabs;
            _browser = browser;
            _isVideoWeb = isVideoWeb;
        }

        public DateTime Adjust(DateTime dateTime)
        {
            if (!_runningOnSauceLabs) return dateTime.ToLocalTime();
            if (_browser == TargetBrowser.Safari ||
                _browser == TargetBrowser.MacChrome ||
                _browser == TargetBrowser.MacFirefox)
            {
                return _isVideoWeb ? dateTime.ToUniversalTime() : dateTime.ToUniversalTime().AddHours(1);
            }
            return dateTime.ToUniversalTime();
        }
    }
}
