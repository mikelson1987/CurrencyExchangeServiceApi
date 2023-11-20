using CurrencyExchangeService.Core.DbContext;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Infrastructure.Data;

public class CurrencyExchangeDbContext : DbContext
{
    public CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "CurrencyExchange");
    }

    public DbSet<UserRow> Users { get; set; } = null!;
    public DbSet<CurrencyRateRow> CurrencyRates { get; set; } = null!;
    public DbSet<CurrencyRow> Currencies { get; set; } = null!;
    public DbSet<UserAccountRow> UserAccounts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("CurrencyExchange");

        BuildUserRow(modelBuilder);
        BuildCurrencyRow(modelBuilder);
        BuildCurrencyRateRow(modelBuilder);
        BuildUserAccountRow(modelBuilder);
    }

    private static void BuildUserRow(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserRow>()
            .HasKey(c => c.Id);
        modelBuilder
            .Entity<UserRow>()
            .Property(c => c.Name);
    }

    private static void BuildCurrencyRow(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<CurrencyRow>()
            .HasKey(c => c.Id);
        modelBuilder
            .Entity<CurrencyRow>()
            .Property(c => c.Code);
        modelBuilder
            .Entity<CurrencyRow>()
            .HasIndex(c => c.Code)
            .IsUnique();
    }

    private static void BuildCurrencyRateRow(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<CurrencyRateRow>()
            .HasKey(c => c.Id);
        modelBuilder
            .Entity<CurrencyRateRow>()
            .HasOne(r => r.BaseCurrency)
            .WithMany()
            .HasForeignKey(c => c.BaseCurrencyId);
        modelBuilder
            .Entity<CurrencyRateRow>()
            .HasOne(r => r.TargetCurrency)
            .WithMany()
            .HasForeignKey(c => c.TargetCurrencyId);
        modelBuilder
            .Entity<CurrencyRateRow>()
            .HasIndex(row => new { row.BaseCurrencyId, row.TargetCurrencyId });
        modelBuilder
            .Entity<CurrencyRateRow>()
            .Property(c => c.Rate);
    }

    private static void BuildUserAccountRow(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserAccountRow>()
            .HasKey(c => c.Id);
        modelBuilder
            .Entity<UserAccountRow>()
            .Property(c => c.AccountName);
        modelBuilder
            .Entity<UserAccountRow>()
            .Property(c => c.UserId);
        modelBuilder
            .Entity<UserAccountRow>()
            .Property(c => c.CurrencyId);
        modelBuilder
            .Entity<UserAccountRow>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(c => c.UserId);
        modelBuilder
            .Entity<UserAccountRow>()
            .HasOne(r => r.Currency)
            .WithMany()
            .HasForeignKey(c => c.CurrencyId);
        modelBuilder
            .Entity<UserAccountRow>()
            .Property(c => c.Balance)
            .HasPrecision(19, 2);
    }
}