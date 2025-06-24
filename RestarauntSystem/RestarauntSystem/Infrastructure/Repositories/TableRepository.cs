using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using RestarauntSystem.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly RestaurantDbContext _context;

        public TableRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Table>> GetAllAsync()
        {
            return await _context.Tables
                .Include(t => t.Zone)
                .Include(t => t.Status)
                .ToListAsync();
        }

        public async Task<Table> GetByIdAsync(int id)
        {
            return await _context.Tables
                .Include(t => t.Zone)
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.TableId == id);
        }

        public async Task<Table> AddAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
            return table;
        }

        public async Task UpdateAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var table = await GetByIdAsync(id);
            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Table>> GetByZoneAsync(int zoneId)
        {
            return await _context.Tables
                .Where(t => t.ZoneId == zoneId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date)
        {
            return await _context.Tables
                .Where(t => t.Status.StatusName == "Available" ||
                           !_context.Reservations.Any(r =>
                               r.TableId == t.TableId &&
                               r.ReservationTime.Date == date.Date))
                .ToListAsync();
        }

        public async Task UpdateTableStatusAsync(int tableId, int statusId)
        {
            var table = await GetByIdAsync(tableId);
            table.StatusId = statusId;
            await UpdateAsync(table);
        }
    }
}
