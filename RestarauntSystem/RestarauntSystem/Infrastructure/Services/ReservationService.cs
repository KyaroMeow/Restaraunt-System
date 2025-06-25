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
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ITableRepository _tableRepository;

        public ReservationService(
            IReservationRepository reservationRepository,
            ICustomerRepository customerRepository,
            ITableRepository tableRepository)
        {
            _reservationRepository = reservationRepository;
            _customerRepository = customerRepository;
            _tableRepository = tableRepository;
        }

        public async Task<Reservation> CreateReservationAsync(int customerId, int tableId, DateTime reservationTime)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new ArgumentException("Customer not found");

            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new ArgumentException("Table not found");

            var reservation = new Reservation
            {
                CustomerId = customerId,
                TableId = tableId,
                ReservationTime = reservationTime,
                StatusId = 1
            };

            return await _reservationRepository.AddAsync(reservation);
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            reservation.StatusId = 1;
            await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task<IEnumerable<Reservation>> GetCustomerReservationsAsync(int customerId)
        {
            return await _reservationRepository.GetByCustomerAsync(customerId);
        }


        public async Task<IEnumerable<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _reservationRepository.GetByDateAsync(date);
        }
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _reservationRepository.GetAllAsync();
        }

        public async Task<Reservation> GetReservationDetailsAsync(int reservationId)
        {
            return await _reservationRepository.GetByIdAsync(reservationId);
        }
    }
}
