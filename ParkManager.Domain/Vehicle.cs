namespace ParkManager.Domain;

public class Vehicle
{
  public Vehicle(string branch, string model, string color, string plate, EVehicleType type)
  {
    Id = Guid.NewGuid();
    Branch = branch;
    Model = model;
    Color = color;
    Plate = plate;
    Type = type;
  }

  public Guid Id { get; } 
  public string Branch { get; }
  public string Model { get; }
  public string Color { get; }
  public string Plate { get; }
  public EVehicleType Type { get; }
  public bool IsMotorcycle() => Type == EVehicleType.Motorcycle;
  public bool IsCar() => Type == EVehicleType.Car;
}

public enum EVehicleType
{
  Motorcycle,
  Car
}
