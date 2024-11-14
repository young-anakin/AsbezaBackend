using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipPayBackend.Migrations
{
    /// <inheritdoc />
    public partial class UserInfoFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "UserAccounts",
                newName: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "UserAccounts",
                newName: "EmployeeId");
        }
    }
}
