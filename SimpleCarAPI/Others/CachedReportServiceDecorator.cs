using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SimpleCar.Models.DTOs;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Others
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

        public async Task<TransactionReport> GetTransactionReport(int transactionId, string currency)
        {
            var key = $"GetTransactionReport-{transactionId}";
            if (_memoryCache.TryGetValue(key, out TransactionReport cachedTransactionReport))
            {
                _logger.LogInformation("Getting transaction report {transactionId} from cache", transactionId);
                return cachedTransactionReport;
            }

            var transactionReport = await _innerReportService.GetTransactionReport(transactionId, currency);
            _memoryCache.Set(key, transactionReport, TimeSpan.FromMinutes(5));

            return transactionReport;
        }

        public Task<List<TransactionReport>> GetTransactionReports(string currency)
        {
            return _innerReportService.GetTransactionReports(currency);
        }
    }
}
