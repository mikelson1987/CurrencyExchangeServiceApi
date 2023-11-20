namespace CurrencyExchangeService.Core.Exceptions;

public class CurrencyNotFoundException : Exception
{
    public CurrencyNotFoundException(string currency) : base($"Currency code {currency} not found")
    {
    }
}
