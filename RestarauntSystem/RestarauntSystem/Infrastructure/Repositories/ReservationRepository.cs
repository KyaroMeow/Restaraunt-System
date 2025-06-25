using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Repositories;
using RestarauntSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace RestarauntSystem.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RestaurantDbContext _context;

        public ReservationRepository(RestaurantDbContext context) => _context = context;

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.ReservationStatus)
                .Select(r => new Reservation
                {
                    ReservationId = r.ReservationId,
                    CustomerId = r.CustomerId,
                    Customer = r.Customer,
                    StatusId = r.StatusId,
                    ReservationStatus = r.ReservationStatus,
                    ReservationTime = r.ReservationTime,
                    TableId = r.TableId
                })
                .AsNoTracking()
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
            var existing = await _context.Reservations.FindAsync(reservation.ReservationId);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = new Reservation { ReservationId = id };
            _context.Reservations.Attach(reservation);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByCustomerAsync(int customerId)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByDateAsync(DateTime date)
        {
            return await _context.Reservations
                .AsNoTracking()
                .Where(r => r.ReservationTime.Date == date.Date)
                .ToListAsync();
        }

        public async Task ChangeReservationStatusAsync(int reservationId, int statusId)
        {
            var reservation = new Reservation { ReservationId = reservationId, StatusId = statusId };
            _context.Reservations.Attach(reservation);
            _context.Entry(reservation).Property(x => x.StatusId).IsModified = true;
            await _context.SaveChangesAsync();
        }
    }
}
