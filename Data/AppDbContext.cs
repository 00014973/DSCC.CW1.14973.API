using Microsoft.EntityFrameworkCore;
using DSCC.CW1._14973.API.Models;

namespace DSCC.CW1._14973.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
