using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class OrdersViewModel : ObservableObject
    {
        private readonly IOrderService _orderService;
        private readonly ITableService _tableService;

        [ObservableProperty]
        private ObservableCollection<Order> _orders;

        [ObservableProperty]
        private ObservableCollection<Table> _tables;

        [ObservableProperty]
        private Order _selectedOrder;

        public IAsyncRelayCommand LoadOrdersCommand { get; }
        public IAsyncRelayCommand CreateOrderCommand { get; }
        public IAsyncRelayCommand CompleteOrderCommand { get; }

        public OrdersViewModel(IOrderService orderService, ITableService tableService)
        {
            _orderService = orderService;
            _tableService = tableService;

            _orders = new ObservableCollection<Order>();
            _tables = new ObservableCollection<Table>();

            LoadOrdersCommand = new AsyncRelayCommand(LoadOrdersAsync);
            CreateOrderCommand = new AsyncRelayCommand(CreateOrderAsync);
            CompleteOrderCommand = new AsyncRelayCommand(CompleteOrderAsync, CanCompleteOrder);
        }

        private async Task LoadOrdersAsync()
        {
            try
            {
                var orders = await _orderService.GetActiveOrdersAsync();
                var tables = await _tableService.GetAllTablesAsync();

                Orders.Clear();
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }

                Tables.Clear();
                foreach (var table in tables)
                {
                    Tables.Add(table);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CreateOrderAsync()
        {
            if (SelectedTable == null)
            {
                MessageBox.Show("Выберите стол для создания заказа", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var order = await _orderService.CreateOrderAsync(SelectedTable.TableId, CurrentEmployee?.EmployeeId);
                Orders.Add(order);
                SelectedOrder = order;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task CompleteOrderAsync()
        {
            if (SelectedOrder == null) return;

            try
            {
                await _orderService.CompleteOrderAsync(SelectedOrder.OrderId);
                await LoadOrdersAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при завершении заказа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanCompleteOrder()
        {
            return SelectedOrder != null && SelectedOrder.StatusId == 4;
        }

        [ObservableProperty]
        private Table _selectedTable;

        [ObservableProperty]
        private Employee _currentEmployee;
    }
}
