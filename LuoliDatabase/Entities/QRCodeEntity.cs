using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("qrcode")]
    public partial class QRCodeEntity
    {
           public QRCodeEntity(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string MD5Code {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int QueryCount {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime FirstQueryDate {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime InsertDate {get;set;}

    }
}
