using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///茶颜悦色消费信息表
    ///</summary>
    [SugarTable("sexytea_consume_info")]
    public partial class ConsumeInfoEntity
    {
           public ConsumeInfoEntity(){


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
           /// Desc:目标代理： 例如sexytea, M stand
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string target_proxy {get;set;}

           /// <summary>
           /// Desc:订单内容（JSON格式）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string goods {get;set;}

           /// <summary>
           /// Desc:这里只能是  sexytea_consume_info
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string goods_type {get;set;}

           /// <summary>
           /// Desc:miniapp/web
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string source {get;set;}

            public string last_name { get; set; }
        public string remark { get; set; }
        public int status { get; set; }

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
