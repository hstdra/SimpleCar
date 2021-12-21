using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SimpleCar.Models.DTOs;
using SimpleCar.Services.Implementations;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Decorators
{
    public class ReportServiceProxy : IReportService
    {
        private readonly BridgeReportService _reportService;

        public ReportServiceProxy(BridgeReportService reportService)
        {
            _reportService = reportService;
        }

        public async Task<TransactionReport?> GetTransactionReport(int transactionId, string currency)
        {
            var transactionReport = await _reportService.GetTransactionReport(transactionId, currency);
            return transactionReport.CustomerType == "Normal" ? transactionReport : null;
        }

        public async Task<List<TransactionReport>> GetTransactionReports(string currency)
        {
            var transactionReports = await _reportService.GetTransactionReports(currency);
            return transactionReports.Where(x => x.CustomerType == "Normal").ToList();
        }
    }
}
