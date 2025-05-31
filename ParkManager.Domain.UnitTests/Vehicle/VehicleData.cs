namespace ParkManager.Domain.UnitTests;

public static class VehicleData
{
  public static Vehicle CreateCar() => new("Test Branch", "Test Model", "Test Color", "ABC-1234", EVehicleType.Car);
  public static Vehicle CreateMotorcycle() => new("Test Branch", "Test Model", "Test Color", "ABC-1234", EVehicleType.Motorcycle);

}
