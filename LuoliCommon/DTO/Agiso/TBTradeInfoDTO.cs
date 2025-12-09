using LuoliCommon.DTO.ExternalOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Agiso
{
    public class TBTradeInfoDTO
    {
        public bool IsSuccess { get; set; }
        public TradeData Data { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public object AllowRetry { get; set; }
        public string RequestId { get; set; }

    }

    public class TradeData
    {
        public string Platform { get; set; }
        public string PlatformUserId { get; set; }
        public Dictionary<string, object> ExtendedFields { get; set; }
        public long Tid { get; set; }
        public string TidStr { get; set; }
        public string Status { get; set; }
        public string SellerNick { get; set; }
        public string BuyerNick { get; set; }
        public string BuyerOpenUid { get; set; }
        public string Type { get; set; }
        public int Num { get; set; }
        public string TotalFee { get; set; }
        public string Payment { get; set; }
        public string PayTime { get; set; }
        public string PostFee { get; set; }
        public string Created { get; set; }
        public string TradeFrom { get; set; }
        public List<Order> Orders { get; set; }
        public int SellerFlag { get; set; }
    }

    //子订单
    public class Order
    {
        public long Oid { get; set; }
        public string OidStr { get; set; }
        public long NumIid { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public int Num { get; set; }
        public string TotalFee { get; set; }
        public string Payment { get; set; }
        public string PicPath { get; set; }
        public string SkuId { get; set; }
        public string SkuPropertiesName { get; set; }
        public string DivideOrderFee { get; set; }
    }
}
