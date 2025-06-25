using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Services
{
    public interface IDishService
    {
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<Dish> GetByIdAsync(int id);
        Task<Dish> AddAsync(Dish dish);
        Task UpdateAsync(Dish dish);
        Task DeleteAsync(int id);
        Task<IEnumerable<Dish>> GetByCategoryAsync(int categoryId);
        Task<Dish> GetWithComponentsAsync(int id);
    }
}
