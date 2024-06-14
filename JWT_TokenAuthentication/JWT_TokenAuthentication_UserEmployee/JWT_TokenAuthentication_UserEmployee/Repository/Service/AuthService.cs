using JWT_TokenAuthentication_UserEmployee.Data;
using JWT_TokenAuthentication_UserEmployee.Models;
using JWT_TokenAuthentication_UserEmployee.Repository.EFCore;
using JWT_TokenAuthentication_UserEmployee.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_TokenAuthentication_UserEmployee.Repository.Service
{
    public class AuthService : IAuthService
    {
        // context for db
        private readonly JwtContext context;
        // configuration for token
        private readonly IConfiguration configuration;

        public AuthService(JwtContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }



        public User AddUser(User user)
        {
            var SavedUser = context.Users.Add(user);
            context.SaveChanges();
            return SavedUser.Entity;
        }


        public string Login(LoginRequest loginRequest)
        {
            if(loginRequest.UserName != null && loginRequest.Password != null)
            {
                var user = context.Users.SingleOrDefault(s=> s.Email == loginRequest.UserName && s.Password == loginRequest.Password);
                
                if(user != null)
                {
                    // these claim chain variable entity is created for token
                    // key-value pairs in token's payload to convey specific details, for authentication and authorization.
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]), // In this claim chain, setting claims name and its value
                        new Claim("Id", user.Id.ToString()), // custom claim with name id and value as user.Id likewise below
                        new Claim("UserName", user.Name),
                        new Claim("Email",user.Email),
                    };


                    // Generate JWT SECURITY TOKEN
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])); // symmetric key using key provided in appsettings.json
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // with symmetric key and algorithm create signing credential entity
                    // token entity
                    var token = new JwtSecurityToken( // method
                        configuration["Jwt:Issuer"], // issuer parameter from appsetting.json
                        configuration["Jwt:Audience"], // audience parameter from appsetting.json
                        claims, // claims parameter defined above
                        expires : DateTime.UtcNow.AddMinutes(10), // to be expired at time, 10 min later after created
                        signingCredentials : signIn); // signing credentials generated above

                    var JwtToken = new JwtSecurityTokenHandler().WriteToken(token); // to return handle the token
                    return JwtToken; // return the token
                }

                else
                {
                    throw new Exception("Credentials are not Valid !!");
                }
            }

            else
            {
                throw new Exception("User is not Valid !");
            }
        }
    }
}
