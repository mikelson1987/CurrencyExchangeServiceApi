namespace CurrencyExchangeService.Core.Interfaces;

public interface ICommandService<TCommand>
{
    Task Execute(TCommand command);
}
