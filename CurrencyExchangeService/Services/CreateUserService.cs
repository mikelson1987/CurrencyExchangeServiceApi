using CurrencyExchangeService.Core.DbContext;
using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;

namespace CurrencyExchangeService.Core.Services;

public class CreateUserService : ICommandService<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Execute(CreateUserCommand command)
    {
        var users = _userRepository.Get(command.UserName);
        if (users.Any())
        {
            throw new UserNotSupportedException(command.UserName);
        }

        var userRow = new UserRow(
            Guid.NewGuid().ToString(),
            command.UserName);

        await _userRepository.AddAsync(userRow);
    }
}
