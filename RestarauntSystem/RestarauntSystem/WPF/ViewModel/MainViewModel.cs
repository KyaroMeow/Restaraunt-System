using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableObject _currentViewModel;

        public ICommand ShowOrdersCommand { get; }
        public ICommand ShowTablesCommand { get; }
        public ICommand ShowMenuCommand { get; }
        public ICommand ShowReservationsCommand { get; }
        public ICommand ShowEmployeesCommand { get; }
        public ICommand ShowInventoryCommand { get; }

        public MainViewModel(
            OrdersViewModel ordersViewModel,
            TablesViewModel tablesViewModel,
            MenuViewModel menuViewModel,
            ReservationsViewModel reservationsViewModel,
            EmployeesViewModel employeesViewModel,
            InventoryViewModel inventoryViewModel)
        {
            _ordersViewModel = ordersViewModel;
            _tablesViewModel = tablesViewModel;
            _menuViewModel = menuViewModel;
            _reservationsViewModel = reservationsViewModel;
            _employeesViewModel = employeesViewModel;
            _inventoryViewModel = inventoryViewModel;

            ShowOrdersCommand = new RelayCommand(() => CurrentViewModel = _ordersViewModel);
            ShowTablesCommand = new RelayCommand(() => CurrentViewModel = _tablesViewModel);
            ShowMenuCommand = new RelayCommand(() => CurrentViewModel = _menuViewModel);
            ShowReservationsCommand = new RelayCommand(() => CurrentViewModel = _reservationsViewModel);
            ShowEmployeesCommand = new RelayCommand(() => CurrentViewModel = _employeesViewModel);
            ShowInventoryCommand = new RelayCommand(() => CurrentViewModel = _inventoryViewModel);

            // Установка ViewModel по умолчанию
            CurrentViewModel = _ordersViewModel;
        }

        private readonly OrdersViewModel _ordersViewModel;
        private readonly TablesViewModel _tablesViewModel;
        private readonly MenuViewModel _menuViewModel;
        private readonly ReservationsViewModel _reservationsViewModel;
        private readonly EmployeesViewModel _employeesViewModel;
        private readonly InventoryViewModel _inventoryViewModel;
    }
}
