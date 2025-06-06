using MediatR;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Summary.TotalSumary;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Summary.DailySummaryGroupedByHour;

public class GetDailySummaryGroupByHourQueryHandler : IRequestHandler<GetDailySummaryGroupByHourQuery, Result<DailySummaryGroupedByHourResponse>>
{
  private readonly IEstablishmentRepository _establishmentRepository;

  public GetDailySummaryGroupByHourQueryHandler(IEstablishmentRepository establishmentRepository)
  {
    _establishmentRepository = establishmentRepository;
  }

  public async Task<Result<DailySummaryGroupedByHourResponse>> Handle(GetDailySummaryGroupByHourQuery request, CancellationToken cancellationToken)
  {
    var establishment = await _establishmentRepository.GetByIdAsync(request.EstablishmentId, cancellationToken);
    if (establishment == null)
    {
      return Result.Failure<DailySummaryGroupedByHourResponse>(new Error("999", "Establishment not found!"));
    }

    var hourSummary = await _establishmentRepository.GetDailySummaryGroupedByHourAsync(request.EstablishmentId, request.Date, cancellationToken);

    var dailySummary = new DailySummaryGroupedByHourResponse(establishment.Id, request.Date, hourSummary.ToList());

    return Result.Success(dailySummary);
  }
}
