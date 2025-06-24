using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Repositories
{
    public interface IOrderItemRepository
    {
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<OrderItem> AddAsync(OrderItem orderItem);
        Task UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(int orderId, int dishId);
        Task DeleteAllForOrderAsync(int orderId);
    }
}
