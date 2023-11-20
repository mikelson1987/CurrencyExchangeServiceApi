using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.DbContext;

public class UserRow
{
    public UserRow(
        string id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public string Id { get; }
    public string Name { get; set; }

    public static UserQuery ToQuery(UserRow row)
    {
        return new UserQuery(row.Id, row.Name);
    }
}
