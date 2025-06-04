using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Read;

public class GetVehicleQueryHandler : IRequestHandler<GetVehicleQuery, Result<VehicleResponse>>
{
  private readonly IVehicleRepository _repository;

  public GetVehicleQueryHandler(IVehicleRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<VehicleResponse>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
  {
    var vehicle = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (vehicle == null)
    {
      return Result.Failure<VehicleResponse>(new Error("404", "Vehicle not found"));
    }

    var vehicleResponse = VehicleResponse.FromVehicle(vehicle);
    return Result.Success(vehicleResponse);
  }
}
