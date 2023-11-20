using CurrencyExchange.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CurrencyExchange.Migrations;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CurrencyExchangeDbContext>
{
    public ServiceBinding ServiceBinding => WorklegramServiceRegistration.DbServiceBinding;

    public override string AssemblyName => ThisAssembly.AssemblyName;

    public CurrencyExchangeDbContext CreateDbContext(params string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CurrencyExchangeDbContext>();

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        optionsBuilder.UseNpgsql(
            config,
            ServiceBinding,
        builder =>
        builder.MigrationsAssembly(AssemblyName));
        return Activator.CreateInstance(typeof(CurrencyExchangeDbContext), optionsBuilder.Options);
    }
}

