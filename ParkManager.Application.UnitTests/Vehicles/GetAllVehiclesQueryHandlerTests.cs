using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Vehicles.Read;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Vehicles;

public class GetAllVehiclesQueryHandlerTests
{
  private readonly Mock<IVehicleRepository> _vehicleRepositoryMock;
  private readonly GetAllVehiclesQueryHandler _handler;

  public GetAllVehiclesQueryHandlerTests()
  {
    _vehicleRepositoryMock = new Mock<IVehicleRepository>();
    _handler = new GetAllVehiclesQueryHandler(_vehicleRepositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ShouldReturnSuccess_WithVehicleResponses()
  {
    // Arrange
    var vehicles = new List<Vehicle>
        {
            new Vehicle("Branch1", "Model1", "Color1", "Plate1", EVehicleType.Car),
            new Vehicle("Branch2", "Model2", "Color2", "Plate2", EVehicleType.Motorcycle)
        };

    _vehicleRepositoryMock
        .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(vehicles);

    var query = new GetAllVehiclesQuery();

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    var responses = result.Value.ToList();
    Assert.Equal(2, responses.Count);
    Assert.Contains(responses, r => r.Branch == "Branch1" && r.Model == "Model1");
    Assert.Contains(responses, r => r.Branch == "Branch2" && r.Model == "Model2");
    _vehicleRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
  }

  [Fact]
  public async Task Handle_ShouldReturnFailure_WhenRepositoryThrowsException()
  {
    // Arrange
    _vehicleRepositoryMock
        .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
        .ThrowsAsync(new Exception("DB error"));

    var query = new GetAllVehiclesQuery();

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("999", result.Error.Code);
    Assert.Contains("Error when retrieving vehicles", result.Error.Name);
    _vehicleRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
  }
}
