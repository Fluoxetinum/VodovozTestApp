using System;
using Microsoft.EntityFrameworkCore;
using TestApp.Common.Enums;
using TestApp.Model;
using TestApp.UI.Controllers;
using TestApp.UI.ViewModels.Entities;
using Xunit;

namespace TestApp.Tests
{
    public class CompanyControllerIntegrationTests
    {
        public CompanyController CompanyController { get; set; }

        public CompanyControllerIntegrationTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CompanyContext>();
            string connectionString = "server=localhost;database=companydb_test;user=root;password=1234";
            optionsBuilder.UseMySQL(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
            CompanyContext context = new CompanyContext(optionsBuilder.Options);
            SeedRepository seedRepository = new SeedRepository(context);
            CompanyRepository companyRepository = new CompanyRepository(context);
            CompanyController = new CompanyController(companyRepository, seedRepository);
            context.Database.Migrate();
            CompanyController.ClearData();
        }

        [Fact]
        public void CrudTest()
        {
            var divisionViewModel = 
                new DivisionViewModel(CompanyController, "Division1", manager:null);

            var employeeViewModel1 = 
                new EmployeeViewModel(CompanyController, "Employee1", "", "", EGender.Female, DateTime.Now, division:null);
            var employeeViewModel2 = 
                new EmployeeViewModel(CompanyController, "Employee2", "", "", EGender.Female, DateTime.Now, division:null);

            var orderViewModel1 = 
                new OrderViewModel(CompanyController, 1, "Product1", employee:null);
            var orderViewModel2 = 
                new OrderViewModel(CompanyController, 2, "Product2", employee:null);

            CompanyController.Add(divisionViewModel);

            CompanyController.Add(employeeViewModel1);
            CompanyController.Add(employeeViewModel2);

            CompanyController.Add(orderViewModel1);
            CompanyController.Add(orderViewModel2);

            Assert.Contains(CompanyController.GetDivisions(), (d) => d.Name.Equals("Division1"));

            Assert.Contains(CompanyController.GetEmployees(), (e) => e.Name.Equals("Employee1"));
            Assert.Contains(CompanyController.GetEmployees(), (e) => e.Name.Equals("Employee2"));

            Assert.Contains(CompanyController.GetOrders(), (d) => d.Number == 1);
            Assert.Contains(CompanyController.GetOrders(), (d) => d.Number == 2);

            divisionViewModel.Manager = employeeViewModel1;
            employeeViewModel2.Division = divisionViewModel;
            orderViewModel1.Employee = employeeViewModel2;

            CompanyController.Update(divisionViewModel);
            CompanyController.Update(employeeViewModel2);
            CompanyController.Update(orderViewModel1);

            Assert.Contains(CompanyController.GetDivisions(), (d) => d.Manager?.Id == employeeViewModel1.Id);
            Assert.Contains(CompanyController.GetEmployees(), (e) => e.Division?.Id == divisionViewModel.Id);
            Assert.Contains(CompanyController.GetOrders(), (o) => o.Employee?.Id == employeeViewModel2.Id);

            Assert.NotEmpty(CompanyController.FindDivisions("on1"));
            Assert.NotEmpty(CompanyController.FindEmployees("ee2"));
            Assert.NotEmpty(CompanyController.FindOrders("uct1"));

            CompanyController.Remove(orderViewModel2);
            CompanyController.Remove(orderViewModel1);

            CompanyController.Remove(employeeViewModel2);
            CompanyController.Remove(employeeViewModel1);

            CompanyController.Remove(divisionViewModel);
            
            Assert.Empty(CompanyController.GetDivisions());
            Assert.Empty(CompanyController.GetEmployees());
            Assert.Empty(CompanyController.GetOrders());
        }
    }
}
