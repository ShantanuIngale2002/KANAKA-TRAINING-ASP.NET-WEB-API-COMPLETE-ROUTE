using Microsoft.EntityFrameworkCore;
using PracticeForTelerickUI01.Data;
using PracticeForTelerickUI01.Models;

namespace PracticeForTelerickUI01.Repository.EFCore
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options) { }
        

        public DbSet<Book> Books { get; set; }
        public DbSet<BookInformation> BooksInformation { get; set; }
    }
}
