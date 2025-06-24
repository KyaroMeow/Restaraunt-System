using RestarauntSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestarauntSystem.Core.Services
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(int customerId, int tableId, DateTime reservationTime);
        Task CancelReservationAsync(int reservationId);
        Task<IEnumerable<Reservation>> GetCustomerReservationsAsync(int customerId);
        Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date);
        Task<Reservation> GetReservationDetailsAsync(int reservationId);
    }
}
