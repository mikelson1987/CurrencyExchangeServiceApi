using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.Interfaces;

public interface ICurrencyRepository
{
    Task<CurrencyId> AddAsync(CurrencyRow model);
    List<CurrencyQuery> Get(string? code);
    Task<CurrencyQuery?> GetByCodeAsync(string code);
}