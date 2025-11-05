

using LuoliCommon.Entities;
using ThirdApis;

namespace GatewayService.User
{
    public class UserRepository : IUserRepository
    {

        private readonly AsynsApis _asynsApis;
        public UserRepository(AsynsApis asynsApis)
        {
            _asynsApis = asynsApis;
        }

        public async Task<ApiResponse<bool>> ChangePassword(string userName, string newPassword)
        {
            return await _asynsApis.UserChangePassword(userName, newPassword);
        }

        public async Task<ApiResponse<bool>> Login(string userName, string password)
        {
            return await _asynsApis.UserLogin(userName, password);
        }

        public async Task<ApiResponse<string>> Register(string userName, string phoneNum, bool genter)
        {
            return await _asynsApis.UserRegister(userName, phoneNum, genter);
        }
    }
}
