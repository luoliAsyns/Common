using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Coupon
{
    public class GenerateManualRequest
    {
        public string from_platform { get; set; }
        public string tid { get; set; }
        public decimal amount { get; set; }
    }
}
