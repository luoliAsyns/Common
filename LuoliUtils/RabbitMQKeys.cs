using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  LuoliUtils
{
    public static class RabbitMQKeys
    {
        public static string CreatedOrderChannel = "Order:Created";
        public static string ConsumingOrderChannel = "Order:Consuming";
    }
}
