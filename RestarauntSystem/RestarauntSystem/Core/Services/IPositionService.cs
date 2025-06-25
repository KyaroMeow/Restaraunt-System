using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Services
{
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllAsync();
        Task<Position> GetByIdAsync(int id);
        Task<Position> AddAsync(Position position);
        Task UpdateAsync(Position position);
        Task DeleteAsync(int id);
    }
}
