using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Enums
{
   public enum EEvent
    {


        [Description("收到付款推送")]
        Received_Paid_EO = 0,
        [Description("收到退款推送")]
        Received_Refund_EO,

        [Description("收到消费信息")]
        Received_CI =10,
        [Description("收到手动作废卡密")]
        Receive_Manual_Cancel_Coupon,


        [Description("发送卡密")]
        Coupon_Shipment=20,
        [Description("发送卡密失败")]
        Coupon_ShipFailed,

        [Description("下单成功")]
        Placed,
        [Description("下单失败")]
        PlaceFailed,

        [Description("代理订单退款")]
        ProxyRefund,

        [Description("代理订单查询")]
        ProxyQuery,


        [Description("收到手动恢复卡密")]
        Receive_Manual_Recover_Coupon = 100,


    }
}
