using ParkManager.Domain;

namespace ParkManager.Controllers.Vehicles;

public class UpdateVehicleRequest
{
  public string Branch { get;  set; }
  public string Model { get;  set; }
  public string Color { get;  set; }
  public string Plate { get;  set; }
  public EVehicleType Type { get;  set; }
}
