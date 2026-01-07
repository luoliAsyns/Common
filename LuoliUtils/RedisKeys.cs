namespace LuoliUtils
{
    public static class RedisKeys
    {

        /// <summary>
        /// hashtable
        /// </summary>
        public const string SkuId2Proxy = "skuid2proxy";
        public const string AgisoAccessToken = "agiso_access_token";


        //用于去重
        public const string ReceivedExternalOrder = "received.external.orders";
        public const string NotUsedCoupons = "notused.coupons";

        public const string PlaceOrderButtonEnable = "place.order.button.enable";//hset 多平台


        //Sexytea相关
        public const string SexyteaBranchId2City = "sexytea.branchid2city";
        public const string SexyteaTokenAccount = "sexytea.tokens"; //hset 多账号
        public const string SexyteaBannedBranchId = "sexytea.banned.branchids";



        public const string NotifyUsers = "notify.users";

        //publish channels
        public const string Pub_RefreshShipStatus="refresh.ship.status";
        public const string Pub_RefreshPlaceOrderStatus = "refresh.placeorder.status";

        //for Prometheus
        public const string Prom_ReceivedOrders = "prom.external-order.pull";
        public const string Prom_ReceivedRefund = "prom.external-order.refund";
        public const string Prom_CouponsGenerated = "prom.coupon.generated";
        public const string Prom_Shipped = "prom.shipbot.shipped";
        public const string Prom_ShipFailed = "prom.shipbot.failed";
        public const string Prom_ReceivedConsumeInfo = "prom.consume-info.received";
        public const string Prom_InsertedConsumeInfo = "prom.consume-info.inserted";
        public const string Prom_PlacedOrders = "prom.placeorderbot.placed";
        public const string Prom_PlacedOrdersFailed = "prom.placeorderbot.failed";

        //for cache store sec
        public const string CICacheStoreSec = "ci.cache.store.sec";
        public const string EOCacheStoreSec = "eo.cache.store.sec";
        public const string CouponCacheStoreSec = "coupon.cache.store.sec";
        public const string ProxyOrderCacheStoreSec = "proxyorder.cache.store.sec";
    }
}
