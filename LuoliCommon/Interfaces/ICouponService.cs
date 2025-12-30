using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Entities;
using LuoliCommon.Enums;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace LuoliCommon.Interfaces
{
    public interface ICouponService
    {
        [Get("/api/coupon/query-coupon")]
        Task<Entities.ApiResponse<CouponDTO>> Query(string coupon);

        [Get("/api/coupon/query-tid")]
        Task<Entities.ApiResponse<CouponDTO>> Query(string from_platform, string tid);

        [Get("/api/coupon/page-query")]
        Task<Entities.ApiResponse<PageResult<CouponDTO>>> PageQuery(
            int page,
            int size,
            byte? status = null,
            DateTime? from = null,
            DateTime? to = null);

        [Get("/api/coupon/personal-coupons")]
        Task<Entities.ApiResponse<IEnumerable<CouponDTO>>> PersonalCoupons(
            string coupon,
            string targetProxy,
            DateTime? from,
            DateTime? to,
            int? limit);



        [Get("/api/coupon/validate")]
        Task<Entities.ApiResponse<List<CouponDTO>>> Validate(string[] coupons,  ECouponStatus status = ECouponStatus.Default);
        [Post("/api/coupon/invalidate")]
        Task<Entities.ApiResponse<bool>> Invalidate([Body] UpdateErrorCodeRequest coupon);
       
       
        [Post("/api/coupon/generate")]
        Task<Entities.ApiResponse<CouponDTO>> Generate([Body] ExternalOrderDTO dto);
        [Post("/api/coupon/generate-manual")]
        Task<Entities.ApiResponse<CouponDTO>> GenerateManual([Body] GenerateManualReqest jObject);



        [Post("/api/coupon/delete")] 
        Task<Entities.ApiResponse<bool>> Delete([Query] string coupon);
        [Post("/api/coupon/update")] 
        Task<Entities.ApiResponse<bool>> Update([Body] LuoliCommon.DTO.Coupon.UpdateRequest ur);

        [Post("/api/coupon/update-error")] 
        Task<Entities.ApiResponse<bool>> UpdateErrorCode([Body] LuoliCommon.DTO.Coupon.UpdateErrorCodeRequest ur);


    }

}
