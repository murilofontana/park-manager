using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Summary.TotalSumary;

public class GetTotalSummaryQueryHandler : IRequestHandler<GetTotalSummaryQuery, Result<TotalSummaryResponse>>
{
  private readonly IEstablishmentRepository _establishmentRepository;

  public GetTotalSummaryQueryHandler(IEstablishmentRepository repository)
  {
    _establishmentRepository = repository;
  }

  public async Task<Result<TotalSummaryResponse>> Handle(GetTotalSummaryQuery request, CancellationToken cancellationToken)
  {

    var establishment = await _establishmentRepository.GetByIdAsync(request.EstablishmentId, cancellationToken);
    if (establishment == null)
    {
      return Result.Failure<TotalSummaryResponse>(new Error("999", "Establishment not found!"));
    }

    var totalCarsEntry = await _establishmentRepository.GetTotalCarsEntry(request.EstablishmentId);
    var totalCarsExit = await _establishmentRepository.GetTotalCarsExit(request.EstablishmentId);

    var totalMotorcyclesEntry = await _establishmentRepository.GetTotalMotorcyclesEntry(request.EstablishmentId);
    var totalMotorcyclesExit = await _establishmentRepository.GetTotalMotorcyclesExit(request.EstablishmentId);

    var totalGeneralEntry = await _establishmentRepository.GetTotalGeneralEntry(request.EstablishmentId);
    var totalGeneralExit = await _establishmentRepository.GetTotalGeneralExit(request.EstablishmentId);

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
