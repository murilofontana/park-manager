using Microsoft.AspNetCore.Mvc;

namespace ParkManager.Controllers.Reports
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReportsController : ControllerBase
  {

    [HttpGet("/total-summary")]
    public IActionResult GetTotalSummary()
    {
      // This is a placeholder for the actual implementation.
      // You would typically call a service to get the total summary data.
      var totalSummary = new
      {
        TotalEstablishments = 100,
        TotalVisitors = 5000,
        TotalRevenue = 250000.00
      };
      return Ok(totalSummary);
    }
  }
}
