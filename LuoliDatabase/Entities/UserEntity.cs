using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///用户表（存储系统所有用户的基础信息）
    ///</summary>
    [SugarTable("users")]
    public partial class UserEntity
    {
           public UserEntity(){


           }
           /// <summary>
           /// Desc:用户ID（主键）
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:用户名（登录账号，唯一）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string username {get;set;}

           /// <summary>
           /// Desc:密码（加密存储，如BCrypt哈希）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string password {get;set;}

           /// <summary>
           /// Desc:手机号（唯一，用于登录/验证码）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string phone {get;set;}

           /// <summary>
           /// Desc:邮箱（可选，用于登录/找回密码）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string email {get;set;}

           /// <summary>
           /// Desc:真实姓名（用于身份验证）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string real_name {get;set;}

           /// <summary>
           /// Desc:用户状态：0-禁用，1-正常，2-待审核，3-已注销
           /// Default:true
           /// Nullable:False
           /// </summary>           
           public bool status {get;set;}

           /// <summary>
           /// Desc:性别：0-未知，1-男，2-女
           /// Default:true
           /// Nullable:False
           /// </summary>           
           public bool gender {get;set;}

           /// <summary>
           /// Desc:生日
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? birthday {get;set;}

           /// <summary>
           /// Desc:所属部门ID（关联dept表id字段）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public long? dept_id {get;set;}

           /// <summary>
           /// Desc:最后登录时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? last_login_time {get;set;}

           /// <summary>
           /// Desc:最后登录IP地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string last_login_ip {get;set;}

           /// <summary>
           /// Desc:创建时间（用户注册时间）
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:更新时间（信息修改时间）
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime update_time {get;set;}

           /// <summary>
           /// Desc:逻辑删除：0-正常，1-已删除
           /// Default:true
           /// Nullable:False
           /// </summary>           
           public bool is_deleted {get;set;}

           /// <summary>
           /// Desc:备注（如用户来源、特殊说明）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string remark {get;set;}

    }
}
