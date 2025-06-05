namespace ParkManager.Controllers.Establishments
{
  public class VehicleParkingMovementRequest
  {
    public Guid EstablishmentId { get; set; }
    public Guid VehicleId { get; set; }
    public VehicleParkingMovementRequest(Guid establishmentId, Guid vehicleId)
    {
      EstablishmentId = establishmentId;
      VehicleId = vehicleId;
    }
  }
}
