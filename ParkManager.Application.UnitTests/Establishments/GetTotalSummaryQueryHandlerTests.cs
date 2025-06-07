using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Summary.TotalSumary;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;
public class GetTotalSummaryQueryHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _repositoryMock;
  private readonly GetTotalSummaryQueryHandler _handler;

  public GetTotalSummaryQueryHandlerTests()
  {
    _repositoryMock = new Mock<IEstablishmentRepository>();
    _handler = new GetTotalSummaryQueryHandler(_repositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ReturnsSuccess_WhenEstablishmentExists()
  {
    // Arrange
    var establishmentId = Guid.NewGuid();
    var establishment = new Establishment(
        "Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );

    _repositoryMock
        .Setup(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    _repositoryMock.Setup(r => r.GetTotalCarsEntry(establishmentId)).ReturnsAsync(10);
    _repositoryMock.Setup(r => r.GetTotalCarsExit(establishmentId)).ReturnsAsync(8);
    _repositoryMock.Setup(r => r.GetTotalMotorcyclesEntry(establishmentId)).ReturnsAsync(5);
    _repositoryMock.Setup(r => r.GetTotalMotorcyclesExit(establishmentId)).ReturnsAsync(3);

    var query = new GetTotalSummaryQuery(establishmentId);

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(10, result.Value.TotalCarsEntry);
    Assert.Equal(8, result.Value.TotalCarsExit);
    Assert.Equal(5, result.Value.TotalMotorcyclesEntry);
    Assert.Equal(3, result.Value.TotalMotorcyclesExit);
    Assert.Equal(15, result.Value.TotalGeneralEntry);
    Assert.Equal(11, result.Value.TotalGeneralExit);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenEstablishmentNotFound()
  {
    // Arrange
    var establishmentId = Guid.NewGuid();
    _repositoryMock
        .Setup(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Establishment)null);

    var query = new GetTotalSummaryQuery(establishmentId);

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("999", result.Error.Code);
  }
}
