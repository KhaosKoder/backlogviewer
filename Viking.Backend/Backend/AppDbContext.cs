using Microsoft.EntityFrameworkCore;


namespace TheVikingsApp.Backend
{
    public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<Transaction> Transactions { get; set; }
            public DbSet<Authentication> Authentications { get; set; }
        }
}
