using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum EConsumeInfoStatus
    {

        [Description("初始状态")]
        Default = 0,

        [Description("已拉取")]
        Pulled,


        [Description("已下单")]
        Placed=10,
        [Description("下单失败")]
        PlaceFailed,
    }
}
