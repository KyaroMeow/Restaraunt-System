using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Core.Services;


namespace RestarauntSystem.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public InventoryService(
            IInventoryRepository inventoryRepository,
            IDeliveryRepository deliveryRepository)
        {
            _inventoryRepository = inventoryRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<IEnumerable<Inventory>> GetCurrentInventoryAsync()
        {
            return await _inventoryRepository.GetAllAsync();
        }

        public async Task UpdateInventoryAsync(int productId, decimal quantityChange)
        {
            await _inventoryRepository.UpdateStockAsync(productId, quantityChange);
        }

        public async Task<IEnumerable<Inventory>> GetLowStockItemsAsync(decimal threshold)
        {
            return await _inventoryRepository.GetLowStockItemsAsync(threshold);
        }

        public async Task<Inventory> GetProductInventoryAsync(int productId)
        {
            return await _inventoryRepository.GetByProductIdAsync(productId);
        }
    }
}
