using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitementManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class Addfirstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "int", nullable: false),
                        
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.IdAdmin);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "int", nullable: false),
                        
                    DateNaiss = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdEmployee);
                });

            migrationBuilder.CreateTable(
                name: "RHs",
                columns: table => new
                {
                    IdRh = table.Column<int>(type: "int", nullable: false),
                     
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RHs", x => x.IdRh);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    profilecompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formations",
                columns: table => new
                {
                    IdFormation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    published = table.Column<bool>(type: "bit", nullable: false),
                    archived = table.Column<bool>(type: "bit", nullable: false),
                    IdRh = table.Column<int>(type: "int", nullable: false),
                    LeRhIdRh = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formations", x => x.IdFormation);
                    table.ForeignKey(
                        name: "FK_Formations_RHs_LeRhIdRh",
                        column: x => x.LeRhIdRh,
                        principalTable: "RHs",
                        principalColumn: "IdRh");
                });

            migrationBuilder.CreateTable(
                name: "employeeFormation",
                columns: table => new
                {
                    IdEmployee = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdFormation = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EmployeeIdEmployee = table.Column<int>(type: "int", nullable: false),
                    FormationIdFormation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeFormation", x => x.IdEmployee);
                    table.ForeignKey(
                        name: "FK_employeeFormation_Employees_EmployeeIdEmployee",
                        column: x => x.EmployeeIdEmployee,
                        principalTable: "Employees",
                        principalColumn: "IdEmployee",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeeFormation_Formations_FormationIdFormation",
                        column: x => x.FormationIdFormation,
                        principalTable: "Formations",
                        principalColumn: "IdFormation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employeeFormation_EmployeeIdEmployee",
                table: "employeeFormation",
                column: "EmployeeIdEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_employeeFormation_FormationIdFormation",
                table: "employeeFormation",
                column: "FormationIdFormation");

            migrationBuilder.CreateIndex(
                name: "IX_Formations_LeRhIdRh",
                table: "Formations",
                column: "LeRhIdRh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "employeeFormation");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Formations");

            migrationBuilder.DropTable(
                name: "RHs");
        }
    }
}
