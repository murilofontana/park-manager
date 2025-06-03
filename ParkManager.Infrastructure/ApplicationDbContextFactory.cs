using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace ParkManager.Infrastructure;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
  public ApplicationDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    // Use your actual connection string or a test one for migrations
    optionsBuilder.UseSqlite("Data Source=parkmanager.db");

    return new ApplicationDbContext(optionsBuilder.Options);
  }
}
