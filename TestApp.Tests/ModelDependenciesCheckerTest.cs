using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Common;
using TestApp.Common.Interfaces;
using TestApp.Model.Entities;
using Xunit;

namespace TestApp.Tests
{
    public class ModelDependenciesCheckerTest
    {
        [Fact]
        public void CyclicDependencyTest()
        {
            var checker = new ModelDependenciesChecker();

            Employee e1 = new Employee() {Id = Guid.NewGuid()};
            Employee e2 = new Employee() {Id = Guid.NewGuid()};
            Division d1 = new Division() {Id = Guid.NewGuid()};
            Division d2 = new Division() {Id = Guid.NewGuid()};

            // Two managers are managing each other.
            d1.Manager = e2;
            e2.Division = d2;
            d2.Manager = e1;
            e1.Division = d1;

            Assert.False(checker.CheckNoCycles(d1));

            Employee e3 = new Employee() {Id = Guid.NewGuid()};
            d2.Manager = e3;

            Assert.True(checker.CheckNoCycles(d1));


        }
    }
}
