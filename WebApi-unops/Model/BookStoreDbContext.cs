using Microsoft.EntityFrameworkCore;

namespace WebApi_unops.Model
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        { }

        public DbSet<BookItem> BookItems { get; set; }
        public DbSet<Authors> Authors { get; set; }
    }
}
