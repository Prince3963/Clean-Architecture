using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApp.Infrastrcture.Migrations
{
    /// <inheritdoc />
    public partial class ApplyValidators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Employees_Salary",
                table: "Employees",
                sql: "[Salary] > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_Email",
                table: "Employees");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Employees_Salary",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
