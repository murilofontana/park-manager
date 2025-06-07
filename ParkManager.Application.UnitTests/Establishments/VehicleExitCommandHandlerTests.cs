using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.VehicleEntry;
using ParkManager.Application.Establishments.VehicleExit;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;
public class VehicleExitCommandHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _establishmentRepositoryMock;
  private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
  private readonly VehicleExitCommandHandler _handler;
  private readonly VehicleEntryCommandHandler _entryHandler;

  public VehicleExitCommandHandlerTests()
  {
    _establishmentRepositoryMock = new Mock<IEstablishmentRepository>();
    _vehicleRepositoryMock = new Mock<IVehicleRepository>();
    _handler = new VehicleExitCommandHandler(_establishmentRepositoryMock.Object, _vehicleRepositoryMock.Object);
    _entryHandler = new VehicleEntryCommandHandler(_establishmentRepositoryMock.Object, _vehicleRepositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenEstablishmentNotFound()
  {
    // Arrange
    var command = new VehicleExitCommand(Guid.NewGuid(), Guid.NewGuid());
    _establishmentRepositoryMock
        .Setup(r => r.GetByIdAsync(command.EstablishmentId, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Establishment)null);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentNotFound", result.Error.Code);
    _establishmentRepositoryMock.Verify(r => r.GetByIdAsync(command.EstablishmentId, It.IsAny<CancellationToken>()), Times.Once);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenVehicleNotFound()
  {
    // Arrange
    var establishmentId = Guid.NewGuid();
    var vehicleId = Guid.NewGuid();
    var command = new VehicleExitCommand(establishmentId, vehicleId);

    var establishment = new Establishment(
        "Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );

    _establishmentRepositoryMock
        .Setup(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Vehicle)null);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("VehicleNotFound", result.Error.Code);
    _establishmentRepositoryMock.Verify(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()), Times.Once);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ReturnsSuccess_WhenExitSucceeds()
  {
    // Arrange
    var establishment = new Establishment(
        "Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );
    var vehicle = new Vehicle("Branch", "Model", "Color", "Plate", EVehicleType.Car);

    var establishmentId = establishment.Id;
    var vehicleId = vehicle.Id;

    var entryCommand = new VehicleEntryCommand(establishmentId, vehicleId);
    var exitCommand = new VehicleExitCommand(establishmentId, vehicleId);

    _establishmentRepositoryMock
        .Setup(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(vehicle);

    _establishmentRepositoryMock
        .Setup(r => r.UpdateAsync(establishment, It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

    // Act: First, perform entry
    var entryResult = await _entryHandler.Handle(entryCommand, CancellationToken.None);
    Assert.True(entryResult.IsSuccess);

    // Act: Then, perform exit
    var exitResult = await _handler.Handle(exitCommand, CancellationToken.None);

    // Assert
    Assert.True(exitResult.IsSuccess);
    _establishmentRepositoryMock.Verify(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()), Times.Exactly(2));
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()), Times.Exactly(2));
    _establishmentRepositoryMock.Verify(r => r.UpdateAsync(establishment, It.IsAny<CancellationToken>()), Times.Exactly(2));
  }

}
