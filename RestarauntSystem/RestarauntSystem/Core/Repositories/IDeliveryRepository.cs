using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Repositories
{
    public interface IDeliveryRepository
    {
        Task<IEnumerable<Delivery>> GetAllAsync();
        Task<Delivery> GetByIdAsync(int id);
        Task<Delivery> AddAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task DeleteAsync(int id);
        Task<IEnumerable<Delivery>> GetByDateAsync(DateTime date);
        Task<Delivery> GetWithItemsAsync(int id);
    }
}
