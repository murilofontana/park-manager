using Moq;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Read;
using ParkManager.Domain;

namespace ParkManager.Application.UnitTests.Establishments;
public class GetAllEstablishmentQueryHandlerTests
{
  private readonly Mock<IEstablishmentRepository> _repositoryMock;
  private readonly GetAllEstablishmentQueryHandler _handler;

  public GetAllEstablishmentQueryHandlerTests()
  {
    _repositoryMock = new Mock<IEstablishmentRepository>();
    _handler = new GetAllEstablishmentQueryHandler(_repositoryMock.Object);
  }

  [Fact]
  public async Task Handle_ReturnsSuccess_WhenEstablishmentsExist()
  {
    // Arrange
    var establishments = new List<Establishment>
        {
            new Establishment("Name", "12345678000199", "City", "State", "Street", "1", "Comp", "12345-678", "1234567890", 5, 10)
        };
    _repositoryMock
        .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(establishments);

    // Act
    var result = await _handler.Handle(new GetAllEstablishmentsQuery(), CancellationToken.None);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);
    Assert.Single(result.Value);
    Assert.Equal("Name", result.Value[0].Name);
  }

  [Fact]
  public async Task Handle_ReturnsFailure_WhenNoEstablishmentsExist()
  {
    // Arrange
    _repositoryMock
        .Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
        .ReturnsAsync(new List<Establishment>());

    // Act
    var result = await _handler.Handle(new GetAllEstablishmentsQuery(), CancellationToken.None);

    // Assert
    Assert.True(result.IsFailure);
    Assert.Equal("EstablishmentNotFound", result.Error.Code);
  }
}
