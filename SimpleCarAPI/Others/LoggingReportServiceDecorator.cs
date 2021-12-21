using SimpleCar.Models.DTOs;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Others
{
    public class LoggingReportServiceDecorator : IReportService
    {
        private readonly ILogger<IReportService> _logger;
        private readonly IReportService _reportService;

        public LoggingReportServiceDecorator(IReportService reportService, ILogger<IReportService> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        public Task<TransactionReport> GetTransactionReport(int transactionId, string currency)
        {
            _logger.LogInformation("Getting transaction report {transactionId}", transactionId);
            return _reportService.GetTransactionReport(transactionId, currency);
        }

        public Task<List<TransactionReport>> GetTransactionReports(string currency)
        {
            _logger.LogInformation("Getting transaction reports");
            return _reportService.GetTransactionReports(currency);
        }
    }
}
