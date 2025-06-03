using MediatR;
using ParkManager.Domain.Abstraction;
using ParkManager.Domain;

namespace ParkManager.Application.Establishments.Update;

public class UpdateEstablishmentCommand : IRequest<Result<Establishment>>
{
  public UpdateEstablishmentCommand(string name, string cnpj, string city, string state, string street, string number, string complement, string zipCode, string phone, int motorcyclesParkingSpaces, int carsParkingSpaces, Guid id)
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
    Id = id;
  }

  public Guid Id { get; set; }
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
