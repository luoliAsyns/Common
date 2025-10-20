using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///外部订单表
    ///</summary>
    [SugarTable("external_order")]
    public partial class ExternalOrderEntity
    {
           public ExternalOrderEntity(){


           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:目标代理： 例如sexytea, M stand
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string target_proxy {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:平台代码：
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string from_platform {get;set;}

           /// <summary>
           /// Desc:订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string tid {get;set;}

           /// <summary>
           /// Desc:卖家昵称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string seller_nick {get;set;}

           /// <summary>
           /// Desc:卖家id  也就是店铺id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string seller_open_uid {get;set;}

           /// <summary>
           /// Desc:买家昵称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string buyer_nick {get;set;}

           /// <summary>
           /// Desc:买家id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string buyer_open_uid {get;set;}

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
           /// Desc: EExternalOrderStatus 订单状态
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
