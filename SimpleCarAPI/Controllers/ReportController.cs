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
        return Ok(reports.Select(x => x.GetReport()));
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
        return Ok(report.GetReport());
    }

    [HttpGet]
    [Route("test")]
    public async Task<IActionResult> Test(

    )
    {
        await FlyweightTest.Run();
        return Ok();
    }
}