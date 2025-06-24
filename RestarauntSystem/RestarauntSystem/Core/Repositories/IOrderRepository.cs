using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        Task<IEnumerable<Order>> GetByDateAsync(DateTime date);
        Task<IEnumerable<Order>> GetActiveOrdersAsync();
        Task<Order> GetOrderWithItemsAsync(int id);
        Task<IEnumerable<Order>> GetByStatusAsync(int statusId);
        Task<IEnumerable<Order>> GetByTableAsync(int tableId);
    }
}
