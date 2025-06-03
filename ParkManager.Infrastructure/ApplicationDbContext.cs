using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ParkManager.Domain.Exceptions;

namespace ParkManager.Infrastructure
{

  public sealed class ApplicationDbContext : DbContext
  {

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
        throw new ConcurrencyException("Concurrency exception occurred.", ex);
      }
    }

  }

}
