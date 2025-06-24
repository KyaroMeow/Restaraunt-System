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
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;
        private readonly IReservationRepository _reservationRepository;

        public TableService(
            ITableRepository tableRepository,
            IReservationRepository reservationRepository)
        {
            _tableRepository = tableRepository;
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Table>> GetAvailableTablesAsync(DateTime reservationTime)
        {
            var allTables = await _tableRepository.GetAllAsync();
            var reservations = await _reservationRepository.GetByDateAsync(reservationTime);

            var reservedTableIds = reservations
                .Where(r => r.ReservationTime.Date == reservationTime.Date)
                .Select(r => r.TableId)
                .ToList();

            return allTables.Where(t =>
                !reservedTableIds.Contains(t.TableId) &&
                t.Status?.StatusName != "Occupied");
        }

        public async Task<Table> GetTableDetailsAsync(int tableId)
        {
            return await _tableRepository.GetByIdAsync(tableId);
        }

        public async Task ReserveTableAsync(int tableId, int customerId, DateTime reservationTime)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new ArgumentException("Table not found");

            var reservation = new Reservation
            {
                TableId = tableId,
                CustomerId = customerId,
                ReservationTime = reservationTime,
                StatusId = 1
            };

            await _reservationRepository.AddAsync(reservation);
        }

        public async Task ReleaseTableAsync(int tableId)
        {
            var table = await _tableRepository.GetByIdAsync(tableId);
            if (table == null)
                throw new ArgumentException("Table not found");

            table.StatusId = 1;
            await _tableRepository.UpdateAsync(table);
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            return await _tableRepository.GetAllAsync();
        }
    }
}
