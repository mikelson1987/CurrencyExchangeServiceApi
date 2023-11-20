using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Infrastructure.Data;

public class UserAccountRepository : IUserAccountRepository
{
    private readonly CurrencyExchangeDbContext _dbContext;

    public UserAccountRepository(CurrencyExchangeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserAccountId> AddAsync(UserAccountRow model)
    {
        await _dbContext.UserAccounts.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return new(model.Id);
    }

    public async Task<UserAccountId> UpdateAsync(UserAccountRow model)
    {
        _dbContext.UserAccounts.Update(model);
        await _dbContext.SaveChangesAsync();

        return new(model.Id);
    }

    public List<UserAccountQuery> Get(string name)
    {
        return _dbContext.UserAccounts
            .AsNoTracking()
            .Where(c => c.AccountName == name)
            .Select(UserAccountRow.ToQuery)
            .ToList();
    }

    public async Task<UserAccountRow> GetByCurrencyCodeAsync(
        string userId, 
        string currencyCode, 
        string currencyId,
        decimal balance = default)
    {
        var userAccountRow = await _dbContext.UserAccounts
            .Include(x => x.Currency)
            .AsNoTracking()
            .AsSingleQuery()
            .FirstOrDefaultAsync(c => c.Currency != null && c.Currency.Code == currencyCode);

        if (userAccountRow == null)
        {
            userAccountRow = new UserAccountRow(
                Guid.NewGuid().ToString(),
                $"Account_{currencyCode}",
                userId,
                currencyId,
                balance);
            await AddAsync(userAccountRow);
        }

        return userAccountRow;
    }
}