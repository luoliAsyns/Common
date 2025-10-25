using LuoliCommon.DTO.Agiso;
using LuoliCommon.Logger;
using LuoliUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

namespace ThirdApis
{
    public  class AgisoApis
    {


        // 给买家发送旺旺消息
        private string Url_SendWWMsg = "http://gw.api.agiso.com/alds/WwMsg/Send";

        // 发货
        private string Url_ShipOrder = "http://gw.api.agiso.com/alds/Trade/LogisticsDummySend";

        private string Url_TradeInfo = "http://gw.api.agiso.com/alds/Trade/TradeInfo";


        private readonly ILogger _logger;
        public AgisoApis(ILogger logger)
        {
            _logger = logger;
        }


        //发送旺旺消息
        public  async Task<(bool, string)> SendWWMsg(string accessToken, string appSecret, string tid, string message)
        {
            Dictionary<string, dynamic> header = new();
            header["Authorization"] = "Bearer " + accessToken;
            header["ApiVersion"] =  "1";

            //业务参数
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var body = new Dictionary<string, string>() { };

            // 订单号
            body.Add("tid", tid);
            body.Add("msg", message);
            body.Add("timestamp", Convert.ToInt64(ts.TotalSeconds).ToString());
            body.Add("sign", Sign(body, appSecret));
      

            bool success = false;
            string msg =string .Empty;

            Func<Task> sendWWMsg = async () => {
                var resp = await ApiCaller.PostAsync(
                     Url_SendWWMsg,
                     System.Text.Json.JsonSerializer.Serialize(body),
                     header, true);
                string respStr = await resp.Content.ReadAsStringAsync();

                // 请求成功后，取isSuccess  成功了就算了，没成功取 Error_Msg
                JsonDocument responseObj = JsonDocument.Parse(respStr);
                success = responseObj.RootElement.GetProperty("IsSuccess").GetBoolean();

                if (!success)
                {
                    _logger.Error($"AgisoApis.SendWWMsg failed, full response:[{respStr}]");
                    msg = responseObj.RootElement.GetProperty("Error_Msg").GetString();
                }
            };

            await ActionsOperator.ReTryAction(sendWWMsg);

            return (success, msg);
        }

        //发货
        public  async Task<(bool, string)> ShipOrder(string accessToken, string appSecret, string tids)
        {
            Dictionary<string, dynamic> header = new();

            header.Add("Authorization", "Bearer " + accessToken);
            header.Add("ApiVersion", "1");

            //业务参数
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var body = new Dictionary<string, string>() { };
            //订单编号,多个订单编号间用,分隔开
            body.Add("tids", tids);
            body.Add("timestamp", Convert.ToInt64(ts.TotalSeconds).ToString());
            body.Add("sign", Sign(body, appSecret));


            bool success = false;
            string msg = string.Empty;

            Func<Task> sendShipOrder = async () =>
            {

                var resp = await ApiCaller.PostAsync(
                    Url_ShipOrder,
                    System.Text.Json.JsonSerializer.Serialize(body),
                    header, true);
                string respStr = await resp.Content.ReadAsStringAsync();

                // 请求成功后，取QTY赋值
                JsonDocument responseObj = JsonDocument.Parse(respStr);
                success = responseObj.RootElement.GetProperty("IsSuccess").GetBoolean();

                if (!success)
                {
                    _logger.Error($"AgisoApis.ShipOrder failed, full response:[{respStr}]");
                    msg = responseObj.RootElement.GetProperty("Error_Msg").GetString();
                }
            };

            await ActionsOperator.ReTryAction(sendShipOrder);

            return (success, msg);
        }


        //获取订单详情

