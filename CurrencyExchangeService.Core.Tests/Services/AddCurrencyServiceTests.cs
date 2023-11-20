using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;
using CurrencyExchangeService.Core.Models.Queries;
using CurrencyExchangeService.Core.Services;
using Moq;

namespace CurrencyExchangeService.Core.Tests.Services;

public class AddCurrencyServiceTests
{
    // sut
    private readonly AddCurrencyService _addCurrencyService;

    // depencies
    private readonly Mock<ICurrencyRepository> _mockCurrencyRepository;

    private const string Usd = "USD";

    public AddCurrencyServiceTests()
    {
        _mockCurrencyRepository = new Mock<ICurrencyRepository>();
        _addCurrencyService = new AddCurrencyService(_mockCurrencyRepository.Object);
    }

    [Test]
    public void AddCurrencyWithNonExistingBaseCurrencyThrowsAnException()
    {
        // Arrange
        var command = new AddCurrencyCommand(Usd);
        _mockCurrencyRepository
            .Setup(x => x.GetByCodeAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<CurrencyQuery?>(new CurrencyQuery(Guid.NewGuid().ToString(), Usd)));


        // Act & Assert
        Assert.ThrowsAsync<CurrencyNotSupportedException>(async () => await _addCurrencyService.Execute(command));
    }
}