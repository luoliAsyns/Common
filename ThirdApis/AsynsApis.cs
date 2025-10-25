using LuoliCommon.DTO.ConsumeInfo;
using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Entities;
using LuoliCommon.Logger;
using LuoliUtils;
using MethodTimer;
using System.Text.Json;

namespace ThirdApis;

public class AsynsApis
{

    private static JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true, // 关键配置：忽略大小写
    };

    # region  external-order url

    private string Url_ExternalOrder 
    {
        get
        {
            if (string.IsNullOrEmpty(_targetIp))
                return "http://external-order-service:8080/";
            else
                return _targetIp;
        }
    }
    
    private string Url_ExternalOrder_Insert { get { return Url_ExternalOrder + "api/external-order/insert"; } }
    private string Url_ExternalOrder_Delete { get { return Url_ExternalOrder + "api/external-order/delete"; } }  
    private string Url_ExternalOrder_Query { get { return Url_ExternalOrder + "api/external-order/query"; } } 
    private string Url_ExternalOrder_Update { get { return Url_ExternalOrder + "api/external-order/update"; } }

    #endregion

    #region coupon url

    private string Url_Coupon
    {
        get
        {
            if (string.IsNullOrEmpty(_targetIp))
                return "http://coupon-service:8080/";
            else
                return _targetIp;
        }
    }
    private string Url_Coupon_Generate { get { return Url_Coupon + "api/coupon/generate"; } }
    private string Url_Coupon_GenerateManual { get { return Url_Coupon + "api/coupon/generate-manual"; } }
    private string Url_Coupon_Delete { get { return Url_Coupon + "api/coupon/delete"; } }
    private string Url_Coupon_Query { get { return Url_Coupon + "api/coupon/query"; } }
    private string Url_Coupon_PageQuery { get { return Url_Coupon + "api/coupon/page-query"; } }
    private string Url_Coupon_Update { get { return Url_Coupon + "api/coupon/update"; } }
    private string Url_Coupon_Validate { get { return Url_Coupon + "api/coupon/validate"; } }
    private string Url_Coupon_Invalidate { get { return Url_Coupon + "api/coupon/invalidate"; } }

    #endregion

    #region consume-info url
    private string Url_ConsumeInfo
    {
        get
        {
            if (string.IsNullOrEmpty(_targetIp))
                return "http://consume-info-service:8080/";
            else
                return _targetIp;
        }
    }
    private string Url_ConsumeInfo_Delete { get { return Url_ConsumeInfo + "api/consume-info/delete"; } }
    private string Url_ConsumeInfo_Update { get { return Url_ConsumeInfo + "api/consume-info/update"; } }
    private string Url_ConsumeInfo_Query { get { return Url_ConsumeInfo + "api/consume-info/query"; } }
    private string Url_ConsumeInfo_Insert { get { return Url_ConsumeInfo + "api/consume-info/insert"; } }

    #endregion

    private string _targetIp;

    private readonly ILogger _logger;

    public AsynsApis(ILogger logger, string ip)
    {
        _logger = logger;
        _targetIp = ip;
    }

    #region ExternalOrderService apis
    public async Task<ApiResponse<bool>> ExternalOrderInsert(ExternalOrderDTO dto)
    {
        try
        {
            string bodyStr = JsonSerializer.Serialize(dto);

            var response = await ApiCaller.PostAsync(Url_ExternalOrder_Insert, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.ExternalOrderInsert failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }

            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(await response.Content.ReadAsStringAsync(), _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ExternalOrderInsert failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }

    public async Task<ApiResponse<bool>> ExternalOrderDelete(LuoliCommon.DTO.ExternalOrder.DeleteRequest dq)
    {
        try
        {
            string bodyStr = JsonSerializer.Serialize(dq);

            var response = await ApiCaller.PostAsync(Url_ExternalOrder_Delete, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.Error($"AsynsApis.ExternalOrderDelete failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = "未知异常，可能网络波动";
                return resp;
            }

            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(await response.Content.ReadAsStringAsync(), _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ExternalOrderDelete failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }

    public async Task<ApiResponse<ExternalOrderDTO>> ExternalOrderQuery(string from_platform, string tid)
    {
        try
        {
            var url = $"{Url_ExternalOrder_Query}?from_platform={from_platform}&tid={tid}";
            var response = await ApiCaller.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.ExternalOrderQuery failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<ExternalOrderDTO>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<ExternalOrderDTO>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ExternalOrderQuery failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<ExternalOrderDTO>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }

    public async Task<ApiResponse<bool>> ExternalOrderUpdate(LuoliCommon.DTO.ExternalOrder.UpdateRequest ur)
    {
        try
        {

            string bodyStr = JsonSerializer.Serialize(ur);

            var response = await ApiCaller.PostAsync(Url_ExternalOrder_Update, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                _logger.Error($"AsynsApis.ExternalOrderUpdate failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = "未知异常，可能网络波动";
                return resp;
            }

            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(await response.Content.ReadAsStringAsync(), _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ExternalOrderUpdate failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }


    #endregion

    #region CouponService apis

    public async Task<ApiResponse<CouponDTO>> CouponQuery(string coupon)
    {
        try
        {
            var url = $"{Url_Coupon_Query}-coupon?coupon={coupon}";
            var response = await ApiCaller.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponQuery failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<CouponDTO>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<CouponDTO>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponQuery failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<CouponDTO>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }

    public async Task<ApiResponse<CouponDTO>> CouponQuery(string from_platform, string tid)
    {
        try
        {
            var url = $"{Url_Coupon_Query}-tid?tid={tid}&from_platform={from_platform}";
            var response = await ApiCaller.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponQuery failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<CouponDTO>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<CouponDTO>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponQuery failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<CouponDTO>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }


    public async Task<ApiResponse<List<CouponDTO>>> CouponValidate(string[] coupons, byte? status)
    {
        try
        {
            if(coupons.Length==0)
            {
                var resp = new ApiResponse<List<CouponDTO>>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = "your input coupons length is 0";
                return resp;
            }

            var url = $"{Url_Coupon_Validate}?status={status}&{string.Join("&",coupons.Select(cp=>"coupons="+cp))}";
            var response = await ApiCaller.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponValidate failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<List<CouponDTO>>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<List<CouponDTO>>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponValidate failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<List<CouponDTO>>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }


    public async Task<ApiResponse<bool>> CouponInvalidate(string coupon)
    {
        try
        {
            var url = Url_Coupon_Invalidate;
            var bodystr = JsonSerializer.Serialize(new { coupon=coupon});
            var response = await ApiCaller.PostAsync(url, bodystr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponInvalidate failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponInvalidate failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }


    public async Task<ApiResponse<PageResult<CouponDTO>>> CouponPageQuery( int page,
        int size,
        byte? status = null,
        DateTime? from = null,
        DateTime? to = null)
    {
        try
        {
            var url = $"{Url_Coupon_PageQuery}?page={page}&size={size}&status={status}&from={from}&to={to}";
            var response = await ApiCaller.GetAsync(url);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponPageQuery failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<PageResult<CouponDTO>>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<PageResult<CouponDTO>>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponPageQuery failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<PageResult<CouponDTO>>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }


    public async Task<ApiResponse<CouponDTO>> CouponGenerate(ExternalOrderDTO dto)
    {
        try
        {
            var url = Url_Coupon_Generate;
            string bodyStr = JsonSerializer.Serialize(dto);
            var response = await ApiCaller.PostAsync(url, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponGenerate failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<CouponDTO>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<CouponDTO>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponGenerate failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<CouponDTO>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }


    public async Task<ApiResponse<CouponDTO>> CouponGenerateManual(string from_platform, string tid, decimal amount)
    {
        try
        {
            var url = Url_Coupon_GenerateManual;

            var bodyStr = JsonSerializer.Serialize(new
            {
                from_platform = from_platform,
                tid = tid,
                amount = amount
            });

            var response = await ApiCaller.PostAsync(url, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponGenerateManual failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<CouponDTO>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<CouponDTO>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponGenerateManual failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<CouponDTO>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }


    public async Task<ApiResponse<bool>> CouponDelete(string coupon)
    {
        try
        {
            var url = Url_Coupon_Delete;

            var bodyStr = JsonSerializer.Serialize(new
            {
                coupon = coupon,
            });

            var response = await ApiCaller.PostAsync(url, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponDelete failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponDelete failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }

    public async Task<ApiResponse<bool>> CouponUpdate(LuoliCommon.DTO.Coupon.UpdateRequest ur)
    {
        try
        {
            var url = Url_Coupon_Update;

            var bodyStr = JsonSerializer.Serialize(ur);

            var response = await ApiCaller.PostAsync(url, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.CouponUpdate failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.CouponUpdate failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }



    #endregion

    #region ConsumeInfo apis

    public async Task<ApiResponse<bool>> ConsumeInfoDelete(string goodsType, long id)
    {
        try
        {
            var url = Url_ConsumeInfo_Delete;

            var bodyStr = JsonSerializer.Serialize(new
            {
                goodsType = goodsType,
                id= id
            });

            var response = await ApiCaller.PostAsync(url, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.ConsumeInfoDelete failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ConsumeInfoDelete failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }
    public async Task<ApiResponse<bool>> ConsumeInfoInsert(ConsumeInfoDTO dto)
    {
        try
        {
            var url = Url_ConsumeInfo_Insert;

            var bodyStr = JsonSerializer.Serialize(dto);

            var response = await ApiCaller.PostAsync(url, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.ConsumeInfoInsert failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ConsumeInfoInsert failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }
    public async Task<ApiResponse<ConsumeInfoDTO>> ConsumeInfoQuery(string goodsType, long id)
    {
        try
        {
            var url = Url_ConsumeInfo_Query;


            var response = await ApiCaller.GetAsync($"{url}-id?goodsType={goodsType}&id={id}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.ConsumeInfoQuery failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<ConsumeInfoDTO>();
                resp.data = null ;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<ConsumeInfoDTO>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ConsumeInfoQuery failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<ConsumeInfoDTO>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }
    public async Task<ApiResponse<ConsumeInfoDTO>> ConsumeInfoQuery(string goodsType, string coupon)
    {
        try
        {
            var url = Url_ConsumeInfo_Query;


            var response = await ApiCaller.GetAsync($"{url}-coupon?goodsType={goodsType}&coupon={coupon}");

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.ConsumeInfoQuery failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<ConsumeInfoDTO>();
                resp.data = null;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<ConsumeInfoDTO>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ConsumeInfoQuery failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<ConsumeInfoDTO>();
            resp.data = null;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }
    public async Task<ApiResponse<bool>> ConsumeInfoUpdate(LuoliCommon.DTO.ConsumeInfo.UpdateRequest ur)
    {
        try
        {
            var url = Url_ConsumeInfo_Update;

            var bodyStr = JsonSerializer.Serialize(ur);

            var response = await ApiCaller.PostAsync(url, bodyStr);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();

                _logger.Error($"AsynsApis.ConsumeInfoUpdate failed, StatusCode:[{response.StatusCode}]");
                var resp = new ApiResponse<bool>();
                resp.data = false;
                resp.code = LuoliCommon.Enums.EResponseCode.Fail;
                resp.msg = errorMessage;
                return resp;
            }
            var resultStr = await response.Content.ReadAsStringAsync();
            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(resultStr, _options);

            return successResp;
        }
        catch (Exception ex)
        {
            _logger.Error($"AsynsApis.ConsumeInfoUpdate failed");
            _logger.Error(ex.Message);

            var resp = new ApiResponse<bool>();
            resp.data = false;
            resp.code = LuoliCommon.Enums.EResponseCode.Fail;
            resp.msg = "未知异常";

            return resp;
        }
    }

    #endregion

    #region 2
    #endregion

    #region 3
    #endregion
}