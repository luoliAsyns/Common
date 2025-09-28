using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum EConsumeType
    {
        [Description("自提")]
        Pickup = 0,
        [Description("外卖")]
        Takeout
    }
}
