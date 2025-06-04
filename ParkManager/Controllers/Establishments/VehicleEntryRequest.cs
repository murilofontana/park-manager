namespace ParkManager.Controllers.Establishments
{
  public class VehicleEntryRequest
  {
    public Guid EstablishmentId { get; set; }
    public Guid VehicleId { get; set; }
    public VehicleEntryRequest(Guid establishmentId, Guid vehicleId)
    {
      EstablishmentId = establishmentId;
      VehicleId = vehicleId;
    }
  }
}
