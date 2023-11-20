using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.DbContext;

public class CurrencyRow
{
    public CurrencyRow(
        string id,
        string code)
    {
        Id = id;
        Code = code;
    }

    public string Id { get; }
    public string Code { get; set; }

    public static CurrencyQuery ToQuery(CurrencyRow row)
    {
        return new CurrencyQuery(row.Id, row.Code);
    }
}
