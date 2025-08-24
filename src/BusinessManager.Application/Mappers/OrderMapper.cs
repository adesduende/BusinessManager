using BusinessManager.Application.Dtos;
using BusinessManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToDto(Order order)
        {
            return new OrderDto{
                Id = order.Id.ToString(),
                Description = order.Description,
                Status = order.Status,
                createdAt = order.createdAt,
                updatedAt = order.updatedAt,
                CustomerId = order.CustomerId.ToString()
            };
        }
        public static OrderDto ToDto(Order order,List<User> technicians)
        {
            return new OrderDto
            {
                Id = order.Id.ToString(),
                Description = order.Description,
                Status = order.Status,
                createdAt = order.createdAt,
                updatedAt = order.updatedAt,
                CustomerId = order.CustomerId.ToString(),
                Technicians = technicians?.Select(t => t.Id.ToString()).ToList()
            };
        }
    }
}
