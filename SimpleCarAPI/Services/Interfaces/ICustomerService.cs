using SimpleCar.Models.Entities;

namespace SimpleCar.Services.Interfaces;

public interface ICustomerService
{
    Task<List<Customer>> GetAll();

    Task<Customer?> GetById(int id);
}
