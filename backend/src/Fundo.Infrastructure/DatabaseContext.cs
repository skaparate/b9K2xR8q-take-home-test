using Fundo.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Loan> Loans { get; set; }

    public DbSet<AccountHolder> AccountHolders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure precision and scale for Loan entities
        modelBuilder.Entity<Loan>(entity =>
        {
            entity.Property(l => l.AmountRequested)
                .HasPrecision(19, 4);

            entity.Property(l => l.AmountPaid)
                .HasPrecision(19, 4);
        });
    }
}