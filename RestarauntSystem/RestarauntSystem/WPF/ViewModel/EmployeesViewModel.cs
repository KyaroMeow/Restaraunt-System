using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestarauntSystem.Core.Models;
using RestarauntSystem.Core.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace RestarauntSystem.WPF.ViewModel
{
    public partial class EmployeesViewModel : ObservableObject
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;

        [ObservableProperty]
        private ObservableCollection<Employee> _employees;

        [ObservableProperty]
        private ObservableCollection<Position> _positions;

        [ObservableProperty]
        private Employee _selectedEmployee;

        public IAsyncRelayCommand LoadEmployeesCommand { get; }
        public IAsyncRelayCommand AddEmployeeCommand { get; }
        public IAsyncRelayCommand UpdateEmployeeCommand { get; }
        public IAsyncRelayCommand DeleteEmployeeCommand { get; }

        public EmployeesViewModel(IEmployeeService employeeService, IPositionService positionService)
        {
            _employeeService = employeeService;
            _positionService = positionService;

            Employees = new ObservableCollection<Employee>();
            Positions = new ObservableCollection<Position>();

            LoadEmployeesCommand = new AsyncRelayCommand(LoadEmployeesAsync);
            AddEmployeeCommand = new AsyncRelayCommand(AddEmployeeAsync);
            UpdateEmployeeCommand = new AsyncRelayCommand(UpdateEmployeeAsync);
        }

        private async Task LoadEmployeesAsync()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                var positions = await _positionService.GetAllAsync();

                Employees.Clear();
                foreach (var employee in employees)
                {
                    Employees.Add(employee);
                }

                Positions.Clear();
                foreach (var position in positions)
                {
                    Positions.Add(position);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сотрудников: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AddEmployeeAsync()
        {
            if (string.IsNullOrWhiteSpace(NewEmployeeLastName) ||
                string.IsNullOrWhiteSpace(NewEmployeeFirstName))
            {
                MessageBox.Show("Введите фамилию и имя сотрудника", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var employee = new Employee
                {
                    LastName = NewEmployeeLastName,
                    FirstName = NewEmployeeFirstName,
                    MiddleName = NewEmployeeMiddleName,
                    PositionId = SelectedPosition?.PositionId,
                    BirthDate = NewEmployeeBirthDate,
                    Phone = NewEmployeePhone,
                    Email = NewEmployeeEmail
                };

                await _employeeService.CreateEmployeeAsync(employee);
                Employees.Add(employee);

                // Сброс формы
                NewEmployeeLastName = string.Empty;
                NewEmployeeFirstName = string.Empty;
                NewEmployeeMiddleName = string.Empty;
                NewEmployeeBirthDate = null;
                NewEmployeePhone = string.Empty;
                NewEmployeeEmail = string.Empty;

                MessageBox.Show("Сотрудник успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task UpdateEmployeeAsync()
        {
            if (SelectedEmployee == null) return;

            try
            {
                await _employeeService.UpdateEmployeeAsync(SelectedEmployee);
                MessageBox.Show("Данные сотрудника обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        [ObservableProperty]
        private Position _selectedPosition;

        [ObservableProperty]
        private string _newEmployeeLastName;

        [ObservableProperty]
        private string _newEmployeeFirstName;

        [ObservableProperty]
        private string _newEmployeeMiddleName;

        [ObservableProperty]
        private DateTime? _newEmployeeBirthDate;

        [ObservableProperty]
        private string _newEmployeePhone;

        [ObservableProperty]
        private string _newEmployeeEmail;
    }
}
