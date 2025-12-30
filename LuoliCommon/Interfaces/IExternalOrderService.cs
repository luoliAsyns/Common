using LuoliCommon.DTO.ExternalOrder;
using LuoliCommon.Entities;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuoliCommon.Interfaces
{
    public interface IExternalOrderService
    {
        [Get("/api/external-order/query")]
        Task<Entities.ApiResponse<ExternalOrderDTO>> Get(string from_platform, string tid);

        [Get("/api/external-order/page-query")]
        Task<Entities.ApiResponse<PageResult<ExternalOrderDTO>>> PageQuery(int page = 1,
          int size = 10,
          DateTime? startTime = null,
          DateTime? endTime = null);

        [Post("/api/external-order/update")]
        Task<Entities.ApiResponse<bool>> Update([Body] UpdateRequest ur);

        [Post("/api/external-order/delete")]
        Task<Entities.ApiResponse<bool>> Delete([Body] DeleteRequest dto);

        [Post("/api/external-order/insert")]
        Task<Entities.ApiResponse<bool>> Insert([Body] ExternalOrderDTO dto);
    }
}
