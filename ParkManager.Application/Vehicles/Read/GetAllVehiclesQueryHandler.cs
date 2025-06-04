using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Read;

public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, Result<IEnumerable<VehicleResponse>>>
{
  private readonly IVehicleRepository _repository;
  public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository)
  {
    _repository = vehicleRepository;
  }
  public async Task<Result<IEnumerable<VehicleResponse>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
  {
    try
    {
      IEnumerable<Vehicle> vehicles = await _repository.GetAllAsync(cancellationToken);
      var vehicleResponses = VehicleResponse.FromVehicles(vehicles);
      return Result.Success(vehicleResponses);
    }
    catch (Exception ex)
    {
      return Result.Failure<IEnumerable<VehicleResponse>>(new Error("999", $"Error when retrieving vehicles: {ex.Message}"));
    }
  }
}