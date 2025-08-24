using BusinessManager.Domain.Entities;
using BusinessManager.Domain.Repositories;
using BusinessManager.Domain.ValueObjects;
using BusinessManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessManager.Infrastructure.Repositories.SqlServerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BusinessContext _context;

        public CustomerRepository(BusinessContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Set<Customer>().AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.Set<Customer>().Remove(customer);
            await _context.SaveChangesAsync();
            throw new NotImplementedException();
        }

        public async Task DeleteCustomerByIdAsync(Guid id)
        {
            var customer = await _context.Set<Customer>().FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
            _context.Set<Customer>().Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomersAsync(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                _context.Set<Customer>().Remove(customer);
            }
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Customer>> FindCustomersByExpressionAsync(CustomerSearchExpression customerSearchExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var customers = await _context.Set<Customer>().ToListAsync();
            return customers;
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _context.Set<Customer>().FindAsync(id);
            if (customer == null)
            {
                throw new KeyNotFoundException($"Customer with ID {id} not found.");
            }
            return customer;
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var res = _context.Set<Customer>().Update(customer);
            if (res == null)
            {
                throw new KeyNotFoundException($"Customer with ID {customer.Id} not found.");
            }
            await _context.SaveChangesAsync();
        }
    }
}
