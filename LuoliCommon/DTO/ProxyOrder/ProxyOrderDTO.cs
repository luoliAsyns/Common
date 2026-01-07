using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.ProxyOrder
{
    public class ProxyOrderDTO
    {
        public ProxyOrderDTO()
        {
        }
        public DateTime CreateTime { get; set; }
        public DateTime InsertTime { get; set; }
        public string BuyerOpenUid { get; set; }
        public ETargetProxy TargetProxy { get; set; }
        public string? ProxyOrderId { get; set; }
        public string? ProxyOpenId { get; set; }
        public string Order { get; set; }
        public string OrderStatus { get; set; }

        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// order & order status is blank here
        /// generate by DTO, not exist in database
        /// </summary>
        /// <param name="eo"></param>
        /// <param name="coupon"></param>
        public ProxyOrderDTO(ExternalOrderDTO eo, CouponDTO coupon)
        {
            CreateTime = coupon.UpdateTime;
            InsertTime = DateTime.Now;
            BuyerOpenUid = eo.BuyerOpenUid;
            TargetProxy = eo.TargetProxy;
            ProxyOrderId = coupon.ProxyOrderId;
            ProxyOpenId = coupon.ProxyOpenId;
            UpdateTime = DateTime.Now;
        }
    }
}
