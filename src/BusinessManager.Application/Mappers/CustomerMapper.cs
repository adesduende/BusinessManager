using BusinessManager.Application.Dtos;
using BusinessManager.Domain.Entities;

namespace BusinessManager.Application.Mappers
{
    public static class CustomerMapper
    {
        public static CustomerDto ToDto(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }
            return new CustomerDto
            {
                Id = customer.Id.ToString(),
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                Nif = customer.Nif,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address.ToString()
            };
        }
    }
}
