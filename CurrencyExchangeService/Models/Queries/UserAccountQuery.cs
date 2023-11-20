namespace CurrencyExchangeService.Core.Models.Queries;

public record UserAccountQuery(string Id, string Name, string userName, string CurrencyCode, decimal Balance);