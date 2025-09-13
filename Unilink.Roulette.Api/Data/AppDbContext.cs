using Microsoft.EntityFrameworkCore;
using Unilink.Roulette.Api.models;

namespace Unilink.Roulette.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
{
    public DbSet<UserBalance> Users => Set<UserBalance>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<UserBalance>()
          .HasIndex(u => u.Name)
          .IsUnique(false);
        // Comparación case-insensitive: normalizamos a Upper al consultar/guardar.
    }
}
