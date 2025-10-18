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
        Pulled =1,
       
        [Description("申请了退款")]
        Refunding,

    }
}
