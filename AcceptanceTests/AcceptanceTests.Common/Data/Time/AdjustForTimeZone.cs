using System;
using AcceptanceTests.Common.Driver.Support;

namespace AcceptanceTests.Common.Data.Time
{
    public class TimeZone
    {
        private readonly bool _runningOnSauceLabs;
        private readonly TargetOS _targetOS;
        public TimeZone(bool runningOnSaucelabs, TargetOS targetOS)
        {
            _runningOnSauceLabs = runningOnSaucelabs;
            _targetOS = targetOS;
        }

        public DateTime Adjust(DateTime dateTime)
        {
            if (!_runningOnSauceLabs) return dateTime.ToLocalTime();
            return _targetOS == TargetOS.macOS ? dateTime.ToUniversalTime().AddHours(1) : dateTime.ToUniversalTime();
        }
    }
}
