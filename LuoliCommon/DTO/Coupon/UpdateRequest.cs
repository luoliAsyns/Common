using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Coupon
{
    public class UpdateRequest
    {
        public CouponDTO Coupon { get; set; }
        public EEvent Event { get; set; }


        public UpdateRequest()
        {
            
        }

        public bool UpdateStatus(CouponDTO coupon, EEvent e)
        {
            if (e == EEvent.ProxyQuery)
                return true;

            if (coupon.Status == ECouponStatus.Generated && e == EEvent.Coupon_Shipment)
            {
                coupon.Status = ECouponStatus.Shipped;
                return true;
            }

            if (coupon.Status == ECouponStatus.Generated && e == EEvent.Coupon_ShipFailed)
            {
                coupon.Status = ECouponStatus.ShipFailed;
                return true;
            }

            if (coupon.Status == ECouponStatus.Shipped && e == EEvent.Received_Refund_EO)
            {
                coupon.Status = ECouponStatus.Recycled;
                return true;
            }

            if (coupon.Status == ECouponStatus.Shipped && e == EEvent.Receive_Manual_Cancel_Coupon)
            {
                coupon.Status = ECouponStatus.Recycled;
                return true;
            }

            if (coupon.Status == ECouponStatus.Recycled && e == EEvent.Receive_Manual_Recover_Coupon)
            {
                coupon.Status = ECouponStatus.Shipped;
                return true;
            }

            if (coupon.Status == ECouponStatus.Shipped && e == EEvent.Placed)
            {
                coupon.Status = ECouponStatus.Consumed;
                return true;
            }

            if (coupon.Status == ECouponStatus.Shipped && e == EEvent.PlaceFailed)
            {
                coupon.Status = ECouponStatus.ConsumeFailed;
                return true;
            }


            if (coupon.Status == ECouponStatus.Consumed && e == EEvent.ProxyRefund)
            {
                coupon.Status = ECouponStatus.ProxyRefund;
                return true;
            }


            return false;
        }

    }
}
