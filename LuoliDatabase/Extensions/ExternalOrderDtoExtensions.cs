using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LuoliCommon.DTO.Coupon;
using LuoliHelper.StaticClasses;

namespace LuoliDatabase.Extensions
{
    public static class ExternalOrderDtoExtensions
    {

        public static ExternalOrderEntity ToEntity(this ExternalOrderDTO dto)
        {

            if (dto is null)
                return null;

            var entity = new ExternalOrderEntity();

            entity.target_proxy = dto.TargetProxy.ToString();
            entity.tid = dto.Tid;
            entity.payment = dto.PayAmount;
            entity.status = ( byte)dto.Status;
            entity.create_time = dto.CreateTime;
            entity.update_time = dto.UpdateTime;
            entity.content = JsonSerializer.Serialize(dto.SubOrders);
          
            entity.buyer_nick = dto.BuyerNick;
            entity.buyer_open_uid = dto.BuyerOpenUid;
            entity.seller_nick = dto.SellerNick;
            entity.seller_open_uid=dto.SellerOpenUid;

            entity.from_platform = dto.FromPlatform;
            entity.target_proxy = dto.TargetProxy.ToString();

            return entity;

        }

        public static CouponDTO ToCouponDTO(this ExternalOrderDTO dto, string appSecret)
        {

            if (dto is null)
                return null;

            var couponDTO = new CouponDTO();

            couponDTO.Coupon = LuoliUtils.Decoder.SHA256($"{dto.FromPlatform}-{dto.Tid}-{appSecret}");

            couponDTO.ExternalOrderTid = dto.Tid;
            couponDTO.ExternalOrderFromPlatform = dto.FromPlatform;

            couponDTO.Payment = dto.PayAmount;
            couponDTO.AvailableBalance = couponDTO.Payment;
            
            couponDTO.Status = ECouponStatus.Generated;
            couponDTO.CreateTime = DateTime.Now;
            couponDTO.UpdateTime = couponDTO.CreateTime;

            return couponDTO;

        }

        
        public static (bool,string) Validate(this ExternalOrderDTO dto)
        {
            if (string.IsNullOrEmpty(dto.FromPlatform))
                return (false, "FromPlatform is required");

            if (string.IsNullOrEmpty(dto.Tid))
                return (false, "Tid is required");

            if (dto.PayAmount <= 0)
                return (false, "PayAmount must be greater than 0");

            if (dto.SubOrders is null)
                return (false, "ExternalOrderDTO.SubOrders cannot be null");

            if (dto.SubOrders.Count== 0)
                return (false, "ExternalOrderDTO.SubOrders count need to be greater than 0");

            return (true, string.Empty);

        }
        public static (bool, string) ValidateBeforeGenCoupon(this ExternalOrderDTO dto)
        {

            if (dto.Status == EExternalOrderStatus.Refunding)
                return (false, "平台订单处于退款中");

            return (true, string.Empty);

        }
        

    }
}
