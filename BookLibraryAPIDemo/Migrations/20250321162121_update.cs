using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookLibraryAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookId1",
                table: "Reviews");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "0092f69e-2b92-4397-a555-fe3f08e19fd6");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "1c9dbeb7-c041-4db4-9794-28daa168c9ae");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: "95b3208c-d6e5-4d56-807c-7f4711625b57");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "Reviews");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "BookId",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BookId1",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Description", "IsDeleted", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { "0092f69e-2b92-4397-a555-fe3f08e19fd6", "system", new DateTime(2025, 3, 21, 16, 19, 24, 1, DateTimeKind.Utc).AddTicks(2654), "", null, "This is about Tech", false, "Tech", "", null },
                    { "1c9dbeb7-c041-4db4-9794-28daa168c9ae", "system", new DateTime(2025, 3, 21, 16, 19, 24, 1, DateTimeKind.Utc).AddTicks(2685), "", null, "Books on science and nature", false, "Science", "", null },
                    { "95b3208c-d6e5-4d56-807c-7f4711625b57", "system", new DateTime(2025, 3, 21, 16, 19, 24, 1, DateTimeKind.Utc).AddTicks(2670), "", null, "Books on Finance ", false, "Finance ", "", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId1",
                table: "Reviews",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId1",
                table: "Reviews",
                column: "BookId1",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
