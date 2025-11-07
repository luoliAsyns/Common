using LuoliCommon.DTO.ConsumeInfo;
using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO
{
    public class TableItemVM
    {


        public ExternalOrderDTO EO { get; set; }
           
       public CouponDTO Coupon { get; set; }
        public ConsumeInfoDTO CI { get; set; }

        public TableItemVM(ExternalOrderDTO eo, CouponDTO coupon, ConsumeInfoDTO ci)
        {
            EO = eo;
            Coupon = coupon;
            CI = ci;
          //  CreateTime = eo.CreateTime;
          //  Platform = eo.FromPlatform;
          //  Tid = eo.Tid;
          //Coupon=  coupon.Coupon;
          //  CouponErrorCode = 


        }
    }
}
