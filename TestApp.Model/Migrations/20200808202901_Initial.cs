using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApp.Model.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    MiddleName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    DivisionId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ManagerId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Divisions_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_ManagerId",
                table: "Divisions",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DivisionId",
                table: "Employees",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Divisions_DivisionId",
                table: "Employees",
                column: "DivisionId",
                principalTable: "Divisions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            string triggerTemplate =
                "CREATE TRIGGER before_add_{0} BEFORE INSERT ON {0} FOR EACH ROW " +
                "IF new.Id IS NULL THEN SET new.Id = uuid(); END IF;";

            migrationBuilder.Sql(string.Format(triggerTemplate, "employees"));
            migrationBuilder.Sql(string.Format(triggerTemplate, "divisions"));
            migrationBuilder.Sql(string.Format(triggerTemplate, "orders"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string triggerTemplate =
                "DROP TRIGGER IF EXISTS before_add_{0}";
            migrationBuilder.Sql(string.Format(triggerTemplate, "employees"));
            migrationBuilder.Sql(string.Format(triggerTemplate, "divisions"));
            migrationBuilder.Sql(string.Format(triggerTemplate, "orders"));

            migrationBuilder.DropForeignKey(
                name: "FK_Divisions_Employees_ManagerId",
                table: "Divisions");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Divisions");
        }
    }
}
