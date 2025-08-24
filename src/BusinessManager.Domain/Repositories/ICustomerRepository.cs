using BusinessManager.Domain.Entities;
using BusinessManager.Domain.ValueObjects;

namespace BusinessManager.Domain.Repositories
{
    public interface ICustomerRepository
    {
        //Create
        Task AddCustomerAsync(Customer customer);

        //Read
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<Customer>> FindCustomersByExpressionAsync(CustomerSearchExpression customerSearchExpression);

        //Update
        Task UpdateCustomerAsync(Customer customer);

        //Delete
        Task DeleteCustomerAsync(Customer customer);
        Task DeleteCustomerByIdAsync(Guid id);
        Task DeleteCustomersAsync(IEnumerable<Customer> customers);

    }
}
