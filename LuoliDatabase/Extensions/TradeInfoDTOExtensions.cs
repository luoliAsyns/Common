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

        public static ExternalOrderDTO ToExternalOrderDTO(this TradeInfoDTO tradeInfo, Func<Order, ETargetProxy> getProxy)
        {
            if (tradeInfo is null)
                return null;

            //失败的response
            if (!tradeInfo.IsSuccess)
                return null;

            ExternalOrderDTO dto = new ExternalOrderDTO();


            dto.SubOrders = tradeInfo.Data.Orders;


            //string targetProxy = RedisHelper.HGet(RedisKeys.SkuId2Proxy, order.SkuId);

            //if (!EnumOperator.TryParseIgnoringCaseAndSpaces(targetProxy, out ETargetProxy eTargetProxy))
            //    return ETargetProxy.Default;
          
            dto.TargetProxy = getProxy(tradeInfo.Data.Orders.First());

            dto.CreateTime = DateTime.Parse(tradeInfo.Data.Created);
            dto.UpdateTime = dto.CreateTime;
            dto.PayAmount = tradeInfo.Data.Orders.Sum(o=> decimal.Parse(o.DivideOrderFee));

            dto.FromPlatform = tradeInfo.Data.Platform;
            dto.Tid = tradeInfo.Data.Tid.ToString();
            dto.Status = tradeInfo.Data.Status switch {
                "WAIT_SELLER_SEND_GOODS" => EExternalOrderStatus.Pulled,
                //这里理论上只会收到付款和退款两种状态的订单
                //"WAIT_BUYER_CONFIRM_GOODS" =>  EExternalOrderStatus.Shipped,
                _ => throw new Exception($"不支持的订单状态:{tradeInfo.Data.Status}")
            };

            dto.SellerNick = tradeInfo.Data.SellerNick;
            //tradeinfo里没有这个
            //dto.SellerOpenUid = tradeInfo.Data.SellerOpenUid;
            dto.BuyerNick = tradeInfo.Data.BuyerNick;
            dto.BuyerOpenUid = tradeInfo.Data.BuyerOpenUid;

            return dto;
        }

    }
}
