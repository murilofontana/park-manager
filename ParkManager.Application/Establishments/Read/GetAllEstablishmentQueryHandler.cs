using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Read;

public class GetAllEstablishmentQueryHandler : IRequestHandler<GetAllEstablishmentsQuery, Result<List<EstablishmentResponse>>>
{
  private readonly IEstablishmentRepository _repository;
  public GetAllEstablishmentQueryHandler(IEstablishmentRepository repository)
  {
    _repository = repository;
  }
  public async Task<Result<List<EstablishmentResponse>>> Handle(GetAllEstablishmentsQuery request, CancellationToken cancellationToken)
  {
    var establishments = await _repository.GetByAllAsync(cancellationToken);
    if (!establishments.Any())
    {
      return Result.Failure<List<EstablishmentResponse>>(new Error("999", "There is no Establishment"));
    }

    var response = EstablishmentResponse.FromEntities(establishments.ToList());

    return Result.Success(response);
  }
}
