using LuoliCommon.Enums;

namespace LuoliCommon.DTO.Coupon;

public class CouponDTO
{
    /// <summary>
    /// Desc:创建时间
    /// Default:CURRENT_TIMESTAMP
    /// Nullable:False
    /// </summary>           
    public DateTime CreateTime {get;set;}

    /// <summary>
    /// Desc:卡密32位哈希值
    /// Default:
    /// Nullable:False
    /// </summary>           
    public string Coupon {get;set;}

    /// <summary>
    /// Desc:对应的外部订单的平台
    /// Default:
    /// Nullable:False
    /// </summary>           
    public string ExternalOrderFromPlatform {get;set;}

    /// <summary>
    /// Desc:对应的外部订单id
    /// Default:
    /// Nullable:False
    /// </summary>           
    public string ExternalOrderTid {get;set;}

    public string ProxyOrderId { get; set; }
    public string ProxyOpenId { get; set; }
    public decimal ProxyPayment { get; set; }

    /// <summary>
    /// Desc:实付金额，和external order 中的payment一致
    /// Default:
    /// Nullable:False
    /// </summary>           
    public decimal Payment {get;set;}
    public decimal CreditLimit { get; set; }
    /// <summary>
    /// Desc:可用余额
    /// Default:
    /// Nullable:False
    /// </summary>           
    public decimal AvailableBalance {get;set;}

    /// <summary>
    /// Desc:订单状态：ECouponStatus
    /// Default:
    /// Nullable:False
    /// </summary>           
    public ECouponStatus Status {get;set;}
    public ECouponErrorCode ErrorCode { get; set; }

    public string ShortUrl { get; set; }

    public string RawUrl { get; set; }

    /// <summary>
    /// Desc:更新时间
    /// Default:CURRENT_TIMESTAMP
    /// Nullable:False
    /// </summary>           
    public DateTime UpdateTime {get;set;}
}