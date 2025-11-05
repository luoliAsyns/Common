

namespace GatewayService.User
{
    public class UserRepository : IUserRepository
    {

      
        public UserRepository()
        {
        }

        public async Task<(bool, string)> ChangePassword(string userName, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string)> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string)> Logout(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<(bool, string)> Register(string userName, string phoneNum, bool genter)
        {
            throw new NotImplementedException();
        }
    }
}
