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
    if (string.IsNullOrWhiteSpace(branch)) throw new DomainException("Branch cannot be empty");
    if (string.IsNullOrWhiteSpace(model)) throw new DomainException("Model cannot be empty");
    if (string.IsNullOrWhiteSpace(color)) throw new DomainException("Color cannot be empty");
    if (string.IsNullOrWhiteSpace(plate)) throw new DomainException("Plate cannot be empty");
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
