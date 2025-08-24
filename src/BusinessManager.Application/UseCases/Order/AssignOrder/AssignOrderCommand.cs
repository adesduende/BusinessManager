using BusinessManager.Application.Dtos;
using BusinessManager.Application.Interfaces;

namespace BusinessManager.Application.UseCases.Order.AssignOrder
{
    public record AssignOrderCommand(Guid userId, Guid orderId) : IRequest<OrderDto>;
}
