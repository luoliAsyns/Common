using LuoliCommon.DTO.Agiso;
using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using LuoliDatabase.Entities;
using LuoliHelper.StaticClasses;
using LuoliUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace LuoliDatabase.Extensions
{
    public static class TradeInfoDTOExtensions
    {

        public static List<ExternalOrderDTO> ToExternalOrderDTO(this TradeInfoDTO tradeInfo)
        {
            List<ExternalOrderDTO> list = new(4);
            if (tradeInfo is null)
                return list;

            //失败的response
            if(!tradeInfo.IsSuccess)
                return list;

            if (tradeInfo.Data.Orders.Count == 1)
            {
                var dto = ToExternalOrderDTO(tradeInfo, tradeInfo.Data.Orders.First());
                if (dto != null)  // 仅当dto不为null时才添加到列表
                    list.Add(dto);
                return list;
            }

            //一笔订单多个item
            foreach(var order in tradeInfo.Data.Orders.OrderBy(o=>o.Oid))
            {
                var orderDTO =ToExternalOrderDTO(tradeInfo, tradeInfo.Data.Orders.First());

                if (orderDTO == null)
                    continue;

                // 仅当dto不为null时才添加到列表
                list.Add(orderDTO);

                //直接以订单编号 Tid-1  Tid-2 来表示
                orderDTO.Tid += "-" + list.Count;
            }

            return list;
        }

        private static ExternalOrderDTO? ToExternalOrderDTO(TradeInfoDTO tradeInfo, Order order)
        {
            try { 
          
                var dto = new ExternalOrderDTO();
                string targetProxy = RedisHelper.HGet(RedisKeys.SkuId2Proxy, order.SkuId);

                if (!EnumOperator.TryParseIgnoringCaseAndSpaces(targetProxy, out ETargetProxy eTargetProxy))
                    return null;

                dto.TargetProxy = eTargetProxy;

                dto.CreateTime = DateTime.Parse(tradeInfo.Data.Created);
                dto.UpdateTime = dto.CreateTime;
                dto.PayAmount = decimal.Parse(order.DivideOrderFee);

                dto.FromPlatform = tradeInfo.Data.Platform;
                dto.Tid = tradeInfo.Data.Tid.ToString();
                dto.Status = tradeInfo.Data.Status;

                dto.SellerNick = tradeInfo.Data.SellerNick;
                //tradeinfo里没有这个
                //dto.SellerOpenUid = tradeInfo.Data.SellerOpenUid;
                dto.BuyerNick = tradeInfo.Data.BuyerNick;
                dto.BuyerOpenUid = tradeInfo.Data.BuyerOpenUid;

                dto.Order = order;

                return dto;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
           
        }

        public static (bool,string) Validate(this CouponDTO dto)
        {

            if (string.IsNullOrEmpty(dto.Coupon))
                return (false, "Coupon is required");

            if (string.IsNullOrEmpty(dto.ExternalOrderFromPlatform))
                return (false, "ExternalOrderFromPlatform is required");
           
            if (string.IsNullOrEmpty(dto.ExternalOrderTid))
                return (false, "ExternalOrderTid is required");

            if (dto.AvailableBalance <= 0)
                return (false, "AvailableBalance  must be greater than 0");

            if (dto.Payment <= 0)
                return (false, "PayAmount must be greater than 0");

           
            return (true, string.Empty);
        }
    }
}
