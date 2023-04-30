using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtOrders.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class OrderDateAndChatNameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "chats",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "chats");
        }
    }
}
