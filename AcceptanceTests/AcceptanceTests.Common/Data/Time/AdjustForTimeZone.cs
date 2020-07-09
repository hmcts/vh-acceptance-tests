using System;
using AcceptanceTests.Common.Driver.Enums;

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
            return _targetOS == TargetOS.MacOs ? dateTime.ToUniversalTime().AddHours(1) : dateTime.ToUniversalTime();
        }

        public DateTime AdjustAnyOS(DateTime dateTime)
        {
            return !_runningOnSauceLabs ? dateTime.ToLocalTime() : dateTime.ToUniversalTime();
        }

        public DateTime AdjustAdminWeb(DateTime dateTime)
        {
            if (!_runningOnSauceLabs) return dateTime.ToLocalTime();
            return _targetOS == TargetOS.MacOs ? dateTime.Add(OffsetUkToUtcIncludingDaylightSaving()) : dateTime.ToUniversalTime();
        }

        public TimeSpan OffsetUkToUtcIncludingDaylightSaving()
        {
            var utcTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var ukTime = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
            return utcTime - ukTime;
        }
    }
}
