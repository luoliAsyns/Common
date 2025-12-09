using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Agiso
{ 
     /// <summary>
     /// 闲鱼订单创建
     /// </summary>
    public class XYOrderCreateRequest
    {
        //这4个从QueryString里取
        public long Timestamp { get; set; }
        public long Aopic { get; set; }
        public string Sign { get; set; }
        public string FromPlatform { get; set; }


      
        [Required(ErrorMessage = "订单号不能为空")]
        public long biz_order_id { get; set; }         

        [Required(ErrorMessage = "skuid不能为空")]
        public string item_id { get; set; }           

        [Required(ErrorMessage = "卖家seller_id不能为空")]
        public long seller_id { get; set; }           // 卖家昵称

        public string MsgTypeDes { get; set; }       // 描述
        public int order_status { get; set; }        // 订单状态  已付款是2
       

    }
}
