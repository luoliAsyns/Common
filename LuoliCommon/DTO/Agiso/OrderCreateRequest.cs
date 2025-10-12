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
        /// <summary>
        /// 用户ID（关联users表id）
        /// </summary>
        [Required(ErrorMessage = "下单用户ID不能为空")]
        public long user_id { get; set; }

        /// <summary>
        /// 品牌名，确定创建什么订单
        /// </summary>
        [Required(ErrorMessage = "品牌名不能为空")]
        public string brand_name { get; set; }

        /// <summary>
        /// 平台代码，确认订单来源
        /// </summary>
        [Required(ErrorMessage = "平台代码不能为空")]
        public byte platform_code { get; set; }

        /// <summary>
        /// 平台订单号
        /// </summary>
        [Required(ErrorMessage = "平台订单号不能为空")]
        public string platform_order_no { get; set; }


        /// <summary>
        /// 平台订单号
        /// </summary>
        [Required(ErrorMessage = "充值码不能为空")]
        [MaxLength(32, ErrorMessage = "充值码要求是32位")]
        [MinLength(32, ErrorMessage = "充值码要求是32位")]
        public string consume_coupon { get; set; }



        /// <summary>
        /// 订单总金额（单位：元）
        /// </summary>
        [Required(ErrorMessage = "总金额不能为空")]
        [Range(0.01, 999.99, ErrorMessage = "总金额必须在0.01-999.99之间")]
        public decimal pay_amount { get; set; }



        /// <summary>
        /// 收货人姓名
        /// </summary>
        [MaxLength(32, ErrorMessage = "收货人姓名不能超过32个字符")]
        public string receiver_name { get; set; }

        /// <summary>
        /// 收货人手机号
        /// </summary>
        [MaxLength(20, ErrorMessage = "手机号不能超过20个字符")]
        [RegularExpression(@"^1[3-9]\d{9}$|^\+[0-9]{1,3}\d{1,14}$", ErrorMessage = "手机号格式不正确")]
        public string receiver_phone { get; set; }

        /// <summary>
        /// 收货地址（省/市/区/详细地址）
        /// </summary>
        [MaxLength(512, ErrorMessage = "收货地址不能超过512个字符")]
        public string receiver_address { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [MaxLength(512, ErrorMessage = "订单备注不能超过512个字符")]
        public string remark { get; set; }

    }
}
