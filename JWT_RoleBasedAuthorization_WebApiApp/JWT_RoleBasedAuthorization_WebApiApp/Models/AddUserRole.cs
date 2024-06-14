using Microsoft.Identity.Client;

namespace JWT_RoleBasedAuthorization_WebApiApp.Models
{
    public class AddUserRole
    {
        public int UsertId { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
