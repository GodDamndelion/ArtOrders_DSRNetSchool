using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtOrders.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class OrderChatRework : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_orders_ChatLink",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ChatLink",
                table: "orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatLink",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ChatLink",
                table: "orders",
                column: "ChatLink",
                unique: true);
        }
    }
}
