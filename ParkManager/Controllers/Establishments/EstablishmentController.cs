using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Establishments.Create;
using ParkManager.Application.Establishments.Delete;
using ParkManager.Application.Establishments.Read;
using ParkManager.Application.Establishments.Update;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkManager.Controllers.Establishments
{
  [Route("api/[controller]")]
  [ApiController]
  public class EstablishmentController : ControllerBase
  {
    private readonly ISender _sender;

    public EstablishmentController(ISender sender)
    {
      _sender = sender;
    }

    // GET: api/<EstablishmentController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var command = new GetAllEstablishmentsQuery();

      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok(result.Value);
    }

    // GET api/<EstablishmentController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
      var command = new GetEstablishmentQuery
      {
        Id = id
      };

      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok(result.Value);
    }

    // POST api/<EstablishmentController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateEstablishmentRequest value)
    {
      var command = new CreateEstablishmentCommand(
        value.Name,
        value.Cnpj,
        value.City,
        value.State,
        value.Street,
        value.Number,
        value.Complement,
        value.ZipCode,
        value.Phone,
        value.MotorcyclesParkingSpaces,
        value.CarsParkingSpaces
      );

      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok(result.Value);
    }

    // PUT api/<EstablishmentController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateEstablishmentRequest value)
    {
      var command = new UpdateEstablishmentCommand(
        value.Name,
        value.Cnpj,
        value.City,
        value.State,
        value.Street,
        value.Number,
        value.Complement,
        value.ZipCode,
        value.Phone,
        value.MotorcyclesParkingSpaces,
        value.CarsParkingSpaces,
        id
      );

      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok(result.Value);
    }

    // DELETE api/<EstablishmentController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var command = new DeleteEstablishmentCommand { Id = id };

      var result = await _sender.Send(command);

      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }

      return Ok();
    }
  }
}
