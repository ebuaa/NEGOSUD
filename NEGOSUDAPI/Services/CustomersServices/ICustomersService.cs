using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Services.CustomersServices
{
    public interface ICustomersService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
    }
}
