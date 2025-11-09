using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Grpc.Core.Metadata;

namespace LuoliCommon.DTO.Agiso
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
            //一笔多spu sku 订单
            //TargetProxy以第一个sku为准
            dto.TargetProxy = getProxy(tradeInfo.Data.Orders.First());

            dto.CreateTime = DateTime.Parse(tradeInfo.Data.Created);
            dto.UpdateTime = dto.CreateTime;

            //一笔多spu sku 订单
            //金额以在目标sku范围内的为准
            //例如本后台只卖茶颜，那订单中的星巴克金额就不应该计入
            dto.PayAmount = tradeInfo.Data.Orders.Where(order=> getProxy(order) != ETargetProxy.Default).Sum(o=> decimal.Parse(o.TotalFee));

            dto.FromPlatform = tradeInfo.Data.Platform;
            dto.Tid = tradeInfo.Data.Tid.ToString();
            dto.Status = tradeInfo.Data.Status switch
            {
                "WAIT_SELLER_SEND_GOODS" => EExternalOrderStatus.Pulled,

                "TRADE_CLOSED" => EExternalOrderStatus.Refunding,
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
