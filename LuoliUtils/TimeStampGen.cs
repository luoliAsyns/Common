using LuoliCommon.DTO.Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliUtils
{
    public static class TimeStampGen
    {

        public static long GetTimeStampSec()
        {

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static long GetTimeStampSec(DateTime datetime)
        {
            TimeSpan ts = datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return Convert.ToInt64(ts.TotalSeconds);
        }

        public static long GetTimeStampMs(DateTime datetime)
        {
            TimeSpan ts = datetime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        public static long GetTimeStampMs()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}
