using MediatR;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Create;

public class CreateEstablishmentCommand : IRequest<Result<Establishment>>
{
  public CreateEstablishmentCommand(string name, string cnpj, string city, string state, string street, string number, string complement, string zipCode, string phone, int motorcyclesParkingSpaces, int carsParkingSpaces)
  {
    Name = name;
    Cnpj = cnpj;
    City = city;
    State = state;
    Street = street;
    Number = number;
    Complement = complement;
    ZipCode = zipCode;
    Phone = phone;
    MotorcyclesParkingSpaces = motorcyclesParkingSpaces;
    CarsParkingSpaces = carsParkingSpaces;
  }

  public string Name { get; set; }
  public string Cnpj { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string Street { get; set; }
  public string Number { get; set; }
  public string Complement { get; set; }
  public string ZipCode { get; set; }
  public string Phone { get; set; }
  public int MotorcyclesParkingSpaces { get; set; }
  public int CarsParkingSpaces { get; set; }
}
