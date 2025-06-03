using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Read;

public class GetEstablishmentQuery  : IRequest<Result<EstablishmentResponse>>
{
  public Guid Id { get; set; }
}
