using MediatR;
using ParkManager.Domain;
using ParkManager.Domain.Abstraction;

namespace ParkManager.Application.Vehicles.Update;

public class UpdateVehicleCommand : IRequest<Result<Vehicle>>
{
  public UpdateVehicleCommand(Guid id,string branch, string model, string plate, string color, EVehicleType type)
  {
    Id = id;
    Branch = branch;
    Model = model;
    Plate = plate;
    Color = color;
    Type = type;
  }

  public Guid Id { get; set; }
  public string Branch { get; set; }
  public string Model { get; set; }
  public string Plate { get; set; }
  public string Color { get; set; }
  public EVehicleType Type { get; set; }
}
