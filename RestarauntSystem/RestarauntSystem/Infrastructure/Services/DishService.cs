using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Infrastructure.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;

        public DishService(IDishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }

        public async Task<Dish> AddAsync(Dish dish)
        {
            return await _dishRepository.AddAsync(dish);
        }

        public async Task DeleteAsync(int id)
        {
            await _dishRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            return await _dishRepository.GetAllAsync();
        }

        public async Task<Dish> GetByIdAsync(int id)
        {
            return await _dishRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Dish>> GetByCategoryAsync(int categoryId)
        {
            return await _dishRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<Dish> GetWithComponentsAsync(int id)
        {
            return await _dishRepository.GetWithComponentsAsync(id);
        }

        public async Task UpdateAsync(Dish dish)
        {
            await _dishRepository.UpdateAsync(dish);
        }
    }
}
