namespace LuoliUtils
{
    public static class RedisKeys
    {

        /// <summary>
        /// hashtable
        /// </summary>
        public static string SkuId2Proxy = "skuid2proxy";


        //用于去重
        public static string ReceivedExternalOrder = "received.external.orders";
        public static string NotUsedCoupons = "notused.coupons";
        

     
        public static string BranchId2City = "sexytea.branchid2city";
        public static string SexyteaTokenAccount = "sexytea.token";

        public static string WWMsgTemplate = "msg.template";


        //publish channels
        public static string Pub_RefreshShipStatus="refresh.ship.status";
        public static string Pub_RefreshPlaceOrderStatus = "refresh.placeorder.status";

        //for Prometheus
        public static string Prom_ReceivedOrders = "prom.external-order.pull";
        public static string Prom_ReceivedRefund = "prom.external-order.refund";
        public static string Prom_CouponsGenerated = "prom.coupon.generated";
        public static string Prom_Shipped = "prom.shipbot.shipped";
        public static string Prom_ShipFailed = "prom.shipbot.failed";
        public static string Prom_ReceivedConsumeInfo = "prom.consume-info.received";
        public static string Prom_InsertedConsumeInfo = "prom.consume-info.inserted";
        public static string Prom_PlacedOrders = "prom.placeorderbot.placed";
        public static string Prom_PlacedOrdersFailed = "prom.placeorderbot.failed";
    }
}
