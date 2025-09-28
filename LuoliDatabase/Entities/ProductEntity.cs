using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///商品信息表
    ///</summary>
    [SugarTable("products")]
    public partial class ProductEntity
    {
           public ProductEntity(){


           }
           /// <summary>
           /// Desc:商品主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:商品标题
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string title {get;set;}

           /// <summary>
           /// Desc:商品价格（精确到分）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal price {get;set;}

           /// <summary>
           /// Desc:商品图片路径
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string img_path {get;set;}

           /// <summary>
           /// Desc:商品特性
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string features {get;set;}

           /// <summary>
           /// Desc:商品链接
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string link {get;set;}

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
           /// Default:true
           /// Nullable:False
           /// </summary>           
           public bool is_deleted {get;set;}

    }
}
