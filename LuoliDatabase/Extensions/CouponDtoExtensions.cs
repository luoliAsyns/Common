using LuoliCommon.DTO.Coupon;
using LuoliCommon.Enums;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuoliDatabase.Extensions
{
    public static class CouponDtoExtensions
    {

        public static CouponEntity ToEntity(this CouponDTO dto)
        {

            if (dto is null)
                return null;
            // 映射属性（处理命名差异和类型转换）
            
            var entity = new CouponEntity();
            
            entity.coupon = dto.Coupon;
            entity.payment = dto.Payment;
            entity.status = (byte)dto.Status;
            entity.create_time = dto.CreateTime;
            entity.update_time = dto.UpdateTime;
            entity.short_url = dto.ShortUrl;
            entity.raw_url = dto.RawUrl;
            entity.proxy_order_id = dto.ProxyOrderId;
            entity.proxy_open_id = dto.ProxyOpenId;
            entity.credit_limit= dto.CreditLimit;

            entity.available_balance = dto.AvailableBalance;
            entity.external_order_from_platform = dto.ExternalOrderFromPlatform;
            entity.external_order_tid = dto.ExternalOrderTid;
            entity.error_code = (int) dto.ErrorCode;
            return entity;

            // return new CouponEntity();
        }

        public static (bool,string) Validate(this CouponDTO dto)
        {

            if (string.IsNullOrEmpty(dto.Coupon))
                return (false, "Coupon is required");

            if (string.IsNullOrEmpty(dto.ExternalOrderFromPlatform))
                return (false, "ExternalOrderFromPlatform is required");
           
            if (string.IsNullOrEmpty(dto.ExternalOrderTid))
                return (false, "ExternalOrderTid is required");

            if (dto.AvailableBalance < 0)
                return (false, "AvailableBalance  must be greater than 0");

            if (dto.Payment <= 0)
                return (false, "PayAmount must be greater than 0");

           
            return (true, string.Empty);
        }
    }
}
