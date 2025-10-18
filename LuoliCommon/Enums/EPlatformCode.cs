using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum  EPlatformCode
    {
        [Description("淘宝")]
        Taobao = 1,
        [Description("闲鱼")]
        Xianyu,
        [Description("微信小程序")]
        Xiaochengxu,
        [Description("个人")]
        Personal,

    }
}
