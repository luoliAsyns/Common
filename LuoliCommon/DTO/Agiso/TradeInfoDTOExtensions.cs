using LuoliCommon.DTO.Admin;
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

        public static ExternalOrderDTO ToExternalOrderDTO(this TradeInfoDTO tradeInfo, Func<Order, SkuIdMapItem> getSkuIdMapItem)
        {
            if (tradeInfo is null)
                return null;

            //失败的response
            if (!tradeInfo.IsSuccess)
                return null;




            var item = getSkuIdMapItem(tradeInfo.Data.Orders.First());

            if(item is null)
                return null;

            ExternalOrderDTO dto = new ExternalOrderDTO();

            dto.SubOrders = tradeInfo.Data.Orders;

            //一笔多spu sku 订单
            //TargetProxy以第一个sku为准
            if (EnumHandler.TryParseIgnoringCaseAndSpaces(item.TargetProxy, out ETargetProxy eTargetProxy))
                dto.TargetProxy = eTargetProxy;
            else
                dto.TargetProxy = ETargetProxy.Default;

            dto.CreateTime = DateTime.Parse(tradeInfo.Data.Created);
            dto.UpdateTime = dto.CreateTime;

            //一笔多spu sku 订单
            //金额以在目标sku范围内的为准
            //例如本后台只卖茶颜，那订单中的星巴克金额就不应该计入
            dto.PayAmount = tradeInfo.Data.Orders.Where(order=> !(getSkuIdMapItem(order) is null)).Sum(o=> decimal.Parse(o.TotalFee));

            //授信额度从redis里的sku映射表里取
            dto.CreditLimit = tradeInfo.Data.Orders.Sum(order => {
                var skuItem = getSkuIdMapItem(order);
                if (skuItem is null) 
                    return 0;
                return skuItem.CreditLimit;
            });


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
