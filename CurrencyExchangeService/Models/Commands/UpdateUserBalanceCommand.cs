namespace CurrencyExchangeService.Core.Models.Commands;

public record UpdateUserBalanceCommand(
    string UserId,
    string BaseCurrencyCode,
    string TargetCurrencyCode,
    decimal Value,
    decimal Rate,
    decimal Commission = 0.05m);
