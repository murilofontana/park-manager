using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Vehicles.Read;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Vehicles;

public class GetVehicleQueryHandlerTests
{
  private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
  private readonly GetVehicleQueryHandler _handler;

  public GetVehicleQueryHandlerTests()
  {
    _vehicleRepositoryMock = new Mock<IVehicleRepository>();
    _handler = new GetVehicleQueryHandler(_vehicleRepositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ShouldReturnSuccess_WhenVehicleExists()
  {
    // Arrange
    var vehicle = new Vehicle("Branch", "Model", "Color", "Plate", EVehicleType.Car);
    var vehicleId = vehicle.Id;

    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(vehicle);

    var query = new GetVehicleQuery { Id = vehicleId };

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(vehicleId, result.Value.Id);
    Assert.Equal(vehicle.Branch, result.Value.Branch);
    Assert.Equal(vehicle.Model, result.Value.Model);
    Assert.Equal(vehicle.Color, result.Value.Color);
    Assert.Equal(vehicle.Plate, result.Value.Plate);
    Assert.Equal(vehicle.Type, result.Value.Type);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ShouldReturnFailure_WhenVehicleDoesNotExist()
  {
    // Arrange
    var vehicleId = Guid.NewGuid();
    _vehicleRepositoryMock
        .Setup(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Vehicle)null);

    var query = new GetVehicleQuery { Id = vehicleId };

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("404", result.Error.Code);
    Assert.Equal("Vehicle not found", result.Error.Name);
    _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(vehicleId, It.IsAny<CancellationToken>()), Times.Once);
  }
}

