using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestApp.Common.Enums;
using TestApp.Common.Interfaces;
using TestApp.UI.Controllers;

namespace TestApp.UI.ViewModels.Entities
{
    public class DivisionViewModel : BaseEntityViewModel, IModelDependency, IEquatable<DivisionViewModel>
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                ValidateName();
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

        public void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                SetRequiredFieldError(nameof(Name));
            }
            else
            {
                ClearErrors(nameof(Name));
            }
        }

        private EmployeeViewModel _manager;
        public EmployeeViewModel Manager
        {
            get => _manager;
            set
            {
                _manager = value;
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

        public DivisionViewModel()
        {
            ValidateName();
        }

        public DivisionViewModel(ICompanyController companyController) : base(companyController)
        {
            
        }

        public DivisionViewModel(ICompanyController companyController, string name, EmployeeViewModel manager) 
            : base(companyController)
        {
            _name = name;
            _manager = manager;
        }

        public DivisionViewModel(ICompanyController companyController, Guid id, string name, EmployeeViewModel employee) 
            : this(companyController, name, employee)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(DivisionViewModel other)
        {
            return Id.Equals(other?.Id);
        }

        public IEnumerable<IModelDependency> GetDependencies()
        {
            return new List<IModelDependency>() {Manager};
        }

        public override BaseEntityViewModel Clone()
        {
            return new DivisionViewModel(CompanyController, Id, Name, Manager)
            {
                Employees = Employees
            };
        }

        public override void Apply(BaseEntityViewModel viewModel) 
        {
            base.Apply(viewModel);

            var temp = Action;
            Action = EEntityViewAction.Update;
            
            var divisionVm = (DivisionViewModel) viewModel;
            Name = divisionVm.Name;
            Manager = divisionVm.Manager;
            Employees = divisionVm.Employees;

            Action = temp;
        }
    }
}
