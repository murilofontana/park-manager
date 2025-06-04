using ParkManager.Domain;

namespace ParkManager.Application.Common;

public interface IVehicleRepository
{
  Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
  Task DeleteAsync(Vehicle vechile, CancellationToken cancellationToken);
  Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken);
  Task<Vehicle> GetByIdAsync(Guid id, CancellationToken cancellationToken);
  Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken);
}
