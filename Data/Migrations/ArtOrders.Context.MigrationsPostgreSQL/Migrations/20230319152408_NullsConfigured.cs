using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtOrders.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class NullsConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_images_orders_OrderCurrentResultId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_users_Id",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_images_work_example_items_WorkExampleItemImageId",
                table: "images");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_chats_Id",
                table: "orders");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_work_example_items_ImageId",
                table: "work_example_items");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_users_AvatarId",
                table: "users");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_orders_CurrentResultId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_images_OrderCurrentResultId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_WorkExampleItemImageId",
                table: "images");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_chats_OrderId",
                table: "chats");

            migrationBuilder.DropColumn(
                name: "OrderCurrentResultId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "WorkExampleItemImageId",
                table: "images");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarId",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CurrentResultId",
                table: "orders",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "orders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "images",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "chats",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_work_example_items_ImageId",
                table: "work_example_items",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_AvatarId",
                table: "users",
                column: "AvatarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_CurrentResultId",
                table: "orders",
                column: "CurrentResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_chats_OrderId",
                table: "chats",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_chats_orders_OrderId",
                table: "chats",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_images_CurrentResultId",
                table: "orders",
                column: "CurrentResultId",
                principalTable: "images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_images_AvatarId",
                table: "users",
                column: "AvatarId",
                principalTable: "images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_work_example_items_images_ImageId",
                table: "work_example_items",
                column: "ImageId",
                principalTable: "images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chats_orders_OrderId",
                table: "chats");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_images_CurrentResultId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_users_images_AvatarId",
                table: "users");

            migrationBuilder.DropForeignKey(
                name: "FK_work_example_items_images_ImageId",
                table: "work_example_items");

            migrationBuilder.DropIndex(
                name: "IX_work_example_items_ImageId",
                table: "work_example_items");

            migrationBuilder.DropIndex(
                name: "IX_users_AvatarId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_orders_CurrentResultId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_chats_OrderId",
                table: "chats");

            migrationBuilder.AlterColumn<int>(
                name: "AvatarId",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrentResultId",
                table: "orders",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "orders",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "images",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "OrderCurrentResultId",
                table: "images",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkExampleItemImageId",
                table: "images",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "chats",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_work_example_items_ImageId",
                table: "work_example_items",
                column: "ImageId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_users_AvatarId",
                table: "users",
                column: "AvatarId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_orders_CurrentResultId",
                table: "orders",
                column: "CurrentResultId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_chats_OrderId",
                table: "chats",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_images_OrderCurrentResultId",
                table: "images",
                column: "OrderCurrentResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_WorkExampleItemImageId",
                table: "images",
                column: "WorkExampleItemImageId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_images_orders_OrderCurrentResultId",
                table: "images",
                column: "OrderCurrentResultId",
                principalTable: "orders",
                principalColumn: "CurrentResultId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_images_users_Id",
                table: "images",
                column: "Id",
                principalTable: "users",
                principalColumn: "AvatarId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_images_work_example_items_WorkExampleItemImageId",
                table: "images",
                column: "WorkExampleItemImageId",
                principalTable: "work_example_items",
                principalColumn: "ImageId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_chats_Id",
                table: "orders",
                column: "Id",
                principalTable: "chats",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
