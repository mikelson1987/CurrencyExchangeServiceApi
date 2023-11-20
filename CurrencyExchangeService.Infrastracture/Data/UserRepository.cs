using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Queries;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly CurrencyExchangeDbContext _dbContext;

    public UserRepository(CurrencyExchangeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserId> AddAsync(UserRow model)
    {
        await _dbContext.Users.AddAsync(model);
        await _dbContext.SaveChangesAsync();

        return new(model.Id);
    }

    public List<UserQuery> Get(string? name)
    {
        return _dbContext.Users
            .AsNoTracking()
            .Where(c => name == null || c.Name == name)
            .Select(UserRow.ToQuery)
            .ToList();
    }

    public async Task<UserQuery?> GetByIdAsync(string id)
    {
        var userRow = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return userRow != null ? UserRow.ToQuery(userRow) : null;
    }
}