using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Coupon
{
    public class BackUpRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string TargetProxy { get; set; }
    }
}
