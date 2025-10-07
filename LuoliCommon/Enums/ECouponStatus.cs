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
        [Description("已生成")]
        Generated = 0,
        [Description("已开始处理，等待BOT消费")]
        Consuming,
        [Description("已被BOT消费")]
        Consumed,
        [Description("已回收")]
        Recycled,
        
    }
}
