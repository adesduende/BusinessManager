using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.Customer.GetAllCustomers
{
    public class GetAllCustomerCommandHandler : IRequestHandler<GetAllCustomerCommand,List<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerDto>> HandleAsync(GetAllCustomerCommand request)
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            if (customers == null || !customers.Any())
            {
                return new List<CustomerDto>();
            }
            return customers.Select(customer => CustomerMapper.ToDto(customer)).ToList();
        }
    }
}
