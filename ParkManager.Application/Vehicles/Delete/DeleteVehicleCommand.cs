using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Delete;

public class DeleteVehicleCommand : IRequest<Result>
{
  public DeleteVehicleCommand(Guid id)
  {
    Id = id;
  }
  public Guid Id { get; set; }
}
