namespace ParkManager.Domain.UnitTests;

public class EstablishmentTests
{

  [Fact]
  public void Entry_Should_CreateParkingMovement()
  {
    // Arrange
    var establishment = EstablishmentData.Create();
    var vehicle = VehicleData.CreateCar();
    var entryDate = DateTime.Now;

    // Act
    establishment.Entry(vehicle, entryDate);

    // Assert
    var parkingMovement = establishment.GetParkingMovements().FirstOrDefault(m => m.VehicleId == vehicle.Id && m.ExitDate == null);
    Assert.NotNull(parkingMovement);
    Assert.Equal(vehicle.Id, parkingMovement.VehicleId);
    Assert.Equal(entryDate, parkingMovement.EntryDate);
  }

  [Fact]
  public void Exit_Should_UpdateParkingMovementExitDate()
  {
    // Arrange
    var establishment = EstablishmentData.Create();
    var vehicle = VehicleData.CreateCar();
    var entryDate = DateTime.Now;
    establishment.Entry(vehicle, entryDate);

    // Act
    var exitDate = DateTime.Now.AddHours(1);
    establishment.Exit(vehicle.Id, exitDate);
    // Assert
    var parkingMovement = establishment.GetParkingMovements().FirstOrDefault(m => m.VehicleId == vehicle.Id && m.ExitDate != null);
    Assert.NotNull(parkingMovement);
    Assert.Equal(exitDate, parkingMovement.ExitDate);
  }

  [Fact]
  public void Entry_Should_Return_NoAvailableParkingSpacesForMotorcycles_When_NoSpacesAvailable()
  {
    // Arrange
    var establishment = EstablishmentData.Create(mortocyclesParkingSpaces: 1);
    var vehicle = VehicleData.CreateMotorcycle();
    establishment.Entry(vehicle, DateTime.Now);
    // Act & Assert
    Assert.Throws<DomainException>(() => establishment.Entry(vehicle, DateTime.Now));
  }

  [Fact]
  public void Entry_Should_Return_NoAvailableParkingSpacesForCars_When_NoSpacesAvailable()
  {
    // Arrange
    var establishment = EstablishmentData.Create(carsParkingSpaces: 1);
    var vehicle = VehicleData.CreateCar();
    establishment.Entry(vehicle, DateTime.Now);
    // Act & Assert
    Assert.Throws<DomainException>(() => establishment.Entry(vehicle, DateTime.Now));
  }

  [Fact]
  public void Exit_Should_Return_VehicleNotFoundOrAlreadyExited_When_VehicleNotFound()
  {
    // Arrange
    var establishment = EstablishmentData.Create();
    var vehicle = VehicleData.CreateCar();
    var exitDate = DateTime.Now;
    // Act & Assert
    Assert.Throws<DomainException>(() => establishment.Exit(vehicle.Id, exitDate));
  }

  [Fact]
  public void Exit_Should_Return_VehicleNotFoundOrAlreadyExited_When_VehicleAlreadyExited()
  {
    // Arrange
    var establishment = EstablishmentData.Create();
    var vehicle = VehicleData.CreateCar();
    var entryDate = DateTime.Now;
    establishment.Entry(vehicle, entryDate);
    var exitDate = DateTime.Now.AddHours(1);
    establishment.Exit(vehicle.Id, exitDate);

    // Act & Assert
    Assert.Throws<DomainException>(() => establishment.Exit(vehicle.Id, DateTime.Now.AddHours(2)));
  }

  [Fact]
  public void NewEstablishment_Should_Return_InvalidParkingSpaces_When_MotorcyclesParkingSpacesAreZeroOrNegative()
  {
    // Arrange
    var invalidParkingSpaces = new[] { -2, -1, -10 };

    foreach (var spaces in invalidParkingSpaces)
    {
      // Act & Assert
      var exception = Assert.Throws<DomainException>(() => EstablishmentData.Create(mortocyclesParkingSpaces: spaces));
      Assert.Equal(EstablishmentErros.InvalidParkingSpaces, exception.Message);
    }
  }

  [Fact]
  public void NewEstablishment_Should_Return_InvalidParkingSpaces_When_CarsParkingSpacesAreZeroOrNegative()
  {
    // Arrange
    var invalidParkingSpaces = new[] { -2, -1, -10 };

    foreach (var spaces in invalidParkingSpaces)
    {
      // Act & Assert
      var exception = Assert.Throws<DomainException>(() => EstablishmentData.Create(carsParkingSpaces: spaces));
      Assert.Equal(EstablishmentErros.InvalidParkingSpaces, exception.Message);
    }
  }

}
