using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class WorkScheduleRepository : IWorkScheduleRepository
    {
        private readonly RestaurantDbContext _context;

        public WorkScheduleRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkSchedule>> GetAllAsync()
        {
            return await _context.WorkSchedules.ToListAsync();
        }

        public async Task<WorkSchedule> GetByIdAsync(int id)
        {
            return await _context.WorkSchedules.FindAsync(id);
        }

        public async Task<WorkSchedule> AddAsync(WorkSchedule schedule)
        {
            await _context.WorkSchedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task UpdateAsync(WorkSchedule schedule)
        {
            _context.WorkSchedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var schedule = await GetByIdAsync(id);
            _context.WorkSchedules.Remove(schedule);
            await _context.SaveChangesAsync();
        }
    }
}
