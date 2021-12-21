using ExcelMapper;
using SimpleCar.Models.Entities;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Services.Implementations;

public class CustomerService : ICustomerService
{
    private readonly IReadOnlyList<Customer> _customers;

    public CustomerService()
    {
        const string sheetName = "Customers";
        var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}/source-data.xlsx";

        using var stream = File.OpenRead(filePath);
        using var importer = new ExcelImporter(stream);
        var sheet = importer.ReadSheet(sheetName);
        _customers = sheet.ReadRows<Customer>().ToList().AsReadOnly();
    }

    public Task<List<Customer>> GetAll()
    {
        return Task.FromResult(_customers.ToList());
    }

    public Task<Customer?> GetById(int id)
    {
        return Task.FromResult(_customers.FirstOrDefault(t => t.Id == id));
    }
}