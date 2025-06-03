using MediatR;
using ParkManager.Application.Establishments.Create;
using ParkManager.Domain.Abstraction;
using ParkManager.Domain;
using ParkManager.Application.Common;

namespace ParkManager.Application.Establishments.Delete;

public class DeleteEstablishmentCommandHandler : IRequestHandler<DeleteEstablishmentCommand, Result>
{
  private readonly IEstablishmentRepository _repository;

  public DeleteEstablishmentCommandHandler(IEstablishmentRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result> Handle(DeleteEstablishmentCommand request, CancellationToken cancellationToken)
  {
    var establishment = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (establishment == null)
    {
      return Result.Failure<bool>(new Error("999","Establishment not found!"));
    }

    try
    {
      await _repository.DeleteAsync(establishment, cancellationToken);
    }
    catch (Exception e)
    {
      return Result.Failure<bool>(new Error("999", $"Error deleting establishment: {e.Message}"));
    }
    
    return Result.Success(true);
  }
}
