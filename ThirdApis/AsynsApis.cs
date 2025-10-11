using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.DTO.Sexytea;
using LuoliCommon.Entities;
using LuoliCommon.Logger;
using LuoliUtils;
using System.Text.Json;

namespace ThirdApis;

public class AsynsApis
{

    # region  external-order
    private string Url_ExternalOrder_Insert { get { return _targetIp + "api/external-order/insert"; } }

    private string Url_ExternalOrder_Delete { get { return _targetIp + "api/external-order/delete"; } }  
    private string Url_ExternalOrder_Query { get { return _targetIp + "api/external-order/query"; } } 
    private string Url_ExternalOrder_Update { get { return _targetIp + "api/external-order/update"; } } 

    #endregion

    #region coupon
    private string Url_Coupon_Generate { get { return _targetIp + "api/coupon/generate"; } }
    private string Url_Coupon_GenerateManual { get { return _targetIp + "api/coupon/generate-manual"; } } 
    private string Url_Coupon_Delete { get { return _targetIp + "api/coupon/delete"; } } 
    private string Url_Coupon_Query { get { return _targetIp + "api/coupon/query"; } }  
    private string Url_Coupon_PageQuery { get { return _targetIp + "api/coupon/page-query"; } }  
    private string Url_Coupon_Update { get { return _targetIp + "api/coupon/update"; } } 
    private string Url_Coupon_Validate { get { return _targetIp + "api/coupon/valiadate"; } }  
    private string Url_Coupon_Invalidate { get { return _targetIp + "api/coupon/invalidate"; } }  

    #endregion

    private string _targetIp;

    private readonly ILogger _logger;

    public AsynsApis(ILogger logger, string ip)
    {
        _logger = logger;
        _targetIp = ip;
    }

    #region ExternalOrderService
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

            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());

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

    public async Task<ApiResponse<bool>> ExternalOrderDelete(DeleteRequest dq)
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

            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());

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
            var successResp = JsonSerializer.Deserialize<ApiResponse<ExternalOrderDTO>>(resultStr);

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

    public async Task<ApiResponse<bool>> ExternalOrderUpdate(ExternalOrderDTO dto)
    {
        try
        {
            string bodyStr = JsonSerializer.Serialize(dto);

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

            var successResp = JsonSerializer.Deserialize<ApiResponse<bool>>(await response.Content.ReadAsStringAsync());

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


    #region CouponService

    public async Task<ApiResponse<CouponDTO>> CouponQuery(string coupon)
    {
        try
        {
            var url = $"{Url_Coupon_Query}?coupon={coupon}";
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
            var successResp = JsonSerializer.Deserialize<ApiResponse<CouponDTO>>(resultStr);

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


    #endregion

    #region 1
    #endregion


    #region 2
    #endregion

    #region 3
    #endregion
}