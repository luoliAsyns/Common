using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ProxyOrder;
using LuoliCommon.Enums;
using LuoliDatabase.Entities;
using LuoliHelper.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliDatabase.Extensions
{
    public static class ProxyOrderEntityExtensions
    {
        public static ProxyOrderDTO ToDTO(this ProxyOrderEntity entity)
        {
            // 映射属性（处理命名差异和类型转换）

            if (entity is null)
                return null;

            var dto = new ProxyOrderDTO();
            dto.CreateTime = entity.create_time;
            dto.UpdateTime = entity.update_time;

            if (!EnumOperator.TryParseIgnoringCaseAndSpaces(entity.target_proxy, out ETargetProxy targetProxy))
                return null;


            dto.BuyerOpenUid = entity.buyer_open_uid;
            dto.TargetProxy = targetProxy;

            dto.ProxyOrderId = entity.proxy_order_id;
            dto.ProxyOpenId = entity.proxy_open_id;

            dto.Order = entity.order;
            dto.OrderStatus = entity.order_status;

            return dto;

        }
    }
}
