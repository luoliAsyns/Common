using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum EPayType
    {
        [Description("平台付款")]
        Platform = 0,
        [Description("个人收款")]
        Personal,
    }
}
