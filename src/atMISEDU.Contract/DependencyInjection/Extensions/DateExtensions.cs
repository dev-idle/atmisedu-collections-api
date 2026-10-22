using atMISEDU.Contract.Constants;

namespace atMISEDU.Contract.DependencyInjection.Extensions
{
    public static class IExtensions
    {
        public static bool DateEqual(this int date, int dateDiff)
        {
            long currentDay = (dateDiff + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;
            return (date + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY == currentDay;
        }

        public static bool DateLessThan(this int date, int dateDiff)
        {
            long currentDay = (dateDiff + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;
            return (date + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY < currentDay;
        }

        public static bool DateLessThanOrEqual(this int date, int dateDiff)
        {
            long currentDay = (dateDiff + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;
            return (date + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY <= currentDay;
        }

        public static bool DateGreaterThan(this int date, int dateDiff)
        {
            long currentDay = (dateDiff + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;
            return (date + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY > currentDay;
        }

        public static bool DateGreaterThanOrEqual(this int date, int dateDiff)
        {
            long currentDay = (dateDiff + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY;
            return (date + Const.TIME_ZONE_UTC_7) / Const.SECONDS_IN_ADAY >= currentDay;
        }
    }
}