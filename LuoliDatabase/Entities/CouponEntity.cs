using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///卡密表
    ///</summary>
    [SugarTable("coupon")]
    public partial class CouponEntity
    {
           public CouponEntity(){


           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:卡密32位哈希值
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string coupon {get;set;}

           /// <summary>
           /// Desc:对应的外部订单的平台
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string external_order_from_platform {get;set;}

           /// <summary>
           /// Desc:对应的外部订单id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string external_order_tid {get;set;}

           public string proxy_order_id { get; set; }

        /// <summary>
        /// Desc:实付金额，和external order 中的payment一致
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal payment {get;set;}

           /// <summary>
           /// Desc:可用余额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal available_balance {get;set;}

           /// <summary>
           /// Desc:订单状态：ECouponStatus
           /// Default:
           /// Nullable:False
           /// </summary>           
           public byte status {get;set;}


        public string short_url { get; set; }

        public string raw_url { get; set; }

        /// <summary>
        /// Desc:更新时间
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime update_time {get;set;}

           /// <summary>
           /// Desc:是否删除：0-未删除，1-已删除
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public byte is_deleted {get;set;}

    }
}
