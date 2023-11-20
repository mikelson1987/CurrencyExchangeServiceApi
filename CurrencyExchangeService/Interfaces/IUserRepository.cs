using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.Interfaces;

public interface IUserRepository
{
    Task<UserId> AddAsync(UserRow model);

    List<UserQuery> Get(string? name);

    Task<UserQuery?> GetByIdAsync(string id);
}
