using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TestApp.Common.Enums;
using TestApp.Common.Interfaces;
using TestApp.UI.Controllers;

namespace TestApp.UI.ViewModels.Entities
{
    public class EmployeeViewModel : BaseEntityViewModel, IModelDependency, IEquatable<EmployeeViewModel>
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

        private string _secondName;
        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

        private EGender _gender;
        public EGender Gender
        {
            get => _gender;
            set
            {
                _gender = value;
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

        private DateTime _birthDate;
        public DateTime BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

        private DivisionViewModel _division;
        public DivisionViewModel Division
        {
            get => _division;
            set
            {
                _division = value;
                OnPropertyChanged();
                UpdateIfInstantEdit();
            }
        }

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

        public EmployeeViewModel()
        {
            ValidateName();
        }

        public EmployeeViewModel(ICompanyController companyController) : base(companyController)
        {
            _birthDate = DateTime.Now.AddYears(-20);
        }

        public EmployeeViewModel(ICompanyController companyController, string name, string secondName, string middleName, EGender gender,
            DateTime birthDate, DivisionViewModel division) : base(companyController)
        {
            _name = name;
            _secondName = secondName;
            _middleName = middleName;
            _gender = gender;
            _birthDate = birthDate;
            _division = division;
        }

        public EmployeeViewModel(ICompanyController companyController, Guid id, string name, string secondName, string middleName, EGender gender,
            DateTime birthDate, DivisionViewModel division)
            : this(companyController, name, secondName, middleName, gender, birthDate, division)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(EmployeeViewModel other)
        {
            return Id.Equals(other?.Id);
        }

        public IEnumerable<IModelDependency> GetDependencies()
        {
            return new List<IModelDependency>() { Division };
        }

        public override BaseEntityViewModel Clone()
        {
            return new EmployeeViewModel(CompanyController, Id, Name, SecondName, MiddleName,
                Gender, BirthDate, Division)
            {
                Divisions = Divisions
            };
        }

        public override void Apply(BaseEntityViewModel viewModel)
        {
            base.Apply(viewModel);

            var temp = Action;
            Action = EEntityViewAction.Update;

            var employeeVm = (EmployeeViewModel) viewModel;
            MiddleName = employeeVm.MiddleName;
            SecondName = employeeVm.SecondName;
            Gender = employeeVm.Gender;
            BirthDate = employeeVm.BirthDate;
            Division = employeeVm.Division;
            Divisions = employeeVm.Divisions;

            Action = temp;
        }
    }
}
