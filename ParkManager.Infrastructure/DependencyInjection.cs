using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkManager.Application.Common;
using ParkManager.Infrastructure.Repositories;

namespace ParkManager.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructue(this IServiceCollection services)
  {

    AddPersistence(services);

    return services;
  }

  private static void AddPersistence(IServiceCollection services)
  {
    services.AddDbContext<ApplicationDbContext>(options =>
    {
      options.UseSqlite("DataSource=parkmanager.db");
    });

    services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();    
    services.AddScoped<IVehicleRepository, VehicleRepository>();
  }
}
