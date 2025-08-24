using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.Customer.AddCustomer
{
    internal class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerDto?>
    {
        private readonly ICustomerRepository _customerRepository;

        public AddCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto?> HandleAsync(AddCustomerCommand request)
        {
            try
            {
                var newCustomer = new Domain.Entities.Customer(
                    Guid.NewGuid(),
                    request.Name,
                    request.Surname,
                    request.Email,
                    request.PhoneNumber,
                    request.Nif,
                    request.Address
                );

                await _customerRepository.AddCustomerAsync(newCustomer);

                return CustomerMapper.ToDto(newCustomer);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the customer.", ex);
            }
        }
    }
}
