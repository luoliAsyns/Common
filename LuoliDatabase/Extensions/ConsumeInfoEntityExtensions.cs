using LuoliCommon.DTO.ConsumeInfo;
using LuoliCommon.DTO.Coupon;
using LuoliCommon.Enums;
using LuoliDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LuoliDatabase.Extensions
{

    public static class ConsumeInfoEntityExtensions
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, // 关键配置：忽略大小写
        };

        public static ConsumeInfoDTO ToDTO(this ConsumeInfoEntity entity)
        {

            if (entity is null)
                return null;
            // 映射属性（处理命名差异和类型转换）

            var dto = new ConsumeInfoDTO();

            dto.Id = entity.id;
            dto.TargetProxy = entity.target_proxy;
            dto.GoodsType = entity.goods_type;

            dto.Coupon = entity.coupon;
            dto.Goods = dto.GoodsType switch
            {
                "sexytea_consume_info" => JsonSerializer.Deserialize<SexyteaGoods>(entity.goods, _options),
                _ => throw new Exception($"unknown goodsType {dto.GoodsType}"),
            };

            dto.Status= (EConsumeInfoStatus)entity.status;
            dto.LastName = entity.last_name;
            dto.Remark = entity.remark;
            dto.CreateTime = entity.create_time;
            dto.Source = entity.source;
            return dto;
        }

        public static (bool, string) Validate(this ConsumeInfoEntity dto)
        {


            return (true, string.Empty);
        }
    }
}
