using System.Diagnostics.Metrics;
using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;
using CurrencyExchangeService.Core.Models.Queries;
using CurrencyExchangeService.Core.Services;
using Moq;
using Newtonsoft.Json.Linq;

namespace UnitTests;

public class UpdateUserBalanceServiceTests
{
    // sut
    private UpdateUserBalanceService _updateUserBalanceService;

    // dependencies
    private Mock<ICurrencyRepository> _mockCurrencyRepository;
    private Mock<IUserAccountRepository> _mockUserRepository;

    private readonly string _testUserId;

    private const string Usd = "USD";
    private const string Rub = "RUB";

    public UpdateUserBalanceServiceTests()
    {
        _mockCurrencyRepository = new Mock<ICurrencyRepository>();
        _mockUserRepository = new Mock<IUserAccountRepository>();
        _updateUserBalanceService = new UpdateUserBalanceService(_mockCurrencyRepository.Object, _mockUserRepository.Object);

        _testUserId = Guid.NewGuid().ToString();
    }

    [TestCase(10, 0.6, 0)]
    [TestCase(-10, 0.6, 0)]
    public void UpdateUpdateUserBalanceValueOrCommissionOutOfRangeThrowsAnException(decimal value, decimal rate, decimal commission = 0.05m)
    {
        // Arrange
        var command = new UpdateUserBalanceCommand(_testUserId, Usd, Rub, value, rate, commission);

        _mockCurrencyRepository
            .Setup(x => x.GetByCodeAsync(It.Is<string>(c => c == Usd)))
            .Returns(Task.FromResult<CurrencyQuery?>(new(Guid.NewGuid().ToString(), Usd)));
        _mockCurrencyRepository
           .Setup(x => x.GetByCodeAsync(It.Is<string>(c => c == Rub)))
           .Returns(Task.FromResult<CurrencyQuery?>(new(Guid.NewGuid().ToString(), Rub)));

        // Act & Assert
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await _updateUserBalanceService.Execute(command));
    }

    [Test]
    public void UpdateUpdateUserBalanceValueOrCommissionBaseCurrencyNotFound()
    {
        // Arrange
        var command = new UpdateUserBalanceCommand(_testUserId, Usd, Rub, 10m, 0.6m, 0.05m);


        // Act & Assert
        Assert.ThrowsAsync<CurrencyNotFoundException>(async () => await _updateUserBalanceService.Execute(command));
    }

    [Test]
    public void UpdateUpdateUserBalanceValueOrCommissionTargetCurrencyNotFound()
    {
        // Arrange
        var command = new UpdateUserBalanceCommand(_testUserId, Usd, Rub, 10m, 0.6m, 0.05m);
        _mockCurrencyRepository
            .Setup(x => x.GetByCodeAsync(It.Is<string>(c => c == Usd)))
            .Returns(Task.FromResult<CurrencyQuery?>(new(Guid.NewGuid().ToString(), Usd)));

        // Act & Assert
        Assert.ThrowsAsync<CurrencyNotFoundException>(async () => await _updateUserBalanceService.Execute(command));
    }
}