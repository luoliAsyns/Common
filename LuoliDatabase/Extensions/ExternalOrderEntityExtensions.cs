using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliDatabase
{
    public static class ExternalOrderEntityExtensions
    {

        public static ExternalOrderDTO  ToDTO(this ExternalOrderEntity entity)
        {
            // 映射属性（处理命名差异和类型转换）

            if (entity is null)
                return null;

            var dto = new ExternalOrderDTO();
            dto.CreateTime = entity.create_time;
            dto.UpdateTime = entity.update_time;
            dto.ExternalOrderItems = new List<ExternalOrderItem>();
            dto.PayAmount = entity.payment;

            dto.FromPlatform = entity.from_platform;
            dto.Tid = entity.tid;
            dto.Status = entity.status;

            dto.SellerNick = entity.seller_nick;
            dto.SellerOpenUid = entity.seller_open_uid;
            dto.BuyerNick = entity.buyer_nick;
            dto.BuyerOpenUid = entity.buyer_open_uid;

            return dto;

        }
    }
}
