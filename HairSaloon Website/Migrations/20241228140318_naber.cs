using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HairSaloon_Website.Migrations
{
    /// <inheritdoc />
    public partial class naber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkStartTime",
                table: "Employees",
                newName: "StartHour");

            migrationBuilder.RenameColumn(
                name: "WorkEndTime",
                table: "Employees",
                newName: "EndHour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartHour",
                table: "Employees",
                newName: "WorkStartTime");

            migrationBuilder.RenameColumn(
                name: "EndHour",
                table: "Employees",
                newName: "WorkEndTime");
        }
    }
}
