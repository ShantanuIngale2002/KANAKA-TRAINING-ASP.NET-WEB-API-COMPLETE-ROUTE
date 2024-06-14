using JWT_RoleBasedAuthorization_WebApiApp.Data;
using JWT_RoleBasedAuthorization_WebApiApp.Models;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.EFCore;
using JWT_RoleBasedAuthorization_WebApiApp.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_RoleBasedAuthorization_WebApiApp.Repository.Service
{
    public class AuthService : IAuthRepository
    {

        private readonly RoleBasedAppContext context; // dbcontext requirement
        private readonly IConfiguration configuration; // jwt bearer token requirement
        public AuthService(RoleBasedAppContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }



        public Role AddRole(Role role)
        {
            var addedRole = context.Roles.Add(role);
            context.SaveChanges();
            return addedRole.Entity;
        }

        public User AddUser(User user)
        {
            var addedUser = context.Users.Add(user);
            context.SaveChanges();
            return addedUser.Entity;
        }

        public bool AssignRoleToUser(AddUserRole obj)
        {
            try
            {

                var user = context.Users.SingleOrDefault(s => s.Id == obj.UsertId);
                if (user == null)
                {
                    throw new Exception("User is not Valid !!");
                }

                var addRole = new List<UserRole>();
                foreach (int role in obj.RoleIds)
                {
                    var userrole = new UserRole();
                    userrole.UserID = user.Id;
                    userrole.RoleID = role;
                    addRole.Add(userrole);
                }

                context.UserRoles.AddRange(addRole);
                context.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }


        // important since validate credentials and then if validated create jwt token and return its string
        public string Login(LoginRequestModel loginRequest)
        {
            if(loginRequest.Username != null && loginRequest.Password != null)
            {
                var user = context.Users.SingleOrDefault(s=> s.Username== loginRequest.Username && s.Password==loginRequest.Password);
                if(user != null)
                {
                    // these claim chain variable entity is created for token
                    // key-value pairs in token's payload to convey specific details, for authentication and authorization.
                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]), // In this claim chain, setting claims name and its value
                        new Claim("Id", user.Id.ToString()), // custom claim with name id and value as user.Id likewise below
                        new Claim("UserName", user.Name),
                    };

                    // now get all associated roles of user
                    var userRoles = context.UserRoles.Where(u=> u.UserID == user.Id).ToList(); // get all roles ids w/ users.
                    var roleIds = userRoles.Select(u => u.RoleID).ToList(); // get their unique ids using role ids
                    var roles = context.Roles.Where(r=> roleIds.Contains(r.Id)).ToList(); // from unique ids get roles content

                    // acquired all user's roles in claims as claim.
                    foreach(var role in roles)
                    {
                        claims.Add(new Claim("Role", role.Name));
                    }

                    // Generate JWT SECURITY TOKEN
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])); // symmetric key using key provided in appsettings.json
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // with symmetric key and algorithm create signing credential entity
                    // token entity
                    var token = new JwtSecurityToken( // method
                        configuration["Jwt:Issuer"], // issuer parameter from appsetting.json
                        configuration["Jwt:Audience"], // audience parameter from appsetting.json
                        claims, // claims parameter defined above
                        expires: DateTime.UtcNow.AddMinutes(10), // to be expired at time, 10 min later after created
                        signingCredentials: signIn); // signing credentials generated above

                    var JwtToken = new JwtSecurityTokenHandler().WriteToken(token); // to return handle the token
                    return JwtToken; // return the token

                }

                else { throw new Exception("User not Found !!"); }
            
            }

            else { throw new Exception("Credentials are Invalid !!"); }
        }
    }
}
