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
            
            entity.available_balance = dto.AvailableBalance;
            entity.external_order_from_platform = dto.ExternalOrderFromPlatform;
            entity.external_order_tid = dto.ExternalOrderTid;
            
            return entity;

            // return new CouponEntity();
        }

        public static (bool,string) Validate(this CouponDTO dto)
        {

            return (true,string.Empty);
        }
    }
}
