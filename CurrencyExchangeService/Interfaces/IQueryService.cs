namespace CurrencyExchangeService.Core.Interfaces;

public interface IQueryService<TQuery, TResult>
{
    IEnumerable<TResult> Run(TQuery? query);
}
