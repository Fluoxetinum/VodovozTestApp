using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestApp.Common.Enums;
using TestApp.UI.Controllers;

namespace TestApp.UI.ViewModels.Entities
{
    public class OrderViewModel : BaseEntityViewModel, IEquatable<OrderViewModel>
    {
        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                _number = value;
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

        private string _productName;
        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                ValidateProductName();
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

        public void ValidateProductName()
        {
            if (string.IsNullOrWhiteSpace(ProductName))
            {
                SetRequiredFieldError(nameof(ProductName));
            }
            else
            {
                ClearErrors(nameof(ProductName));
            }
        }

        private EmployeeViewModel _employee;
        public EmployeeViewModel Employee
        {
            get => _employee;
            set
            {
                _employee = value;
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

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

        public OrderViewModel()
        {
            ValidateProductName();
        }

        public OrderViewModel(ICompanyController companyController) : base(companyController) {}

        public OrderViewModel(ICompanyController companyController, int number, string productName, EmployeeViewModel employee)
            :base(companyController)
        {
            _number = number;
            _productName = productName;
            _employee = employee;
        }

        public OrderViewModel(ICompanyController companyController, Guid id, int number, string productName, EmployeeViewModel employee) 
            : this(companyController, number, productName, employee)
        {
            Id = id;
        }

        public override string ToString()
        {
            return $"{Number}";
        }

        public bool Equals(OrderViewModel other)
        {
            return Id.Equals(other?.Id);
        }

        public override BaseEntityViewModel Clone()
        {
            return new OrderViewModel(CompanyController, Id, Number, ProductName, Employee)
            {
                Employees = Employees
            };
        }

        public override void Apply(BaseEntityViewModel viewModel)
        {
            base.Apply(viewModel);

            var temp = Action;
            Action = EEntityViewAction.Update;

            var orderVm = (OrderViewModel) viewModel;
            Number = orderVm.Number;
            ProductName = orderVm.ProductName;
            Employee = orderVm.Employee;
            Employees = orderVm.Employees;

            Action = temp;
        }
    }
}
