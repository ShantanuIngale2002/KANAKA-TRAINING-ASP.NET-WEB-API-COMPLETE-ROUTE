using JWT_RoleBasedAuthorization_WebApiApp.Data;
using JWT_RoleBasedAuthorization_WebApiApp.Models;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_RoleBasedAuthorization_WebApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthRepository authRepoServe;
        public AuthController(IAuthRepository authRepoServe)
        {
            this.authRepoServe = authRepoServe;
        }




        [HttpPost("login")]
        public string Login([FromBody] LoginRequestModel obj)
        {
            var token = authRepoServe.Login(obj);
            return token;
        }
        

        [HttpPost("addUser")]
        public User AddUser([FromBody] User user)
        {
            var addedUser = authRepoServe.AddUser(user);
            return addedUser;
        }


        [HttpPost("addRole")]
        public Role AddRole([FromBody] Role role)
        {
            var addedRole = authRepoServe.AddRole(role);
            return addedRole;
        }


        [HttpPost("assignRole")]
        public bool AssignRoleToUser([FromBody] AddUserRole userRole)
        {
            var addedUserRole = authRepoServe.AssignRoleToUser(userRole);
            return addedUserRole;
        }

    }
}
