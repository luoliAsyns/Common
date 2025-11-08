using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.DTO.Agiso
{
    public class OrderRefundRequest
    {
        //这4个从QueryString里取
        public long Timestamp { get; set; }
        public long Aopic { get; set; }
        public string Sign { get; set; }


        public string Platform { get; set; }
      
        [Required(ErrorMessage = "退款Id不能为空")]
        public long RefundId { get; set; }

        [Required(ErrorMessage = "订单编号Tid不能为空")]
        public long Tid { get; set; }

        [Required(ErrorMessage = "子订单编号Oid不能为空")]
        public long Oid { get; set; }

        [Required(ErrorMessage = "卖家OpenId不能为空")]
        public string SellerOpenUid { get; set; }    // 卖家OpenUid


        [Required(ErrorMessage = "退款阶段不能为空")]
        public string RefundPhase { get; set; }
        [Required(ErrorMessage = "退款类型不能为空")]
        public string BillType { get; set; }

        public string SellerNick { get; set; }       // 卖家昵称
        public string BuyerNick { get; set; }        // 买家昵称
        [Required]
        public string BuyerOpenUid { get; set; }     // 买家OpenUid

        [Required(ErrorMessage = "退款金额不能为空")]
        [Range(0.01, 200.00, ErrorMessage = "总金额必须在0.01-200.00之间")]
        public decimal RefundFee { get; set; }     

    }
}
