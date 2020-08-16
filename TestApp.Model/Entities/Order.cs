using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TestApp.Model.Entities
{
    public class Order 
    {
        [Key]
        public Guid Id { get; set; }
        public int Number { get; set; }
        public string ProductName { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public Order()
        {

        }

        public Order(Guid id, int number, string productName, Employee employee)
            :this(number, productName, employee)
        {
            Id = id;
        }

        public Order(int number, string productName, Employee employee)
        {
            Number = number;
            ProductName = productName;
            Employee = employee;
        }
    }
}
