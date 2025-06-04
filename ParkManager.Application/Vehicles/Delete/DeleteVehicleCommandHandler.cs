using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Delete;

public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
{
  private readonly IVehicleRepository _repository;

  public DeleteVehicleCommandHandler(IVehicleRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
  {
    var vechile = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (vechile == null)
    {
      return Result.Failure(new Error("999", "Vechile not found!"));
    }

    try
    {
      await _repository.DeleteAsync(vechile, cancellationToken);
    }
    catch (Exception e)
    {
      return Result.Failure(new Error("999", $"Error deleting vechile: {e.Message}"));
    }

    return Result.Success();
  }
}
