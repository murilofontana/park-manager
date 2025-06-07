using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ParkManager.Domain;
using ParkManager.Domain.Exceptions;

namespace ParkManager.Infrastructure
{

  public sealed class ApplicationDbContext : DbContext
  {
    public DbSet<Establishment> Establishments { get; set; } = null!;
    public DbSet<Vehicle> Vehicles { get; set; } = null!;
    public DbSet<ParkingMovement> ParkingMovement { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Scan the assembly that contains the ApplicationDbContext and Apply all configuration from that assembly
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

      base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
      try
      {

        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        return result;
      }
      catch (DbUpdateConcurrencyException ex)
      {
        // Handle concurrency exceptions
        throw new ConcurrencyException("A concurrency error occurred while saving changes. Please try again.", ex);
      }
      catch (Exception)
      {
        throw;
      }
    }

  }

}
