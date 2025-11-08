

using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Entities;
using LuoliCommon.Enums;

namespace ThirdApis.Services.ExternalOrder
{

    public interface IExternalOrderRepository
    {
        Task<ApiResponse<ExternalOrderDTO>> Get(string from_platform, string tid);
        Task<ApiResponse<ExternalOrderDTO>> Get(string coupon);

        Task<ApiResponse<PageResult<ExternalOrderDTO>>> PageQueryAsync(int page = 1,
          int size = 10,
          DateTime? startTime = null,
          DateTime? endTime = null);

        Task<ApiResponse<bool>> Update(UpdateRequest ur);
        Task<ApiResponse<bool>> Delete(LuoliCommon.DTO.ExternalOrder.DeleteRequest dto);
        Task<ApiResponse<bool>> Insert(ExternalOrderDTO dto);

    }
}
