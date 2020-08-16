using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TestApp.Common.Interfaces;

namespace TestApp.Model.Entities
{
    public class Division : IModelDependency
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("ManagerId")]
        public Employee Manager { get; set; }

        public Division()
        {

        }

        public Division(Guid id, string name, Employee manager)
            : this(name, manager)
        {
            Id = id;
        }

        public Division(string name, Employee manager)
        {
            Name = name;
            Manager = manager;
        }

        public IEnumerable<IModelDependency> GetDependencies()
        {
            return new List<IModelDependency>() { Manager };
        }

    }
}
