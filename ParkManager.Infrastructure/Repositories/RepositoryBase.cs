using Microsoft.EntityFrameworkCore;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Infrastructure.Repositories;

public class RepositoryBase<T>
  where T : AggregateRoot
{
  protected readonly ApplicationDbContext _dbContext;
  public RepositoryBase(ApplicationDbContext dbContext)
  {
    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  }
  public async Task AddAsync(T entity, CancellationToken cancellationToken)
  {
    await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }
  public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
  {
    _dbContext.Set<T>().Remove(entity);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }
  public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
  {
    return await _dbContext.Set<T>().ToListAsync(cancellationToken);
  }
  public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
  }

  public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
  {
    _dbContext.Set<T>().Update(entity);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }
}
