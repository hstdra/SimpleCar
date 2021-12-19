using MoneyHelperLib;
using SimpleCar.Models.DTOs;
using Transaction = SimpleCar.Models.Entities.Transaction;

namespace SimpleCar.Services;

public interface IReportService
{
    Task<CustomerReport?> GetCustomerReport(int customerId, string currency);
    Task<TransactionReport?> GetTransactionReport(Transaction transaction, string currency);
}

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

    public async Task<CustomerReport?> GetCustomerReport(int customerId, string currency)
    {
        var customer = await _customerService.GetById(customerId);
        if (customer is null) throw new ArgumentNullException(nameof(customerId), "Customer not found");

        var transactions = await _transactionService.GetByCustomerId(customerId);

        return new()
        {
            Name = customer.Name,
            CarReports = transactions.Select(x => GetTransactionReport(x, currency)).Select(x => x.Result).ToList()
        };
    }

    public async Task<TransactionReport?> GetTransactionReport(Transaction transaction, string currency)
    {
        var car = await _carService.GetById(transaction.CarId);
        if (car is null) throw new ArgumentNullException(nameof(car), "Car not found");

        return new()
        {
            CarName = $"{car.Brand} {car.Model} {car.Year}",
            PurchaseDate = transaction.PurchasedDate,
            Price = $"{_moneyHelper.Convert(transaction.Amount, transaction.Currency, currency)} {currency.ToUpper()}"
        };
    }
}