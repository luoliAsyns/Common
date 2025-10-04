using LuoliCommon.Logger;
using LuoliUtils;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Web;

namespace ThirdApis
{
    public  class AgisoApis
    {

      
        // 给买家发送旺旺消息
        private  string Url_SendWWMsg = "http://gw.api.agiso.com/alds/WwMsg/Send";

        // 发货
        private  string Url_ShipOrder = "http://gw.api.agiso.com/alds/Trade/LogisticsDummySend";


        private readonly ILogger _logger;
        public AgisoApis(ILogger logger)
        {
            _logger = logger;
        }

        public  async Task<(bool, string)> SendWWMsg(string accessToken, string appSecret, long tid, string message)
        {

            Dictionary<string, dynamic> header = new();
            header["ContentType"] = "application/x-www-form-urlencoded";
            header["Authorization"] = "Bearer " + accessToken;
            header["ApiVersion"] =  "1";


            //业务参数
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);

            var body = new Dictionary<string, string>() { };

            // 订单号
            body.Add("tid", tid.ToString());
            // 发送给买家的内容
            body.Add("msg", message);

            body.Add("timestamp", Convert.ToInt64(ts.TotalSeconds).ToString());
            body.Add("sign", Sign(body, appSecret));
      

            bool success = false;
            string msg =string .Empty;

            Action sendWWMsg = () => {
                string respStr = ApiCaller.PostAsync(
                    Url_SendWWMsg,
                    ConvertBody2String(body), 
                    header, isFormUrlEncode:true).Result.Content.ReadAsStringAsync().Result;

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

            Action sendShipOrder = () =>
            {
                string respStr = ApiCaller.PostAsync(
                    Url_ShipOrder,
                    JsonSerializer.Serialize(body),
                    header).Result.Content.ReadAsStringAsync().Result;

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


     


        private  string ConvertBody2String(Dictionary<string, string> body)
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
