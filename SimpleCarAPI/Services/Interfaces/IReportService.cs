namespace SimpleCar.Services.Interfaces;

public interface IReportService
{
    Task<string> GetTransactionReport(int transactionId, string currency);
    Task<string> GetTransactionReports(string currency);
}
