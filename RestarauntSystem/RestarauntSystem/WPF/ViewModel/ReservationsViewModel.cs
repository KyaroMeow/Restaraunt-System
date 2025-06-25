using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class ReservationsViewModel : ObservableObject
    {
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;
        private readonly ITableService _tableService;

        [ObservableProperty]
        private ObservableCollection<Reservation> _reservations;

        [ObservableProperty]
        private ObservableCollection<Customer> _customers;

        [ObservableProperty]
        private ObservableCollection<Table> _tables;

        [ObservableProperty]
        private Reservation _selectedReservation;

        public IAsyncRelayCommand LoadReservationsCommand { get; }
        public IAsyncRelayCommand CreateReservationCommand { get; }
        public IAsyncRelayCommand CancelReservationCommand { get; }

        public ReservationsViewModel(
            IReservationService reservationService,
            ICustomerService customerService,
            ITableService tableService)
        {
            _reservationService = reservationService;
            _customerService = customerService;
            _tableService = tableService;

            Reservations = new ObservableCollection<Reservation>();
            Customers = new ObservableCollection<Customer>();
            Tables = new ObservableCollection<Table>();

            LoadReservationsCommand = new AsyncRelayCommand(LoadReservationsAsync);
            CreateReservationCommand = new AsyncRelayCommand(CreateReservationAsync);
            CancelReservationCommand = new AsyncRelayCommand(CancelReservationAsync, () => SelectedReservation != null);
        }

        private async Task LoadReservationsAsync()
        {
            try
            {
                var reservations = await _reservationService.GetReservationsByDateAsync(DateTime.Today);
                var customers = await _customerService.GetAllAsync();
                var tables = await _tableService.GetAllTablesAsync();

                Reservations.Clear();
                foreach (var reservation in reservations)
                {
                    Reservations.Add(reservation);
                }

                Customers.Clear();
                foreach (var customer in customers)
                {
                    Customers.Add(customer);
                }

                Tables.Clear();
                foreach (var table in tables)
                {
                    Tables.Add(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке бронирований: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CreateReservationAsync()
        {
            if (SelectedCustomer == null || SelectedTable == null)
            {
                MessageBox.Show("Выберите клиента и стол", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var reservation = await _reservationService.CreateReservationAsync(
                    SelectedCustomer.CustomerId,
                    SelectedTable.TableId,
                    ReservationDate);

                Reservations.Add(reservation);
                MessageBox.Show("Бронирование создано успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании бронирования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CancelReservationAsync()
        {
            try
            {
                await _reservationService.CancelReservationAsync(SelectedReservation.ReservationId);
                SelectedReservation.StatusId = 2;
                MessageBox.Show("Бронирование отменено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отмене бронирования: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [ObservableProperty]
        private Customer _selectedCustomer;

        [ObservableProperty]
        private Table _selectedTable;

        [ObservableProperty]
        private DateTime _reservationDate = DateTime.Now;
    }
}
