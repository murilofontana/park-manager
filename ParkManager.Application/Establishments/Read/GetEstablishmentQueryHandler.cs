using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Read;

public class GetEstablishmentQueryHandler : IRequestHandler<GetEstablishmentQuery, Result<EstablishmentResponse>>
{
  private readonly IEstablishmentRepository _repository;
  public GetEstablishmentQueryHandler(IEstablishmentRepository repository)
  {
    _repository = repository;
  }
  public async Task<Result<EstablishmentResponse>> Handle(GetEstablishmentQuery request, CancellationToken cancellationToken)
  {
    var establishment = await _repository.GetByIdAsync(request.Id, cancellationToken);
    if (establishment == null)
    {
      return Result.Failure<EstablishmentResponse>(new Error("999", "Establishment not found!"));
    }

    var response = EstablishmentResponse.FromEntity(establishment);

    return Result.Success(response);
  }
}
