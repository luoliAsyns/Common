using System;
using System.Linq;
using System.Text;

namespace LuoliCommon.DTO
{
    ///<summary>
    ///订单表（存储代下单业务的订单主信息）
    ///</summary>
    public  class ExternalOrderDTO
    {
        public ExternalOrderDTO()
        {


        }
        /// <summary>
        /// Desc:订单ID（主键）
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long Id { get; set; }

        /// <summary>
        /// Desc:自有订单编号（业务唯一标识，如: 20240912153000001）
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string OrderNo { get; set; } = null!;

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BrandName { get; set; } = null!;

        /// <summary>
        /// Desc:电商平台编码（如：1：淘宝）
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Platform { get; set; }

        /// <summary>
        /// Desc:平台订单编号（电商平台返回的订单号）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? OrderNo_Platform { get; set; }

        /// <summary>
        /// Desc:下单人  luoli （关联users表id）
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long UserId { get; set; }

        /// <summary>
        /// Desc:实付金额（单位：元，扣除优惠后）
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal PayAmount { get; set; }

        /// <summary>
        /// Desc:订单状态
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Status { get; set; }

        /// <summary>
        /// Desc:平台订单状态（存储电商平台返回的原始状态）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? Status_Platform { get; set; }

        /// <summary>
        /// Desc:支付方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? PayType { get; set; }


        /// <summary>
        /// Desc:订单备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string? remark { get; set; }

        /// <summary>
        /// Desc:创建时间（订单生成时间）
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime create_time { get; set; }

        /// <summary>
        /// Desc:更新时间（订单状态变更时间）
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime update_time { get; set; }

        /// <summary>
        /// Desc:逻辑删除：0-正常，1-已删除
        /// Default:true
        /// Nullable:False
        /// </summary>           
        public bool is_deleted { get; set; }

    }
}
