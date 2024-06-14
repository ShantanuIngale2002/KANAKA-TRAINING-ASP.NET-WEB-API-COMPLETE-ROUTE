using JWT_TokenAuthentication_UserEmployee.Data;
using Microsoft.EntityFrameworkCore;

namespace JWT_TokenAuthentication_UserEmployee.Repository.EFCore
{
    public class JwtContext : DbContext
    {
        public JwtContext(DbContextOptions<JwtContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
