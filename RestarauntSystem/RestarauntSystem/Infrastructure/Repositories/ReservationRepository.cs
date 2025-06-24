using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantDbContext _context;

        public ReservationRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Include(r => r.ReservationStatus)
                .ToListAsync();
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Table)
                .Include(r => r.ReservationStatus)
                .FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await GetByIdAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByCustomerAsync(int customerId)
        {
            return await _context.Reservations
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date)
        {
            return await _context.Reservations
                .Where(r => r.ReservationTime.Date == date.Date)
                .ToListAsync();
        }

        public async Task ChangeReservationStatusAsync(int reservationId, int statusId)
        {
            var reservation = await GetByIdAsync(reservationId);
            reservation.StatusId = statusId;
            await UpdateAsync(reservation);
        }
    }
}
