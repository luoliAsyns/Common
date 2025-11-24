using LuoliCommon.DTO.Agiso;
using LuoliCommon.DTO.ConsumeInfo.Sexytea;
using LuoliCommon.Logger;
using LuoliUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ThirdApis
{
    public  class SexyteaApis
    {
        //心跳
        private  string R1_Url_Heartbeat = "https://miniapp.sexytea2013.com/beat/push";

        //用户信息
        private  string R2_Url_UserInfo = "https://miniapp.sexytea2013.com/api/v2/user/info";

        //校验购物车产品
        private  string R3_Url_Validate = "https://miniapp.sexytea2013.com/api/v2/orderActivity/swapActivities";

        //订单计算
        private  string R4_Url_OrderCal = "https://miniapp.sexytea2013.com/api/order/calc";

        //创建订单
        private  string R5_Url_OrderCreate = "https://miniapp.sexytea2013.com/api/order/create";

        //支付订单
        private  string R6_Url_OrderPay = "https://miniapp.sexytea2013.com/api/common/pay/balanceAndWxPay";

        //因为有积点，这个可以算订单我需要付多少钱
        private  string R7_Url_OrderPayCal = "https://miniapp.sexytea2013.com/api/common/pay/queryMixPayPrepareInfo";

        //查询订单
        private string R8_Url_OrderDetail = "https://miniapp.sexytea2013.com/api/v5/order/detail";

        //订单退款
        private string R9_Url_OrderRefund = "https://miniapp.sexytea2013.com/api/mall/refundAll";


        private string W1_Url_Regions = "https://miniapp.sexytea2013.com/api/v2/branch/regions";
        private  string W2_Url_BranchIdsInRegion = "https://miniapp.sexytea2013.com/api/map/branch/queryALL";

        private readonly ILogger _logger;
        public SexyteaApis(ILogger logger)
        {
            _logger = logger;
        }


        public  async Task<bool> Heartbeat(Account account)
        {
            string body = $@"{{""openId"":""{account.OpenId}""}}";
            Dictionary<string, dynamic> header = new(4);
            header.Add("token", account.Token);
            header.Add("sign", "cdf5b9ac73fd490d82f34ecd302d3997");
            header.Add("nonce", "474276");

            var response = await  ApiCaller.PostAsync(R1_Url_Heartbeat, body, header);
            return response.StatusCode ==  System.Net.HttpStatusCode.OK;
        }


        /// <summary>
        /// 订单核算
        /// </summary>
        /// <param name="account"></param>
        /// <param name="branchId"></param>
        /// <param name="needToPack"> 前期只自提  无外卖  default is false</param>
        /// <returns></returns>
        public  async Task<(bool, decimal,string)> OrderCal(Account account, int branchId, List<OrderItem> orderItems, decimal totalAmount, bool needToPack =false )
        {
            Dictionary<string,dynamic> body = new();

            try
            {
                body.Add("branchId", branchId);
                body.Add("orderItems", orderItems);

                // 固定部分
                body.Add("platform", "APP_SELF_SERVICE");
                body.Add("useBalance", false);
                body.Add("needToPack", needToPack);

                Dictionary<string, dynamic> header = new(4);
                header.Add("token", account.Token);

                string bodyStr = JsonSerializer.Serialize(body);

                var response = await ApiCaller.PostAsync(R4_Url_OrderCal, bodyStr, header);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.Error($"SexyteaApis, OrderCal failed, StatusCode:[{response.StatusCode}]");
                    return (false, -1, "网络波动 OrderCal失败了");
                }

                // 解析 JSON 并直接获取根节点
                JsonDocument responseObj = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                int code = responseObj.RootElement.GetProperty("code").GetInt32();

                if (code != 200)
                {
                    string msg = responseObj.RootElement.GetProperty("msg").GetString();
                    _logger.Error($"SexyteaApis, OrderCal failed, responseObj.code:[{code}], msg:[{msg}]");
                    return (false, -1, msg);
                }

                return (true, responseObj.RootElement.GetProperty("data").GetProperty("finalAmount").GetDecimal(),string.Empty);
            }
            catch (Exception ex)
            {
                _logger.Error($"SexyteaApis, OrderCal failed");
                _logger.Error(ex.Message);
                return (false, -1, "未知异常，可以重试一下");
            }

        }

        public  async Task<(bool, string, decimal)> OrderCreate(Account account, int branchId, List<OrderItem> orderItems, string lastName, string comments, decimal creditLimit, int selectPoint, bool needToPack = false)
        {
            Dictionary<string, dynamic> body = new();

            try
            {
                body.Add("branchId", branchId);
                body.Add("orderItems", orderItems);
                body.Add("comments", comments);
                // 固定部分
                body.Add("platform", "APP_SELF_SERVICE");
                body.Add("serviceType", "RESTAURANT");
                body.Add("groupBuyNo", "");
                body.Add("selectPoint", selectPoint); // 1: 优先使用积点   0: 不使用积点
                body.Add("needToPack", needToPack);
                // 使用Newtonsoft.Json构建userTitle，避免手动拼接JSON
                body.Add("userTitle", new
                {
                    lastname = lastName,
                    appellation = "小主"
                });
                Dictionary<string, dynamic> header = new(4);
                header.Add("token", account.Token);

                string bodyStr = JsonSerializer.Serialize(body); 

                var response = await ApiCaller.PostAsync(R5_Url_OrderCreate, bodyStr, header);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.Error($"SexyteaApis, OrderCreate failed, StatusCode:[{response.StatusCode}]");
                    return (false, null, -1);
                }

                // 解析 JSON 并直接获取根节点
                JsonDocument responseObj = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                int code = responseObj.RootElement.GetProperty("code").GetInt32();
                string msg = responseObj.RootElement.GetProperty("msg").GetString();
               
                if (code != 200)
                {
                    _logger.Error($"SexyteaApis, OrderCreate failed, responseObj.code:[{code}], msg:[{msg}]");
                    return (false, msg, -1);
                }

                decimal finalAmount = responseObj.RootElement.GetProperty("data").GetProperty("finalAmount").GetDecimal();
                //积点抵扣的钱
                decimal discountAmount = responseObj.RootElement.GetProperty("data").GetProperty("discountAmount").GetDecimal();
                string orderNo = responseObj.RootElement.GetProperty("data").GetProperty("orderNo").GetString();

                if ((finalAmount + discountAmount - creditLimit) <0.02m)
                {
                    _logger.Info($"订单创建成功, orderNo:[{orderNo}], 订单金额:[{finalAmount + discountAmount}]");
                    return (true, orderNo, finalAmount + discountAmount);
                }

                _logger.Error($"SexyteaApis, OrderCreate failed, 会员余额支付:{finalAmount + discountAmount}，其中{discountAmount}是充值赠送, 卡密额度:{creditLimit}");
                return (false, "unknown exception", -1);
            }
            catch (Exception ex)
            {
                _logger.Error($"SexyteaApis, OrderCreate failed");
                _logger.Error(ex.Message);
                return (false, ex.Message, -1);
            }
        }

        public  async Task<bool> OrderPay(Account account, string orderNo, decimal payAmount)
        {
            Dictionary<string, dynamic> body = new();

            try
            {
                body.Add("orderNo", orderNo);
                body.Add("balancePayAmount", payAmount);
                body.Add("openId", account.OpenId);

                // 固定部分
                body.Add("orderType", "1");

                Dictionary<string, dynamic> header = new(4);
                header.Add("token", account.Token);

                string bodyStr = JsonSerializer.Serialize(body);


                var response = await ApiCaller.PostAsync(R6_Url_OrderPay, bodyStr, header);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.Error($"SexyteaApis, OrderPay failed, StatusCode:[{response.StatusCode}]");
                    return false;
                }

                // 解析 JSON 并直接获取根节点
                JsonDocument responseObj = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                int code = responseObj.RootElement.GetProperty("code").GetInt32();
                string msg = responseObj.RootElement.GetProperty("msg").GetString();

                if (code != 200)
                {
                    _logger.Error($"SexyteaApis, OrderPay failed, responseObj.code:[{code}], msg:[{msg}]");
                    return false;
                }
                bool ok = responseObj.RootElement.GetProperty("ok").GetBoolean();
                if (ok)
                {
                    _logger.Info($"订单付款成功, orderNo:[{orderNo}], 支付的会员余额:[{payAmount}]");
                    return true;
                }

                _logger.Error($"SexyteaApis, OrderPay failed, consume_order_no:{orderNo}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Error($"SexyteaApis, OrderPay failed, consume_order_no:{orderNo}");
                _logger.Error(ex.Message);
                return false;
            }
        }

        public  async Task<(bool, decimal, string)> OrderPayCal(Account account, string consumeOrderNo)
        {
            decimal need2Pay = -1m;
            bool result = false;
            string msg= string.Empty;
            try
            {

                StringBuilder sb = new StringBuilder();

                sb.Append(R7_Url_OrderPayCal);
                sb.Append($"?orderNo={consumeOrderNo}");
                sb.Append($"&orderType=1");
                sb.Append($"&token={account.Token}");


                var response = await ApiCaller.GetAsync(sb.ToString());

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    msg = $"HttpStatusCode: {response.StatusCode }";
                    _logger.Error($"SexyteaApis, OrderPayCal failed, StatusCode:[{response.StatusCode}]");
                    return (result, need2Pay, msg);
                }

                // 解析 JSON 并直接获取根节点
                JsonDocument responseObj = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                int code = responseObj.RootElement.GetProperty("code").GetInt32();
                msg = responseObj.RootElement.GetProperty("msg").GetString();

                if (code != 200)
                {
                    _logger.Error($"SexyteaApis, OrderPayCal failed, responseObj.code:[{code}], msg:[{msg}]");
                    return (result, need2Pay, msg);
                }
                bool ok = responseObj.RootElement.GetProperty("ok").GetBoolean();
                need2Pay = responseObj.RootElement.GetProperty("data").GetProperty("payAmount").GetDecimal();
                if (ok)
                {
                    result = true;
                    _logger.Info($"订单计算成功, orderNo:[{consumeOrderNo}], 计算需要支付会员余额:[{need2Pay}]");
                    return (result, need2Pay, msg);
                }

                _logger.Error($"SexyteaApis, OrderPayCal failed, consume_order_no:{consumeOrderNo}");
                return (result, need2Pay,"unknown exception");
            }
            catch (Exception ex)
            {
                _logger.Error($"SexyteaApis, OrderPayCal failed, consume_order_no:{consumeOrderNo}");
                _logger.Error(ex.Message);
                return (result, need2Pay , ex.Message);
            }
        }

        public async Task<(bool,  string)> OrderRefund(Account account, string orderNo)
        {
            bool result = false;
            string msg = string.Empty;
            try
            {
                Dictionary<string, dynamic> header = new(4);
                header.Add("token", account.Token);

                Dictionary<string, dynamic> body = new();

                body.Add("orderNo", orderNo);

                var response = await ApiCaller.PostAsync(R9_Url_OrderRefund, JsonSerializer.Serialize(body), header, isFormUrlEncode: true);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    msg = $"HttpStatusCode: {response.StatusCode}";
                    _logger.Error($"SexyteaApis, OrderRefund failed, StatusCode:[{response.StatusCode}]");
                    return (result, msg);
                }

                // 解析 JSON 并直接获取根节点
                JsonDocument responseObj = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                int code = responseObj.RootElement.GetProperty("code").GetInt32();
                msg = responseObj.RootElement.GetProperty("msg").GetString();

                return (code == 200, msg);
            }
            catch (Exception ex)
            {
                _logger.Error($"SexyteaApis, OrderRefund failed, sexytea orderNo:{orderNo}");
                _logger.Error(ex.Message);
                return (result, ex.Message);
            }
        }

        public async Task<string> UserInfo(Account account)
        {

            Dictionary<string, dynamic> header = new(4);
            header.Add("token", account.Token);

            var response = await ApiCaller.GetAsync(R2_Url_UserInfo, headers: header);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.Error($"SexyteaApis, UserInfo failed, StatusCode:[{response.StatusCode}]");
                return null;
            }
            return await response.Content.ReadAsStringAsync();

        }


        public  async Task<List<string>> GetRegions()
        {
            List<string> cities = new List<string>();

            try
            {

                var response = await ApiCaller.GetAsync(W1_Url_Regions);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.Error($"SexyteaApis, GetRegions failed, StatusCode:[{response.StatusCode}]");
                    return cities;
                }

                string str = await response.Content.ReadAsStringAsync();


                // 解析 JSON 并直接获取根节点
                using (JsonDocument doc = JsonDocument.Parse(str))
                {
                    JsonElement root = doc.RootElement;

                    // 1. 直接获取 code
                    int code = root.GetProperty("code").GetInt32();
                    if (code !=200)
                        return cities;


                    // 2. 直接获取 data 数组并遍历
                    foreach (JsonElement dataItem in root.GetProperty("data").EnumerateArray())
                    {
                        // 直接获取城市列表并遍历
                        foreach (JsonElement city in dataItem.GetProperty("branchRegionTreeDtoList").EnumerateArray())
                        {
                            // 直接获取城市名和编码
                            string cityName = city.GetProperty("label").GetString();
                            cities.Add(cityName);
                        }
                    }

                    return cities;
                }

                _logger.Error($"SexyteaApis, GetRegions failed");
                return cities;
            }
            catch (Exception ex)
            {
                _logger.Error($"SexyteaApis, GetRegions failed");
                _logger.Error(ex.Message);
                return cities;
            }
        }

        public  async Task<List<int>> GetBranchIdsInRegion(string region)
        {
            List<int> branchIds = new List<int>();

            try
            {
                Dictionary<string, dynamic> body = new();

                body.Add("city", region);
                body.Add("county", "");
                body.Add("lat", 10.0);
                body.Add("lng", 120.0);
                body.Add("name", "");
                body.Add("platform", "");
                body.Add("province", "");
                body.Add("type", "");
                body.Add("pageNum", 1);
                body.Add("pageSize", 10);
                body.Add("brandId", 100); //意思是茶颜悦色

                var response = await ApiCaller.PostAsync(W2_Url_BranchIdsInRegion, JsonSerializer.Serialize(body));

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    _logger.Error($"SexyteaApis, GetBranchIdsInRegion failed, StatusCode:[{response.StatusCode}]");
                    return branchIds;
                }

                string str = await response.Content.ReadAsStringAsync();


                // 解析 JSON 并直接获取根节点
                using (JsonDocument doc = JsonDocument.Parse(str))
                {
                    JsonElement root = doc.RootElement;

                    // 1. 直接获取 code
                    int code = root.GetProperty("code").GetInt32();
                    if (code != 200)
                        return branchIds;


                    // 2. 直接获取 data 数组并遍历
                    foreach (JsonElement dataItem in root.GetProperty("data").EnumerateArray())
                    {
                        // 直接获取店铺id  branchId
                        int branchId = dataItem.GetProperty("fId").GetInt32();
                        branchIds.Add(branchId);
                    }

                    return branchIds;
                }

                _logger.Error($"SexyteaApis, GetBranchIdsInRegion failed");
                return branchIds;
            }
            catch (Exception ex)
            {
                _logger.Error($"SexyteaApis, GetBranchIdsInRegion failed");
                _logger.Error(ex.Message);
                return branchIds;
            }
        }

        public async Task<dynamic> GetOrderInfo(Account account, string orderNo)
        {

            Dictionary<string, dynamic> header = new(4);
            header.Add("token", account.Token);

            var response = await ApiCaller.GetAsync(R8_Url_OrderDetail + "/" + orderNo, headers: header);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.Error($"SexyteaApis, GetOrderInfo failed, StatusCode:[{response.StatusCode}]");
                return null;
            }
            var result = JsonSerializer.Deserialize<dynamic>(await response.Content.ReadAsStringAsync());
            return result;
        }
    }
}
