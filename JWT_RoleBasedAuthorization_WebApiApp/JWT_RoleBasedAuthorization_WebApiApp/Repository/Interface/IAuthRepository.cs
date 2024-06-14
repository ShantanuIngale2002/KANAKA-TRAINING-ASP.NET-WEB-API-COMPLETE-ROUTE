using JWT_RoleBasedAuthorization_WebApiApp.Data;
using JWT_RoleBasedAuthorization_WebApiApp.Models;

namespace JWT_RoleBasedAuthorization_WebApiApp.Repository.Interface
{
    public interface IAuthRepository
    {
        User AddUser(User user);
        string Login(LoginRequestModel loginRequest);
        Role AddRole (Role role);
        bool AssignRoleToUser(AddUserRole obj);

    }
}
