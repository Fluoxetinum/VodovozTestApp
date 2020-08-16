using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApp.Common.Enums;
using TestApp.Model.Entities;

namespace TestApp.Model
{
    public class SeedRepository : ISeedRepository
    {
        private readonly CompanyContext _companyContext;

        public SeedRepository(CompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public void SeedData()
        {
            const int divisionsCount = 20;
            const int employeesCount = 500;
            const int ordersCount = 30000;
            if (!(_companyContext.Database.GetPendingMigrations().Any()))
            {
                var employees = new List<Employee>();
                for (int i = 0; i < employeesCount; i++)
                {
                    var employee = new Employee($"Name{i}", $"SecondName{i}", $"MiddleName", 
                        (EGender)(i % 2), DateTime.Now.AddMonths(-1*i), division:null);
                    employees.Add(employee);
                }
                _companyContext.Employees.AddRange(employees);
                _companyContext.SaveChanges();

                var divisions = new List<Division>();
                for (int i = 0; i < divisionsCount; i++)
                {
                    var division = new Division($"Division{i}", manager:employees[i]);
                    divisions.Add(division);
                }
                _companyContext.Divisions.AddRange(divisions);
                _companyContext.SaveChanges();

                for (int i = divisionsCount; i < employeesCount; i++)
                {
                    employees[i].Division = divisions[i % divisionsCount];
                }
                _companyContext.Employees.UpdateRange(employees);
                _companyContext.SaveChanges();

                var orders = new List<Order>();
                for (int i = 0; i < ordersCount; i++)
                {
                    var order = new Order(i, $"Product{i}", employees[i % employeesCount]);
                    orders.Add(order);
                }
                _companyContext.Orders.AddRange(orders);
                _companyContext.SaveChanges();
            }
        }

        public void ClearData()
        {
            if (_companyContext.Orders.Any())
            {
               _companyContext.Database.ExecuteSqlRaw("DELETE FROM orders;");
            }

            if (_companyContext.Employees.Any())
            {
                foreach (var division in _companyContext.Divisions)
                {
                    _companyContext.Entry(division).Property("ManagerId").CurrentValue = null;
                }
                _companyContext.SaveChanges();
                _companyContext.Database.ExecuteSqlRaw("DELETE FROM employees;");
            }

            if (_companyContext.Divisions.Any())
            {
                _companyContext.Database.ExecuteSqlRaw("DELETE FROM divisions;");
            }
        }
    }
}
