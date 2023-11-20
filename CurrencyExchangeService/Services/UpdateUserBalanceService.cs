using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;

namespace CurrencyExchangeService.Core.Services;

public class UpdateUserBalanceService : ICommandService<UpdateUserBalanceCommand>
{
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IUserAccountRepository _userAccountRepository;

    public UpdateUserBalanceService(
        ICurrencyRepository currencyRepository,
        IUserAccountRepository userAccountRepository)
    {
        _currencyRepository = currencyRepository;
        _userAccountRepository = userAccountRepository;
    }

    public async Task Execute(UpdateUserBalanceCommand command)
    {
        if (command.Value <= 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(command.Value)} must be greater then 0.");
        }

        if (command.Commission <= 0)
        {
            throw new ArgumentOutOfRangeException($"{nameof(command.Commission)} must be greater then 0.");
        }

        var baseCurrency = await _currencyRepository.GetByCodeAsync(command.BaseCurrencyCode);

        if (baseCurrency == null)
        {
            throw new CurrencyNotFoundException(command.BaseCurrencyCode);
        }

        var targetCurrency = await _currencyRepository.GetByCodeAsync(command.TargetCurrencyCode);

        if (targetCurrency == null)
        {
            throw new CurrencyNotFoundException(command.TargetCurrencyCode);
        }

        var baseAccount = await _userAccountRepository.GetByCurrencyCodeAsync(
            command.UserId, 
            command.BaseCurrencyCode,
            baseCurrency.Id);

        var newBaseBalance = baseAccount.Balance - command.Value;
        if (newBaseBalance <= 0)
        {
            throw new ArgumentOutOfRangeException("Insufficient funds in the base account");
        }

        baseAccount.Balance = newBaseBalance;
        await _userAccountRepository.UpdateAsync(baseAccount);

        var targetAccount = await _userAccountRepository.GetByCurrencyCodeAsync(
            command.UserId,
            command.TargetCurrencyCode,
            targetCurrency.Id);

        var convertedValue = command.Value * command.Rate;
        var newTargetBalance = targetAccount.Balance + convertedValue - convertedValue * command.Commission;

        targetAccount.Balance = newTargetBalance;
        await _userAccountRepository.UpdateAsync(targetAccount);
    }
}