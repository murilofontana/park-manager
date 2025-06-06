using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Summary.DailySummaryGroupedByHour;

public class GetDailySummaryGroupByHourQuery : IRequest<Result<DailySummaryGroupedByHourResponse>>
{
  public GetDailySummaryGroupByHourQuery(Guid establishmentId, DateOnly date)
  {
    EstablishmentId = establishmentId;
    Date = date;
  }

  public Guid EstablishmentId { get; set; }

  public DateOnly Date { get; set; }
}
