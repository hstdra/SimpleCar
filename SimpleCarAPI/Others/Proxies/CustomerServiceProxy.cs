using SimpleCar.Models.Entities;
using SimpleCar.Services.Implementations;
using SimpleCar.Services.Interfaces;
using System.Security;

namespace SimpleCar.Others.Proxies
{
    public class CustomerServiceProxy : ICustomerService
    {
        private readonly CustomerService _customerService;

        public CustomerServiceProxy(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<List<Customer>> GetAll()
        {
            var customers = await _customerService.GetAll();
            return customers.Where(x => x.Type == "Normal").ToList();
        }

        public async Task<Customer?> GetById(int id)
        {
            var customer = await _customerService.GetById(id);
            if (customer?.Type != "Normal")
                throw new SecurityException($"Don't enough permission to read user {id}!");
            
            return customer;
        }
    }
}
