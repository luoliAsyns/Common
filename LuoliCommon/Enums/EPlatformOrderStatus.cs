using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum EPlatformOrderStatus
    {

        [Description("已拉取")]
        Pulled =0,
       
        [Description("申请了退款")]
        Refunding,

        [Description("ShipBOT已发送短链并发货")]
        Shipped,

        [Description("PlaceOrderBOT已下单")]
        Placed,
    }
}
