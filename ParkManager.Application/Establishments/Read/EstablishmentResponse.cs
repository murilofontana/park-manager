using ParkManager.Domain;

namespace ParkManager.Application.Establishments.Read;

public class EstablishmentResponse
{
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

  public static EstablishmentResponse FromEntity(Establishment establishment)
  {
    return new EstablishmentResponse
    {
      Name = establishment.Name,
      Cnpj = establishment.Cnpj.Cnpj,
      City = establishment.Address.City,
      State = establishment.Address.State,
      Street = establishment.Address.Street,
      Number = establishment.Address.Number,
      Complement = establishment.Address.Complement,
      ZipCode = establishment.Address.ZipCode,
      Phone = establishment.Phone,
      MotorcyclesParkingSpaces = establishment.MotorcyclesParkingSpaces,
      CarsParkingSpaces = establishment.CarsParkingSpaces
    };
  }

  public static List<EstablishmentResponse> FromEntities(List<Establishment> establishments)
  {
    var responses = new List<EstablishmentResponse>();
    foreach (var establishment in establishments)
    {
      var response = new EstablishmentResponse
      {
        Name = establishment.Name,
        Cnpj = establishment.Cnpj.Cnpj,
        City = establishment.Address.City,
        State = establishment.Address.State,
        Street = establishment.Address.Street,
        Number = establishment.Address.Number,
        Complement = establishment.Address.Complement,
        ZipCode = establishment.Address.ZipCode,
        Phone = establishment.Phone,
        MotorcyclesParkingSpaces = establishment.MotorcyclesParkingSpaces,
        CarsParkingSpaces = establishment.CarsParkingSpaces
      };
      responses.Add(response);
    }
    return responses;
  }
}