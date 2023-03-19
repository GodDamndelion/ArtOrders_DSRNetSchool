using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtOrders.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class OrderDescriptionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "orders");
        }
    }
}
