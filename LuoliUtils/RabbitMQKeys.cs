using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  LuoliUtils
{
    public static class RabbitMQKeys
    {
        public static string ExternalOrderInserted = ":insert";
        public static string CouponGenerated = "coupon:generated";
        
        public static string ConsumeInfoInserted = ":consuming";
    }
}
