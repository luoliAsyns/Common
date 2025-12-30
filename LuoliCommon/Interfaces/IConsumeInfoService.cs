using LuoliCommon.DTO.ConsumeInfo;
using LuoliCommon.DTO.Coupon;
using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Entities;
using Refit;
using DeleteRequest = LuoliCommon.DTO.ConsumeInfo.DeleteRequest;

namespace LuoliCommon.Interfaces
{
    public interface IConsumeInfoService
    {
       
        [Get("/api/consume-info/query-id")]
        Task<Entities.ApiResponse<ConsumeInfoDTO>> GetAsync(string goodsType, long id);
        [Get("/api/consume-info/query-coupon")]
        Task<Entities.ApiResponse<ConsumeInfoDTO>> GetAsync(string goodsType, string coupon);
        [Post("/api/consume-info/update")]
        Task<Entities.ApiResponse<bool>> UpdateAsync(LuoliCommon.DTO.ConsumeInfo.UpdateRequest ur);
        [Post("/api/consume-info/delete")]
        Task<Entities.ApiResponse<bool>> DeleteAsync(DeleteRequest dr);
        [Post("/api/consume-info/insert")]
        Task<Entities.ApiResponse<bool>> InsertAsync(ConsumeInfoDTO dto);

    }
}
