using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;
using CurrencyExchangeService.Core.Models.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeService.App.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ICommandService<CreateUserCommand> _createUserService;
    private readonly ICommandService<UpdateUserBalanceCommand> _updateUserBalanceService;
    private readonly IQueryService<string, UserQuery> _userQueryService;

    public UserController(
        ICommandService<CreateUserCommand> createUserService,
        ICommandService<UpdateUserBalanceCommand> updateUserBalanceService,
        IQueryService<string, UserQuery> userQueryService)
        
    {
        _createUserService = createUserService;
        _updateUserBalanceService = updateUserBalanceService;
        _userQueryService = userQueryService;
    }

    /// <summary>
    /// Get the list of users
    /// </summary>
    /// <param name="name">User name</param>
    /// <returns></returns>
    [HttpGet]
    public IEnumerable<UserQuery> Get(string? name)
    {
        return _userQueryService.Run(name);
    }

    /// <summary>
    /// Add a new user
    /// </summary>
    /// <param name="model"></param>
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand model)
    {
        await _createUserService.Execute(model);

        return Ok();
    }

    /// <summary>
    /// Update user balance 
    /// </summary>
    /// <param name="model"></param>
    [HttpPost("balance")]
    public async Task<IActionResult> UpdateUserBalance(UpdateUserBalanceCommand model)
    {
        await _updateUserBalanceService.Execute(model);

        return Ok();
    }
}
