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
            // 映射属性（处理命名差异和类型转换）

            var entity = new ExternalOrderEntity();

            entity.target_proxy = dto.TargetProxy.ToString();
            entity.tid = dto.Tid;
            entity.payment = dto.PayAmount;
            entity.status = dto.Status;
            entity.create_time = dto.CreateTime;
            entity.update_time = dto.UpdateTime;
            entity.content = JsonSerializer.Serialize(dto.Order);
          
            entity.buyer_nick = dto.BuyerNick;
            entity.buyer_open_uid = dto.BuyerOpenUid;
            entity.seller_nick = dto.SellerNick;
            entity.seller_open_uid=dto.SellerOpenUid;

            entity.from_platform = dto.FromPlatform;
            entity.target_proxy = dto.TargetProxy.ToString();

            return entity;

        }

        public static CouponDTO ToCouponDTO(this ExternalOrderDTO dto)
        {

            if (dto is null)
                return null;
            // 映射属性（处理命名差异和类型转换）

            var couponDTO = new CouponDTO();

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

            if (dto.Order is null)
                return (false, "ExternalOrderDTO.Order cannot be null");

            return (true, string.Empty);

        }
    }
}
