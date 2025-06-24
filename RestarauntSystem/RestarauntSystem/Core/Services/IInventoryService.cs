using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetCurrentInventoryAsync();
        Task UpdateInventoryAsync(int productId, decimal quantityChange);
        Task<IEnumerable<Inventory>> GetLowStockItemsAsync(decimal threshold);
        Task<Inventory> GetProductInventoryAsync(int productId);
    }
}
