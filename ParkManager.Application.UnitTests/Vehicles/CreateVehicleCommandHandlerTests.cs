using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Vehicles.Create;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Vehicles;

public class CreateVehicleCommandHandlerTests
{
  private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
  private readonly CreateVehicleCommandHandler _handler;

  public CreateVehicleCommandHandlerTests()
  {
    _vehicleRepositoryMock = new Mock<IVehicleRepository>();
    _handler = new CreateVehicleCommandHandler(_vehicleRepositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ShouldReturnSuccessResult_WhenVehicleIsCreated()
  {
    // Arrange
    var command = new CreateVehicleCommand(
        branch: "Main",
        model: "ModelX",
        plate: "ABC123",
        color: "Red",
        type: EVehicleType.Car
    );

    var createdVehicle = new Vehicle(
        command.Branch,
        command.Model,
        command.Color,
        command.Plate,
        command.Type
    );

    // Simulate repository assigns an Id
    var vehicleId = createdVehicle.Id;

    _vehicleRepositoryMock
        .Setup(r => r.AddAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()))
        .Returns(Task.CompletedTask);
        //.Callback<Vehicle, CancellationToken>((v, _) => v.Id = vehicleId);

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(createdVehicle);

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(vehicleId, result.Value.Id);
    Assert.Equal(command.Branch, result.Value.Branch);
    Assert.Equal(command.Model, result.Value.Model);
    Assert.Equal(command.Color, result.Value.Color);
    Assert.Equal(command.Plate, result.Value.Plate);
    Assert.Equal(command.Type, result.Value.Type);

    _vehicleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()), Times.Once);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ShouldReturnFailureResult_WhenExceptionIsThrown()
  {
    // Arrange
    var command = new CreateVehicleCommand(
        branch: "Main",
        model: "ModelX",
        plate: "ABC123",
        color: "Red",
        type: EVehicleType.Car
    );

    _vehicleRepositoryMock
        .Setup(r => r.AddAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()))
        .ThrowsAsync(new Exception("DB error"));

    // Act
    var result = await _handler.Handle(command, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("999", result.Error.Code);
    Assert.Equal("Error When creating Vehicle", result.Error.Name);

    _vehicleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()), Times.Once);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
  }
}