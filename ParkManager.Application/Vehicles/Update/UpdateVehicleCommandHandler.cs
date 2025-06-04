using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Update;

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Result<Vehicle>>
{
  private readonly IVehicleRepository _repository;

  public UpdateVehicleCommandHandler(IVehicleRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<Vehicle>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
  {
    var vehicle = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (vehicle == null)
    {
      return Result.Failure<Vehicle>(new Error("999", "Vehicle not found!"));
    }

    vehicle.Update(
      request.Branch,
      request.Model,
      request.Plate,
      request.Color,
      request.Type
    );

    try
    {
      await _repository.UpdateAsync(vehicle, cancellationToken);

      var vehicleUpdated = await _repository.GetByIdAsync(vehicle.Id, cancellationToken);

      return Result.Success(vehicleUpdated);
    }
    catch (Exception e)
    {
      return Result.Failure<Vehicle>(new Error("999", $"Error updating vehicle: {e.Message}"));
    }
  }
}
