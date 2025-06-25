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
        .Select(t => new Table // Явная проекция без Reservations
        {
            TableId = t.TableId,
            ZoneId = t.ZoneId,
            StatusId = t.StatusId,
            Zone = t.Zone,
            Status = t.Status
            // Намеренно не включаем Reservations!
        })
        .AsNoTracking()
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
                .Include(t => t.Status) // Явно включаем статус
                .Where(t => t.StatusId == 1) // ID статуса "Available" (лучше использовать константу)
                .Where(t => !_context.Reservations
                    .Where(r => r.ReservationTime.Date == date.Date)
                    .Select(r => r.TableId)
                    .Contains(t.TableId))
                .AsNoTracking()
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
