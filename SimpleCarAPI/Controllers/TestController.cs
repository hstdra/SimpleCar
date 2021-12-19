using Microsoft.AspNetCore.Mvc;
using SimpleCar.Services;

namespace SimpleCar.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("/get-customer-car-details/{customerId}")]
    public async Task<IActionResult> GetCustomerCarDetails(
        [FromRoute] int customerId,
        [FromServices] IReportService reportService,
        [FromQuery] string currency = "USD"
    )
    {
        var customerReport = await reportService.GetCustomerReport(customerId, currency);
        return Ok(customerReport);
    }
}