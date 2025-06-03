using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Create;

public class CreateEstablishmentCommandHandler : IRequestHandler<CreateEstablishmentCommand, Result<Establishment>>
{
  private readonly IEstablishmentRepository _establishmentRepository;

  public CreateEstablishmentCommandHandler(IEstablishmentRepository establishmentRepository)
  {
    _establishmentRepository = establishmentRepository;
  }

  public async Task<Result<Establishment>> Handle(CreateEstablishmentCommand request, CancellationToken cancellationToken)
  {
    var establishment = new Establishment(
            request.Name,
            request.Cnpj,
            request.City,
            request.State,
            request.Street,
            request.Number,
            request.Complement,
            request.ZipCode,
            request.Phone,
            request.MotorcyclesParkingSpaces,
            request.CarsParkingSpaces
          );

    try
    {
      await _establishmentRepository.AddAsync(establishment, cancellationToken);

      var establishmentInserted = await _establishmentRepository.GetByIdAsync(establishment.Id, cancellationToken);

      return Result.Success(establishmentInserted);

    }
    catch (Exception)
    {
      return Result.Failure<Establishment>(new Error("999", "Error When creating establishment"));
    }
  }
}
