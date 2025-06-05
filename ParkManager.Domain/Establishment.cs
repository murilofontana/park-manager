namespace ParkManager.Domain;

public class Establishment
{
  private Establishment()
  {

  }
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

    Name = name;
    Cnpj = new CNPJ(cnpj);
    Address = new Address(city, state, street, number, complement, zipCode);
    Phone = phone;
    MotorcyclesParkingSpaces = motorcyclesParkingSpaces;
    CarsParkingSpaces = carsParkingSpaces;
  }

  public Guid Id { get; private set; }
  public string Name { get; private set; }
  public CNPJ Cnpj { get; private set; }
  public Address Address { get; private set; }
  public string Phone { get; private set; }
  public int MotorcyclesParkingSpaces { get; private set; }
  public int CarsParkingSpaces { get; private set; }

  private List<ParkingMovement> _parkingMovements = [];

  public IReadOnlyList<ParkingMovement> GetParkingMovements() => _parkingMovements.AsReadOnly();

  public void Entry(Vehicle vehicle, DateTime entryDate)
  {
    if (vehicle.IsMotorcycle() && MotorcyclesParkingSpaces <= _parkingMovements.Count(m => m.Type == EVehicleType.Motorcycle))
      throw new DomainException(EstablishmentErros.NoAvailableParkingSpacesForMotorcycles);

    if (vehicle.IsCar() && CarsParkingSpaces <= _parkingMovements.Count(m => m.Type == EVehicleType.Car))
      throw new DomainException(EstablishmentErros.NoAvailableParkingSpacesForCars);

    if (_parkingMovements.Any(m => m.VehicleId == vehicle.Id && m.ExitDate == null))
      throw new DomainException(EstablishmentErros.VehicleAlreadyParked);

    var movement = new ParkingMovement(Id, vehicle.Id, entryDate, null, vehicle.Type);

    _parkingMovements.Add(movement);
  }

  public void Exit(Guid vehicleId, DateTime exitDate)
  {
    var movement = _parkingMovements.FirstOrDefault(m => m.VehicleId == vehicleId && m.ExitDate == null);

    if (movement == null)
      throw new DomainException(EstablishmentErros.VehicleNotFoundOrAlreadyExited);

    movement.Exit(exitDate);
  }

  public void Update(
        string name,
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

    Name = name;
    Cnpj = new CNPJ(cnpj);
    Address = new Address(city, state, street, number, complement, zipCode);
    Phone = phone;
    MotorcyclesParkingSpaces = motorcyclesParkingSpaces;
    CarsParkingSpaces = carsParkingSpaces;
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
