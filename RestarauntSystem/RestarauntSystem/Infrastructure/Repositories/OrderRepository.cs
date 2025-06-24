using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly RestaurantDbContext _context;

        public OrderRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<Order> AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetByDateAsync(DateTime date)
        {
            return await _context.Orders
                .Where(o => o.OrderTime.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetActiveOrdersAsync()
        {
            return await _context.Orders
                .Where(o => o.StatusId == 2)
                .ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetByStatusAsync(int statusId)
        {
            return await _context.Orders
                .Where(o => o.StatusId == statusId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
                .ToListAsync();
        }
        public async Task<Order> GetOrderWithItemsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Dish)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<IEnumerable<Order>> GetByTableAsync(int tableId)
        {
            return await _context.Orders
                .Where(o => o.TableId == tableId)
                .ToListAsync();
        }
    }
}
