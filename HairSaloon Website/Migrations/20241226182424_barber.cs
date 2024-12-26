using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSaloon_Website.Migrations
{
    /// <inheritdoc />
    public partial class barber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Processs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employeesProcess",
                columns: table => new
                {
                    EmployeeProcessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeesProcess", x => x.EmployeeProcessId);
                    table.ForeignKey(
                        name: "FK_employeesProcess_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employeesProcess_Processs_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Processs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employeesProcess_EmployeeId",
                table: "employeesProcess",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_employeesProcess_ProcessId",
                table: "employeesProcess",
                column: "ProcessId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employeesProcess");

            migrationBuilder.DropTable(
                name: "Processs");

            migrationBuilder.AddColumn<float>(
                name: "Review",
                table: "Employees",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Order",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
