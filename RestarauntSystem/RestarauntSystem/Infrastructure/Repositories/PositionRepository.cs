using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class PositionRepository : IPositionRepository
    {
        private readonly RestaurantDbContext _context;

        public PositionRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Position>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<Position> GetByIdAsync(int id)
        {
            return await _context.Positions.FindAsync(id);
        }

        public async Task<Position> AddAsync(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
            return position;
        }

        public async Task UpdateAsync(Position position)
        {
            _context.Positions.Update(position);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var position = await GetByIdAsync(id);
            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
        }
    }
}
