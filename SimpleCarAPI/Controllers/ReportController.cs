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
        var reports = await reportService.GetReports(currency);
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
        var report = await reportService.GetReport(transactionId, currency);
        return Ok(report);
    }

    [HttpGet]
    [Route("benchmark-flyweight")]
    public async Task<IActionResult> BenchmarkFlyweight()
    {
        var result = await FlyweightBenchmarks.Run();
        return Content(result, "text/html");
    }

    [HttpGet]
    [Route("show-car-composite")]
    public async Task<IActionResult> ShowCarComposite([FromServices] CarCompositeTests tests)
    {
        await tests.ShowInfomation();
        return Ok();
    }
}