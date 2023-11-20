using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Infrastructure.Data;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly CurrencyExchangeDbContext _dbContext;

    public CurrencyRepository(CurrencyExchangeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CurrencyId> AddAsync(CurrencyRow currencyModel)
    {
        await _dbContext.Currencies.AddAsync(currencyModel);
        await _dbContext.SaveChangesAsync();

        return new(currencyModel.Id);
    }

    public List<CurrencyQuery> Get(string? code)
    {
        return _dbContext.Currencies
            .AsNoTracking()
            .Where(c => code == null || c.Code == code)
            .Select(CurrencyRow.ToQuery)
            .ToList();
    }

    public async Task<CurrencyQuery?> GetByCodeAsync(string code)
    {
        var currencyRow = await _dbContext.Currencies
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Code == code);

        return currencyRow != null ? CurrencyRow.ToQuery(currencyRow) : null;
    }
}