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

        [Description("已发货")]
        Shipped = 0,
        [Description("申请了退款")]
        Refunding,
    }
}
