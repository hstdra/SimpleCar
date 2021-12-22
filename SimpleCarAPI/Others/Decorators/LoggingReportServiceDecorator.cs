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

        public Task<string> GetReport(int transactionId, string currency)
        {
            _logger.LogInformation("Getting transaction report {transactionId}", transactionId);
            return _reportService.GetReport(transactionId, currency);
        }

        public Task<string> GetReports(string currency)
        {
            _logger.LogInformation("Getting transaction reports");
            return _reportService.GetReports(currency);
        }
    }
}
