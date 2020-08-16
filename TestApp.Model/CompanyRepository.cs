using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApp.Model.Entities;

namespace TestApp.Model
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _companyContext;

        public CompanyRepository(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public List<Division> Divisions => _companyContext.Divisions
            .Include(d => d.Manager).AsNoTracking().ToList();
        public List<Employee> Employees => _companyContext.Employees
            .Include(e => e.Division).AsNoTracking().ToList();
        public List<Order> Orders => _companyContext.Orders
            .Include(o => o.Employee).AsNoTracking().ToList();

        public Division AddDivision(Division division)
        {
            if (division.Manager != null)
            {
                division.Manager.Division = null;
                _companyContext.Employees.Attach(division.Manager);
            }

            _companyContext.Divisions.Add(division);
            _companyContext.SaveChanges();

            _companyContext.Entry(division).State = EntityState.Detached;
            if (division.Manager != null)
            {
                _companyContext.Entry(division.Manager).State = EntityState.Detached;
            }

            return division;
        }

        public void UpdateDivision(Division division)
        {
            if (division.Manager != null)
            {
                division.Manager.Division = null;
                var employee = _companyContext.Employees.Find(division.Manager.Id);
                _companyContext.Entry(employee).State = EntityState.Detached;
            }

            var trackedDivision = _companyContext.Divisions.Find(division.Id);
            if (trackedDivision != null)
            {
                _companyContext.Entry(trackedDivision).State = EntityState.Detached;
            }

            _companyContext.Update(division);
            _companyContext.SaveChanges();

            _companyContext.Entry(division).State = EntityState.Detached;
            if (division.Manager != null)
            {
                _companyContext.Entry(division.Manager).State = EntityState.Detached;
            }
        }

        public void RemoveDivision(Division division)
        {
            foreach (var employee in _companyContext.Employees
                .Include(e => e.Division)
                .Where(e => e.Division.Id == division.Id))
            {
                employee.Division = null;
                _companyContext.Update(employee);
            }
            _companyContext.SaveChanges();
            var trackedDivision = _companyContext.Divisions.Find(division.Id);
            _companyContext.Divisions.Remove(trackedDivision);
            _companyContext.SaveChanges();
        }

        public Employee AddEmployee(Employee employee)
        {
            if (employee.Division != null)
            {
                employee.Division.Manager = null;
                _companyContext.Divisions.Attach(employee.Division);
            }

            _companyContext.Add(employee);
            _companyContext.SaveChanges();

            _companyContext.Entry(employee).State = EntityState.Detached;
            if (employee.Division != null)
            {
                _companyContext.Entry(employee.Division).State = EntityState.Detached;
            }
            return employee;
        }

        public void UpdateEmployee(Employee employee)
        {
            if (employee.Division != null)
            {
                employee.Division.Manager = null;
                var division = _companyContext.Divisions.Find(employee.Division.Id);
                _companyContext.Entry(division).State = EntityState.Detached;
            }
            
            var trackedEmployee = _companyContext.Employees.Find(employee.Id);
            if (trackedEmployee != null)
            {
                _companyContext.Entry(trackedEmployee).State = EntityState.Detached;
            }
            
            _companyContext.Update(employee);
            _companyContext.SaveChanges();
            
            _companyContext.Entry(employee).State = EntityState.Detached;
            if (employee.Division != null)
            {
                _companyContext.Entry(employee.Division).State = EntityState.Detached;
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            foreach (var division in _companyContext.Divisions
                .Include(d => d.Manager)
                .Where(d => d.Manager.Id == employee.Id))
            {
                division.Manager = null;
                _companyContext.Update(division);
            }
            foreach (var order in _companyContext.Orders
                .Include(o => o.Employee)
                .Where(o => o.Employee.Id == employee.Id))
            {
                order.Employee = null;
                _companyContext.Update(order);
            }
            _companyContext.SaveChanges();

            var trackedEmployee = _companyContext.Employees.Find(employee.Id);
            _companyContext.Employees.Remove(trackedEmployee);
            _companyContext.SaveChanges();
        }

        public Order AddOrder(Order order)
        {
            if (order.Employee != null)
            {
                order.Employee.Division = null;
                _companyContext.Employees.Attach(order.Employee);
            }

            _companyContext.Add(order);
            _companyContext.SaveChanges();
            return order;
        }

        public void UpdateOrder(Order order)
        {
            if (order.Employee != null)
            {
                order.Employee.Division = null;
                var employee = _companyContext.Employees.Find(order.Employee.Id);
                _companyContext.Entry(employee).State = EntityState.Detached;
            }

            var trackedOrder = _companyContext.Orders.Find(order.Id);
            if (trackedOrder != null)
            {
                _companyContext.Entry(trackedOrder).State = EntityState.Detached;
            }

            _companyContext.Update(order);
            _companyContext.SaveChanges();
        }

        public void  RemoveOrder(Order order)
        {
            var trackedOrder = _companyContext.Orders.Find(order.Id);
            _companyContext.Orders.Remove(trackedOrder);
            _companyContext.SaveChanges();
        }

        public IEnumerable<Division> FindDivisions(string name)
        {
            return _companyContext.Divisions.Where(d => d.Name.Contains(name)).AsNoTracking().ToList();
        }

        public IEnumerable<Employee> FindEmployees(string name)
        {
            return _companyContext.Employees.Where(d => d.Name.Contains(name) 
                                                        || d.MiddleName.Contains(name) 
                                                        || d.SecondName.Contains(name)).AsNoTracking().ToList();
        }

        public IEnumerable<Order> FindOrders(string productName)
        {
            return _companyContext.Orders.Where(o => o.ProductName.Contains(productName)).AsNoTracking().ToList();
        }
    }
}
