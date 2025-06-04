using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Create;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<Vehicle>>
{
  private readonly IVehicleRepository _repository;

  public CreateVehicleCommandHandler(IVehicleRepository vehicleRepository)
  {
    this._repository = vehicleRepository;
  }

  public async Task<Result<Vehicle>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
  {
    var vehicle = new Vehicle(
      request.Branch,
      request.Model,
      request.Plate,
      request.Color,
      request.Type
    );
    try
    {
      await _repository.AddAsync(vehicle, cancellationToken);

      var vehicleInserted = await _repository.GetByIdAsync(vehicle.Id, cancellationToken);

      return Result.Success(vehicleInserted);

    }
    catch (Exception)
    {
      return Result.Failure<Vehicle>(new Error("999", "Error When creating Vehicle"));
    }
  }
}
