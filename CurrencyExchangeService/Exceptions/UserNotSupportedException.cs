namespace CurrencyExchangeService.Core.Exceptions;

public class UserNotSupportedException : Exception
{
    public UserNotSupportedException(string name) : base($"{name} user is not supported")
    {
    }
}