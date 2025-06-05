using ParkManager.Domain;

namespace ParkManager.Application.Common;

public interface IEstablishmentRepository
{
  Task AddAsync(Establishment establishment, CancellationToken cancellationToken);
  Task DeleteAsync(Establishment establishment, CancellationToken cancellationToken);
  Task<IEnumerable<Establishment>> GetByAllAsync(CancellationToken cancellationToken);
  Task<Establishment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
  Task<int> GetTotalCarsEntry(Guid establishmentId);
  Task<int> GetTotalCarsExit(Guid establishmentId);
  Task<int> GetTotalGeneralEntry(Guid establishmentId);
  Task<int> GetTotalGeneralExit(Guid establishmentId);
  Task<int> GetTotalMotorcyclesEntry(Guid establishmentId);
  Task<int> GetTotalMotorcyclesExit(Guid establishmentId);
  Task UpdateAsync(Establishment establishment, CancellationToken cancellationToken);
}
