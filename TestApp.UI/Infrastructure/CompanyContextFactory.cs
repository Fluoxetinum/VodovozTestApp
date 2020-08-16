using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TestApp.Model;

namespace TestApp.UI.Infrastructure
{
    public class CompanyContextFactory : IDesignTimeDbContextFactory<CompanyContext>
    {
        public CompanyContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CompanyContext>();
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
            optionsBuilder.UseMySQL(connectionString);
            return new CompanyContext(optionsBuilder.Options);
        }
    }
}
