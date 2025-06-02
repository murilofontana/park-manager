namespace ParkManager.Controllers.Establishments
{
  public record CreateEstablishmentRequest
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
  }
}
