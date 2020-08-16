﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestApp.Model;

namespace TestApp.Model.Migrations
{
    [DbContext(typeof(CompanyContext))]
    partial class CompanyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TestApp.Model.Entities.Division", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("ManagerId")
                        .HasColumnType("varbinary(16)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("TestApp.Model.Entities.Employee", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<byte[]>("DivisionId")
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DivisionId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TestApp.Model.Entities.Order", b =>
                {
                    b.Property<byte[]>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varbinary(16)");

                    b.Property<byte[]>("EmployeeId")
                        .HasColumnType("varbinary(16)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TestApp.Model.Entities.Division", b =>
                {
                    b.HasOne("TestApp.Model.Entities.Employee", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId");
                });

            modelBuilder.Entity("TestApp.Model.Entities.Employee", b =>
                {
                    b.HasOne("TestApp.Model.Entities.Division", "Division")
                        .WithMany()
                        .HasForeignKey("DivisionId");
                });

            modelBuilder.Entity("TestApp.Model.Entities.Order", b =>
                {
                    b.HasOne("TestApp.Model.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });
#pragma warning restore 612, 618
        }
    }
}
