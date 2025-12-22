using LuoliCommon.Entities;

namespace LuoliCommon.Interfaces
{
    public interface IUserService
    {

        Task<ApiResponse<bool>> Login(string userName, string password);
        Task<ApiResponse<string>> Register(string userName,  string phoneNum, bool genter);
        Task<ApiResponse<bool>> ChangePassword(string userName, string newPassword);




    }
}
