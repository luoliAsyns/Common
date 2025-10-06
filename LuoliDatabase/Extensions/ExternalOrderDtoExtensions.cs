using LuoliCommon.DTO.ExternalOrder;
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
    public static class ExternalOrderDtoExtensions
    {

        public static ExternalOrderEntity ToEntity(this ExternalOrderDTO dto)
        {

            if (dto is null)
                return null;
            // 映射属性（处理命名差异和类型转换）

            var entity = new ExternalOrderEntity();

            entity.tid = dto.Tid;
            entity.payment = dto.PayAmount;
            entity.status = dto.Status;
            entity.create_time = dto.CreateTime;
            entity.update_time = dto.UpdateTime;
            entity.content = JsonSerializer.Serialize( dto.ExternalOrderItems);
          
            entity.buyer_nick = dto.BuyerNick;
            entity.buyer_open_uid = dto.BuyerOpenUid;
            entity.seller_nick = dto.SellerNick;
            entity.seller_open_uid=dto.SellerOpenUid;

            entity.from_platform = dto.FromPlatform;
            entity.target_proxy = "sexytea"; // 这里应该通过 dto.ExternalOrderItems 计算

            return entity;

        }

        public static (bool,string) Validate(this ExternalOrderDTO dto)
        {

            return (true,string.Empty);
        }
    }
}
