using JWT_TokenAuthentication_UserEmployee.Data;
using JWT_TokenAuthentication_UserEmployee.Models;
using JWT_TokenAuthentication_UserEmployee.Repository.Interface;
using JWT_TokenAuthentication_UserEmployee.Repository.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_TokenAuthentication_UserEmployee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthService authService;
        public AuthController (AuthService authService)
        {
            this.authService = authService;
        }



        // POST api/<AuthController>
        [HttpPost]
        public string Login([FromBody] LoginRequest loginModel)
        {
            var results = authService.Login(loginModel);
            return results;
        }

        // PUT api/<AuthController>/5
        [HttpPost("addUser")]
        public User AddUser([FromBody] User value)
        {
            var user = authService.AddUser(value);
            return user;
        }

    }
}
