namespace ParkManager.Domain;

public class Establishment
{
  public Establishment(string name, 
    string cnpj,
    string city, 
    string state, 
    string street, 
    string number,
    string complement,
    string zipCode,
    string phone,
    int motorcyclesParkingSpaces, 
    int carsParkingSpaces)
  {
    Id = Guid.NewGuid();
    Name = name;
    Cnpj = new CNPJ(cnpj);
    Address = new Address(city, state, street, number, complement, zipCode);
    Phone = phone;
    MotorcyclesParkingSpaces = motorcyclesParkingSpaces;
    CarsParkingSpaces = carsParkingSpaces;
  }

  public Guid Id { get; }
  public string Name { get; }
  public CNPJ Cnpj { get; }
  public Address Address { get; }
  public string Phone { get; }
  public int MotorcyclesParkingSpaces { get; }
  public int CarsParkingSpaces { get; }

  private List<ParkingMovement> _parkingMovementList = [];

  public void Entry(Vehicle vehicle, DateTime entryDate)
  {
    if (vehicle.IsMotorcycle() && MotorcyclesParkingSpaces <= _parkingMovementList.Count(m => m.Type == EVehicleType.Motorcycle))
      throw new InvalidOperationException("No available parking spaces for motorcycles.");

    if (vehicle.IsCar() && CarsParkingSpaces <= _parkingMovementList.Count(m => m.Type == EVehicleType.Car))
      throw new InvalidOperationException("No available parking spaces for cars.");

    var movement = new ParkingMovement(Id, vehicle.Id, entryDate, null, vehicle.Type);
    
    _parkingMovementList.Add(movement);
  }

  public void Exit(Guid vehicleId, DateTime exitDate)
  {
    var movement = _parkingMovementList.FirstOrDefault(m => m.VehicleId == vehicleId && m.ExitDate == null);

    if (movement == null)
      throw new InvalidOperationException("Vehicle not found or already exited.");

    movement.Exit(exitDate);
  }

}

public class ParkingMovement
{
  public ParkingMovement(Guid parkingId, Guid vehicleId, DateTime entryDate, DateTime? exitDate, EVehicleType type)
  {
    Guid = Guid.NewGuid();
    ParkingId = parkingId;
    VehicleId = vehicleId;
    EntryDate = entryDate;
    ExitDate = exitDate;
    Type = type;
  }

  public Guid Guid { get; }
  public Guid ParkingId { get; }
  public Guid VehicleId { get; }
  public EVehicleType Type { get; }
  public DateTime EntryDate { get; }
  public DateTime? ExitDate { get; set; }

  public void Exit(DateTime exitDate)
  {
    ExitDate = exitDate;
  }
}


public record Address
{
  public Address(string city, string state, string street, string number, string complement, string zipCode)
  {
    City = city;
    State = state;
    Street = street;
    Number = number;
    Complement = complement;
    ZipCode = zipCode;
  }

  public string City { get; }
  public string State { get; }
  public string Street { get; }
  public string Number { get; }
  public string Complement { get; }
  public string ZipCode { get; }
}

public record CNPJ
{
  public string Cnpj { get; }

  public CNPJ(string cnpj)
  {
    if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
      throw new ArgumentException("CNPJ must be a 14-digit number.", nameof(cnpj));

    if (!ValidadeCnpj(cnpj))
      throw new ArgumentException("CNPJ invalid", nameof(cnpj));

    Cnpj = cnpj;
  }

  private bool ValidadeCnpj(string cnpj)
  {
    // Implement CNPJ validation logic here
    // This is a placeholder for the actual validation logic
    return true;
  }
};
