using ExcelMapper;
using SimpleCar.Models.Entities;

namespace SimpleCar.Services.Implementations;

public class TransactionService : ITransactionService
{
    private readonly IReadOnlyList<Transaction> _transactions;

    public TransactionService(IWebHostEnvironment webHostEnvironment)
    {
        const string sheetName = "Transactions";
        var filePath = $"{webHostEnvironment.ContentRootPath}/source-data.xlsx";

        using var stream = File.OpenRead(filePath);
        using var importer = new ExcelImporter(stream);
        var sheet = importer.ReadSheet(sheetName);
        _transactions = sheet.ReadRows<Transaction>().ToList().AsReadOnly();
    }

    public Task<List<Transaction>> GetAll()
    {
        return Task.FromResult(_transactions.ToList());
    }

    public Task<Transaction?> GetById(int id)
    {
        return Task.FromResult(_transactions.FirstOrDefault(t => t.Id == id));
    }

    public Task<List<Transaction>> GetByCarId(int carId)
    {
        return Task.FromResult(_transactions.Where(t => t.CarId == carId).ToList());
    }

    public Task<List<Transaction>> GetByCustomerId(int customerId)
    {
        return Task.FromResult(_transactions.Where(t => t.CustomerId == customerId).ToList());
    }
}