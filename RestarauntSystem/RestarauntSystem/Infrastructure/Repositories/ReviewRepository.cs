using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RestaurantDbContext _context;

        public ReviewRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }

        public async Task<Review> AddAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task UpdateAsync(Review review)
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await GetByIdAsync(id);
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Review>> GetByCustomerAsync(int customerId)
        {
            return await _context.Reviews
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<double> GetAverageRatingAsync()
        {
            return await _context.Reviews
                .AverageAsync(r => (double)r.Rating);
        }
    }
}
