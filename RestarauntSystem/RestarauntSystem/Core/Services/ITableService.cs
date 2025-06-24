using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestarauntSystem.Core.Models;


namespace RestarauntSystem.Core.Services
{
    public interface ITableService
    {
        Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime reservationTime);
        Task<Table> GetTableDetailsAsync(int tableId);
        Task ReserveTableAsync(int tableId, int customerId, DateTime reservationTime);
        Task ReleaseTableAsync(int tableId);
        Task<IEnumerable<Table>> GetAllTablesAsync();
    }
}
