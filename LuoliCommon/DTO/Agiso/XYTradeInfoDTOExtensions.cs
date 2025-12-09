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
    public static class XYTradeInfoDTOExtensions
    {

        public static ExternalOrderDTO ToExternalOrderDTO(this XYTradeInfoDTO tradeInfo, Func<XYTradeData, SkuIdMapItem> getSkuIdMapItem)
        {
            if (tradeInfo is null)
                return null;

            //失败的response
            if (!tradeInfo.IsSuccess)
                return null;




            var item = getSkuIdMapItem(tradeInfo.Data);

            if(item is null)
                return null;

            ExternalOrderDTO dto = new ExternalOrderDTO();

            var order = new Order()
            {
                Oid = tradeInfo.Data.biz_order_id,
                OidStr = tradeInfo.Data.biz_order_id.ToString(),
                Title = tradeInfo.Data.item.title,
                Price = (tradeInfo.Data.item.price / 100.0m).ToString(),
                Num = tradeInfo.Data.buy_amount,
                
                Payment = (tradeInfo.Data.payment / 100.0m).ToString(),
                PicPath = tradeInfo.Data.item.pic_url,
                SkuId = tradeInfo.Data.sku,
                DivideOrderFee = (tradeInfo.Data.payment / 100.0m).ToString(),
            };
            dto.SubOrders = new List<Order>() { order} ;

            //一笔多spu sku 订单
            //TargetProxy以第一个sku为准
            if (EnumHandler.TryParseIgnoringCaseAndSpaces(item.TargetProxy, out ETargetProxy eTargetProxy))
                dto.TargetProxy = eTargetProxy;
            else
                dto.TargetProxy = ETargetProxy.Default;

            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dto.CreateTime = epoch.AddMilliseconds(tradeInfo.Data.create_time).ToLocalTime();

            dto.UpdateTime = dto.CreateTime;

            //闲鱼只能单个购买，没有购物车子订单
            dto.PayAmount = dto.SubOrders.Sum(order=> decimal.Parse( order.Payment ));

            //授信额度从redis里的sku映射表里取
            var skuItem = getSkuIdMapItem(tradeInfo.Data);
            if (skuItem is null)
                dto.CreditLimit = 0;
            else
                dto.CreditLimit = skuItem.CreditLimit * tradeInfo.Data.buy_amount;


            dto.FromPlatform = "XIANYU";
            dto.Tid = tradeInfo.Data.biz_order_id.ToString();
            dto.Status = tradeInfo.Data.order_status switch
            {
                2 => EExternalOrderStatus.Pulled, //已付款
                3 => EExternalOrderStatus.Pulled, //已发货  因为闲鱼可能会自动发货

                _ => throw new Exception($"不支持的订单状态:{tradeInfo.Data.order_status}")
            };


            dto.SellerNick = tradeInfo.Data.seller_nick;
            //tradeinfo里没有这个
            //dto.SellerOpenUid = tradeInfo.Data.SellerOpenUid;
            dto.BuyerNick = tradeInfo.Data.buyer_nick;
            dto.BuyerOpenUid = tradeInfo.Data.encryption_buyer_id;

            return dto;
        }

    }
}
