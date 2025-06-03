using MediatR;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Establishments.Read;

public class GetAllEstablishmentsQuery : IRequest<Result<List<EstablishmentResponse>>>
{
}
