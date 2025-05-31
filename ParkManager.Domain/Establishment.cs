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
    if (motorcyclesParkingSpaces < 0 || carsParkingSpaces < 0)
      throw new DomainException(EstablishmentErros.InvalidParkingSpaces);

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

  public IReadOnlyList<ParkingMovement> GetParkingMovements() => _parkingMovementList.AsReadOnly();

  public void Entry(Vehicle vehicle, DateTime entryDate)
  {
    if (vehicle.IsMotorcycle() && MotorcyclesParkingSpaces <= _parkingMovementList.Count(m => m.Type == EVehicleType.Motorcycle))
      throw new DomainException(EstablishmentErros.NoAvailableParkingSpacesForMotorcycles);

    if (vehicle.IsCar() && CarsParkingSpaces <= _parkingMovementList.Count(m => m.Type == EVehicleType.Car))
      throw new DomainException(EstablishmentErros.NoAvailableParkingSpacesForCars);

    var movement = new ParkingMovement(Id, vehicle.Id, entryDate, null, vehicle.Type);
    
    _parkingMovementList.Add(movement);
  }

  public void Exit(Guid vehicleId, DateTime exitDate)
  {
    var movement = _parkingMovementList.FirstOrDefault(m => m.VehicleId == vehicleId && m.ExitDate == null);

    if (movement == null)
      throw new DomainException(EstablishmentErros.VehicleNotFoundOrAlreadyExited);

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
    if (ExitDate.HasValue)
      throw new DomainException("The vehicle has already exited.");

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
    if (!ValidadeCnpj(cnpj))
      throw new DomainException(EstablishmentErros.InvalidCNPJ);

    Cnpj = cnpj;
  }

  private bool ValidadeCnpj(string cnpj)
  {

    if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14)
      return false;

    // Implement CNPJ validation logic here
    // This is a placeholder for the actual validation logic
    return true;
  }
};
