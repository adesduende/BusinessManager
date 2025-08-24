using BusinessManager.Domain.Entities;
using BusinessManager.Domain.ValueObjects;

namespace BusinessManager.Domain.Repositories
{
    public interface IOrderRepository
    {
        //Create
        Task AddOrderAsync(Order order);

        //Read
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<Order>?> GetAllOrdersAsync();
        Task<IEnumerable<Order>?> FindOrdersByExpressionAsync(OrderSearchExpression orderSearchExpression);

        //Update
        Task UpdateOrderAsync(Order order);

        //Delete
        Task DeleteOrderAsync(Order order);
        Task DeleteOrderByIdAsync(Guid id);
        Task DeleteOrdersAsync(IEnumerable<Order> orders);
    }
}
