using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Repositories
{
    public interface IDishCategoryRepository
    {
        Task<IEnumerable<DishCategory>> GetAllAsync();
        Task<DishCategory> GetByIdAsync(int id);
        Task<DishCategory> AddAsync(DishCategory category);
        Task UpdateAsync(DishCategory category);
        Task DeleteAsync(int id);
    }
}
