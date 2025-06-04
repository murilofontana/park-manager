using MediatR;
using ParkManager.Domain.Abstraction;
using ParkManager.Domain;

namespace ParkManager.Application.Vehicles.Create;

public class CreateVehicleCommand : IRequest<Result<Vehicle>>
{
  public CreateVehicleCommand(string branch, string model, string plate, string color, EVehicleType type)
  {
    Branch = branch;
    Model = model;
    Plate = plate;
    Color = color;
    Type = type;
  }

  public string Branch { get; }
  public string Model { get; }
  public string Plate { get; set; }
  public string Color { get; set; }
  public EVehicleType Type { get; set; }

}
