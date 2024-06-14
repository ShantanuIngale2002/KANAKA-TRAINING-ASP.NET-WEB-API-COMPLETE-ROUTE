using JWT_TokenAuthentication_UserEmployee.Data;
using JWT_TokenAuthentication_UserEmployee.Models;

namespace JWT_TokenAuthentication_UserEmployee.Repository.Interface
{
    public interface IAuthService
    {
        public User AddUser(User user);
        public string Login(LoginRequest loginRequest);
    }
}
