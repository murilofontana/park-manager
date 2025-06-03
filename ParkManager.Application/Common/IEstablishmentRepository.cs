using ParkManager.Domain;

namespace ParkManager.Application.Common;

public interface IEstablishmentRepository
{
  Task AddAsync(Establishment establishment, CancellationToken cancellationToken);
  Task DeleteAsync(Establishment establishment, CancellationToken cancellationToken);
  Task<IEnumerable<Establishment>> GetByAllAsync(CancellationToken cancellationToken);
  Task<Establishment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
  Task UpdateAsync(Establishment establishment, CancellationToken cancellationToken);
}
