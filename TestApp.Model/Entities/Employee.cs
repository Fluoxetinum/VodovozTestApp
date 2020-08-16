using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApp.Common.Enums;
using TestApp.Common.Interfaces;

namespace TestApp.Model.Entities
{
    public class Employee : IModelDependency
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public EGender Gender { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("DivisionId")]
        public Division Division { get; set; }

        public Employee()
        {

        }

        public Employee(Guid id, string name, string secondName, string middleName, 
            EGender gender, DateTime birthDate, Division division)
            : this(name, secondName, middleName, gender, birthDate, division)
        {
            Id = id;
        }

        public Employee(string name, string secondName, string middleName, EGender gender, DateTime birthDate, Division division)
        {
            Name = name;
            SecondName = secondName;
            MiddleName = middleName;
            Gender = gender;
            BirthDate = birthDate;
            Division = division;
        }

        public IEnumerable<IModelDependency> GetDependencies()
        {
            return new List<IModelDependency>() { Division };
        }
    }
}
