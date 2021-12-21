using SimpleCar.Models.DTOs;

namespace SimpleCar.Services.Interfaces;

public interface IReportService
{
    Task<TransactionReport> GetTransactionReport(int transactionId, string currency);
    Task<List<TransactionReport>> GetTransactionReports(string currency);
}
