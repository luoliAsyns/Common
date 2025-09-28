using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///合作伙伴信息表
    ///</summary>
    [SugarTable("partners")]
    public partial class PartnerEntity
    {
           public PartnerEntity(){


           }
           /// <summary>
           /// Desc:主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:合作伙伴名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string name {get;set;}

           /// <summary>
           /// Desc:店铺链接
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string store_link {get;set;}

           /// <summary>
           /// Desc:LOGO路径
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string logo_path {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string comment {get;set;}

           /// <summary>
           /// Desc:合作伙伴类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string platform {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:更新时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime update_time {get;set;}

           /// <summary>
           /// Desc:
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public float rate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:true
           /// Nullable:False
           /// </summary>           
           public bool is_deleted {get;set;}

    }
}
