using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;

namespace CurrencyExchangeService.Core.Services;

public class AddCurrencyService : ICommandService<AddCurrencyCommand>
{
    private readonly ICurrencyRepository _currencyRepository;

    public AddCurrencyService(ICurrencyRepository currencyRepository)
    {
        _currencyRepository = currencyRepository;
    }

    public async Task Execute(AddCurrencyCommand command)
    {
        var currency = await _currencyRepository.GetByCodeAsync(command.Currency);
        if (currency != null)
        {
            throw new CurrencyNotSupportedException(command.Currency);
        }

        var currencyRow = new CurrencyRow(Guid.NewGuid().ToString(), command.Currency);

        await _currencyRepository.AddAsync(currencyRow);
    }
}
