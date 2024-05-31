using CashManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashManager.Infrastructure;

public class CashManagerDbContext(DbContextOptions<CashManagerDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<PasswordHistory> PasswordHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase("CashManagerInMemoryDb");
        }
    }
}
