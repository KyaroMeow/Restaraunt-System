using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(int id);
        Task<IEnumerable<Reservation>> GetByCustomerAsync(int customerId);
        Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date);
        Task ChangeReservationStatusAsync(int reservationId, int statusId);
    }
}
