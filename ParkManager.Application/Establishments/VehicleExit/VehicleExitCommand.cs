using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.VehicleExit;

public class VehicleExitCommand : IRequest<Result>
{
  public VehicleExitCommand(Guid establishmentId, Guid vehicleId)
  {
    EstablishmentId = establishmentId;
    VehicleId = vehicleId;
  }

  public Guid EstablishmentId { get; set; }
  public Guid VehicleId { get; internal set; }
}
