using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Vehicles.Update;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Vehicles;

public class UpdateVehicleCommandHandlerTests
{
  private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
  private readonly UpdateVehicleCommandHandler _handler;

  public UpdateVehicleCommandHandlerTests()
  {
    _vehicleRepositoryMock = new Mock<IVehicleRepository>();
    _handler = new UpdateVehicleCommandHandler(_vehicleRepositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ShouldReturnFailure_WhenVehicleNotFound()
  {
    // Arrange
    var command = new UpdateVehicleCommand(
        Guid.NewGuid(), "Branch", "Model", "Plate", "Color", EVehicleType.Car
    );

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Vehicle)null);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("999", result.Error.Code);
    Assert.Equal("Vehicle not found!", result.Error.Name);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    _vehicleRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()), Times.Never);
  }

  [Fact]
  public async Task Handle_ShouldReturnSuccess_WhenVehicleUpdated()
  {
    // Arrange
    var vehicle = new Vehicle("OldBranch", "OldModel", "OldColor", "OldPlate", EVehicleType.Motorcycle);

    var command = new UpdateVehicleCommand(
        vehicle.Id, "Branch", "Model", "Plate", "Color", EVehicleType.Car
    );

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(vehicle);

    _vehicleRepositoryMock
        .Setup(r => r.UpdateAsync(vehicle, It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(vehicle.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(vehicle);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(command.Id, result.Value.Id);
    _vehicleRepositoryMock.Verify(r => r.UpdateAsync(vehicle, It.IsAny<CancellationToken>()), Times.Once);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicle.Id, It.IsAny<CancellationToken>()), Times.Exactly(2));
  }

  [Fact]
  public async Task Handle_ShouldReturnFailure_WhenUpdateThrowsException()
  {
    // Arrange
    var vehicle = new Vehicle("OldBranch", "OldModel", "OldColor", "OldPlate", EVehicleType.Motorcycle);

    var command = new UpdateVehicleCommand(
        vehicle.Id, "Branch", "Model", "Plate", "Color", EVehicleType.Car
    );

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(vehicle);

    _vehicleRepositoryMock
        .Setup(r => r.UpdateAsync(vehicle, It.IsAny<CancellationToken>()))
        .ThrowsAsync(new Exception("DB error"));

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("999", result.Error.Code);
    Assert.Contains("Error updating vehicle", result.Error.Name);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
    _vehicleRepositoryMock.Verify(r => r.UpdateAsync(vehicle, It.IsAny<CancellationToken>()), Times.Once);
  }
}
