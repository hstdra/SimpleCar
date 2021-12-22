using System.Text;
using SimpleCar.Models.DTOs;
using SimpleCar.Others;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Services.Implementations;

public class FlyweightReportService : IReportService
{
    private readonly ICustomerService _customerService;
    private readonly ICarService _carService;
    private readonly ITransactionService _transactionService;
    private readonly ICurrencyConverter _currencyConverter;
    private readonly ReportFlyweightFactory _flyweightFactory;

    public FlyweightReportService(CustomerService customerService, CarService carService,
        TransactionService transactionService, ICurrencyConverter currencyConverter,
        ReportFlyweightFactory flyweightFactory)
    {
        _customerService = customerService;
        _carService = carService;
        _transactionService = transactionService;
        _currencyConverter = currencyConverter;
        _flyweightFactory = flyweightFactory;
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
            
            var reportFlyweight = _flyweightFactory.GetReportFlyweight(car);
            stringBuilder.AppendLine(reportFlyweight.GetReport(customer, transaction));
        }

        return stringBuilder.ToString();
    }
}