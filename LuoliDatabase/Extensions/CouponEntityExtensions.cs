using LuoliCommon.DTO.Coupon;
using LuoliCommon.Enums;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliDatabase
{
    public static class CouponEntityExtensions
    {

        public static CouponDTO  ToDTO(this CouponEntity entity)
        {
            // 映射属性（处理命名差异和类型转换）

            if (entity is null)
                return null;

            var dto = new CouponDTO();
            dto.CreateTime = entity.create_time;
            dto.UpdateTime = entity.update_time;
            dto.Payment = entity.payment;
            dto.AvailableBalance = entity.available_balance;

            dto.ShortUrl = entity.short_url;
            dto.RawUrl = entity.raw_url;
            dto.Status = (ECouponStatus)entity.status ;
            dto.Coupon = entity.coupon;
            dto.ExternalOrderFromPlatform = entity.external_order_from_platform;
            dto.ExternalOrderTid = entity.external_order_tid;
            dto.ProxyOrderId = entity.proxy_order_id;

            return dto;

        }
    }
}
