using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace JWT_TokenAuthentication_UserEmployee.Data
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
    }
}
