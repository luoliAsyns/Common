using LuoliCommon.DTO.Admin;
using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.DTO.ProxyOrder;
using LuoliCommon.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Interfaces
{
    public interface IProxyOrderService
    {
        [Get("/api/proxy-order/query")]
        Task<Entities.ApiResponse<ProxyOrderDTO>> GetAsync(string coupon);

        [Get("/api/proxy-order/query-coupons")]
        Task<Entities.ApiResponse<IEnumerable<ProxyOrderDTO>>> GetAsync(string targetProxy, string[] coupons, string? orderStatus = null);

        [Post("/api/proxy-order/insert")]
        Task<Entities.ApiResponse<bool>> InsertAsync([Body] ProxyOrderDTO dto);

        [Post("/api/proxy-order/update")]
        Task<Entities.ApiResponse<bool>> UpdateAsync([Body] ProxyOrderDTO dto);

        [Post("/api/proxy-order/backup")]
        Task<Entities.ApiResponse<int>> BackUpAsync([Body] BackUpRequest dto);
    }
}
