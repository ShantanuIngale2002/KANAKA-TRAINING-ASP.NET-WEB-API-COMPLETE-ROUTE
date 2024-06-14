using System.ComponentModel.DataAnnotations;

namespace JWT_TokenAuthentication_UserEmployee.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string EmailConfirmed { get; set; } = string.Empty;
    }
}
