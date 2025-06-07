using global::ParkManager.Application.Common;
using global::ParkManager.Application.Vehicles.Delete;
using global::ParkManager.Domain;
using Moq;

namespace ParkManager.Application.UnitTests.Vehicles
{


  public class DeleteVehicleCommandHandlerTests
  {
    private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
    private readonly DeleteVehicleCommandHandler _handler;

    public DeleteVehicleCommandHandlerTests()
    {
      _vehicleRepositoryMock = new Mock<IVehicleRepository>();
      _handler = new DeleteVehicleCommandHandler(_vehicleRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenVehicleNotFound()
    {
      // Arrange
      var command = new DeleteVehicleCommand(Guid.NewGuid());
      _vehicleRepositoryMock
          .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
          .ReturnsAsync((Vehicle)null);

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.True(result.IsFailure);
      Assert.Equal("999", result.Error.Code);
      Assert.Equal("Vechile not found!", result.Error.Name);

      _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
      _vehicleRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenVehicleDeleted()
    {
      // Arrange
      var command = new DeleteVehicleCommand(Guid.NewGuid());
      var vehicle = new Vehicle("Branch", "Model", "Color", "Plate", EVehicleType.Car);

      _vehicleRepositoryMock
          .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
          .ReturnsAsync(vehicle);

      _vehicleRepositoryMock
          .Setup(r => r.DeleteAsync(vehicle, It.IsAny<CancellationToken>()))
          .Returns(Task.CompletedTask);

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.True(result.IsSuccess);
      _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
      _vehicleRepositoryMock.Verify(r => r.DeleteAsync(vehicle, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenDeleteThrowsException()
    {
      // Arrange
      var vehicle = new Vehicle("Branch", "Model", "Color", "Plate", EVehicleType.Car);
      var command = new DeleteVehicleCommand(vehicle.Id);

      _vehicleRepositoryMock
          .Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
          .ReturnsAsync(vehicle);

      _vehicleRepositoryMock
          .Setup(r => r.DeleteAsync(vehicle, It.IsAny<CancellationToken>()))
          .ThrowsAsync(new Exception("DB error"));

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.True(result.IsFailure);
      Assert.Equal("999", result.Error.Code);
      Assert.Contains("Error deleting vechile", result.Error.Name);

      _vehicleRepositoryMock.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
      _vehicleRepositoryMock.Verify(r => r.DeleteAsync(vehicle, It.IsAny<CancellationToken>()), Times.Once);
    }
  }
}
