using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestApp.Model.Entities;

namespace TestApp.Model
{
    public class CompanyContext : DbContext
    {
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
