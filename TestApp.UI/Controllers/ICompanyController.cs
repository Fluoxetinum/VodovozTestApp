using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.UI.ViewModels.Entities;

namespace TestApp.UI.Controllers
{
    public interface ICompanyController
    {
        public List<DivisionViewModel> GetDivisions();
        public List<EmployeeViewModel> GetEmployees();
        public List<OrderViewModel> GetOrders();

        public void Add(BaseEntityViewModel vm);
        public void Update(BaseEntityViewModel vm);
        public void Remove(BaseEntityViewModel vm);
        
        public void AddDivision(DivisionViewModel division);
        public void UpdateDivision(DivisionViewModel division);
        public void RemoveDivision(DivisionViewModel division);
               
        public void AddEmployee(EmployeeViewModel employee);
        public void UpdateEmployee(EmployeeViewModel employee);
        public void RemoveEmployee(EmployeeViewModel employee);
               
        public void AddOrder(OrderViewModel order);
        public void UpdateOrder(OrderViewModel order);
        public void RemoveOrder(OrderViewModel order);
               
        public void SeedData();
        public void ClearData();

        public IEnumerable<DivisionViewModel> FindDivisions(string name);
        public IEnumerable<EmployeeViewModel> FindEmployees(string name);
        public IEnumerable<OrderViewModel> FindOrders(string productName);
    }
}
