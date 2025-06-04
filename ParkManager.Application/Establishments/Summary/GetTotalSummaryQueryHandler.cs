using MediatR;
using ParkManager.Application.Common;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Summary;

public class GetTotalSummaryQueryHandler : IRequestHandler<GetTotalSummaryQuery, Result<TotalSummaryResponse>>
{
  private readonly IEstablishmentRepository _repository;

  public GetTotalSummaryQueryHandler(IEstablishmentRepository repository)
  {
    _repository = repository;
  }

  public async Task<Result<TotalSummaryResponse>> Handle(GetTotalSummaryQuery request, CancellationToken cancellationToken)
  {
    var totalCarsEntry = await _repository.GetTotalCarsEntry();
    var totalCarsExit = await _repository.GetTotalCarsExit();
    
    var totalMotorcyclesEntry = await _repository.GetTotalMotorcyclesEntry();
    var totalMotorcyclesExit = await _repository.GetTotalMotorcyclesExit();

    var totalGeneralEntry = await _repository.GetTotalGeneralEntry();
    var totalGeneralExit = await _repository.GetTotalGeneralExit();

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
