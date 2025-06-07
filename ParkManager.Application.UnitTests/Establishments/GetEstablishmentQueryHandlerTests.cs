using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Read;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;
public class GetEstablishmentQueryHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _repositoryMock;
  private readonly GetEstablishmentQueryHandler _handler;

  public GetEstablishmentQueryHandlerTests()
  {
    _repositoryMock = new Mock<IEstablishmentRepository>();
    _handler = new GetEstablishmentQueryHandler(_repositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ReturnsSuccess_WhenEstablishmentExists()
  {
    // Arrange
    var establishment = new Establishment(
        "Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10
    );
    var id = establishment.Id;

    _repositoryMock
        .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishment);

    var query = new GetEstablishmentQuery { Id = id };

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Equal(id, result.Value.Id);
    Assert.Equal("Name", result.Value.Name);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenEstablishmentNotFound()
  {
    // Arrange
    var id = Guid.NewGuid();
    _repositoryMock
        .Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
        .ReturnsAsync((Establishment)null);

    var query = new GetEstablishmentQuery { Id = id };

    // Act
    var result = await _handler.Handle(query, CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentNotFound", result.Error.Code);
  }
}
