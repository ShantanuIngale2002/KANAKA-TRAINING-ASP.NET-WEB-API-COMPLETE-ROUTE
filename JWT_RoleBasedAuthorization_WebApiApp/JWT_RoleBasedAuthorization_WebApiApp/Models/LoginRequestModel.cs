namespace JWT_RoleBasedAuthorization_WebApiApp.Models
{
    public class LoginRequestModel
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
