using ParkManager.Domain;

namespace ParkManager.Application.Vehicles.Read;

public class VehicleResponse
{
  public VehicleResponse(string branch, string model, string plate, string color, EVehicleType type, Guid id)
  {
    Branch = branch;
    Model = model;
    Plate = plate;
    Color = color;
    Type = type;
    Id = id;
  }

  public Guid Id { get; set; }
  public string Branch { get; }
  public string Model { get; }
  public string Plate { get; set; }
  public string Color { get; set; }
  public EVehicleType Type { get; set; }

  public static VehicleResponse FromVehicle(Vehicle vehicle)
  {
    return new VehicleResponse(vehicle.Branch, vehicle.Model, vehicle.Plate, vehicle.Color, vehicle.Type, vehicle.Id);
  }

  public static IEnumerable<VehicleResponse> FromVehicles(IEnumerable<Vehicle> vehicles)
  {
    return vehicles.Select(FromVehicle);
  }
}
