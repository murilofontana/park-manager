using ParkManager.Domain.Abstraction;

namespace ParkManager.Domain;

public class Vehicle : AggregateRoot
{
  private Vehicle()
  {
    
  }
  public Vehicle(string branch, string model, string color, string plate, EVehicleType type)
  {
    Branch = branch;
    Model = model;
    Color = color;
    Plate = plate;
    Type = type;
  }

  public Guid Id { get; private set; } 
  public string Branch { get; private set; }
  public string Model { get; private set; }
  public string Color { get; private set; }
  public string Plate { get; private set; }
  public EVehicleType Type { get; private set; }
  public bool IsMotorcycle() => Type == EVehicleType.Motorcycle;
  public bool IsCar() => Type == EVehicleType.Car;

  public void Update(string branch, string model, string color, string plate, EVehicleType type)
  {
    if (string.IsNullOrWhiteSpace(branch)) throw new DomainException(VehicleError.BranchCannotBeEmpty);
    if (string.IsNullOrWhiteSpace(model)) throw new DomainException(VehicleError.ModelCannotBeEmpty);
    if (string.IsNullOrWhiteSpace(color)) throw new DomainException(VehicleError.ColorCannotBeEmpty);
    if (string.IsNullOrWhiteSpace(plate)) throw new DomainException(VehicleError.PlateCannotBeEmpty);
    Branch = branch;
    Model = model;
    Color = color;
    Plate = plate;
    Type = type;
  }
}

public enum EVehicleType
{
  Motorcycle,
  Car
}
