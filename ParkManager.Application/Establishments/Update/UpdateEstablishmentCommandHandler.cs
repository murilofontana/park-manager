using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Update;

public class UpdateEstablishmentCommandHandler : IRequestHandler<UpdateEstablishmentCommand, Result<Establishment>>
{
  private readonly IEstablishmentRepository _repository;

  public UpdateEstablishmentCommandHandler(IEstablishmentRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<Establishment>> Handle(UpdateEstablishmentCommand request, CancellationToken cancellationToken)
  {
    var establishment = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (establishment == null)
    {
      return Result.Failure<Establishment>(new Error("999", "Establishment not found!"));
    }

    establishment.Update(request.Name, request.Cnpj, request.City, request.State, request.Street, request.Number, request.Complement, request.ZipCode, request.Phone, request.MotorcyclesParkingSpaces, request.CarsParkingSpaces);

    try
    {
      await _repository.UpdateAsync(establishment, cancellationToken);

      var establishmentUpdated = await _repository.GetByIdAsync(establishment.Id, cancellationToken);

      return Result.Success(establishmentUpdated);
    }
    catch (Exception e)
    {
      return Result.Failure<Establishment>(new Error("999", $"Error updating establishment: {e.Message}"));
    }
  }
}
