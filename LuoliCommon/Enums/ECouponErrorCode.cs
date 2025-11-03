using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
    public enum ECouponErrorCode
    {
        [Description("初始状态")]
        Default = 0,


        #region 发货失败

        [Description("卡密重复，生成失败")]
        DuplicateCoupon = 100,
        [Description("调用阿奇索发货api失败了")]
        AgisoShipFailed,

        #endregion

        #region 消费失败

        [Description("Coupon已存在消费信息")]
        UsedCoupon = 200,

        [Description("代理下单校验信息失败，Coupon状态必须是Shipped")]
        CouponStatusNotMacth ,

        [Description("代理下单校验信息失败，Coupon 付款金额和可用金额不一致")]
        CouponPaymentNotEqualABalance,

        [Description("代理下单校验信息失败，订单收到过退款请求")]
        EOReceivedRefund,

        [Description("代理下单校验信息失败，消费信息的状态必须是Pulled")]
        CIStatusNotMacth,

        [Description("代理账号token过期")]
        TokenExpired,

        [Description("代理账号创建订单失败了")]
        CreateOrderFailed,

        [Description("代理账号下单金额超出coupon的可用金额")]
        CouponBalanceNotEnough,

        [Description("代理账号付款失败")]
        AffordOrderFailed,




        [Description("代理账号下单成功后，更新Coupon失败了")]
        UpdateCouponFailed,
        [Description("代理账号下单成功后，更新消费信息失败了")]
        UpdateCIFailed,
        #endregion

    }
}
