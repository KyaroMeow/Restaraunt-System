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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Supplier> AddAsync(Supplier supplier)
        {
            return await _supplierRepository.AddAsync(supplier);
        }

        public async Task DeleteAsync(int id)
        {
            await _supplierRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Supplier>> GetAllAsync()
        {
            return await _supplierRepository.GetAllAsync();
        }

        public async Task<Supplier> GetByIdAsync(int id)
        {
            return await _supplierRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersForProductAsync(int productId)
        {
            return await _supplierRepository.GetSuppliersForProductAsync(productId);
        }

        public async Task UpdateAsync(Supplier supplier)
        {
            await _supplierRepository.UpdateAsync(supplier);
        }
    }
}
