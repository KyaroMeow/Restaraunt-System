using Microsoft.EntityFrameworkCore;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;

namespace RestarauntSystem.Infrastructure.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly RestaurantDbContext _context;

        public DeliveryRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Delivery>> GetAllAsync()
        {
            return await _context.Deliveries
                .Include(d => d.Employee)
                .ToListAsync();
        }

        public async Task<Delivery> GetByIdAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(d => d.DeliveryId == id);
        }

        public async Task<Delivery> AddAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
            return delivery;
        }

        public async Task UpdateAsync(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var delivery = await GetByIdAsync(id);
            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Delivery>> GetByDateAsync(DateTime date)
        {
            return await _context.Deliveries
                .Where(d => d.DeliveryTime.Date == date.Date)
                .ToListAsync();
        }

        public async Task<Delivery> GetWithItemsAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.DeliveryItems)
                .ThenInclude(di => di.Product)
                .FirstOrDefaultAsync(d => d.DeliveryId == id);
        }
    }
}
