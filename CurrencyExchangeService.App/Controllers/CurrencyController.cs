using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;
using CurrencyExchangeService.Core.Models.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeService.App.Controllers;

[ApiController]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICommandService<AddCurrencyCommand> _addCurrencyService;
    private readonly IQueryService<string, CurrencyQuery> _currencyQueryService;

    public CurrencyController(
        ICommandService<AddCurrencyCommand> addCurrencyService,
        IQueryService<string, CurrencyQuery> currencyQueryService)
        
    {
        _addCurrencyService = addCurrencyService;
        _currencyQueryService = currencyQueryService;
    }

    /// <summary>
    /// Get the list of currencies
    /// </summary>
    /// <param name="currency">Currency code</param>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<IEnumerable<CurrencyQuery>> Get(string? currency)
    {
        var currencies = _currencyQueryService.Run(currency);
        return Ok(currencies);
    }

    /// <summary>
    /// Add a new currency.
    /// </summary>
    /// <param name="model"></param>
    [HttpPost]
    public async Task<IActionResult> Post(AddCurrencyCommand model)
    {
        await _addCurrencyService.Execute(model);

        return Ok();
    }
}