using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParkManager.Application.Establishments.Create;

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
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/<EstablishmentController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
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
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<EstablishmentController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
