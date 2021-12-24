using SimpleCar.Services.Interfaces;

namespace SimpleCar.Others.Decorators
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

        public async Task<string> GetReport(int transactionId, string currency)
        {
            _logger.LogInformation("Getting transaction report {transactionId} ...", transactionId);
            var report = await _reportService.GetReport(transactionId, currency);
            _logger.LogInformation("Got transaction report {transactionId}!", transactionId);
            
            return report;
        }

        public async Task<string> GetReports(string currency)
        {
            _logger.LogInformation("Getting all transaction reports...");
            var reports = await _reportService.GetReports(currency);
            _logger.LogInformation("Got all transaction reports!");

            return reports;
        }
    }
}
