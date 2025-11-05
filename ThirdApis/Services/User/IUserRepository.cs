using LuoliCommon.Entities;

namespace GatewayService.User
{
    public interface IUserRepository
    {

        Task<(bool, string)> Login(string userName, string password);
        Task<(bool, string)> Register(string userName,  string phoneNum, bool genter);
        Task<(bool, string)> Logout(string userName);

        Task<(bool, string)> ChangePassword(string userName, string newPassword);




    }
}
