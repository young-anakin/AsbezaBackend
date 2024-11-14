using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquipPayBackend.Migrations
{
    /// <inheritdoc />
    public partial class PrepTimeRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "Recipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "price",
                table: "Recipes",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
