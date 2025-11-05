using LuoliCommon.Entities;

namespace GatewayService.User
{
    public interface IUserRepository
    {

        Task<ApiResponse<bool>> Login(string userName, string password);
        Task<ApiResponse<string>> Register(string userName,  string phoneNum, bool genter);
        Task<ApiResponse<bool>> ChangePassword(string userName, string newPassword);




    }
}
