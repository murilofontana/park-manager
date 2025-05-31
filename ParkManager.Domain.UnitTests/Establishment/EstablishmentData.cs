namespace ParkManager.Domain.UnitTests;

public static class EstablishmentData
{
  public static Establishment Create(int mortocyclesParkingSpaces = 10, int carsParkingSpaces=10) => new(
    "Test Establishment",
    "12345678000195",
    "Test City",
    "Test State",
    "Test Street",
    "123",
    "Apt 1",
    "12345-678",
    "1234567890",
    mortocyclesParkingSpaces,
    carsParkingSpaces
  );
}
