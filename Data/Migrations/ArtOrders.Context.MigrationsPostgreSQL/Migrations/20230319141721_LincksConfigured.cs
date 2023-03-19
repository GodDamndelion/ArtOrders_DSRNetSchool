using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArtOrders.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class LincksConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "images");

            migrationBuilder.RenameIndex(
                name: "IX_Images_Uid",
                table: "images",
                newName: "IX_images_Uid");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "users",
                type: "integer",
                nullable: false);

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

            migrationBuilder.AddUniqueConstraint(
                name: "AK_users_AvatarId",
                table: "users",
                column: "AvatarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_images",
                table: "images",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uuid", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.Id);
                    table.UniqueConstraint("AK_chats_OrderId", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_chats_users_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_chats_users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "price_list_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price_list_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_price_list_items_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_example_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_example_items", x => x.Id);
                    table.UniqueConstraint("AK_work_example_items_ImageId", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_work_example_items_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChatId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    ImageId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_messages_chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_messages_images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "images",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_messages_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ChatLink = table.Column<string>(type: "text", nullable: false),
                    CurrentResultId = table.Column<int>(type: "integer", nullable: false),
                    EditsNumber = table.Column<int>(type: "integer", nullable: false),
                    Uid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.UniqueConstraint("AK_orders_CurrentResultId", x => x.CurrentResultId);
                    table.ForeignKey(
                        name: "FK_orders_chats_Id",
                        column: x => x.Id,
                        principalTable: "chats",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_users_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_orders_users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_Nickname",
                table: "users",
                column: "Nickname",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_Link",
                table: "images",
                column: "Link",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_chats_ArtistId",
                table: "chats",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_chats_CustomerId",
                table: "chats",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_chats_Uid",
                table: "chats",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_ChatId",
                table: "messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_ImageId",
                table: "messages",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_messages_Uid",
                table: "messages",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_messages_UserId",
                table: "messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ArtistId",
                table: "orders",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ChatLink",
                table: "orders",
                column: "ChatLink",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId",
                table: "orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_Uid",
                table: "orders",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_price_list_items_Uid",
                table: "price_list_items",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_price_list_items_UserId",
                table: "price_list_items",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_work_example_items_Uid",
                table: "work_example_items",
                column: "Uid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_work_example_items_UserId",
                table: "work_example_items",
                column: "UserId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "price_list_items");

            migrationBuilder.DropTable(
                name: "work_example_items");

            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_users_AvatarId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_Nickname",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_images",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_Link",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_OrderCurrentResultId",
                table: "images");

            migrationBuilder.DropIndex(
                name: "IX_images_WorkExampleItemImageId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "users");

            migrationBuilder.DropColumn(
                name: "OrderCurrentResultId",
                table: "images");

            migrationBuilder.DropColumn(
                name: "WorkExampleItemImageId",
                table: "images");

            migrationBuilder.RenameTable(
                name: "images",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_images_Uid",
                table: "Images",
                newName: "IX_Images_Uid");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Images",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");
        }
    }
}
