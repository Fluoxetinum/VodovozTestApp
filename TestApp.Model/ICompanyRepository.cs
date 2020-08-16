using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model.Entities;

namespace TestApp.Model
{
    public interface ICompanyRepository
    {
        List<Division> Divisions { get; }
        List<Employee> Employees { get; }
        List<Order> Orders { get; }

        public IEnumerable<Division> FindDivisions(string name);
        public IEnumerable<Employee> FindEmployees(string name);
        public IEnumerable<Order> FindOrders(string name);

        public Division AddDivision(Division division);
        public void UpdateDivision(Division division);
        public void RemoveDivision(Division division);

        public Employee AddEmployee(Employee employee);
        public void UpdateEmployee(Employee employee);
        public void RemoveEmployee(Employee employee);

        public Order AddOrder(Order order);
        public void UpdateOrder(Order order);
        public void RemoveOrder(Order order);
    }
}
