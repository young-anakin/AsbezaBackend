using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipPayBackend.Migrations
{
    /// <inheritdoc />
    public partial class TotalPrepTimeandPriceAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TotalPrepTime",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "price",
                table: "Recipes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrepTime",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Recipes");
        }
    }
}
