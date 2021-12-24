using Microsoft.Extensions.Caching.Memory;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Others.Decorators
{
    public class CachedReportServiceDecorator : IReportService
    {
        private readonly IReportService _innerReportService;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<IReportService> _logger;

        public CachedReportServiceDecorator(IReportService reportService, IMemoryCache memoryCache, ILogger<IReportService> logger)
        {
            _innerReportService = reportService;
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<string> GetReport(int transactionId, string currency)
        {
            _logger.LogInformation("Getting transaction report {transactionId} from cache...", transactionId);
            
            var key = $"GetReport-{transactionId}";
            if (_memoryCache.TryGetValue(key, out string cachedReport))
            {
                _logger.LogInformation("Got transaction report {transactionId} from cache!", transactionId);
                return cachedReport;
            }

            _logger.LogInformation("Could not get report {transactionId} from cache!", transactionId);
            var report = await _innerReportService.GetReport(transactionId, currency);
            _memoryCache.Set(key, report, TimeSpan.FromMinutes(5));

            return report;
        }

        public Task<string> GetReports(string currency)
        {
            return _innerReportService.GetReports(currency);
        }
    }
}
