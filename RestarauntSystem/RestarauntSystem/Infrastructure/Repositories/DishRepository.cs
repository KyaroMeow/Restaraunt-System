using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;



namespace RestarauntSystem.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly RestaurantDbContext _context;

        public DishRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            return await _context.Dishes
                .Include(d => d.DishCategory)
                .ToListAsync();
        }

        public async Task<Dish> GetByIdAsync(int id)
        {
            return await _context.Dishes
                .Include(d => d.DishCategory)
                .FirstOrDefaultAsync(d => d.DishId == id);
        }

        public async Task<Dish> AddAsync(Dish dish)
        {
            await _context.Dishes.AddAsync(dish);
            await _context.SaveChangesAsync();
            return dish;
        }

        public async Task UpdateAsync(Dish dish)
        {
            _context.Dishes.Update(dish);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dish = await GetByIdAsync(id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Dish>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Dishes
                .Where(d => d.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Dish> GetWithComponentsAsync(int id)
        {
            return await _context.Dishes
                .Include(d => d.DishComponents)
                .ThenInclude(dc => dc.Product)
                .FirstOrDefaultAsync(d => d.DishId == id);
        }
    }
}
