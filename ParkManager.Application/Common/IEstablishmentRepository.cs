using ParkManager.Domain;

namespace ParkManager.Application.Common;

public interface IEstablishmentRepository
{
  Task AddAsync(Establishment establishment);
  Task<Establishment> GetById(Guid id);
}
