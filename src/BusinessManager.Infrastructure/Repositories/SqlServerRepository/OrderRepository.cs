using BusinessManager.Domain.Entities;
using BusinessManager.Domain.Repositories;
using BusinessManager.Domain.ValueObjects;
using BusinessManager.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessManager.Infrastructure.Repositories.SqlServerRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BusinessContext _context;

        public OrderRepository(BusinessContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Set<Order>().AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public Task DeleteOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrdersAsync(IEnumerable<Order> orders)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>?> FindOrdersByExpressionAsync(OrderSearchExpression orderSearchExpression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order>?> GetAllOrdersAsync()
        {
            var result = await _context.Orders.ToListAsync();
            return result;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            var result = await _context.Set<Order>().FindAsync(id);
            return result;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            try
            {
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating order", ex);
            }

        }
    }
}
