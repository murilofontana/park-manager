using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Update;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;
public class UpdateEstablishmentCommandHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _repositoryMock;
  private readonly UpdateEstablishmentCommandHandler _handler;

  public UpdateEstablishmentCommandHandlerTests()
  {
    _repositoryMock = new Mock<IEstablishmentRepository>();
    _handler = new UpdateEstablishmentCommandHandler(_repositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenEstablishmentNotFound()
  {
    // Arrange
    var command = new UpdateEstablishmentCommand(
        "Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10, Guid.NewGuid()
    );
    _repositoryMock
        .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Establishment)null);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentNotFound", result.Error.Code);
  }

  [Fact]
  public async Task Handle_ReturnsSuccess_WhenUpdateSucceeds()
  {
    // Arrange
    var establishment = new Establishment(
        "OldName", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );

    var id = establishment.Id;

    var command = new UpdateEstablishmentCommand(
       "UpdatedName", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10, id
    );

    _repositoryMock
        .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    _repositoryMock
        .Setup(r => r.UpdateAsync(establishment, It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

    _repositoryMock
        .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal("UpdatedName", result.Value.Name);
    _repositoryMock.Verify(r => r.UpdateAsync(establishment, It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenExceptionThrownDuringUpdate()
  {
    // Arrange
    var id = Guid.NewGuid();
    var command = new UpdateEstablishmentCommand(
        "Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10, id
    );
    var establishment = new Establishment(
        "OldName", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );

    _repositoryMock
        .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    _repositoryMock
        .Setup(r => r.UpdateAsync(establishment, It.IsAny<CancellationToken>()))
        .ThrowsAsync(new Exception("DB error"));

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentUpdate", result.Error.Code);
  }
}
