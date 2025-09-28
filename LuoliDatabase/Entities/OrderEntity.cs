using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace LuoliDatabase.Entities
{
    ///<summary>
    ///订单表（存储代下单业务的订单主信息）
    ///</summary>
    [SugarTable("orders")]
    public partial class OrderEntity
    {
           public OrderEntity(){


           }
           /// <summary>
           /// Desc:订单ID（主键）
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long id {get;set;}

           /// <summary>
           /// Desc:自有订单编号（业务唯一标识，如: 20240912153000001）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string order_no {get;set;}

           /// <summary>
           /// Desc:品牌名， 如 茶颜悦色
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string brand_name {get;set;}

           /// <summary>
           /// Desc:电商平台编码（如：0：淘宝）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public byte platform_code {get;set;}

           /// <summary>
           /// Desc:平台订单编号（电商平台返回的订单号）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string platform_order_no {get;set;}

           /// <summary>
           /// Desc:下单人  luoli （关联users表id）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long user_id {get;set;}

           /// <summary>
           /// Desc:实付金额（单位：元，扣除优惠后）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal pay_amount {get;set;}

           /// <summary>
           /// Desc:订单状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public byte order_status {get;set;}

           /// <summary>
           /// Desc:平台订单状态（存储电商平台返回的原始状态）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string platform_order_status {get;set;}

           /// <summary>
           /// Desc:支付方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? pay_type {get;set;}

           /// <summary>
           /// Desc:充值码  淘宝/闲鱼平台使用
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string consume_coupon {get;set;}

           /// <summary>
           /// Desc:充值码状态 0:未使用 1：已使用 2:被取消
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte? consume_coupon_status {get;set;}

           /// <summary>
           /// Desc:客户下单时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? consume_time {get;set;}

           /// <summary>
           /// Desc:品牌订单号  例如茶颜悦色平台的订单号，用于取消订单等
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? consume_order_no {get;set;}

           /// <summary>
           /// Desc:自提/外卖...
           /// Default:
           /// Nullable:False
           /// </summary>           
           public byte consume_type {get;set;}

           /// <summary>
           /// Desc:客户下单店铺
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string consume_branch {get;set;}

           /// <summary>
           /// Desc:订单内容（JSON格式，如：{"items":[{"product":"A奶茶","spec":"加冰五分糖","price":15.00}]}）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string consume_content {get;set;}

           /// <summary>
           /// Desc:收货人姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string receiver_name {get;set;}

           /// <summary>
           /// Desc:收货人手机号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string receiver_phone {get;set;}

           /// <summary>
           /// Desc:收货地址（省/市/区/详细地址）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string receiver_address {get;set;}

           /// <summary>
           /// Desc:订单备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string remark {get;set;}

           /// <summary>
           /// Desc:乐观锁版本号（用于并发控制）
           /// Default:0
           /// Nullable:False
           /// </summary>           
           public int version {get;set;}

           /// <summary>
           /// Desc:创建时间（订单生成时间）
           /// Default:CURRENT_TIMESTAMP
           /// Nullable:False
           /// </summary>           
           public DateTime create_time {get;set;}

           /// <summary>
           /// Desc:更新时间（订单状态变更时间）
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

    }
}
