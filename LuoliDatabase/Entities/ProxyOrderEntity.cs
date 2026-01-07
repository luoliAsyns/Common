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
    public partial class ProxyOrderEntity
    {
           public ProxyOrderEntity(){


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
           /// Desc:插入时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime insert_time {get;set;}

           /// <summary>
           /// Desc:买家id
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string buyer_open_uid {get;set;} = null!;

           /// <summary>
           /// Desc:这里只能是 sexytea
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string target_proxy {get;set;} = null!;

           /// <summary>
           /// Desc:店铺订单
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? proxy_order_id {get;set;}

           /// <summary>
           /// Desc:代理平台账号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string? proxy_open_id {get;set;}

           /// <summary>
           /// Desc:订单内容（JSON格式）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string order {get;set;} = null!;

           /// <summary>
           /// Desc:订单状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string order_status {get;set;} = null!;

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
