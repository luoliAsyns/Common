using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Coupon
{
    public class UpdateErrorCodeRequest
    {
        public string Coupon { get; set; }
        public ECouponErrorCode ErrorCode { get; set; }


        public UpdateErrorCodeRequest()
        {
            
        }


    }
}
