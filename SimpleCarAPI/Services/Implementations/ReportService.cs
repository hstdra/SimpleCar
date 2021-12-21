using MoneyHelperLib;
using SimpleCar.Models.DTOs;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Services.Implementations;

public class ReportService : IReportService
{
    private readonly CustomerService _customerService;
    private readonly CarService _carService;
    private readonly TransactionService _transactionService;
    private readonly IMoneyHelper _moneyHelper;

    public ReportService(CustomerService customerService, CarService carService, TransactionService transactionService,
        IMoneyHelper moneyHelper)
    {
        _customerService = customerService;
        _carService = carService;
        _transactionService = transactionService;
        _moneyHelper = moneyHelper;
    }

    public async Task<TransactionReport> GetTransactionReport(int transactionId, string currency)
    {
        var transaction = await _transactionService.GetById(transactionId);
        if (transaction is null) throw new ArgumentNullException(nameof(transactionId), "Transaction not found");

        var car = await _carService.GetById(transaction.CarId);
        if (car is null) throw new ArgumentNullException(nameof(transaction.CarId), "Car not found");

        var customer = await _customerService.GetById(transaction.CustomerId);
        if (customer is null) throw new ArgumentNullException(nameof(transaction.CustomerId), "Customer not found");

        return new()
        {
            CustomerTitle = customer.Title,
            CustomerName = customer.Name,
            CarBrand = car.Brand,
            CarYear = car.Year,
            CarModel = car.Model,
            Currency = currency,
            Amount = _moneyHelper.Convert(transaction.Amount, transaction.Currency, currency),
            PurchasedDate = transaction.PurchasedDate,
            CustomerType = customer.Type,
        };
    }

    public async Task<List<TransactionReport>> GetTransactionReports(string currency)
    {
        var transactions = await _transactionService.GetAll();
        return transactions.Select(async x => await GetTransactionReport(x.Id, currency)).Select(x => x.Result).ToList();
    }
}