using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Repositories
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetByProductIdAsync(int productId);
        Task<Inventory> AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task DeleteAsync(int productId);
        Task<IEnumerable<Inventory>> GetLowStockItemsAsync(decimal threshold);
        Task UpdateStockAsync(int productId, decimal quantityChange);
    }
}
