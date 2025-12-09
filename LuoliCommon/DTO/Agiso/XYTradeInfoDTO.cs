using LuoliCommon.DTO.ExternalOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Agiso
{
    public class XYTradeInfoDTO
    {
        public bool IsSuccess { get; set; }
        public XYTradeData Data { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
        public object AllowRetry { get; set; }
        public string RequestId { get; set; }

    }

    public class XYTradeData
    {
        public long biz_order_id { get; set; }
        public int buy_amount { get; set; }
        public string buyer_nick { get; set; }
        public string encryption_buyer_id { get; set; }


        public long create_time { get; set; }
        public long end_time { get; set; }
        public XYItem item { get; set; }

        public int order_status { get; set; }
        public long pay_time { get; set; }
        public decimal payment { get; set; } //单位是分
        public decimal post_fee { get; set; }
        public string seller_nick { get; set; }
        public long ship_time { get; set; }
        public string sku { get; set; }
    }

    //子订单
    public class XYItem
    {
        public long item_id { get; set; }
        public string pic_url { get; set; }
        public decimal price { get; set; }  //单位是分
        public string title { get; set; }
        public string outer_id_sku { get; set; }
        public string outer_id_spu { get; set; }
    }
}
