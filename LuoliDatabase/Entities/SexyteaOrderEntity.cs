using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///茶颜悦色订单表
    ///</summary>
    [SugarTable("sexytea_order")]
    public partial class SexyteaOrderEntity
    {
           public SexyteaOrderEntity(){


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
           /// Desc:对应的外部订单id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long external_order_id {get;set;}

           /// <summary>
           /// Desc:订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string order_no {get;set;}

           /// <summary>
           /// Desc:代理店铺id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int branch_id {get;set;}

           /// <summary>
           /// Desc:订单内容（JSON格式）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string content {get;set;}

           /// <summary>
           /// Desc:实际支付金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal payment {get;set;}

           /// <summary>
           /// Desc:订单状态：EOrderStatus
           /// Default:
           /// Nullable:False
           /// </summary>           
           public byte status {get;set;}

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
