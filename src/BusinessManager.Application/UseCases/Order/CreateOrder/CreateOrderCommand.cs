using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.Order.CreateOrder
{
    public record CreateOrderCommand(
            Guid CustomerId,
            string Description     
        ): IRequest<OrderDto?>;
}
