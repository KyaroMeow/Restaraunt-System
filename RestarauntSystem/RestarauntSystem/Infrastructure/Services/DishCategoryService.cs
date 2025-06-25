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
    public class DishCategoryService : IDishCategoryService
    {
        private readonly IDishCategoryRepository _categoryRepository;

        public DishCategoryService(IDishCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<DishCategory> AddAsync(DishCategory category)
        {
            return await _categoryRepository.AddAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DishCategory>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<DishCategory> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(DishCategory category)
        {
            await _categoryRepository.UpdateAsync(category);
        }
    }
}
