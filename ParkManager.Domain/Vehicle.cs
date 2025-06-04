namespace ParkManager.Domain;

public class Vehicle
{
  private Vehicle()
  {
    
  }
  public Vehicle(string branch, string model, string color, string plate, EVehicleType type)
  {
    Id = Guid.NewGuid();
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
    if (string.IsNullOrWhiteSpace(branch)) throw new ArgumentException("Branch cannot be empty", nameof(branch));
    if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model cannot be empty", nameof(model));
    if (string.IsNullOrWhiteSpace(color)) throw new ArgumentException("Color cannot be empty", nameof(color));
    if (string.IsNullOrWhiteSpace(plate)) throw new ArgumentException("Plate cannot be empty", nameof(plate));
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
