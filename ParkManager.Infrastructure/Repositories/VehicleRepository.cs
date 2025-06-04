using Microsoft.EntityFrameworkCore;
using ParkManager.Application.Common;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
  private readonly ApplicationDbContext _dbcontext;

  public VehicleRepository(ApplicationDbContext dbcontext)
  {
    _dbcontext = dbcontext;
  }

  public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
  {
    await _dbcontext.Set<Vehicle>().AddAsync(vehicle, cancellationToken);
    await _dbcontext.SaveChangesAsync();
  }

  public async Task DeleteAsync(Vehicle vechile, CancellationToken cancellationToken)
  {
    _dbcontext.Set<Vehicle>().Remove(vechile);
    await _dbcontext.SaveChangesAsync();
  }

  public async Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken)
  {
    return await _dbcontext.Set<Vehicle>().ToListAsync(cancellationToken);
  }

  public async Task<Vehicle> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await _dbcontext.Set<Vehicle>().FirstOrDefaultAsync(e => e.Id == id);
  }

  public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
  {
    _dbcontext.Set<Vehicle>().Update(vehicle);
    await _dbcontext.SaveChangesAsync();
  }
}
