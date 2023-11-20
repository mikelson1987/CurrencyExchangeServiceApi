using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.DbContext;

public class UserAccountRow
{
    public UserAccountRow(
        string id, 
        string accountName, 
        string userId, 
        string currencyId,
        decimal balance)
    {
        Id = id;
        AccountName = accountName;
        UserId = userId;
        CurrencyId = currencyId;
        Balance = balance;
    }

    public string Id { get; }
    public string AccountName { get; set; }
    public string UserId { get; set; }
    public string CurrencyId { get; set; }
    public decimal Balance { get; set; }

    public UserRow? User { get; set; }
    public CurrencyRow? Currency { get; set; }

    public static UserAccountQuery ToQuery(UserAccountRow row)
    {
        return new UserAccountQuery(
            row.Id, 
            row.AccountName, 
            row.User?.Name ?? string.Empty,
            row.Currency?.Code ?? string.Empty,
            row.Balance);
    }
}