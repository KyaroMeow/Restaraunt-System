using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestarauntSystem.Core.Models;

namespace RestarauntSystem.Core.Repositories
{
    public interface ITableRepository
    {
        Task<IEnumerable<Table>> GetAllAsync();
        Task<Table> GetByIdAsync(int id);
        Task<Table> AddAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeleteAsync(int id);
        Task<IEnumerable<Table>> GetByZoneAsync(int zoneId);
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime date);
        Task UpdateTableStatusAsync(int tableId, int statusId);
    }
}
