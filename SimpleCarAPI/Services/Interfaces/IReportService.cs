namespace SimpleCar.Services.Interfaces;

public interface IReportService
{
    Task<string> GetReport(int transactionId, string currency);
    Task<string> GetReports(string currency);
}
