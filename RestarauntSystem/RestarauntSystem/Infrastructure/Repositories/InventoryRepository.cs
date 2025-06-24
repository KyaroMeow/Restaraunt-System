using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly RestaurantDbContext _context;

        public InventoryRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _context.Inventory
                .Include(i => i.Product)
                .ToListAsync();
        }

        public async Task<Inventory> GetByProductIdAsync(int productId)
        {
            return await _context.Inventory
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.ProductId == productId);
        }

        public async Task<Inventory> AddAsync(Inventory inventory)
        {
            await _context.Inventory.AddAsync(inventory);
            await _context.SaveChangesAsync();
            return inventory;
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            _context.Inventory.Update(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int productId)
        {
            var inventory = await GetByProductIdAsync(productId);
            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetLowStockItemsAsync(decimal threshold)
        {
            return await _context.Inventory
                .Where(i => i.Quantity <= threshold)
                .ToListAsync();
        }

        public async Task UpdateStockAsync(int productId, decimal quantityChange)
        {
            var inventory = await GetByProductIdAsync(productId);
            inventory.Quantity += quantityChange;
            await UpdateAsync(inventory);
        }
    }
}
