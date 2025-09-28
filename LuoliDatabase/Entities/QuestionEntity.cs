using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///问题信息表
    ///</summary>
    [SugarTable("questions")]
    public partial class QuestionEntity
    {
           public QuestionEntity(){


           }
           /// <summary>
           /// Desc:问题主键ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:问题标题
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string title {get;set;}

           /// <summary>
           /// Desc:问题内容
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string content {get;set;}

           /// <summary>
           /// Desc:问题相关图片路径数组
           /// Default:
           /// Nullable:False
           /// </summary>
           [SugarColumn(IsJson=true)]           
           public object img_paths {get;set;}

           /// <summary>
           /// Desc:是否删除（0：未删除；1：已删除）
           /// Default:true
           /// Nullable:False
           /// </summary>           
           public bool is_deleted {get;set;}

           /// <summary>
           /// Desc:记录创建时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:记录更新时间
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime update_time {get;set;}

    }
}
