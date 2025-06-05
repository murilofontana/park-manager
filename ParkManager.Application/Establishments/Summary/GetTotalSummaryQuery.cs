using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Summary;

public class GetTotalSummaryQuery : IRequest<Result<TotalSummaryResponse>>
{
  public Guid EstablishmentId { get; set; }

  public GetTotalSummaryQuery(Guid establishmentId)
  {
    EstablishmentId = establishmentId;
  }
}
