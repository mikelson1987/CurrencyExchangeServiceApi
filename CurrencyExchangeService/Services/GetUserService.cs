using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Queries;

namespace CurrencyExchangeService.Core.Services;

public class GetUserService : IQueryService<string, UserQuery>
{
    private readonly IUserRepository _userRepository;

    public GetUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<UserQuery> Run(string? name)
    {
        return _userRepository.Get(name);
    }
}