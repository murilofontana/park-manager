using Microsoft.EntityFrameworkCore;
using ParkManager.Application.Common;
using ParkManager.Application.Establishments.Summary.DailySummaryGroupedByHour;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Repositories;

public class EstablishmentRepository : RepositoryBase<Establishment>, IEstablishmentRepository
{
  private readonly ApplicationDbContext _dbcontext;
  public EstablishmentRepository(ApplicationDbContext dbContext) : base(dbContext)
  {
    _dbcontext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  }

  public async Task<Establishment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
  {
    return await _dbcontext.Set<Establishment>()
      .Include("_parkingMovements")
      .AsTracking()
      .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
  }

  public async Task<IEnumerable<HourSummaryResponse>> GetDailySummaryGroupedByHourAsync(Guid establishmentId, DateOnly date, CancellationToken cancellationToken)
  {
    var startOfDay = date.ToDateTime(TimeOnly.MinValue);
    var endOfDay = startOfDay.AddDays(1);

    var sql = @"
        SELECT
            Hour,
            SUM(TotalCarsEntry) AS TotalCarsEntry,
            SUM(TotalCarsExit) AS TotalCarsExit,
            SUM(TotalMotorcyclesEntry) AS TotalMotorcyclesEntry,
            SUM(TotalMotorcyclesExit) AS TotalMotorcyclesExit
        FROM (
            -- Query for ENTRIES
            SELECT
                CAST(strftime('%H', EntryDate) AS INTEGER) AS Hour,
                SUM(CASE WHEN Type = 1 THEN 1 ELSE 0 END) AS TotalCarsEntry,
                0 AS TotalCarsExit,
                SUM(CASE WHEN Type = 0 THEN 1 ELSE 0 END) AS TotalMotorcyclesEntry,
                0 AS TotalMotorcyclesExit
            FROM ParkingMovements
            WHERE EstablishmentId = {0} AND EntryDate >= {1} AND EntryDate < {2}
            GROUP BY Hour

            UNION ALL

            -- Query for EXITS
            SELECT
                CAST(strftime('%H', ExitDate) AS INTEGER) AS Hour,
                0 AS TotalCarsEntry,
                SUM(CASE WHEN Type = 1 THEN 1 ELSE 0 END) AS TotalCarsExit,
                0 AS TotalMotorcyclesEntry,
                SUM(CASE WHEN Type = 0 THEN 1 ELSE 0 END) AS TotalMotorcyclesExit
            FROM ParkingMovements
            WHERE EstablishmentId = {0} AND ExitDate IS NOT NULL AND ExitDate >= {1} AND ExitDate < {2}
            GROUP BY Hour
        ) AS DailyEvents
        GROUP BY Hour
        ORDER BY Hour;";

    return await _dbcontext.Database.SqlQueryRaw<HourSummaryResponse>(sql, establishmentId, startOfDay, endOfDay)
        .ToListAsync(cancellationToken);
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
}
