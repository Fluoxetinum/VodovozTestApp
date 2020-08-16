using System;
using TestApp.Common;
using TestApp.Model.Entities;
using TestApp.UI.Controllers;
using TestApp.UI.ViewModels.Entities;

namespace TestApp.UI.Infrastructure
{
    public class CompanyEntitiesMapper
    {
        private readonly ICompanyController _companyController;
        private readonly ModelDependenciesChecker _dependenciesChecker;

        public CompanyEntitiesMapper(ICompanyController companyController)
        {
            _companyController = companyController;
            _dependenciesChecker = new ModelDependenciesChecker();
        }

        public DivisionViewModel Map(Division division)
        {
            if (!_dependenciesChecker.CheckNoCycles(division))
            {
                throw new InvalidOperationException("Division hierarchy has cycles.");
            }

            return InternalMap(division);
        }

        public Division Map(DivisionViewModel division)
        {
            if (!_dependenciesChecker.CheckNoCycles(division))
            {
                throw new InvalidOperationException("Division hierarchy has cycles.");
            }

            return InternalMap(division);
        }

        public EmployeeViewModel Map(Employee employee)
        {
            if (!_dependenciesChecker.CheckNoCycles(employee))
            {
                throw new InvalidOperationException("Division hierarchy has cycles.");
            }

            return InternalMap(employee);
        }

        public Employee Map(EmployeeViewModel employee)
        {
            if (!_dependenciesChecker.CheckNoCycles(employee))
            {
                throw new InvalidOperationException("Division hierarchy has cycles.");
            }

            return InternalMap(employee);
        }

        public OrderViewModel Map(Order order)
        {
            var employee = order.Employee == null ? null : Map(order.Employee);
            return new OrderViewModel(_companyController, order.Id, order.Number, order.ProductName, employee);
        }

        public Order Map(OrderViewModel order)
        {
            var employee = order.Employee == null ? null : Map(order.Employee);
            return new Order(order.Id, order.Number, order.ProductName, employee);
        }

        private DivisionViewModel InternalMap(Division division)
        {
            EmployeeViewModel employee =  division.Manager == null ? null : InternalMap(division.Manager);
            return new DivisionViewModel(_companyController, division.Id, division.Name, employee);
        }

        private Division InternalMap(DivisionViewModel division)
        {
            Employee employee = division.Manager == null ? null : InternalMap(division.Manager);
            return new Division(division.Id, division.Name, employee);
        }

        private EmployeeViewModel InternalMap(Employee employee)
        {
            DivisionViewModel division = employee.Division == null ? null : InternalMap(employee.Division);
            return new EmployeeViewModel(_companyController, employee.Id, employee.Name, employee.SecondName, 
                employee.MiddleName, employee.Gender, employee.BirthDate, division);
        }

        private Employee InternalMap(EmployeeViewModel employee)
        {
            Division division =  employee.Division == null ? null : InternalMap(employee.Division);
            return new Employee(employee.Id, employee.Name, employee.SecondName, 
                employee.MiddleName, employee.Gender, employee.BirthDate, division);
        }
    }
}
