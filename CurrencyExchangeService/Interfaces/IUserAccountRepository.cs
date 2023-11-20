using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.Interfaces;

public interface IUserAccountRepository
{
    Task<UserAccountId> AddAsync(UserAccountRow model);
    Task<UserAccountId> UpdateAsync(UserAccountRow model);
    List<UserAccountQuery> Get(string userId);
    Task<UserAccountRow> GetByCurrencyCodeAsync(
        string userId, 
        string currencyCode, 
        string currencyId,
        decimal balance = default);
}
