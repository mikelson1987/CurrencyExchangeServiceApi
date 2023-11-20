namespace CurrencyExchangeService.Core.DbContext;

public class CurrencyRateRow
{
    public CurrencyRateRow(string id, string baseCurrencyId, string targetCurrencyId, decimal rate)
    {
        Id = id;
        BaseCurrencyId = baseCurrencyId;
        TargetCurrencyId = targetCurrencyId;
        Rate = rate;
    }

    public string Id { get; set; }
    public string BaseCurrencyId { get; set; }
    public string TargetCurrencyId { get; set; }
    public decimal Rate { get; set; }

    public CurrencyRow? BaseCurrency { get; init; }
    public CurrencyRow? TargetCurrency { get; init; }
}