        public async Task<(bool, TradeInfoDTO)> TradeInfo(string accessToken, string appSecret, string tid)
        {
            Dictionary<string, dynamic> header = new();

            header.Add("Authorization", "Bearer " + accessToken);
            header.Add("ApiVersion", "1");

            //业务参数
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var body = new Dictionary<string, string>() { };
            //订单编号
            body.Add("tid", tid);
            body.Add("timestamp", Convert.ToInt64(ts.TotalSeconds).ToString());
            body.Add("sign", Sign(body, appSecret));


            bool success = false;
            JsonDocument responseObj= null;
            TradeInfoDTO tradeInfoDTO = null;

            Func<Task> getTradeInfo =async () =>
            {
                var resp = await ApiCaller.PostAsync(
                    Url_TradeInfo,
                     System.Text.Json.JsonSerializer.Serialize(body),
                    header, true);

                string respStr = await resp.Content.ReadAsStringAsync();

                // 请求成功后，取QTY赋值
                responseObj = JsonDocument.Parse(respStr);
                success = responseObj.RootElement.GetProperty("IsSuccess").GetBoolean();

                if (!success)
                    _logger.Error($"AgisoApis.TradeInfo failed, tid:[{tid}], Error_Msg:[{responseObj.RootElement.GetProperty("Error_Msg").GetString()}]");
                else
                {
                    tradeInfoDTO = System.Text.Json.JsonSerializer.Deserialize<TradeInfoDTO>(respStr);
                    File.WriteAllText("tradeinfo" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"), respStr);
                }
            };

            await ActionsOperator.ReTryAction(getTradeInfo);

            return (success, tradeInfoDTO);
        }



        public async Task<(bool, string, OrderCreateRequest)> ValidateOrderCreateAsync(HttpRequest request,string rawJson)
        {
            // 提取并验证Timestamp
            if (!request.Query.TryGetValue("Timestamp", out var timestampValue) ||
                !long.TryParse(timestampValue, out long timestamp))
                return (false, "no Timestamp" , null);

            // 提取并验证Aopic
            if (!request.Query.TryGetValue("Aopic", out var aopicValue) ||
                !long.TryParse(aopicValue, out long aopic))
                return (false, "no Aopic", null);
          

            // 提取并验证Sign
            if (!request.Query.TryGetValue("Sign", out var signValue) ||
                string.IsNullOrWhiteSpace(signValue))
                return (false, "no Sign", null);


            // 提取并验证FromPlatform
            if (!request.Query.TryGetValue("FromPlatform", out var fromPlatformValue) ||
                string.IsNullOrWhiteSpace(fromPlatformValue))
                return (false, "no FromPlatform", null);



           

            // 4. 反序列化为OrderCreateRequest对象
            OrderCreateRequest orderCreateDto;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    FloatParseHandling = FloatParseHandling.Decimal,
                    Culture = System.Globalization.CultureInfo.InvariantCulture // 确保数字格式解析正确
                };

                orderCreateDto = JsonConvert.DeserializeObject<OrderCreateRequest>(rawJson, settings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                return (false, $"body format not correct: {ex.Message}", null);
            }

            orderCreateDto.Sign = signValue;
            orderCreateDto.Timestamp = timestamp;
            orderCreateDto.Aopic = aopic;
            orderCreateDto.FromPlatform = fromPlatformValue;


            return (true, "", orderCreateDto);
        }

        public async Task<(bool, string, OrderRefundRequest)> ValidateOrderRefundAsync(HttpRequest request, string rawJson)
        {
            // 提取并验证Timestamp
            if (!request.Query.TryGetValue("Timestamp", out var timestampValue) ||
                !long.TryParse(timestampValue, out long timestamp))
                return (false, "no Timestamp", null);

            // 提取并验证Aopic
            if (!request.Query.TryGetValue("Aopic", out var aopicValue) ||
                !long.TryParse(aopicValue, out long aopic))
                return (false, "no Aopic", null);


            // 提取并验证Sign
            if (!request.Query.TryGetValue("Sign", out var signValue) ||
                string.IsNullOrWhiteSpace(signValue))
                return (false, "no Sign", null);


            // 提取并验证FromPlatform
            if (!request.Query.TryGetValue("FromPlatform", out var fromPlatformValue) ||
                string.IsNullOrWhiteSpace(fromPlatformValue))
                return (false, "no FromPlatform", null);





            // 4. 反序列化为OrderRefundRequest对象
            OrderRefundRequest orderRefundDto;
            try
            {
                var settings = new JsonSerializerSettings
                {
                    FloatParseHandling = FloatParseHandling.Decimal,
                    Culture = System.Globalization.CultureInfo.InvariantCulture // 确保数字格式解析正确
                };

                orderRefundDto = JsonConvert.DeserializeObject<OrderRefundRequest>(rawJson, settings);
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                return (false, $"body format not correct: {ex.Message}", null);
            }

            orderRefundDto.Sign = signValue;
            orderRefundDto.Timestamp = timestamp;
            orderRefundDto.Aopic = aopic;
            orderRefundDto.FromPlatform = fromPlatformValue;


            return (true, "", orderRefundDto);
        }

        public bool ValidateSign(OrderCreateRequest dto, string rawJson, string appSecret)
        {
            var dictParams = new Dictionary<string, string>();
            dictParams.Add("timestamp", dto.Timestamp.ToString());
            dictParams.Add("json", rawJson);
            //参考签名算法
            var checkSign = Sign(dictParams, appSecret);
            return string.Equals(checkSign, dto.Sign);
        }

        public bool ValidateSign(OrderRefundRequest dto, string rawJson, string appSecret)
        {
            var dictParams = new Dictionary<string, string>();
            dictParams.Add("timestamp", dto.Timestamp.ToString());
            dictParams.Add("json", rawJson);
            //参考签名算法
            var checkSign = Sign(dictParams, appSecret);
            return string.Equals(checkSign, dto.Sign);
        }

        private string ConvertBody2String(Dictionary<string, string> body)
        {
            string postData = "";
            foreach (var p in body)
            {
                if (!String.IsNullOrEmpty(postData))
                {
                    postData += "&";
                }
                string tmpStr = String.Format("{0}={1}", p.Key, HttpUtility.UrlEncode(p.Value));
                postData += tmpStr;
            }
            return postData;
        }

        private  string Sign(IDictionary<string, string> args, string ClientSecret)
        {
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(args, StringComparer.Ordinal);
            string str = "";
            foreach (var m in sortedParams)
            {
                str += m.Key + m.Value;
            }

            //头尾加入AppSecret
            str = ClientSecret + str + ClientSecret;
            var encodeStr = MD5Encrypt(str);
            return encodeStr;
        }
        //Md5摘要
        private  string MD5Encrypt(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");
            }
            return byte2String;


        }
    }
}
