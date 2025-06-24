using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly RestaurantDbContext _context;

        public OrderItemRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Dish)
                .ToListAsync();
        }

        public async Task<OrderItem> AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int orderId, int dishId)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderId == orderId && oi.DishId == dishId);

            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllForOrderAsync(int orderId)
        {
            var items = await GetByOrderIdAsync(orderId);
            _context.OrderItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
