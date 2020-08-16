using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TestApp.Common.Enums;
using TestApp.UI.Controllers;
using TestApp.UI.Infrastructure;
using TestApp.UI.Infrastructure.Dialogs;
using TestApp.UI.Infrastructure.Generic;
using TestApp.UI.ViewModels.Entities;
using TestApp.UI.Views.Entities;

namespace TestApp.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly Dictionary<EEntity, Type> _entityViewModels = 
            new Dictionary<EEntity, Type>()
            {
                [EEntity.Division] = typeof(DivisionViewModel),
                [EEntity.Employee] = typeof(EmployeeViewModel),
                [EEntity.Order] = typeof(OrderViewModel)
            };

        private readonly Dictionary<EEntity, Type> _entityWindows = 
            new Dictionary<EEntity, Type>()
            {
                [EEntity.Division] = typeof(DivisionWindow),
                [EEntity.Employee] = typeof(EmployeeWindow),
                [EEntity.Order] = typeof(OrderWindow)
            };

        private readonly ICompanyController _companyController;

        private ObservableCollection<DivisionViewModel> _divisions;
        public ObservableCollection<DivisionViewModel> Divisions
        {
            get => _divisions;
            set
            {
                _divisions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DivisionViewModel> _divisionsComboBoxCollection;
        private ObservableCollection<EmployeeViewModel> _employeesComboBoxCollection;
        private ObservableCollection<OrderViewModel> _ordersComboBoxCollection;

        private ObservableCollection<EmployeeViewModel> _employees;
        public ObservableCollection<EmployeeViewModel> Employees 
        {
            get => _employees;
            set
            {
                _employees = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<OrderViewModel> _orders;
        public ObservableCollection<OrderViewModel> Orders 
        {
            get => _orders;
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private string _divisionSearchString;
        public string DivisionSearchString
        {
            get => _divisionSearchString;
            set
            {
                _divisionSearchString = value;
                OnPropertyChanged();
                FilterEntities(_divisionSearchString, EEntity.Division);
            }
        }

        private string _employeeSearchString;
        public string EmployeeSearchString
        {
            get => _employeeSearchString;
            set
            {
                _employeeSearchString = value;
                OnPropertyChanged();
                FilterEntities(_employeeSearchString, EEntity.Employee);
            }
        }

        private string _orderSearchString;
        public string OrderSearchString
        {
            get => _orderSearchString;
            set
            {
                _orderSearchString = value;
                OnPropertyChanged();
                FilterEntities(_orderSearchString, EEntity.Order);
            }
        }

        private object _selectedDivision;
        public object SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                _selectedDivision = value;
                OnPropertyChanged();
            }
        }

        private object _selectedEmployee;
        public object SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private object _selectedOrder;
        public object SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel(ICompanyController companyController)
        {
            _companyController = companyController;
            Divisions = new ObservableCollection<DivisionViewModel>();
            Employees = new ObservableCollection<EmployeeViewModel>();
            Orders = new ObservableCollection<OrderViewModel>();

            _divisionsComboBoxCollection = new ObservableCollection<DivisionViewModel>();
            _employeesComboBoxCollection = new ObservableCollection<EmployeeViewModel>();
            _ordersComboBoxCollection = new ObservableCollection<OrderViewModel>();

            Divisions.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    _divisionsComboBoxCollection.Add((DivisionViewModel)args.NewItems[0]);
                }
            };

            Employees.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    _employeesComboBoxCollection.Add((EmployeeViewModel)args.NewItems[0]);
                }
            };

            Orders.CollectionChanged += (sender, args) =>
            {
                if (args.Action == NotifyCollectionChangedAction.Add)
                {
                    _ordersComboBoxCollection.Add((OrderViewModel)args.NewItems[0]);
                }
            };

            FetchAllData();
        }

        public void FetchAllData()
        {
            Divisions.Clear();
            Employees.Clear();
            Orders.Clear();

            _divisionsComboBoxCollection.Clear();
            _employeesComboBoxCollection.Clear();
            _ordersComboBoxCollection.Clear();
         
            foreach (DivisionViewModel division in _companyController.GetDivisions())
            {
                division.Employees = _employeesComboBoxCollection;
                Divisions.Add(division);
            }

            foreach (EmployeeViewModel employee in _companyController.GetEmployees())
            {
                employee.Divisions = _divisionsComboBoxCollection;
                Employees.Add(employee);
            }

            foreach (OrderViewModel order in _companyController.GetOrders())
            {
                order.Employees = _employeesComboBoxCollection;
                Orders.Add(order);
            }
        }

        private ICommand _seedDataCommand;
        public ICommand SeedDataCommand => 
            _seedDataCommand ??= new UserCommand(SeedData);

        private ICommand _clearDataCommand;
        public ICommand ClearDataCommand =>
            _clearDataCommand ??= new UserCommand(ClearData);

        private ICommand _editEntityCommand;
        public ICommand EditEntityCommand =>
            _editEntityCommand ??= new UserCommand<EEntity>(EditEntity);

        private ICommand _addEntityCommand;
        public ICommand AddEntityCommand =>
            _addEntityCommand ??= new UserCommand<EEntity>(AddEntity);

        private ICommand _addDivisionToGridCommand;
        public ICommand AddDivisionToGridCommand =>
            _addDivisionToGridCommand ??= new UserCommand<AddingNewItemEventArgs>(AddDivisionToGrid);

        private ICommand _addEmployeeToGridCommand;
        public ICommand AddEmployeeToGridCommand =>
            _addEmployeeToGridCommand ??= new UserCommand<AddingNewItemEventArgs>(AddEmployeeToGrid);

        private ICommand _addOrderToGridCommand;
        public ICommand AddOrderToGridCommand =>
            _addOrderToGridCommand ??= new UserCommand<AddingNewItemEventArgs>(AddOrderToGrid);

        private ICommand _removeEntityCommand;
        public ICommand RemoveEntityCommand =>
            _removeEntityCommand ??= new UserCommand<EEntity>(RemoveEntity);

        public async void SeedData()
        {
            IsLoading = true;
            await Task.Run(_companyController.SeedData);
            FetchAllData();
            IsLoading = false;
        }

        public async void ClearData()
        {
            IsLoading = true;
            await Task.Run(_companyController.ClearData);
            FetchAllData();
            IsLoading = false;
        }

        public void EditEntity(EEntity entity)
        {
            Type windowType = _entityWindows[entity];

            BaseEntityViewModel viewModel = (BaseEntityViewModel)GetSelectedEntity(entity);
            var window = (Window)Activator.CreateInstance(windowType);
            if (window == null)
            {
                throw new InvalidOperationException("Window cannot be created.");
            }

            BaseEntityViewModel viewModelClone = viewModel.Clone();
            viewModelClone.Action = EEntityViewAction.Update;
            window.DataContext = viewModelClone;
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ResizeMode = ResizeMode.NoResize;

            if (window.ShowDialog() == false) return;

            viewModel.Apply(viewModelClone);

            _companyController.Update(viewModel);
        }

        public void AddDivisionToGrid(AddingNewItemEventArgs args)
        {
            var vm = new DivisionViewModel();
            InitViewModel(vm);
            args.NewItem = vm;
        }

        public void AddEmployeeToGrid(AddingNewItemEventArgs args)
        {
            var vm = new EmployeeViewModel();
            InitViewModel(vm);
            args.NewItem = vm;
        }

        public void AddOrderToGrid(AddingNewItemEventArgs args)
        {
            var vm = new OrderViewModel(_companyController);
            InitViewModel(vm);
            args.NewItem = vm;
        }

        public void AddEntity(EEntity entity)
        {
            Type viewModelType = _entityViewModels[entity];
            Type windowType = _entityWindows[entity];

            var viewModel = (BaseEntityViewModel)Activator.CreateInstance(viewModelType);
            if (viewModel == null)
            {
                throw new InvalidOperationException("BaseEntityViewModel cannot be created.");
            }
            
            var window = (Window)Activator.CreateInstance(windowType);
            if (window == null)
            {
                throw new InvalidOperationException("Window cannot be created.");
            }

            viewModel.Action = EEntityViewAction.Add;
            InitViewModel(viewModel);
            window.DataContext = viewModel;
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ResizeMode = ResizeMode.NoResize;

            if (window.ShowDialog() == false) return;
            
            _companyController.Add(viewModel);
            AddToObservable(viewModel);
        }

        public void RemoveEntity(EEntity entity)
        {
            var viewModel = (BaseEntityViewModel) GetSelectedEntity(entity);
            if (viewModel.Id == Guid.Empty)
            {
                RemoveFromObservable(viewModel);
                return;
            }

            string question = $"Are you sure you want to delete entity with Id = {viewModel.Id}?";
            QuestionWindow questionWindow = new QuestionWindow(question)
            {
                ResizeMode = ResizeMode.NoResize,
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            if (questionWindow.ShowDialog() == true)
            {
                _companyController.Remove(viewModel);
                FetchAllData();
            }
        }

        public void FilterEntities(string name, EEntity entity)
        {
            switch (entity)
            {
                case EEntity.Division:
                    Divisions.Clear();
                    foreach (var division in _companyController.FindDivisions(name))
                    {
                        Divisions.Add(division);
                    }
                    break;
                case EEntity.Employee:
                    Employees.Clear();
                    foreach (var employee in _companyController.FindEmployees(name))
                    {
                        Employees.Add(employee);
                    }
                    break;
                case EEntity.Order:
                    Orders.Clear();
                    foreach (var order in _companyController.FindOrders(name))
                    {
                        Orders.Add(order);
                    }
                    break;
            }
        }

        public void InitViewModel(BaseEntityViewModel vm)
        {
            vm.CompanyController = _companyController;
            switch (vm)
            {
                case DivisionViewModel divisionVm:
                    divisionVm.Employees = _employeesComboBoxCollection;
                    break;
                case EmployeeViewModel employeeVm:
                    employeeVm.Divisions = _divisionsComboBoxCollection;
                    break;
                case OrderViewModel orderVm:
                    orderVm.Employees = _employeesComboBoxCollection;
                    break;
            }
        }

        public object GetSelectedEntity(EEntity entity)
        {
            switch (entity)
            {
                case EEntity.Division:
                    return SelectedDivision;
                case EEntity.Employee:
                    return SelectedEmployee;
                case EEntity.Order:
                    return SelectedOrder;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entity), entity, null);
            }
        }

        public void RemoveFromObservable(BaseEntityViewModel vm)
        {
            switch (vm)
            {
                case DivisionViewModel divisionVm:
                    Divisions.Remove(divisionVm);
                    break;
                case EmployeeViewModel employeeVm:
                    Employees.Remove(employeeVm);
                    break;
                case OrderViewModel orderVm:
                    Orders.Remove(orderVm);
                    break;
            }
        }

        public void AddToObservable(BaseEntityViewModel vm)
        {
            switch (vm)
            {
                case DivisionViewModel divisionVm:
                    Divisions.Add(divisionVm);
                    break;
                case EmployeeViewModel employeeVm:
                    Employees.Add(employeeVm);
                    break;
                case OrderViewModel orderVm:
                    Orders.Add(orderVm);
                    break;
            }
        }

    }
}
