using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Model;
using TestApp.Model.Entities;
using TestApp.UI.Annotations;
using TestApp.UI.Infrastructure;
using TestApp.UI.ViewModels.Entities;

namespace TestApp.UI.Controllers
{
    public class CompanyController : ICompanyController
    {
        private readonly ICompanyRepository _repository;
        private readonly ISeedRepository _seedRepository;
        private readonly CompanyEntitiesMapper _mapper;

        public CompanyController(ICompanyRepository repository, ISeedRepository seedRepository)
        {
            _repository = repository;
            _seedRepository = seedRepository;
            _mapper = new CompanyEntitiesMapper(this);
        }

        public List<DivisionViewModel> GetDivisions()
        {
            var divisionVms = new List<DivisionViewModel>();
            foreach (var division in _repository.Divisions)
            {
                DivisionViewModel vm = _mapper.Map(division);
                divisionVms.Add(vm);
            }
            return divisionVms;
        }

        public List<EmployeeViewModel> GetEmployees()
        {
            var employeeVms = new List<EmployeeViewModel>();
            foreach (var employee in _repository.Employees)
            {

                EmployeeViewModel vm = _mapper.Map(employee);
                employeeVms.Add(vm);
            }
            return employeeVms;
        }

        public List<OrderViewModel> GetOrders()
        {
            var orderVms = new List<OrderViewModel>();
            foreach (var order in _repository.Orders)
            {

                OrderViewModel vm = _mapper.Map(order);
                orderVms.Add(vm);
            }
            return orderVms;
        }

        public void Add(BaseEntityViewModel vm)
        {
            switch (vm)
            {
                case DivisionViewModel divisionVm:
                    AddDivision(divisionVm);
                    break;
                case EmployeeViewModel employeeVm:
                    AddEmployee(employeeVm);
                    break;
                case OrderViewModel orderVm:
                    AddOrder(orderVm);
                    break;
            }
        }

        public void Update(BaseEntityViewModel vm)
        {
            switch (vm)
            {
                case DivisionViewModel divisionVm:
                    UpdateDivision(divisionVm);
                    break;
                case EmployeeViewModel employeeVm:
                    UpdateEmployee(employeeVm);
                    break;
                case OrderViewModel orderVm:
                    UpdateOrder(orderVm);
                    break;
            }
        }

        public void Remove(BaseEntityViewModel vm)
        {
            switch (vm)
            {
                case DivisionViewModel divisionVm:
                    RemoveDivision(divisionVm);
                    break;
                case EmployeeViewModel employeeVm:
                    RemoveEmployee(employeeVm);
                    break;
                case OrderViewModel orderVm:
                    RemoveOrder(orderVm);
                    break;
            }
        }

        public void AddDivision(DivisionViewModel divisionVm)
        {
            Division division = _mapper.Map(divisionVm);
            _repository.AddDivision(division);
            divisionVm.Id = division.Id;
        }

        public void UpdateDivision(DivisionViewModel divisionVm)
        {
            Division division = _mapper.Map(divisionVm);
            _repository.UpdateDivision(division);
            divisionVm.Id = division.Id;
        }

        public void RemoveDivision(DivisionViewModel divisionVm)
        {
            Division division = _mapper.Map(divisionVm);
            _repository.RemoveDivision(division);
        }

        public void AddEmployee(EmployeeViewModel employeeVm)
        {
            Employee employee = _mapper.Map(employeeVm);
            _repository.AddEmployee(employee);
            employeeVm.Id = employee.Id;
        }

        public void UpdateEmployee(EmployeeViewModel employeeVm)
        {
            Employee employee = _mapper.Map(employeeVm);
            _repository.UpdateEmployee(employee);
            employeeVm.Id = employee.Id;
        }

        public void RemoveEmployee(EmployeeViewModel employeeVm)
        {
            Employee employee = _mapper.Map(employeeVm);
            _repository.RemoveEmployee(employee);
        }

        public void AddOrder(OrderViewModel orderVm)
        {
            Order order = _mapper.Map(orderVm);
            _repository.AddOrder(order);
            orderVm.Id = order.Id;
        }

        public void UpdateOrder(OrderViewModel orderVm)
        {
            Order order = _mapper.Map(orderVm);
            _repository.UpdateOrder(order);
            orderVm.Id = order.Id;
        }

        public void RemoveOrder(OrderViewModel orderVm)
        {
            Order order = _mapper.Map(orderVm);
            _repository.RemoveOrder(order);
        }

        public void SeedData()
        {
            _seedRepository.SeedData();
        }

        public void ClearData()
        {
            _seedRepository.ClearData();
        }

        public IEnumerable<DivisionViewModel> FindDivisions(string name)
        {
            var divisionVms = new List<DivisionViewModel>();
            foreach (var division in _repository.FindDivisions(name))
            {
                DivisionViewModel vm = _mapper.Map(division);
                divisionVms.Add(vm);
            }
            return divisionVms;
        }

        public IEnumerable<EmployeeViewModel> FindEmployees(string name)
        {
            var employeeVms = new List<EmployeeViewModel>();
            foreach (var employee in _repository.FindEmployees(name))
            {
                EmployeeViewModel vm = _mapper.Map(employee);
                employeeVms.Add(vm);
            }
            return employeeVms;
        }

        public IEnumerable<OrderViewModel> FindOrders(string productName)
        {
            var orderVms = new List<OrderViewModel>();
            foreach (var order in _repository.FindOrders(productName))
            {
                OrderViewModel vm = _mapper.Map(order);
                orderVms.Add(vm);
            }
            return orderVms;
        }

    }
}
