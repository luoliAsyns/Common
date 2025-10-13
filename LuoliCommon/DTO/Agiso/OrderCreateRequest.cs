using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Agiso
{
    public class OrderCreateRequest
    {
        //这4个从QueryString里取
        public long Timestamp { get; set; }
        public long Aopic { get; set; }
        public string Sign { get; set; }
        public string FromPlatform { get; set; }


      
        [Required(ErrorMessage = "订单号")]
        public long Tid { get; set; }                // 订单ID
        [Required]
        public string SellerOpenUid { get; set; }    // 卖家OpenUid

        [Required(ErrorMessage = "订单状态不能为空")]
        [RegularExpression(@"^WAIT_SELLER_SEND_GOODS$", ErrorMessage = "状态必须为WAIT_SELLER_SEND_GOODS")]
        public string Status { get; set; }           // 订单状态
        public string SellerNick { get; set; }       // 卖家昵称
        public string BuyerNick { get; set; }        // 买家昵称
        [Required]
        public string BuyerOpenUid { get; set; }     // 买家OpenUid

        [Required(ErrorMessage = "支付金额不能为空")]
        [Range(0.01, 200.00, ErrorMessage = "总金额必须在0.01-200.00之间")]
        public decimal Payment { get; set; }          // 支付金额
        public string Type { get; set; }             // 交易类型

    }
}
