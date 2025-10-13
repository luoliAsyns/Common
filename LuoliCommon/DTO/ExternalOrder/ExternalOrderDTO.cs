using LuoliCommon.Enums;
using ProtoBuf;
using System;
using System.Linq;
using System.Text;

namespace LuoliCommon.DTO.ExternalOrder
{
    /// <summary>
    /// 外部订单DTO，用于与agiso系统交互
    /// </summary>
    public class ExternalOrderDTO
    {
        public ExternalOrderDTO()
        {
        }

        /// <summary>
        /// 平台订单编号（电商平台返回的订单号）
        /// 注意：使用string类型以支持超过19位的编号
        /// </summary>
        public string Tid { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 卖家昵称
        /// </summary>
        public string SellerNick { get; set; }

        /// <summary>
        /// 卖家ID
        /// </summary>
        public string SellerOpenUid { get; set; }

        /// <summary>
        /// 买家昵称
        /// </summary>
        public string BuyerNick { get; set; }

        /// <summary>
        /// 买家ID
        /// </summary>
        public string BuyerOpenUid { get; set; }

        /// <summary>
        /// 电商平台编码
        /// 参数值：TbAcs,TbAlds,TbArs,Print,Acpr,PddAlds,AldsIdle,AldsJd,AldsDoudian,AldsKwai,AldsYouzan,AldsWeidian,AldsWxVideoShop,AldsXhs
        /// </summary>
        public string FromPlatform { get; set; }


        public ETargetProxy TargetProxy { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 创建时间（订单生成时间）
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建时间（订单生成时间）
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 订单项列表
        /// </summary>
        public List<ExternalOrderItem> ExternalOrderItems { get; set; } = new List<ExternalOrderItem>();
    }

    
}
