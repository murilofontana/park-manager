using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Delete;

public class DeleteEstablishmentCommand : IRequest<Result>
{
  public Guid Id { get; set; }
}
