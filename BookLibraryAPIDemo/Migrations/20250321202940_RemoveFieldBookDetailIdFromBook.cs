using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFieldBookDetailIdFromBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Reviews_ReviewId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReviewId1",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "198dd69c-77b6-46d0-bb44-54b9bee367ed");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "85baa468-9b51-49cb-aded-29dc1740ca1e");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "b18a26f2-78f5-414b-8bad-1e6190821bb9");

            migrationBuilder.DropColumn(
                name: "BookDetailId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ReviewId1",
                table: "Books");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "107a5a5d-67b3-4a3f-9d3e-87c0e7007f82", "system", new DateTime(2025, 3, 21, 20, 29, 39, 642, DateTimeKind.Utc).AddTicks(8295), "", null, "Books on science and nature", false, "Science", "", null },
                    { "17d32256-0b4d-4386-bc78-642d1e8bf151", "system", new DateTime(2025, 3, 21, 20, 29, 39, 642, DateTimeKind.Utc).AddTicks(8269), "", null, "This is about Tech", false, "Tech", "", null },
                    { "441698f0-d178-47f0-99c0-af948b5a55d4", "system", new DateTime(2025, 3, 21, 20, 29, 39, 642, DateTimeKind.Utc).AddTicks(8283), "", null, "Books on Finance ", false, "Finance ", "", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "BookDetailId",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReviewId1",
                table: "Books",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "198dd69c-77b6-46d0-bb44-54b9bee367ed", "system", new DateTime(2025, 3, 21, 16, 21, 21, 201, DateTimeKind.Utc).AddTicks(5514), "", null, "Books on Finance ", false, "Finance ", "", null },
                    { "85baa468-9b51-49cb-aded-29dc1740ca1e", "system", new DateTime(2025, 3, 21, 16, 21, 21, 201, DateTimeKind.Utc).AddTicks(5527), "", null, "Books on science and nature", false, "Science", "", null },
                    { "b18a26f2-78f5-414b-8bad-1e6190821bb9", "system", new DateTime(2025, 3, 21, 16, 21, 21, 201, DateTimeKind.Utc).AddTicks(5497), "", null, "This is about Tech", false, "Tech", "", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReviewId1",
                table: "Books",
                column: "ReviewId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Reviews_ReviewId1",
                table: "Books",
                column: "ReviewId1",
                principalTable: "Reviews",
                principalColumn: "Id");
        }
    }
}
