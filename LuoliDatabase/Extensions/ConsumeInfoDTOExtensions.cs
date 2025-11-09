using LuoliCommon.DTO.ConsumeInfo;
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

    public static class ConsumeInfoDtoExtensions
    {

        public static ConsumeInfoEntity ToEntity(this ConsumeInfoDTO dto)
        {

            if (dto is null)
                return null;

            var entity = new ConsumeInfoEntity();
            entity.id=dto.Id;
            entity.source = dto.Source;
            entity.goods_type = dto.GoodsType;
            entity.target_proxy = dto.TargetProxy;
            entity.goods= JsonSerializer.Serialize(dto.Goods);
            entity.coupon = dto.Coupon;
            entity.last_name = dto.LastName;
            entity.remark = dto.Remark;
            entity.status = (int)dto.Status;
            return entity;

        }

        public static (bool, string) Validate(this ConsumeInfoDTO dto)
        {



            return (true, string.Empty);
        }
    }


}
