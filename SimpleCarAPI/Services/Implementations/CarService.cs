using ExcelMapper;
using SimpleCar.Models.Entities;
using SimpleCar.Services.Interfaces;

namespace SimpleCar.Services.Implementations;

public class CarService : ICarService
{
    private readonly IReadOnlyList<Car> _cars;

    public CarService(IWebHostEnvironment webHostEnvironment)
    {
        const string sheetName = "Cars";
        var filePath = $"{webHostEnvironment.ContentRootPath}/source-data.xlsx";

        using var stream = File.OpenRead(filePath);
        using var importer = new ExcelImporter(stream);
        var sheet = importer.ReadSheet(sheetName);
        _cars = sheet.ReadRows<Car>().ToList().AsReadOnly();
    }

    public Task<List<Car>> GetAll()
    {
        return Task.FromResult(_cars.ToList());
    }

    public Task<Car?> GetById(int id)
    {
        return Task.FromResult(_cars.FirstOrDefault(t => t.Id == id));
    }
}