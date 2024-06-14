using JWT_RoleBasedAuthorization_WebApiApp.Data;
using Microsoft.EntityFrameworkCore;

namespace JWT_RoleBasedAuthorization_WebApiApp.Repository.EFCore
{
    public class RoleBasedAppContext : DbContext
    {
        public RoleBasedAppContext(DbContextOptions<RoleBasedAppContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
