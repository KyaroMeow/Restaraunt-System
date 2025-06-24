using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(int tableId, int? employeeId);
        Task<Order> GetOrderDetailsAsync(int orderId);
        Task<IEnumerable<Order>> GetActiveOrdersAsync();
        Task AddItemToOrderAsync(int orderId, int dishId, int quantity);
        Task RemoveItemFromOrderAsync(int orderId, int dishId);
        Task CompleteOrderAsync(int orderId);
        Task CancelOrderAsync(int orderId);
    }
}
