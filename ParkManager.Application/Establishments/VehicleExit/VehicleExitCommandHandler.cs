using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.VehicleExit;

public class VehicleExitCommandHandler : IRequestHandler<VehicleExitCommand, Result>
{
  private readonly IEstablishmentRepository _establishmentRepository;
  private readonly IVehicleRepository _vehicleRepository;

  public VehicleExitCommandHandler(IEstablishmentRepository repository, IVehicleRepository vehicleRepository)
  {
    _establishmentRepository = repository;
    _vehicleRepository = vehicleRepository;
  }

  public async Task<Result> Handle(VehicleExitCommand request, CancellationToken cancellationToken)
  {
    var establishment = await _establishmentRepository.GetByIdAsync(request.EstablishmentId, cancellationToken);

    if (establishment is null)
    {
      return Result.Failure(new Error("EstablishmentNotFound", "The specified establishment was not found."));
    }

    var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId, cancellationToken);

    if (vehicle is null)
    {
      return Result.Failure(new Error("VehicleNotFound", "The specified vehicle was not found."));
    }

    establishment.Exit(vehicle.Id, DateTime.Now);

    await _establishmentRepository.UpdateAsync(establishment, cancellationToken);

    return Result.Success();
  }
}
