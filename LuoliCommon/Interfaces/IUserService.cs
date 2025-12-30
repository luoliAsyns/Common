using LuoliCommon.DTO.User;
using LuoliCommon.Entities;
using Refit;

namespace LuoliCommon.Interfaces
{
    public interface IUserService
    {
        [Post("/api/user/login")]
        Task<Entities.ApiResponse<bool>> Login([Body] LoginRequest re);
        [Post("/api/user/register")]
        Task<Entities.ApiResponse<string>> Register([Body] RegisterRequest re);
        [Post("/api/user/change-password")]
        Task<Entities.ApiResponse<bool>> ChangePassword([Body] ChangePasswordRequest re);

    }
}
