using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace RestarauntSystem.Infrastructure.Repositories
{
    public class DishCategoryRepository : IDishCategoryRepository
    {
        private readonly RestaurantDbContext _context;

        public DishCategoryRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DishCategory>> GetAllAsync()
        {
            return await _context.DishCategories.ToListAsync();
        }

        public async Task<DishCategory> GetByIdAsync(int id)
        {
            return await _context.DishCategories.FindAsync(id);
        }

        public async Task<DishCategory> AddAsync(DishCategory category)
        {
            await _context.DishCategories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task UpdateAsync(DishCategory category)
        {
            _context.DishCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await GetByIdAsync(id);
            _context.DishCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
