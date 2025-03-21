using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class reviewEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Reviews_ReviewId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReviewId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "107a5a5d-67b3-4a3f-9d3e-87c0e7007f82");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "17d32256-0b4d-4386-bc78-642d1e8bf151");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "441698f0-d178-47f0-99c0-af948b5a55d4");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "BookId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "4a6e3e51-bb20-46c0-be5a-5acd66a16503", "system", new DateTime(2025, 3, 21, 20, 47, 44, 596, DateTimeKind.Utc).AddTicks(2164), "", null, "This is about Tech", false, "Tech", "", null },
                    { "7814eceb-e25b-4760-aff3-92818913f082", "system", new DateTime(2025, 3, 21, 20, 47, 44, 596, DateTimeKind.Utc).AddTicks(2181), "", null, "Books on Finance ", false, "Finance ", "", null },
                    { "ed0445a0-2c75-4474-a2ef-a3e458507428", "system", new DateTime(2025, 3, 21, 20, 47, 44, 596, DateTimeKind.Utc).AddTicks(2192), "", null, "Books on science and nature", false, "Science", "", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "4a6e3e51-bb20-46c0-be5a-5acd66a16503");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "7814eceb-e25b-4760-aff3-92818913f082");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "ed0445a0-2c75-4474-a2ef-a3e458507428");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "ReviewId",
                table: "Books",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "107a5a5d-67b3-4a3f-9d3e-87c0e7007f82", "system", new DateTime(2025, 3, 21, 20, 29, 39, 642, DateTimeKind.Utc).AddTicks(8295), "", null, "Books on science and nature", false, "Science", "", null },
                    { "17d32256-0b4d-4386-bc78-642d1e8bf151", "system", new DateTime(2025, 3, 21, 20, 29, 39, 642, DateTimeKind.Utc).AddTicks(8269), "", null, "This is about Tech", false, "Tech", "", null },
                    { "441698f0-d178-47f0-99c0-af948b5a55d4", "system", new DateTime(2025, 3, 21, 20, 29, 39, 642, DateTimeKind.Utc).AddTicks(8283), "", null, "Books on Finance ", false, "Finance ", "", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReviewId",
                table: "Books",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Reviews_ReviewId",
                table: "Books",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
