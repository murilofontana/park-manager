using Microsoft.EntityFrameworkCore;
using ParkManager.Application.Common;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Repositories;

public class EstablishmentRepository : IEstablishmentRepository
{
  private readonly ApplicationDbContext _dbcontext;
  public EstablishmentRepository(ApplicationDbContext dbContext)
  {
    _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  }
  public async Task AddAsync(Establishment establishment, CancellationToken cancellationToken)
  {
    await _dbcontext.Set<Establishment>().AddAsync(establishment, cancellationToken);
    await _dbcontext.SaveChangesAsync();
  }

  public async Task DeleteAsync(Establishment establishment, CancellationToken cancellationToken)
  {
    _dbcontext.Set<Establishment>().Remove(establishment);
    await _dbcontext.SaveChangesAsync();
  }

  public async Task<IEnumerable<Establishment>> GetByAllAsync(CancellationToken cancellationToken)
  {
    return await _dbcontext.Set<Establishment>().ToListAsync(cancellationToken);
  }

  public async Task<Establishment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await _dbcontext.Set<Establishment>()
      .Include("_parkingMovements")
      .AsTracking()
      .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
 
  }

  public async Task<int> GetTotalCarsEntry(Guid establishmentId)
  {
    return await _dbcontext.Set<ParkingMovement>()
      .Where(e => e.Type == EVehicleType.Car && e.EntryDate != DateTime.MinValue && e.EstablishmentId == establishmentId)
      .CountAsync();
  }

  public async Task<int> GetTotalCarsExit(Guid establishmentId)
  {
    return await _dbcontext.Set<ParkingMovement>()
     .Where(e => e.Type == EVehicleType.Car && e.ExitDate != null && e.EstablishmentId == establishmentId)
     .CountAsync();
  }

  public async Task<int> GetTotalMotorcyclesEntry(Guid establishmentId)
  {
    return await _dbcontext.Set<ParkingMovement>()
     .Where(e => e.Type == EVehicleType.Motorcycle && e.EntryDate != DateTime.MinValue && e.EstablishmentId == establishmentId)
     .CountAsync();
  }

  public async Task<int> GetTotalMotorcyclesExit(Guid establishmentId)
  {
    return await _dbcontext.Set<ParkingMovement>()
    .Where(e => e.Type == EVehicleType.Motorcycle && e.ExitDate != null && e.EstablishmentId == establishmentId)
    .CountAsync();
  }

  public async Task UpdateAsync(Establishment establishment, CancellationToken cancellationToken)
  {
    var teste = _dbcontext.ChangeTracker.Entries();

    _dbcontext.Set<Establishment>().Update(establishment);
    await _dbcontext.SaveChangesAsync();
  }
}
