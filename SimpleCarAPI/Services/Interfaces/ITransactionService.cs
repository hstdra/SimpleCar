using ExcelMapper;
using SimpleCar.Models.Entities;

namespace SimpleCar.Services.Implementations;

public interface ITransactionService
{
    public Task<List<Transaction>> GetAll();

    public Task<Transaction?> GetById(int id);

    public Task<List<Transaction>> GetByCarId(int carId);

    public Task<List<Transaction>> GetByCustomerId(int customerId);
}