using System;

namespace AcceptanceTests.Common.Data.Time
{
    public class TimeZone
    {
        private readonly bool _runningOnSauceLabs;
        public TimeZone(bool runningOnSaucelabs)
        {
            _runningOnSauceLabs = runningOnSaucelabs;
        }

        public DateTime Adjust(DateTime dateTime)
        {
            return !_runningOnSauceLabs ? dateTime.ToLocalTime() : dateTime.ToUniversalTime();
        }
    }
}
