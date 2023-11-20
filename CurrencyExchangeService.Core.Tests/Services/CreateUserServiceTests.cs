using CurrencyExchangeService.Core.Exceptions;
using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Core.Models.Commands;
using CurrencyExchangeService.Core.Models.Queries;
using CurrencyExchangeService.Core.Services;
using Moq;

namespace CurrencyExchangeService.Core.Tests.Services;

public class CreateUserServiceTests
{
    // sut
    private readonly CreateUserService _createUserService;

    // depencies
    private readonly Mock<IUserRepository> _mockUserRepository;

    private const string TestUserName = "TestUser";

    public CreateUserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _createUserService = new CreateUserService(_mockUserRepository.Object);
    }

    [Test]
    public void AddCurrencyWithNonExistingBaseCurrencyThrowsAnException()
    {
        // Arrange
        var command = new CreateUserCommand(TestUserName);
        _mockUserRepository
            .Setup(x => x.Get(It.IsAny<string?>()))
            .Returns(new List<UserQuery>() { new(Guid.NewGuid().ToString(), TestUserName) });

        // Act & Assert
        Assert.ThrowsAsync<UserNotSupportedException>(async () => await _createUserService.Execute(command));
    }
}