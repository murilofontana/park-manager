using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Delete;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;
public class DeleteEstablishmentCommandHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _repositoryMock;
  private readonly DeleteEstablishmentCommandHandler _handler;

  public DeleteEstablishmentCommandHandlerTests()
  {
    _repositoryMock = new Mock<IEstablishmentRepository>();
    _handler = new DeleteEstablishmentCommandHandler(_repositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ShouldReturnFailure_WhenEstablishmentNotFound()
  {
    // Arrange
    var command = new DeleteEstablishmentCommand { Id = Guid.NewGuid() };
    _repositoryMock
        .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Establishment)null);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentNotFound", result.Error.Code);
    _repositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Establishment>(), It.IsAny<CancellationToken>()), Times.Never);
  }

  [Fact]
  public async Task Handle_ShouldReturnSuccess_WhenEstablishmentDeleted()
  {
    // Arrange
    var command = new DeleteEstablishmentCommand { Id = Guid.NewGuid() };
    var establishment = new Establishment(
        "Test Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );
    _repositoryMock
        .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);
    _repositoryMock
        .Setup(r => r.DeleteAsync(establishment, It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    _repositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    _repositoryMock.Verify(r => r.DeleteAsync(establishment, It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ShouldReturnFailure_WhenExceptionThrownDuringDelete()
  {
    // Arrange
    var command = new DeleteEstablishmentCommand { Id = Guid.NewGuid() };
    var establishment = new Establishment(
        "Test Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );
    _repositoryMock
        .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);
    _repositoryMock
        .Setup(r => r.DeleteAsync(establishment, It.IsAny<CancellationToken>()))
        .ThrowsAsync(new Exception("DB error"));

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentDelete", result.Error.Code);
    _repositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    _repositoryMock.Verify(r => r.DeleteAsync(establishment, It.IsAny<CancellationToken>()), Times.Once);
  }
}
