using ExcelMapper;
using SimpleCar.Models;
using SimpleCar.Models.Entities;

namespace SimpleCar.Services;

public class CustomerService
{
    private readonly IReadOnlyList<Customer> _customers;

    public CustomerService(IWebHostEnvironment webHostEnvironment)
    {
        const string sheetName = "Customers";
        var filePath = $"{webHostEnvironment.ContentRootPath}/source-data.xlsx";
        
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