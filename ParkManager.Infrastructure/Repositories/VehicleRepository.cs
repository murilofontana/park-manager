using Microsoft.EntityFrameworkCore;
using ParkManager.Application.Common;
using ParkManager.Domain;

namespace ParkManager.Infrastructure.Repositories;

public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
{
  public VehicleRepository(ApplicationDbContext dbcontext) : base(dbcontext)
  {
  }
}
