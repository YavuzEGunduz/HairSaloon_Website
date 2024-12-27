using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSaloon_Website.Migrations
{
    /// <inheritdoc />
    public partial class denebakalım : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employeesProcess_Employees_EmployeeId",
                table: "employeesProcess");

            migrationBuilder.DropForeignKey(
                name: "FK_employeesProcess_Processes_ProcessId",
                table: "employeesProcess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employeesProcess",
                table: "employeesProcess");

            migrationBuilder.RenameTable(
                name: "employeesProcess",
                newName: "EmployeeProcesess");

            migrationBuilder.RenameIndex(
                name: "IX_employeesProcess_ProcessId",
                table: "EmployeeProcesess",
                newName: "IX_EmployeeProcesess_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_employeesProcess_EmployeeId",
                table: "EmployeeProcesess",
                newName: "IX_EmployeeProcesess_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProcesess",
                table: "EmployeeProcesess",
                column: "EmployeeProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProcesess_Employees_EmployeeId",
                table: "EmployeeProcesess",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProcesess_Processes_ProcessId",
                table: "EmployeeProcesess",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProcesess_Employees_EmployeeId",
                table: "EmployeeProcesess");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProcesess_Processes_ProcessId",
                table: "EmployeeProcesess");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProcesess",
                table: "EmployeeProcesess");

            migrationBuilder.RenameTable(
                name: "EmployeeProcesess",
                newName: "employeesProcess");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProcesess_ProcessId",
                table: "employeesProcess",
                newName: "IX_employeesProcess_ProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProcesess_EmployeeId",
                table: "employeesProcess",
                newName: "IX_employeesProcess_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employeesProcess",
                table: "employeesProcess",
                column: "EmployeeProcessId");

            migrationBuilder.AddForeignKey(
                name: "FK_employeesProcess_Employees_EmployeeId",
                table: "employeesProcess",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_employeesProcess_Processes_ProcessId",
                table: "employeesProcess",
                column: "ProcessId",
                principalTable: "Processes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
