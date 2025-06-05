using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Summary.TotalSumary;

public class GetTotalSummaryQueryHandler : IRequestHandler<GetTotalSummaryQuery, Result<TotalSummaryResponse>>
{
  private readonly IEstablishmentRepository _repository;

  public GetTotalSummaryQueryHandler(IEstablishmentRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<TotalSummaryResponse>> Handle(GetTotalSummaryQuery request, CancellationToken cancellationToken)
  {

    var establishment = await _repository.GetByIdAsync(request.EstablishmentId, cancellationToken);
    if (establishment == null)
    {
      return Result.Failure<TotalSummaryResponse>(new Error("999", "Establishment not found!"));
    }

    var totalCarsEntry = await _repository.GetTotalCarsEntry(request.EstablishmentId);
    var totalCarsExit = await _repository.GetTotalCarsExit(request.EstablishmentId);

    var totalMotorcyclesEntry = await _repository.GetTotalMotorcyclesEntry(request.EstablishmentId);
    var totalMotorcyclesExit = await _repository.GetTotalMotorcyclesExit(request.EstablishmentId);

    var totalGeneralEntry = await _repository.GetTotalGeneralEntry(request.EstablishmentId);
    var totalGeneralExit = await _repository.GetTotalGeneralExit(request.EstablishmentId);

    var response = new TotalSummaryResponse(
      totalCarsEntry,
      totalCarsExit,
      totalMotorcyclesEntry,
      totalMotorcyclesExit,
      totalGeneralEntry,
      totalGeneralExit
    );

    return Result.Success(response);
  }
}
