using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.VehicleEntry;

public class VehicleEntryCommandHandler : IRequestHandler<VehicleEntryCommand, Result>
{
  private readonly IEstablishmentRepository _establishmentRepository;
  private readonly IVehicleRepository _vehicleRepository;

  public VehicleEntryCommandHandler(IEstablishmentRepository repository, IVehicleRepository vehicleRepository)
  {
    _establishmentRepository = repository;
    _vehicleRepository = vehicleRepository;
  }

  public async Task<Result> Handle(VehicleEntryCommand request, CancellationToken cancellationToken)
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

    establishment.Entry(vehicle, DateTime.Now);

    try
    {
      await _establishmentRepository.UpdateAsync(establishment, cancellationToken);

    }
    catch (Exception e)
    {

      throw e;
    }
    

    return Result.Success();
  }
}
