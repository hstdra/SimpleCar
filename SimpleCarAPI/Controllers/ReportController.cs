using Microsoft.AspNetCore.Mvc;
using SimpleCar.Others;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Controllers;

[ApiController]
[Route("")]
public class ReportController : ControllerBase
{
    [HttpGet]
    [Route("/show-reports")]
    public async Task<IActionResult> ShowReports(
        [FromServices] IReportService reportService,
        [FromQuery] string currency = "USD"
    )
    {
        var reports = await reportService.GetTransactionReports(currency);
        return Ok(reports);
    }

    [HttpGet]
    [Route("/show-reports/{transactionId}")]
    public async Task<IActionResult> ShowReports(
        [FromRoute] int transactionId,
        [FromServices] IReportService reportService,
        [FromQuery] string currency = "USD"
    )
    {
        var report = await reportService.GetTransactionReport(transactionId, currency);
        return Ok(report);
    }

    [HttpGet]
    [Route("benchmark-flyweight")]
    public async Task<IActionResult> BenchmarkFlyweight()
    {
        var result = await FlyweightTest.Run();
        return Content(result, "text/html");
    }
}