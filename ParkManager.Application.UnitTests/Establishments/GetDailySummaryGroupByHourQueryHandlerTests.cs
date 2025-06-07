using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Summary.DailySummaryGroupedByHour;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;

public class GetDailySummaryGroupByHourQueryHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _repositoryMock;
  private readonly GetDailySummaryGroupByHourQueryHandler _handler;

  public GetDailySummaryGroupByHourQueryHandlerTests()
  {
    _repositoryMock = new Mock<IEstablishmentRepository>();
    _handler = new GetDailySummaryGroupByHourQueryHandler(_repositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ReturnsSuccess_WhenEstablishmentExists()
  {
    // Arrange
    var date = new DateOnly(2024, 6, 1);
    var establishment = new Establishment(
        "Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );
    var establishmentId = establishment.Id;

    var hourSummaries = new List<HourSummaryResponse>
        {
            new HourSummaryResponse(8, 1, 0, 0, 0),
            new HourSummaryResponse(9, 2, 1, 0, 0)
        };

    _repositoryMock
        .Setup(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    _repositoryMock
        .Setup(r => r.GetDailySummaryGroupedByHourAsync(establishmentId, date, It.IsAny<CancellationToken>()))
        .ReturnsAsync(hourSummaries);

    var query = new GetDailySummaryGroupByHourQuery(establishmentId, date);

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(establishmentId, result.Value.EstablishmentId);
    Assert.Equal(date, result.Value.Date);
    Assert.Equal(2, result.Value.HourSummaries.Count);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenEstablishmentNotFound()
  {
    // Arrange
    var establishmentId = Guid.NewGuid();
    var date = new DateOnly(2024, 6, 1);

    _repositoryMock
        .Setup(r => r.GetByIdAsync(establishmentId, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Establishment)null);

    var query = new GetDailySummaryGroupByHourQuery(establishmentId, date);

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentNotFound", result.Error.Code);
  }
}
