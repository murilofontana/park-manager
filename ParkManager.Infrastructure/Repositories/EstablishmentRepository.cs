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
  public async Task AddAsync(Establishment establishment)
  {
    await _dbcontext.Set<Establishment>().AddAsync(establishment);
    await _dbcontext.SaveChangesAsync();
  }

  public async Task<Establishment> GetById(Guid id)
  {
    return await _dbcontext.Set<Establishment>()
      .FirstOrDefaultAsync(e => e.Id == id)
      ?? throw new KeyNotFoundException($"Establishment with ID {id} not found.");
  }
}
