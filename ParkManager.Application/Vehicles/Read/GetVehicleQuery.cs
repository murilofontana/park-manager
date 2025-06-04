using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Read;

public class GetVehicleQuery : IRequest<Result<VehicleResponse>>
{
  public Guid Id { get; set; }
}
