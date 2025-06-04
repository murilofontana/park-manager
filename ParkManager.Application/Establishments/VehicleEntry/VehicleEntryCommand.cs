using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.VehicleEntry;

public class VehicleEntryCommand : IRequest<Result>
{
  public VehicleEntryCommand(Guid establishmentId, Guid vehicleId)
  {
    EstablishmentId = establishmentId;
    VehicleId = vehicleId;
  }

  public Guid EstablishmentId { get; set; }
  public Guid VehicleId { get; internal set; }
}
