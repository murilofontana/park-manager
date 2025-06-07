using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Create;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;
public class CreateEstablishmentCommandHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _repositoryMock;
  private readonly CreateEstablishmentCommandHandler _handler;

  public CreateEstablishmentCommandHandlerTests()
  {
    _repositoryMock = new Mock<IEstablishmentRepository>();
    _handler = new CreateEstablishmentCommandHandler(_repositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ShouldReturnSuccess_WhenEstablishmentIsCreated()
  {
    // Arrange
    var command = new CreateEstablishmentCommand(
        "Test Name",
        "12345678000199",
        "Test City",
        "Test State",
        "Test Street",
        "123",
        "Apt 1",
        "12345-678",
        "1234567890",
        10,
        20
    );

    var establishment = new Establishment(
        command.Name,
        command.Cnpj,
        command.City,
        command.State,
        command.Street,
        command.Number,
        command.Complement,
        command.ZipCode,
        command.Phone,
        command.MotorcyclesParkingSpaces,
        command.CarsParkingSpaces
    );

    _repositoryMock
        .Setup(r => r.AddAsync(It.IsAny<Establishment>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

    _repositoryMock
        .Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(command.Name, result.Value.Name);
    _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Establishment>(), It.IsAny<CancellationToken>()), Times.Once);
    _repositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ShouldReturnFailure_WhenExceptionIsThrown()
  {
    // Arrange
    var command = new CreateEstablishmentCommand(
        "Test Name",
        "12345678000199",
        "Test City",
        "Test State",
        "Test Street",
        "123",
        "Apt 1",
        "12345-678",
        "1234567890",
        10,
        20
    );

    _repositoryMock
        .Setup(r => r.AddAsync(It.IsAny<Establishment>(), It.IsAny<CancellationToken>()))
        .ThrowsAsync(new Exception("DB error"));

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentCreate", result.Error.Code);
  }
}
