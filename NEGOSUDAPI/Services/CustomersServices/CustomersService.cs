using Microsoft.EntityFrameworkCore;
using NEGOSUDAPI.Data;
using NEGOSUDAPI.Models.Entities;
using NEGOSUDAPI.Services.CustomersServices;

namespace NEGOSUDAPI.Services.CustomersServices
{
    public class CustomersService : ICustomersService
    {
        private readonly ApplicationDbContext _context;

        public CustomersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();

        }

        public async Task<Customer?> GetCustomerByIdAsync(int customerID)
        {
            return await _context.Customers.FindAsync(customerID);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
