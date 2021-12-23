using System.Text;
using SimpleCar.Models.DTOs;
using SimpleCar.Services.Implementations;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Others.Bridges;

public class BridgeReportService : IReportService
{
    private readonly ICustomerService _customerService;
    private readonly ICarService _carService;
    private readonly ITransactionService _transactionService;
    private readonly ICurrencyConverter _currencyConverter;

    public BridgeReportService(CustomerService customerService, CarService carService, TransactionService transactionService, ICurrencyConverter currencyConverter)
    {
        _customerService = customerService;
        _carService = carService;
        _transactionService = transactionService;
        _currencyConverter = currencyConverter;
    }

    public async Task<string> GetReport(int transactionId, string currency)
    {
        var transaction = await _transactionService.GetById(transactionId);
        if (transaction is null) throw new ArgumentNullException(nameof(transactionId), "Transaction not found");

        var car = await _carService.GetById(transaction.CarId);
        if (car is null) throw new ArgumentNullException(nameof(transaction.CarId), "Car not found");

        var customer = await _customerService.GetById(transaction.CustomerId);
        if (customer is null) throw new ArgumentNullException(nameof(transaction.CustomerId), "Customer not found");

        transaction.Amount = _currencyConverter.Convert(transaction.Currency, currency, transaction.Amount);
        transaction.Currency = currency;

        var report = new Report(car, customer, transaction);

        return report.GetReport();
    }

    public async Task<string> GetReports(string currency)
    {
        var stringBuilder = new StringBuilder();
        var transactions = await _transactionService.GetAll();
        foreach (var transaction in transactions)
        {
            var car = await _carService.GetById(transaction.CarId);
            if (car is null) throw new ArgumentNullException(nameof(transaction.CarId), "Car not found");

            var customer = await _customerService.GetById(transaction.CustomerId);
            if (customer is null) throw new ArgumentNullException(nameof(transaction.CustomerId), "Customer not found");

            transaction.Amount = _currencyConverter.Convert(transaction.Currency, currency, transaction.Amount);
            transaction.Currency = currency;

            var report = new Report(car, customer, transaction);

            stringBuilder.AppendLine(report.GetReport());
        }

        return stringBuilder.ToString();
    }
}