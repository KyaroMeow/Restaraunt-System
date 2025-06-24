using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RestarauntSystem.Core.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByDateAsync(DateTime date);
        Task<IEnumerable<Order>> GetActiveOrdersAsync();
    }
}
