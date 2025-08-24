using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;
using BusinessManager.Application.Mappers;
using BusinessManager.Domain.Repositories;

namespace BusinessManager.Application.UseCases.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto?>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<OrderDto?> HandleAsync(CreateOrderCommand request)
        {
            // Check if customer exist
            var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null)
                throw new KeyNotFoundException("Customer nos exists");

            Domain.Entities.Order order = new (
                    Guid.NewGuid(),
                    customer.Id,
                    request.Description,
                    Domain.Enums.OrderStatus.Pendding                
                );

            await _orderRepository.AddOrderAsync(order);

            return OrderMapper.ToDto(order);
        }
    }
}
