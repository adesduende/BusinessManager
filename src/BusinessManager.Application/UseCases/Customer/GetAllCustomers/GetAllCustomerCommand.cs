using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.Customer.GetAllCustomers
{
    public record GetAllCustomerCommand(): IRequest<List<CustomerDto>>;
}
