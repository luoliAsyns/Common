using System.Text.Json;
using LuoliCommon.Logger;
using LuoliUtils;

namespace ThirdApis;

public class AsynsApis
{

    # region  external-order
    private string Url_ExternalOrder_Insert = "api/external-order/insert";
    private string Url_ExternalOrder_Delete =  "api/external-order/delete";
    private string Url_ExternalOrder_Query =  "api/external-order/query";
    private string Url_ExternalOrder_Update =  "api/external-order/update";

    #endregion

    #region coupon
    private string Url_Coupon_Generate = "api/coupon/generate";
    private string Url_Coupon_GenerateManual = "api/coupon/generate-manual";
    private string Url_Coupon_Delete =  "api/coupon/delete";
    private string Url_Coupon_Query =  "api/coupon/query";
    private string Url_Coupon_PageQuery =  "api/coupon/page-query";
    private string Url_Coupon_Update =  "api/coupon/update";
    private string Url_Coupon_Validate =  "api/coupon/valiadate";
    private string Url_Coupon_Invalidate =  "api/coupon/invalidate";

    #endregion

    private string _targetIp;

    private readonly ILogger _logger;

    public AsynsApis(ILogger logger, string ip = "http://115.190.168.53/")
    {
        _logger = logger;
        _targetIp = ip;
    }

    public async Task<(bool, string)> SendWWMsg(string accessToken, string appSecret, long tid, string message)
    {

        Dictionary<string, dynamic> header = new();
        header["ContentType"] = "application/x-www-form-urlencoded";
        header["Authorization"] = "Bearer " + accessToken;
        header["ApiVersion"] = "1";


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
        string msg = string.Empty;

        Action sendWWMsg = () =>
        {
            string respStr = ApiCaller.PostAsync(
                Url_SendWWMsg,
                ConvertBody2String(body),
                header, isFormUrlEncode: true).Result.Content.ReadAsStringAsync().Result;

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


}