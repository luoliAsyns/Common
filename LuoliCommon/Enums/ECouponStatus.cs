using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum ECouponStatus
    {
        [Description("初始状态")]
        Default = 0,

        [Description("已生成")]
        Generated,

        [Description("已推送")]
        Shipped=10,
        [Description("推送失败")]
        ShipFailed,


        [Description("已消费")]
        Consumed=20,
        [Description("消费失败")]
        ConsumeFailed,

        [Description("已在代理平台退款")]
        ProxyRefund = 25,

        [Description("已作废")]
        Recycled=30,
        [Description("作废失败")]
        RecycleFailed,

    }
}
