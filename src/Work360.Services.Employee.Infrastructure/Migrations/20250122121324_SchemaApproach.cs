using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Work360.Services.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SchemaApproach : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employees",
                newSchema: "public");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Employees",
                schema: "public",
                newName: "Employees");
        }
    }
}
