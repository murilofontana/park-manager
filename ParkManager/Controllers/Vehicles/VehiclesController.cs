using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Vehicles.Create;
using ParkManager.Application.Vehicles.Delete;
using ParkManager.Application.Vehicles.Read;
using ParkManager.Application.Vehicles.Update;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkManager.Controllers.Vehicles
{
  [Route("api/[controller]")]
  [ApiController]
  public class VehiclesController : ControllerBase
  {
    private readonly ISender _sender;

    public VehiclesController(ISender sender)
    {
      _sender = sender;
    }

    // GET: api/<VehiclesController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var command = new GetAllVehiclesQuery();
      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok(result.Value);
    }

    // GET api/<VehiclesController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var command = new GetVehicleQuery { Id = id };
      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok(result.Value);
    }

    // POST api/<VehiclesController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateVehicleRequest value)
    {
      var command = new CreateVehicleCommand(value.Branch, value.Model, value.Color, value.Plate, value.Type);
      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value);
    }

    // PUT api/<VehiclesController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateVehicleRequest value)
    {
      var command = new UpdateVehicleCommand(id, value.Branch, value.Model, value.Color, value.Plate, value.Type);
      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok(result.Value);
    }

    // DELETE api/<VehiclesController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var command = new DeleteVehicleCommand(id);
      var result = await _sender.Send(command);
      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok();
    }
  }
}
