using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.Services;

public class GetCurrencyService : IQueryService<string, CurrencyQuery>
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetCurrencyService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public IEnumerable<CurrencyQuery> Run(string? currency)
    {
        return _currencyRepository.Get(currency);
    }
}
