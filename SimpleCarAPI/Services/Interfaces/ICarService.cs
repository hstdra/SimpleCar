using SimpleCar.Models.Entities;

namespace SimpleCar.Services.Interfaces;

public interface ICarService
{
    Task<List<Car>> GetAll();

    Task<Car?> GetById(int id);
}
