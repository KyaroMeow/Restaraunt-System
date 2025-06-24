using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Repositories
{
    public interface IWorkScheduleRepository
    {
        Task<IEnumerable<WorkSchedule>> GetAllAsync();
        Task<WorkSchedule> GetByIdAsync(int id);
        Task<WorkSchedule> AddAsync(WorkSchedule schedule);
        Task UpdateAsync(WorkSchedule schedule);
        Task DeleteAsync(int id);
    }
}
