using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Application.UseCases.Order.AssignOrder
{
    public class AssignOrderCommandHandler : IRequestHandler<AssignOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public AssignOrderCommandHandler(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> HandleAsync(AssignOrderCommand request)
        {
            var user = await _userRepository.GetUserByIdAsync(request.userId);
            if (user == null)            
                throw new ArgumentException($"User with ID {request.userId} not found.");
            var order = await _orderRepository.GetOrderByIdAsync(request.orderId);
            if (order == null)            
                throw new ArgumentException($"Order with ID {request.orderId} not found.");

            order.AddTechnician(user);

            await  _orderRepository.UpdateOrderAsync(order);

            return OrderMapper.ToDto(order, order.Technicians.ToList());

        }
    }
}
