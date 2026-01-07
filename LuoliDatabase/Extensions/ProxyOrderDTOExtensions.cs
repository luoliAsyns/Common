using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.DTO.ProxyOrder;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuoliDatabase.Extensions
{
    public static class ProxyOrderDTOExtensions
    {
        public static ProxyOrderEntity ToEntity(this ProxyOrderDTO dto)
        {

            if (dto is null)
                return null;

            var entity = new ProxyOrderEntity();
            entity.create_time = dto.CreateTime;
            entity.insert_time = dto.InsertTime;
            entity.update_time = dto.UpdateTime;

            entity.buyer_open_uid = dto.BuyerOpenUid;
            entity.target_proxy = dto.TargetProxy.ToString();
            entity.proxy_order_id = dto.ProxyOrderId;
            entity.proxy_open_id = dto.ProxyOpenId;
            
            entity.order = dto.Order;
            entity.order_status = dto.OrderStatus;

            return entity;

        }
    }
}
