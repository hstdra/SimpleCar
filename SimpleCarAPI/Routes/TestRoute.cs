using Microsoft.AspNetCore.Mvc;
using SimpleCar.Others.Composites;
using SimpleCar.Others.Facades;
using SimpleCar.Others.Flyweights;
using SimpleCar.Services.Interfaces;
using System.Net.Mime;

namespace SimpleCar.Routes;

public static class TestRoute
{
    public static void AddTestRoutes(this IEndpointRouteBuilder app)
    {
        app.MapGet("show-reports", ShowReports);
        app.MapGet("show-report/{transactionId}", ShowReport);
        app.MapGet("show-car-composite", ShowCarComposite);
        app.MapGet("benchmark-flyweight", BenchmarkFlyweight);
    }

    private static async Task<IResult> ShowReports(
        [FromServices] FacadeReportService reportService,
        [FromQuery] string currency = "USD"
    )
    {
        var reports = await reportService.GetReports(currency);
        return Results.Content(reports, MediaTypeNames.Text.Plain);
    }

    public static async Task<IResult> ShowReport(
        [FromRoute] int transactionId,
        [FromServices] IReportService reportService,
        [FromQuery] string currency = "USD"
    )
    {
        var report = await reportService.GetReport(transactionId, currency);
        return Results.Content(report, MediaTypeNames.Text.Plain);
    }

    public static async Task<IResult> ShowCarComposite(
        [FromServices] CarCompositeTests tests
    )
    {
        await tests.ShowInfomation();
        return Results.Ok();
    }

    public static async Task<IResult> BenchmarkFlyweight()
    {
        var result = await FlyweightBenchmarks.Run();
        return Results.Content(result, MediaTypeNames.Text.Html);
    }
}
