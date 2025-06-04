using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Read;

public class GetAllVehiclesQuery : IRequest<Result<IEnumerable<VehicleResponse>>>
{
}
