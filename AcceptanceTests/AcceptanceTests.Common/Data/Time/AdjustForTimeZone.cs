using System;
using AcceptanceTests.Common.Driver.Enums;
using TimeZoneConverter;

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
            return _runningOnSauceLabs ? dateTime.ToUniversalTime() : dateTime.ToLocalTime();
        }

        public DateTime AdjustAdminWeb(DateTime dateTime)
        {
            if (!_runningOnSauceLabs) return dateTime;
            return _targetOS == TargetOS.MacOs ? dateTime.Add(OffsetUkToUtcIncludingDaylightSaving()) : dateTime;
        }

        private static TimeSpan OffsetUkToUtcIncludingDaylightSaving()
        {
            var utcTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var ukTimeInfo = TZConvert.GetTimeZoneInfo("GMT Standard Time");
            var ukTime = TimeZoneInfo.ConvertTime(utcTime, TimeZoneInfo.Utc, ukTimeInfo);
            return utcTime - ukTime;
        }
    }
}
