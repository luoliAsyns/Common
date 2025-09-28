using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum EOrderStatus
    {
        [Description("已创建")]
        Created = 0,
        [Description("提交到队列")]
        Sent2MQ ,
        [Description("代下单完成")]
        Completed,
        [Description("已退款")]
        Refunded 
    }
}
