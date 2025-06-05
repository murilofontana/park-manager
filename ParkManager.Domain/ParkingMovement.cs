namespace ParkManager.Domain;

public class ParkingMovement
{
  private ParkingMovement()
  {
  }
  public ParkingMovement(Guid establishmentId, Guid vehicleId, DateTime entryDate, DateTime? exitDate, EVehicleType type)
  {
    EstablishmentId = establishmentId;
    VehicleId = vehicleId;
    EntryDate = entryDate;
    ExitDate = exitDate;
    Type = type;
  }

  public Guid Id { get; }
  public Guid EstablishmentId { get; }
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
