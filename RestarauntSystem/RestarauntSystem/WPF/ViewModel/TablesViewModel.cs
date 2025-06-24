using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Services;
using System.Collections.ObjectModel;
using System.Windows;


namespace RestarauntSystem.WPF.ViewModel
{
    public partial class TablesViewModel : ObservableObject
    {
        private readonly ITableService _tableService;
        private readonly IReservationService _reservationService;

        [ObservableProperty]
        private ObservableCollection<Table> _tables;

        [ObservableProperty]
        private Table _selectedTable;

        public IAsyncRelayCommand LoadTablesCommand { get; }
        public IAsyncRelayCommand ReserveTableCommand { get; }
        public IAsyncRelayCommand ReleaseTableCommand { get; }

        public TablesViewModel(ITableService tableService, IReservationService reservationService)
        {
            _tableService = tableService;
            _reservationService = reservationService;

            _tables = new ObservableCollection<Table>();

            LoadTablesCommand = new AsyncRelayCommand(LoadTablesAsync);
            ReserveTableCommand = new AsyncRelayCommand(ReserveTableAsync, CanReserveTable);
            ReleaseTableCommand = new AsyncRelayCommand(ReleaseTableAsync, CanReleaseTable);
        }

        private async Task LoadTablesAsync()
        {
            try
            {
                var tables = await _tableService.GetAllTablesAsync();

                _tables.Clear();
                foreach (var table in tables)
                {
                    _tables.Add(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке столов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReserveTableAsync()
        {
            if (_selectedTable == null || SelectedCustomer == null) return;

            try
            {
                await _reservationService.CreateReservationAsync(
                    SelectedCustomer.CustomerId,
                    _selectedTable.TableId,
                    ReservationDate);

                MessageBox.Show("Стол успешно забронирован", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                await LoadTablesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при бронировании стола: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ReleaseTableAsync()
        {
            if (_selectedTable == null) return;

            try
            {
                await _tableService.ReleaseTableAsync(_selectedTable.TableId);
                await LoadTablesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при освобождении стола: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanReserveTable()
        {
            return _selectedTable != null &&
                   SelectedCustomer != null &&
                   ReservationDate >= DateTime.Today;
        }

        private bool CanReleaseTable()
        {
            return _selectedTable != null &&
                   _selectedTable.StatusId == (int)TableStatus.Occupied;
        }

        [ObservableProperty]
        private ObservableCollection<Customer> _customers;

        [ObservableProperty]
        private Customer _selectedCustomer;

        [ObservableProperty]
        private DateTime _reservationDate = DateTime.Now;
    }
}
