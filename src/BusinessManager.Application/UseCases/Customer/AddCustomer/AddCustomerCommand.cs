using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Domain.ValueObjects;

namespace BusinessManager.Application.UseCases.Customer.AddCustomer
{
    public record AddCustomerCommand(
            string Name,
            string Surname,
            string Email,
            string PhoneNumber,
            string Nif,
            Address Address
        ) : IRequest<CustomerDto?>;
}
